using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Seeding
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<IdentityDataSeeder> logger;

        public IdentityDataSeeder(StoreIdentityDbContext dbContext , UserManager<ApplicationUser> userManager , 
            RoleManager<IdentityRole> roleManager , ILogger<IdentityDataSeeder> logger )
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public async Task SeedAsync(CancellationToken ct = default)
        {
            try
            {
                var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (PendingMigrations.Any())
                    await dbContext.Database.MigrateAsync(ct);

                if(! await roleManager.Roles.AnyAsync(ct))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

                }
                if(! await userManager.Users.AnyAsync(ct))
                {
                    var admin = new ApplicationUser()
                    {
                        DisplayName = "Ahmed Mostafa",
                        Email = "ahmed123@gmail.com",
                        UserName = "Admin230",
                        PhoneNumber = "01145535398"
                    };

                    var result = await userManager.CreateAsync(admin, "P@ssw0rd");

                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(admin, "Admin");
                    else
                        logger.LogWarning("The User did not Create");

                }

            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Can not seed the Data");
                throw;
            }
        }
    }
}
