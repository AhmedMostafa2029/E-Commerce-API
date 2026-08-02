using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Entities.Cart
{
    public class CartItem
    {

        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;

        public decimal Price { get; set; }
        public int Quantity { get; set; } 

    }
}
