using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Contracts
{
    public interface IDataSeeder
    {
        Task SeedAsync(CancellationToken ct= default);
    }
}
