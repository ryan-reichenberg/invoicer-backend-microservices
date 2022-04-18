using System;

namespace InvoicingService.Domain
{
    public class LineItem
    {
        public LineItem(string? description, decimal? price, int? quantity)
        {
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }


        // public decimal GetTotal()
        // {
        //     return Quantity * Price;
        // }
    }
}