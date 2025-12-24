using Microsoft.EntityFrameworkCore;
using SellerReturnApi.Controllers;
using SellerReturnApi.Data;
using SellerReturnApi.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SellerReturnApi.UnitTests;

public class ReturnsControllerTests
{
    [Fact]
    public async Task CreateReturn_WithValidData_ReturnsCreatedAt()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new ApplicationDbContext(options);

        // Добавляем тестовые данные
        var seller = new Seller { Id = Guid.NewGuid(), Name = "Test Seller", ContactEmail = "test@example.com" };
        var product = new Product { Id = Guid.NewGuid(), Sku = "SKU-123", Name = "Test Product", Category = "Electronics" };

        context.Sellers.Add(seller);
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var controller = new ReturnsController(context);

        var dto = new CreateReturnDto
        {
            SellerId = seller.Id,
            ProductId = product.Id,
            Quantity = 5,
            Reason = "DEFECT"
        };

        // Act
        var result = await controller.CreateReturn(dto);

        // Assert
        var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<ReturnRequest>(createdAt.Value);
        Assert.Equal("CREATED", returnValue.Status);
        Assert.Equal(5, returnValue.Quantity);
        Assert.Equal(seller.Id, returnValue.SellerId);
        Assert.Equal(product.Id, returnValue.ProductId);
    }
}