using E_Commerce.Application.Contracts;
using E_Commerce.Application.Profiles;
using E_Commerce.Application.Services;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application
{
    public static class ApplicationServicesRegisterations
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(c => c.AddProfile(new ProductProfile()) , typeof(ApplicationServicesRegisterations).Assembly);
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<ICartServices, CartServices>();
            services.AddScoped<IIdentityServices , IdentityServices>();
            services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IPaymentService, PaymentService>();



            services.AddSingleton<ICacheServices , CacheServices>();

            return services;
        }
    }
}
