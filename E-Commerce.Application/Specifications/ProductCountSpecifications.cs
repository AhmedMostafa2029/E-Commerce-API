using E_Commerce.Application.Params;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public class ProductCountSpecifications : BaseSpecifications<Product , int>
    {
        public ProductCountSpecifications(ProductQueryParams productQuery)
            : base(p => (!productQuery.brandId.HasValue || p.BrandId == productQuery.brandId)
            && (!productQuery.typeId.HasValue || p.TypeId == productQuery.typeId)
            && (string.IsNullOrEmpty(productQuery.searchValue) || p.Name.ToLower().Contains(productQuery.searchValue.ToLower())))
        { 
            
        }
    }
}
