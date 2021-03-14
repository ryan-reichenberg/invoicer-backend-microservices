using Invoicer.Common.Types.DDD;

namespace UserService.Entities
{
    public class User : AggregateRoot
    {
        public string Name { get; private set; }
        public string StreetAddress { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string MobileNumber { get; private set; }
        public string EmailAddress { get; private set; }
        public bool Deactivated { get; private set; }

        public User(AggregateId id, string name, string streetAddress, string postalCode, string city, string mobileNumber, string emailAddress)
        {
            Id = id;
            Name = name;
            StreetAddress = streetAddress;
            PostalCode = postalCode;
            City = city;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
            Deactivated = false;
        }
    }
}