using System;
using Invoicer.Common.Types;

namespace AuthenticationService.Repositories.Documents
{
    internal sealed  class RefreshTokenDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedAt { get; set; }
    }
}