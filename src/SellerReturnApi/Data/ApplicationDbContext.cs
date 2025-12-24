// src/SellerReturnApi/Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using SellerReturnApi.Models;

namespace SellerReturnApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Seller> Sellers => Set<Seller>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ReturnRequest> ReturnRequests => Set<ReturnRequest>();
}