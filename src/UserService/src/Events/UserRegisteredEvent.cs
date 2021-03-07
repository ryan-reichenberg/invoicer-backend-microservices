using System;
using Invoicer.Common;
using Invoicer.Common.RabbitMq.Attributes;

namespace UserService.Events
{
    [Message("authentication")]
    public class UserRegisteredEvent : IEvent
    {
        public string UserId { get; }
        public string Name { get; }
        public string StreetAddress { get; }
        public string PostalCode { get; }
        public string City { get; }
        public string MobileNumber { get; }
        public string EmailAddress { get; }
        
        
        public UserRegisteredEvent(Guid userId, string name, 
            string streetAddress, string postalCode, string city, string mobileNumber, string emailAddress
        )
        {
            UserId = userId.ToString("N");
            Name = name;
            StreetAddress = streetAddress;
            PostalCode = postalCode;
            City = city;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
        }
    }
}