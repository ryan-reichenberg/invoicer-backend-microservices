namespace InvoicingService.Domain
{
    public class LineItem
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}