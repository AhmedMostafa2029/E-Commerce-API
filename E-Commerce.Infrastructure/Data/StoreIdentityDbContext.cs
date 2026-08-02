using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Data
{
    public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<Address>().ToTable("Addresses");


        }
    }
}
