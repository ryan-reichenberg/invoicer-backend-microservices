using System;

namespace InvoicingService.Domain
{
    public class LineItem
    {
        public Guid Id { get; set; }
        public LineItem(Guid id, string? description, decimal? price, int? quantity)
        {
            Id = id;
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