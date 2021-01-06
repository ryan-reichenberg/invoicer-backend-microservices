using System;

namespace Invoicer.Common.Types
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}