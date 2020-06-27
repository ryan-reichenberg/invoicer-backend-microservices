using System;
namespace Invoicer.Common
{
    public interface ICommandBus
    {
        void Send<T>(T Command) where T : ICommand;
    }
}
