using System;
namespace AuthenticationService.Models
{
    public class AuthenticatedUser
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public AuthenticatedUser()
        {
        }
    }
}
