using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface IPaymentService
    {
        Task<Result<CartDto>> CreateOrUpdatePaymentIntentAsync(string cartId, CancellationToken ct = default);

    }
}
