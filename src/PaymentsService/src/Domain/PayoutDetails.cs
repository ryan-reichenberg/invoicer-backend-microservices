namespace PaymentsService.Domain
{
    public class PayoutDetails
    {
        public string Currency { get; set; }
        public long Amount { get; set; }
        public string AccountId { get; set; }
    }
}