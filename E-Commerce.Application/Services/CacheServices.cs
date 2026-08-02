using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Application.Services
{
    public class CacheServices : ICacheServices
    {
        private readonly ICacheRepository cacheRepository;

        public CacheServices(ICacheRepository cacheRepository)
        {
            this.cacheRepository = cacheRepository;
        }

        public Task<string?> GetAsync(string cachekey, CancellationToken ct = default)
        {
            return cacheRepository.GetAsync(cachekey, ct);
        }

        public Task SetAsync(string cachekey, object cacheValue, TimeSpan timeToLive, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(cacheValue, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return cacheRepository.SetAsync(cachekey, json, timeToLive, ct);
        }
    }
}
