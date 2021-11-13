using System;

namespace PaymentsService.Domain
{
    public class UserDetails
    {
        public string Type { get; set; } = "express";
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string BusinessType { get; set; } = "individual";
        
        public string BusinessName { get; set; }
    }
}