using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities.Cart
{
    public class CustomerCart
    {
        public string Id { get; set; } = default!;

        public ICollection<CartItem> Items { get; set; } = [];

        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }

        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingPrice { get; set; }



    }
}
