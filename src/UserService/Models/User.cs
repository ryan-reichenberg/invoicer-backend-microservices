using Invoicer.Common;

namespace UserService.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool Deactivated { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
