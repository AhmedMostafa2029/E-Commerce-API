using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Cart;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartRepository cartRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPaymentGateway paymentGateway;
        private readonly PaymentGatewaySettings _stripe;
        private readonly IMapper mapper;

        public PaymentService(ICartRepository cartRepository,IUnitOfWork unitOfWork, IPaymentGateway paymentGateway ,
            IOptions<PaymentGatewaySettings> stripeSetting , IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.unitOfWork = unitOfWork;
            this.paymentGateway = paymentGateway;
            this._stripe = stripeSetting.Value;
            this.mapper = mapper;
        }

        public async Task<Result<CartDto>> CreateOrUpdatePaymentIntentAsync(string cartId, CancellationToken ct = default)
        {
            // 1. Check Cart and Items
            var cart = await cartRepository.GetCartAsync(cartId, ct);
            if (cart is null)
                return Result<CartDto>.Fail(Error.NotFound("Cart Not Found", $"Cart With Id {cartId} Not Found"));

            if(cart.Items.Count == 0)
                return Result<CartDto>.Fail(Error.Validation("Cart Is Empty", $"Cart With Id {cartId} IS Empty"));

            // 2.Check Product Is Exsist or not
            var productRepo = unitOfWork.GetRepository<Product, int>();
            var productIds = cart.Items.Select(i => i.Id).ToHashSet();
            var products = await productRepo.GetAllWithSpecificationsAsync(new ProductWithIdsSpecifications(productIds));

            foreach(var item in cart.Items)
            {
                var product = products.FirstOrDefault(p => p.Id == item.Id);

                if(product is null)
                    return Result<CartDto>.Fail(Error.NotFound("Product Not Found", $"Product With Id {item.Id} Not Found"));

                item.Price = product.Price;
            }

            var deliveryRepo = unitOfWork.GetRepository<DeliveryMethod, int>();
            var DeliveryMethod = await deliveryRepo.GetByIdAsync(cart.DeliveryMethodId.Value, ct);

            if(DeliveryMethod is null)
                return Result<CartDto>.Fail(Error.NotFound("Delivery Method Not Found", $"Delivery Method with id {cart.DeliveryMethodId.Value} Not Found"));

            cart.ShippingPrice = DeliveryMethod.Cost;
            var subTotal = cart.Items.Sum(i => i.Quantity * i.Price);
            var amount = (long)Math.Round((subTotal + DeliveryMethod.Cost) * 100m);

            if(!string.IsNullOrEmpty(cart.PaymentIntentId))
            {
                var result = await paymentGateway.UpdatePaymentIntentAsync(cart.PaymentIntentId,amount, ct);
                cart.PaymentIntentId = result.PaymentIntentId;
                cart.ClientSecret = result.ClientSecret;
            }
            else
            {
                var result = await paymentGateway.CreatePaymentIntentAsync(amount, _stripe.DefaultCurrency, ct);
                cart.PaymentIntentId = result.PaymentIntentId;
                cart.ClientSecret = result.ClientSecret;
            }
            

            await cartRepository.CreateOrUpdateCartAsync(cart,null, ct:ct);
            return Result<CartDto>.Ok(mapper.Map<CartDto>(cart));

        }
    }

    public class PaymentGatewaySettings
    {
        public string SecretKey { get; set; } = default!;
        public string DefaultCurrency { get; set; } = "USD";
    }
}
