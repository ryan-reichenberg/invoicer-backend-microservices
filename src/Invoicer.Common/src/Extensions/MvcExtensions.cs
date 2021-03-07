using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Invoicer.Common.Exceptions;
using Invoicer.Common.Types;
using Invoicer.Common.WebApi;
using Invoicer.Common.WebApi.Formatters;
using Invoicer.Common.WebApi.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Open.Serialization.Json;

namespace Invoicer.Common.Extensions
{
    public static class MvcExtensions
    {
        private static readonly byte[] InvalidJsonRequestBytes = Encoding.UTF8.GetBytes("An invalid JSON was sent.");
        private static bool _bindRequestFromRoute;
        private const string SectionName = "webApi";
        private const string RegistryName = "webApi";
        private const string EmptyJsonObject = "{}";
        private const string JsonContentType = "application/json";

        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app, Action<IEndpointsBuilder> build,
            bool useAuthorization = true, Action<IApplicationBuilder> middleware = null)
        {
            var definitions = app.ApplicationServices.GetRequiredService<WebApiEndpointDefinitions>();
            app.UseRouting();
            if (useAuthorization)
            {
                app.UseAuthorization();
            }
            
            middleware?.Invoke(app);
        
            app.UseEndpoints(router => build(new EndpointsBuilder(router, definitions)));
        
            return app;
        }
        public static IInitializationContainer AddWebApi(this IInitializationContainer container, Action<IMvcCoreBuilder> configureMvc = null,
            IJsonSerializer jsonSerializer = null, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }
            
            if (!container.TryRegister(RegistryName))
            {
                return container;
            }

            if (jsonSerializer is null)
            {
                var factory = new Open.Serialization.Json.Newtonsoft.JsonSerializerFactory(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = {new StringEnumConverter(new CamelCaseNamingStrategy())}
                });
                jsonSerializer = factory.GetSerializer();
            }

            if (jsonSerializer.GetType().Namespace?.Contains("Newtonsoft") == true)
            {
                container.Services.Configure<KestrelServerOptions>(o => o.AllowSynchronousIO = true);
                container.Services.Configure<IISServerOptions>(o => o.AllowSynchronousIO = true);
            }

            container.Services.AddSingleton(jsonSerializer);
            container.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            container.Services.AddSingleton(new WebApiEndpointDefinitions());
            var options = container.GetOptions<WebApiOptions>(sectionName);
            container.Services.AddSingleton(options);
            _bindRequestFromRoute = options.BindRequestFromRoute;

            var mvcCoreBuilder = container.Services
                .AddLogging()
                .AddMvcCore();

            mvcCoreBuilder.AddMvcOptions(o =>
                {
                    o.OutputFormatters.Clear();
                    o.OutputFormatters.Add(new JsonOutputFormatter(jsonSerializer));
                    o.InputFormatters.Clear();
                    o.InputFormatters.Add(new JsonInputFormatter(jsonSerializer));
                })
                .AddDataAnnotations()
                .AddApiExplorer()
                .AddAuthorization();

            configureMvc?.Invoke(mvcCoreBuilder);

            container.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>))
                        .WithoutAttribute(typeof(DecoratorAttribute)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            container.Services.AddTransient<IRequestDispatcher, RequestDispatcher>();

            if (container.Services.All(s => s.ServiceType != typeof(IExceptionToResponseMapper)))
            {
                container.Services.AddTransient<IExceptionToResponseMapper, EmptyExceptionToResponseMapper>();
            }

            return container;
        }
        public static T ReadQuery<T>(this HttpContext context) where T : class
        {
            var request = context.Request;
            RouteValueDictionary values = null;
            if (HasRouteData(request))
            {
                values = request.HttpContext.GetRouteData().Values;
            }

            if (HasQueryString(request))
            {
                var queryString = HttpUtility.ParseQueryString(request.HttpContext.Request.QueryString.Value);
                values ??= new RouteValueDictionary();
                foreach (var key in queryString.AllKeys)
                {
                    values.TryAdd(key, queryString[key]);
                }
            }

            var serializer = context.RequestServices.GetRequiredService<IJsonSerializer>();
            if (values is null)
            {
                return serializer.Deserialize<T>(EmptyJsonObject);
            }

            var serialized = serializer.Serialize(values.ToDictionary(k => k.Key, k => k.Value))
                ?.Replace("\\\"", "\"")
                .Replace("\"{", "{")
                .Replace("}\"", "}")
                .Replace("\"[", "[")
                .Replace("]\"", "]");

            return serializer.Deserialize<T>(serialized);
        }
         public static async Task<T> ReadJsonAsync<T>(this HttpContext httpContext)
        {
            if (httpContext.Request.Body is null)
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.Body.WriteAsync(InvalidJsonRequestBytes, 0, InvalidJsonRequestBytes.Length);

                return default;
            }

            try
            {
                var request = httpContext.Request;
                var payload = await httpContext.RequestServices.GetRequiredService<IJsonSerializer>().DeserializeAsync<T>(request.Body);
                if (_bindRequestFromRoute && HasRouteData(request))
                {
                    var values = request.HttpContext.GetRouteData().Values;
                    foreach (var (key, value) in values)
                    {
                        var field = payload.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                            .SingleOrDefault(f => f.Name.ToLowerInvariant().StartsWith($"<{key}>",
                                StringComparison.InvariantCultureIgnoreCase));

                        if (field is null)
                        {
                            continue;
                        }
                        
                        var fieldValue = TypeDescriptor.GetConverter(field.FieldType)
                            .ConvertFromInvariantString(value.ToString());
                        field.SetValue(payload, fieldValue);
                    }
                }

                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(payload, new ValidationContext(payload), results))
                {
                    return payload;
                }

                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteJsonAsync(results);

                return default;
            }
            catch
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.Body.WriteAsync(InvalidJsonRequestBytes, 0, InvalidJsonRequestBytes.Length);

                return default;
            }
        }
         public static object SetDefaultInstanceProperties(this object instance)
         {
             var type = instance.GetType();
             foreach (var propertyInfo in type.GetProperties())
             {
                 SetValue(propertyInfo, instance);
             }

             return instance;
         }
         private static void SetValue(PropertyInfo propertyInfo, object instance)
         {
             var propertyType = propertyInfo.PropertyType;
             if (propertyType == typeof(string))
             {
                 SetDefaultValue(propertyInfo, instance, string.Empty);
                 return;
             }
            
             if (propertyType.Name == "IDictionary`2")
             {
                 return;
             }

             if (typeof(IEnumerable).IsAssignableFrom(propertyType))
             {
                 SetCollection(propertyInfo, instance);

                 return;
             }

             if (propertyType.IsInterface)
             {
                 return;
             }

             if (propertyType.IsArray)
             {
                 SetCollection(propertyInfo, instance);
                 return;
             }

             if (!propertyType.IsClass)
             {
                 return;
             }

             var propertyInstance = FormatterServices.GetUninitializedObject(propertyInfo.PropertyType);
             SetDefaultValue(propertyInfo, instance, propertyInstance);
             SetDefaultInstanceProperties(propertyInstance);
         }

         public static async Task WriteJsonAsync<T>(this HttpResponse response, T value)
         {
             response.ContentType = JsonContentType;
             var serializer = response.HttpContext.RequestServices.GetRequiredService<IJsonSerializer>();
             await serializer.SerializeAsync(response.Body, value);
         }
         private static void SetCollection(PropertyInfo propertyInfo, object instance)
         {
             var elementType = propertyInfo.PropertyType.IsGenericType
                 ? propertyInfo.PropertyType.GenericTypeArguments[0]
                 : propertyInfo.PropertyType.GetElementType();
             if (elementType is null)
             {
                 return;
             }

             if (typeof(IEnumerable).IsAssignableFrom(elementType))
             {
                 if (elementType == typeof(string))
                 {
                     SetDefaultValue(propertyInfo, instance, Array.Empty<string>());
                     return;
                 }
                
                 return;
             }

             var array = Array.CreateInstance(elementType, 0);
             SetDefaultValue(propertyInfo, instance, array);
         }
         private static void SetDefaultValue(PropertyInfo propertyInfo, object instance, object value)
         {
             if (propertyInfo.CanWrite)
             {
                 propertyInfo.SetValue(instance, value);
                 return;
             }

             var propertyName = propertyInfo.Name;
             var field = instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                 .SingleOrDefault(x => x.Name.StartsWith($"<{propertyName}>"));
             field?.SetValue(instance, value);
         }
         
         private static bool HasQueryString(this HttpRequest request)
            => request.Query.Any();

        private static bool HasRouteData(this HttpRequest request)
            => request.HttpContext.GetRouteData().Values.Any();

        private class EmptyExceptionToResponseMapper : IExceptionToResponseMapper
        {
            public ExceptionResponse Map(Exception exception) => null;
        }
    }
}