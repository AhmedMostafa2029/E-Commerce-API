using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface ICartServices
    {
        Task<Result<CartDto>> GetCartAsync(string id, CancellationToken ct = default);
        Task<Result<CartDto>> CreateOrUpdateAsync(CartDto cart, CancellationToken ct = default);

        Task<Result<bool>> DeleteCartAsync(string id, CancellationToken ct = default);

    }
}
