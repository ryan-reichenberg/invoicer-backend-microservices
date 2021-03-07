using System;

namespace AuthenticationService.Exceptions.ApplicationExceptions
{
    public abstract class AppException : Exception
    {
        public virtual string Code { get; }

        protected AppException(string message) : base(message)
        {
        }
    }
}