using System;

namespace Invoicer.Common.Types
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}