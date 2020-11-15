using System;
namespace Invoicer.Common.Exceptions
{
    public class MultipleHandlerException : System.Exception
    {
        public MultipleHandlerException(string message)
        : base(message)
        {
        }
    }
}
