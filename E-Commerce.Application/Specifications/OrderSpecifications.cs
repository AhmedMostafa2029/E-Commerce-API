using E_Commerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Order , Guid>
    {

        public OrderSpecifications(string email) : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.items);

            AddOrderByDesc(o => o.OrdeDate);
        }

        public OrderSpecifications(Guid id,string email) 
            : base(o => o.Id == id && o.BuyerEmail == email)
        {

        }


    }
}
