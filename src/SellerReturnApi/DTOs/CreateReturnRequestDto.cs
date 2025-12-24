using System;

namespace SellerReturnApi.DTOs;

public record CreateReturnRequestDto(
    Guid SellerId,
    Guid ProductId,
    int Quantity,
    string Reason
);