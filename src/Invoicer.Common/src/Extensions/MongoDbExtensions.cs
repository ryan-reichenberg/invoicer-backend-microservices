using System;
using Invoicer.Common.MongoDB;
using Invoicer.Common.MongoDB.Builders;
using Invoicer.Common.MongoDB.Factories;
using Invoicer.Common.MongoDB.Repositories;
using Invoicer.Common.MongoDB.Seeders;
using Invoicer.Common.Types;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Invoicer.Common.Extensions
{
    public static class MongoDbExtensions
    {
        private static bool _conventionsRegistered;
        private const string SectionName = "mongo";
        private const string RegistryName = "persistence.mongoDb";

        public static IInitializationContainer AddMongo(this IInitializationContainer container, string sectionName = SectionName,
            Type seederType = null, bool registerConventions = true)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }
            var mongoOptions = container.GetOptions<MongoDbOptions>(sectionName);
            return container.AddMongo(mongoOptions, seederType, registerConventions);
        }

        public static IInitializationContainer AddMongo(this IInitializationContainer container, Func<IMongoDbOptionsBuilder,
            IMongoDbOptionsBuilder> buildOptions, Type seederType = null, bool registerConventions = true)
        {
            var mongoOptions = buildOptions(new MongoDbOptionsBuilder()).Build();
            return container.AddMongo(mongoOptions, seederType, registerConventions);
        }

        public static IInitializationContainer AddMongo(this IInitializationContainer container, MongoDbOptions mongoOptions,
            Type seederType = null, bool registerConventions = true)
        {
            if (!container.TryRegister(RegistryName))
            {
                return container;
            }

            if (mongoOptions.SetRandomDatabaseSuffix)
            {
                var suffix = $"{Guid.NewGuid():N}";
                Console.WriteLine($"Setting a random MongoDB database suffix: '{suffix}'.");
                mongoOptions.Database = $"{mongoOptions.Database}_{suffix}";
            }

            container.Services.AddSingleton(mongoOptions);
            container.Services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();
                return new MongoClient(options.ConnectionString);
            });
            container.Services.AddTransient(sp =>
            {
                var options = sp.GetService<MongoDbOptions>();
                var client = sp.GetService<IMongoClient>();
                return client.GetDatabase(options.Database);
            });
            container.Services.AddTransient<IMongoDbInitializer, MongoDbInitializer>();
            container.Services.AddTransient<IMongoSessionFactory, MongoSessionFactory>();

            if (seederType is null)
            {
                container.Services.AddTransient<IMongoDbSeeder, MongoDbSeeder>();
            }
            else
            {
                container.Services.AddTransient(typeof(IMongoDbSeeder), seederType);
            }

            container.AddInitializer<IMongoDbInitializer>();
            if (registerConventions && !_conventionsRegistered)
            {
                RegisterConventions();
            }

            return container;
        }

        private static void RegisterConventions()
        {
            _conventionsRegistered = true;
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?),
                new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            ConventionRegistry.Register("convey", new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
            }, _ => true);
        }

        public static IInitializationContainer AddMongoRepository<TEntity, TIdentifiable>(this IInitializationContainer container,
            string collectionName)
            where TEntity : IIdentifiable<TIdentifiable>
        {
            container.Services.AddTransient<IMongoRepository<TEntity, TIdentifiable>>(sp =>
            {
                var database = sp.GetService<IMongoDatabase>();
                return new MongoRepository<TEntity, TIdentifiable>(database, collectionName);
            });

            return container;
        }
    }
}