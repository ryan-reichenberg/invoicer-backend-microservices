using System;

namespace AuthenticationService.Exceptions
{
    public class TokenAlreadyRevokedException : Exception
    {
        public ErrorCodes Code { get; }

        public TokenAlreadyRevokedException(ErrorCodes code, string message, params object[] args)
            : base(string.Format(message, args))
        {
            Code = code;
        }
    }
}