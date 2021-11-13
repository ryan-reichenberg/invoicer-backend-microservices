namespace InvoicingService.Domain
{
    public class Contact
    {
        public string BusinessName { get; set; }
        public string Name { get; private set; }
        public string StreetAddress { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string MobileNumber { get; private set; }
        public string EmailAddress { get; private set; }
    }
}