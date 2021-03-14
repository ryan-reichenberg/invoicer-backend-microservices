using System;
using Invoicer.Common.Types;

namespace UserService.DTO
{
    public class UserDto : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool Deactivated { get; set; }


        public UserDto(Guid id, string name, string streetAddress, string postalCode, string city, string mobileNumber, string emailAddress)
        {
            Id = id;
            Name = name;
            StreetAddress = streetAddress;
            PostalCode = postalCode;
            City = city;
            MobileNumber = mobileNumber;
            EmailAddress = emailAddress;
        }

        public override string ToString()
        {
            return
                $"UserDTO[Id={Id}, Name={Name}, StreetAddresss={StreetAddress}, PostalCode={PostalCode}, City={City}, " +
                $"MobileNumber={MobileNumber}, EmailAdress={EmailAddress}, Deactivated={Deactivated}]";
        }
    }
}
