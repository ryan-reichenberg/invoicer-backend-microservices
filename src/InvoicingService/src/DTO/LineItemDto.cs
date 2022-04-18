using System;
using Microsoft.EntityFrameworkCore;

namespace InvoicingService.DTO
{
    [Owned]
    public class LineItemDto
    {

        public LineItemDto(string? description, decimal? price, int? quantity)
        {
            Quantity = quantity;
            Price = price;
            Description = description;
        }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
    }
}