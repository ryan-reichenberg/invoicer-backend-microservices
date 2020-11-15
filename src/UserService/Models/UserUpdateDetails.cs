using System;
namespace UserService.Models
{
    public class UserUpdateDetails
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
