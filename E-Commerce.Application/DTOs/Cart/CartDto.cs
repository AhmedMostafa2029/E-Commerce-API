using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.DTOs.Cart
{
    public class CartDto
    {
        public string Id { get; set; } = default!;

        public ICollection<CartItemDto> Items { get; set; } = [];

        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }

        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }
    }
}
