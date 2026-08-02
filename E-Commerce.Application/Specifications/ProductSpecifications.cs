using E_Commerce.Application.Params;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product , int>
    {
        public ProductSpecifications(ProductQueryParams productQuery) 
            : base(p => (!productQuery.brandId.HasValue || p.BrandId == productQuery.brandId) 
            && (!productQuery.typeId.HasValue || p.TypeId == productQuery.typeId)
            && (string.IsNullOrEmpty(productQuery.searchValue) || p.Name.ToLower().Contains(productQuery.searchValue.ToLower())))
        // True & true (All )
        // value & true (brand)
        // true & value (type)
        // value & value (brand & type)

        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            switch(productQuery.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.Name);
                break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;

                _ : break;

            }

            ApplyPagination(productQuery.pageSize , productQuery.PageIndex);

        }

        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }

    }
}
