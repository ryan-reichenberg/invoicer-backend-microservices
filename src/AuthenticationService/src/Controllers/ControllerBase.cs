using System;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    public class ControllerBase : Controller
    {
        protected Guid UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ? 
                Guid.Empty : 
                Guid.Parse(User.Identity.Name);
    }
}