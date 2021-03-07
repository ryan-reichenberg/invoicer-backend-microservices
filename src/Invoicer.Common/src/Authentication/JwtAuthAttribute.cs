using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Invoicer.Common.Authentication
{
    public class JwtAuthAttribute :  AuthorizeAttribute
    {
        // TODO: Not sure if this is needed because we have Istio
        public JwtAuthAttribute(String scheme = JwtBearerDefaults.AuthenticationScheme, string policy = "") 
            : base(policy)
        {
            AuthenticationSchemes = scheme;
        }
    }
}