namespace AuthenticationService.Exceptions
{
    public class InvalidPasswordException : DomainException
    {
        public InvalidPasswordException() : base("Invalid password")
        {
        }
    }
}