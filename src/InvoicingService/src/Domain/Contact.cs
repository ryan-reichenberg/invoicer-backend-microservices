using System;

namespace InvoicingService.Domain
{
    public class Contact
    {
        public Contact()
        {
        }

        public Contact(Guid id, string? businessName, string? name, string? streetAddress, string? postalCode, string? city, string? mobileNumber, string? emailAddress)
        {
            Id = id;
            BusinessName = businessName;
            Name = name;
            StreetAddress = streetAddress;
            PostalCode = postalCode;
            City = city;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
        }
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public string Name { get; private set; }
        public string StreetAddress { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string MobileNumber { get; private set; }
        public string EmailAddress { get; private set; }
    }
}