using System;
using Invoicer.Common.Types;

namespace Invoicer.Common
{
    public interface IStartupInitializer : IInitializer
    {
        void AddInitializer(IInitializer initializer);
        bool TryRegister(string name);
        void AddBuildAction(Action<IServiceProvider> execute);
        void AddInitializer<TInitializer>() where TInitializer : IInitializer;
    }
}