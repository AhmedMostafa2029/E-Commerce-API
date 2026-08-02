using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public class ProductWithIdsSpecifications : BaseSpecifications<Product , int>
    {

        public ProductWithIdsSpecifications(IEnumerable<int> Ids)
            :base(p => Ids.Contains(p.Id))
        {

        }

    }
}
