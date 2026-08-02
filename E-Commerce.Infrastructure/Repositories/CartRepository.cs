using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Cart;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _databaae;

        public CartRepository(IConnectionMultiplexer connection)
        {
            _databaae = connection.GetDatabase();
        }

        public async Task<CustomerCart?> CreateOrUpdateCartAsync(CustomerCart cart, TimeSpan? timeToLive, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(cart);

            var success = await _databaae.StringSetAsync(cart.Id, json, timeToLive ?? TimeSpan.FromDays(30));

            return success ? cart : null;
        }

        public async Task<bool> DeleteCartAsync(string cartId, CancellationToken ct = default)
        {
            return await _databaae.KeyDeleteAsync(cartId);
        }

        public async Task<CustomerCart?> GetCartAsync(string cartId, CancellationToken ct = default)
        {
            var cart = await _databaae.StringGetAsync(cartId);

            if (cart.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<CustomerCart>(cart.ToString());
        }
    }
}
