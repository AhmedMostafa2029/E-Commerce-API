using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Orders;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class OrderServices: IOrderServices
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICartRepository cartRepository;

        public OrderServices(IMapper mapper, IUnitOfWork unitOfWork , ICartRepository cartRepository)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.cartRepository = cartRepository;
        }

        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default)
        {
            // 1 . validate Cart Found and Items
            var cart = await cartRepository.GetCartAsync(orderDto.CartId, ct);

            if(cart == null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Cart.NotFound", "Cart IS Not Found"));

            if(cart.Items.Count == 0)
                return Result<OrderToReturnDto>.Fail(Error.Validation("Cart.Empty", "Cart IS Empty"));

            // 2.Get Items From Cart Validate As Product
            var productRepo = unitOfWork.GetRepository<Product, int>();

            var ProductIds = cart.Items.Select(i => i.Id).ToHashSet();

            var Products = await productRepo.GetAllWithSpecificationsAsync(new ProductWithIdsSpecifications(ProductIds), ct);

            var orderItems = new List<OrderItem>(cart.Items.Count);

            foreach(var item in cart.Items)
            {
                var product = Products.FirstOrDefault(p => p.Id == item.Id);

                if(product is null)
                    return Result<OrderToReturnDto>.Fail(Error.NotFound("Product.NotFound", "Product IS Not Found"));


                orderItems.Add(new OrderItem()
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrder()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    }
                });

            }

            // 3. Get Order Addres
            var orderAddress = mapper.Map<OrderAddress>(orderDto.ShippingAddresss);

            // 4. Get Delivery Method
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId, ct);
            if(deliveryMethod is null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Delivery.NotFound", "Delivery IS Not Found"));

            // 5. Calcualtions
            var subTotal = orderItems.Sum(i => i.Price * i.Quantity);

            // 6.Generate Order
            var order = new Order()
            {
                BuyerEmail = email,
                items = orderItems,
                ShippingAddress = orderAddress,
                DeliveryMethodId = deliveryMethod.Id,
                DeliveryMethod = deliveryMethod,
                SubTotal = subTotal,
                PaymentIntentId = cart.PaymentIntentId
            };

            unitOfWork.GetRepository<Order, Guid>().Add(order);
            var result = await unitOfWork.SaveChangesAsync();

            // 7. Return Order
            if(result <= 0)
                return Result<OrderToReturnDto>.Fail(Error.Failure("Order.Failure", "Order Can Not Created"));

            await cartRepository.DeleteCartAsync(orderDto.CartId, ct);

            return Result<OrderToReturnDto>.Ok(mapper.Map<OrderToReturnDto>(order));




        }

        public async Task<Result<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethodsAsync(CancellationToken ct = default)
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync(ct);

            return Result<IReadOnlyList<DeliveryMethodDto>>.Ok(mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods));
        }

        public async Task<Result<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersByEmailAsync(string email, CancellationToken ct = default)
        {
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllWithSpecificationsAsync(new OrderSpecifications(email));

            return Result<IReadOnlyList<OrderToReturnDto>>.Ok(mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));

        }

        public async Task<Result<OrderToReturnDto>> GetOrderByIdAndEmailAsync(Guid Id, string email, CancellationToken ct = default)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetByIdWithSpecificationsAsync(new OrderSpecifications(Id,email));

            if(order is null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Order.NotFound", "Order IS Not Found"));


            return Result<OrderToReturnDto>.Ok(mapper.Map<OrderToReturnDto>(order));
        }
    }
}
