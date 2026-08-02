using E_Commerce.Domain.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Contracts
{
    public interface ICartRepository
    {
        Task<CustomerCart?> GetCartAsync(string cartId, CancellationToken ct = default);

        Task<CustomerCart?> CreateOrUpdateCartAsync(CustomerCart cart,TimeSpan? timeToLive, CancellationToken ct = default);

        Task<bool> DeleteCartAsync(string cartId, CancellationToken ct = default);


    }
}
