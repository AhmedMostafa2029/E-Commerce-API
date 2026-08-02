using AutoMapper;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Entities.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Profiles
{
    public class PicturelUrlResolver(IOptions<UrlSettings> options) : IValueResolver<Product, ProductDto, string?>
    {
        private readonly UrlSettings _urlSettings = options.Value;
        public string? Resolve(Product source, ProductDto destination, string? destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return null;

            var BaseUrl = _urlSettings.BaseUrl.TrimEnd("/");
            var Path = source.PictureUrl.TrimStart("/");

            return $"{BaseUrl}/Files/{Path}";
        }
    }

    public class UrlSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
    }
}
