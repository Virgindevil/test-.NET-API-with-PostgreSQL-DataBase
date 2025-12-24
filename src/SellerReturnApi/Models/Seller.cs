using System;

namespace SellerReturnApi.Models;

public class Seller
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
}