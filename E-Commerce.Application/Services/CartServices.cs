using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Cart;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class CartServices : ICartServices
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public CartServices(ICartRepository cartRepository , IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        public async Task<Result<CartDto>> CreateOrUpdateAsync(CartDto cart, CancellationToken ct = default)
        {
            var customerCart = mapper.Map<CustomerCart>(cart);
            var result = await cartRepository.CreateOrUpdateCartAsync(customerCart, TimeSpan.FromDays(1), ct);

            return result is not null ? Result<CartDto>.Ok(mapper.Map<CartDto>(result))
                : Result<CartDto>.Fail(Error.Failure("CreateOrUpdateCart.Failure", "Can not Set this Cart")); 
        }

        public async Task<Result<bool>> DeleteCartAsync(string id, CancellationToken ct = default)
        {
            var result = await cartRepository.DeleteCartAsync(id, ct);

            return result ? Result<bool>.Ok(true)
                : Result<bool>.Fail(Error.Failure("DeleteCart", "Can not Delete this Cat"));
        }

        public async Task<Result<CartDto>> GetCartAsync(string id, CancellationToken ct = default)
        {
            var cart = await cartRepository.GetCartAsync(id, ct);

            if (cart is null)
                return Result<CartDto>.Fail(Error.NotFound("GteCart.NotFound", "Can not Find the Cart"));

            return Result<CartDto>.Ok(mapper.Map<CartDto>(cart));
        }
    }
}
