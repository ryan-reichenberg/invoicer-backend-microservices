using System;
using Invoicer.Common.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Common
{
    public interface IInitializationContainer
    {
        IServiceCollection Services { get; }
        bool TryRegister(string name);
        void AddBuildAction(Action<IServiceProvider> execute);
        void AddInitializer(IInitializer initializer);
        void AddInitializer<TInitializer>() where TInitializer : IInitializer;
        IServiceProvider Initialize();
    }
}