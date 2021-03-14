namespace AuthenticationService.Exceptions
{
    public class InvalidCredentialsException : DomainException
    {
        public override string Code { get; } = ErrorCodes.InvalidCredentials.ToString();
        public string Email { get; }

        public InvalidCredentialsException(string email) : base("Invalid credentials.")
        {
            Email = email;
        }
    }
}