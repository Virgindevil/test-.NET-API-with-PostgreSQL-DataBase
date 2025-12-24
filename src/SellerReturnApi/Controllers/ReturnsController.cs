using Microsoft.AspNetCore.Mvc;
using SellerReturnApi.Data;
using SellerReturnApi.DTOs;
using SellerReturnApi.Models;
using System;
using System.Threading.Tasks;

namespace SellerReturnApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReturnsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReturnsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReturn([FromBody] CreateReturnRequestDto dto)
    {
        if (dto.Quantity <= 0)
            return BadRequest("Quantity must be positive.");

        var seller = await _context.Sellers.FindAsync(dto.SellerId);
        var product = await _context.Products.FindAsync(dto.ProductId);

        if (seller == null || product == null)
            return NotFound("Seller or Product not found.");

        var returnRequest = new ReturnRequest
        {
            Id = Guid.NewGuid(),
            SellerId = dto.SellerId,
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Reason = dto.Reason,
            Status = "CREATED",
            CreatedAt = DateTime.UtcNow
        };

        _context.ReturnRequests.Add(returnRequest);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateReturn), new { id = returnRequest.Id }, returnRequest);
    }
}