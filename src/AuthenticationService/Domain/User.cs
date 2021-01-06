using System;
using Invoicer.Common.Types;

namespace AuthenticationService.Domain
{
    public class User : IIdentifiable
    {
        public Guid Id { get; }
    }
}