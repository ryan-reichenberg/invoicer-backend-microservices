namespace PaymentsService.Domain
{
    public class PaymentDetails
    {
        public string Currency { get; set; }
        public long Amount { get; set; }
        public string AccountId { get; set; }
    }
}