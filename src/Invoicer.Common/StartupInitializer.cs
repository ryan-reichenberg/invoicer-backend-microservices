using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invoicer.Common.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Common
{
    public class StartupInitializer : IStartupInitializer
    {
        private readonly ConcurrentDictionary<string, bool> _registry = new ConcurrentDictionary<string, bool>();
        private readonly List<Action<IServiceProvider>> _buildActions;
        private readonly IList<IInitializer> _initializers = new List<IInitializer>();
        
        public StartupInitializer()
        {
            _buildActions = new List<Action<IServiceProvider>>();
        }
        public bool TryRegister(string name) => _registry.TryAdd(name, true);
        
        public void AddInitializer<TInitializer>() where TInitializer : IInitializer
            => AddBuildAction(sp =>
            {
                var initializer = sp.GetService<TInitializer>();
                AddInitializer(initializer);
            });
        public void AddBuildAction(Action<IServiceProvider> execute)
            => _buildActions.Add(execute);

        public void AddInitializer(IInitializer initializer)
        {
            if (initializer is null || _initializers.Contains(initializer))
            {
                return;
            }

            _initializers.Add(initializer);
        }

        public async Task InitializeAsync()
        {
            foreach (var initializer in _initializers)
            {
                await initializer.InitializeAsync();
            }
        }
    }
}