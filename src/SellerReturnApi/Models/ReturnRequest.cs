using System;

namespace SellerReturnApi.Models;

public class ReturnRequest
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; } = string.Empty; // "WRONG_DELIVERY", "DEFECT", etc.
    public string Status { get; set; } = "CREATED";     // CREATED → PICKED → SHIPPED → COMPLETED
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}