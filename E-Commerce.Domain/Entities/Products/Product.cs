using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_Commerce.Domain.Entities.Products
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;


        public ProductType Type { get; set; } = null!;

        [ForeignKey("Type")]
        public int TypeId { get; set; }


        public ProductBrand Brand { get; set; } = null!;

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
    }
}
