using System;
namespace Invoicer.Common.Exceptions
{
    public class NoHandlerException : System.Exception
    {
        public NoHandlerException(string message)
        : base(message)
        {
        }
    }
}
