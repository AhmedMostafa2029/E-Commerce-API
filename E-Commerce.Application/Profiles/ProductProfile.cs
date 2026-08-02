using AutoMapper;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dist => dist.TypeName, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dist => dist.PictureUrl, opt => opt.MapFrom<PicturelUrlResolver>());  // resolve

        }
    }
}
