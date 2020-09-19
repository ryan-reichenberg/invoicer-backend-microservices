using System;
using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class Address
    {
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
