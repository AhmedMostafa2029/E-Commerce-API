using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Params
{
    public class ProductQueryParams
    {
        public int? brandId {  get; set; }
        public int? typeId { get; set; }
        public string? searchValue { get; set; }

        public ProductSortingOptions sort { get; set; }

        public int PageIndex { get; set; } = 1;


        // بالاتفاق مع ال frontend
        private const int DefaultPageSize = 4;
        private const int MaxPageSize = 8;


        private int PageSize = DefaultPageSize;

        public int pageSize
        {
            get => PageSize;
            set => PageSize = (value > MaxPageSize ? MaxPageSize : (value < 1 ? DefaultPageSize : value));
        }

    }
}
