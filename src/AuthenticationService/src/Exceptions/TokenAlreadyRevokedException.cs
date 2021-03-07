using System;

namespace AuthenticationService.Exceptions
{
    public class TokenAlreadyRevokedException : DomainException
    {
        public override string Code { get; } = ErrorCodes.RefreshTokenAlreadyRevoked.ToString();

        public TokenAlreadyRevokedException( string message)
            : base(message)
        {
        }
    }
}