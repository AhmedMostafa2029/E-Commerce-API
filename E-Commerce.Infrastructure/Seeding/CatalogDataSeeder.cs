using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace E_Commerce.Infrastructure.Seeding
{
    public class CatalogDataSeeder(StoreDbContext dbContext, ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedAsync(CancellationToken ct = default)
        {
            try
            {

                // Pending Migrations ??
                var pending = await dbContext.Database.GetPendingMigrationsAsync();
                if (pending.Count() > 0)
                    await dbContext.Database.MigrateAsync();


                var seedPath = Path.Combine(AppContext.BaseDirectory, "JSONFiles");

                await SeedIfEmptyAsync<ProductBrand>(seedPath, "brands.json", ct);
                await SeedIfEmptyAsync<ProductType>(seedPath, "types.json", ct);
                await SeedIfEmptyAsync<Product>(seedPath, "products.json", ct);
                await SeedIfEmptyAsync<DeliveryMethod>(seedPath, "delivery.json", ct);



            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Fiald to seed data");
                throw;
            }

        }

        private async Task SeedIfEmptyAsync<T>(string root, string fileName , CancellationToken ct) where T:class
        {
            if(await dbContext.Set<T>().AnyAsync(ct)) return;


            var filePath = Path.Combine(root, fileName);

            if (!File.Exists(filePath))
            {
                logger.LogWarning("The specified seed file was not found.");
                return;
            }

            await using var stream = File.OpenRead(filePath);

            var items = await JsonSerializer.DeserializeAsync<List<T>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true } , ct);

            if (items?.Count > 0)
                await dbContext.Set<T>().AddRangeAsync(items, ct);

            await dbContext.SaveChangesAsync(ct);


        }
    }
}
