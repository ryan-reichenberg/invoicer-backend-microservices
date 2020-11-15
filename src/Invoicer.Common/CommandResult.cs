
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace Invoicer.Common
{
    public abstract class CommandResult
    {
        public  bool Ok { get; set; }

        public static CommandResult Success()
        {
          
            return new Succeed();
        }

        public static CommandResult Failure(HttpStatusCode responseCode, params string[] reasons)
        {
          
            return new Failed(responseCode, reasons);
        }

    }
    public sealed class Succeed : CommandResult
    {
        internal Succeed()
        {  
            Ok = true;
        }
    }

    public sealed class Failed : CommandResult
    {
        public IList<string> Reasons { get; }
        public HttpStatusCode ResponseCode { get; }

        internal Failed(HttpStatusCode responseCode, IList<string> reasons)
        {
            Ok = false;
            ResponseCode = responseCode;
            Reasons = reasons;
        }
    }
}