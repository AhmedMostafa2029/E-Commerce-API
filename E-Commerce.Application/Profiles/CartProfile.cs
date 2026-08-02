using AutoMapper;
using E_Commerce.Application.DTOs.Cart;
using E_Commerce.Domain.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Profiles
{
    public class CartProfile : Profile
    {
        public CartProfile() 
        {
            CreateMap<CustomerCart , CartDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();

        }
    }
}
