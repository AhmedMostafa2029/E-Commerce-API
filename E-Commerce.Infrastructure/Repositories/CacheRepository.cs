using E_Commerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class CacheRepository : ICacheRepository
    {

        private readonly IDatabase _database;

        public CacheRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }

        public async Task<string?> GetAsync(string cacheKey, CancellationToken ct = default)
        {
            var result = await _database.StringGetAsync(cacheKey);

            return result.IsNullOrEmpty ? null : result.ToString();
        }

        public async Task SetAsync(string cacheKey, string cacheValue, TimeSpan timeToLive, CancellationToken ct = default)
        {
            await _database.StringSetAsync(cacheKey, cacheValue, timeToLive);
        }
    }
}
