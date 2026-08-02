using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Extentions
{
    public static class ProgramExtentions
    {
        public static async Task MigratedAndSeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            var Seeder = services.GetRequiredKeyedService<IDataSeeder>("Catalog");
            await Seeder.SeedAsync();

            var IdentitySeeder = services.GetRequiredKeyedService<IDataSeeder>("Identity");
            await IdentitySeeder.SeedAsync();

        }
    }
}
