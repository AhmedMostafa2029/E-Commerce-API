using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Infrastructure.Data;
using E_Commerce.Infrastructure.Payments;
using E_Commerce.Infrastructure.Repositories;
using E_Commerce.Infrastructure.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace E_Commerce.Infrastructure
{
    public static class InfraStructureServicesRegisterations
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StoreDbConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");


            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IConnectionMultiplexer>(oprions =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
            });

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddSingleton<ICacheRepository, CacheRepository>();
            services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = configuration.GetSection("JWT")["Issuer"],
                    ValidAudience = configuration.GetSection("JWT")["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( configuration.GetSection("JWT")["SecretKey"])),
                };
            });

            services.AddScoped<IPaymentGateway, StripePaymentGateway>();

            return services;
        }
    }
}
