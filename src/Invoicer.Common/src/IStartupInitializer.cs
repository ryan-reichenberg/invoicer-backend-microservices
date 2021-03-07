using System;
using Invoicer.Common.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Common
{
    public interface IStartupInitializer : IInitializer
    {
        void AddInitializer(IInitializer initializer);
    }
}