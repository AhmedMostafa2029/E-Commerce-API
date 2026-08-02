using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface ICacheServices
    {
        Task<String?> GetAsync(String cachekey , CancellationToken ct = default);
        Task SetAsync(String cachekey,object cacheValue,TimeSpan timeToLive, CancellationToken ct = default);

    }
}
