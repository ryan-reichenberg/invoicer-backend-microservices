using System;
using Microsoft.EntityFrameworkCore;

namespace InvoicingService.DTO
{
    public class LineItemDto
    {
        public LineItemDto()
        {
        }
        
        public LineItemDto(Guid id, string? description, decimal? price, int? quantity)
        {
            Id = id;
            Quantity = quantity;
            Price = price;
            Description = description;
        }
        public Guid Id { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
    }
}