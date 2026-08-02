using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Application.Params;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductServices(IUnitOfWork unitOfWork , IMapper mapper) 
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllProductBrandsAsync(CancellationToken ct = default)
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);

            var mappedBrands = mapper.Map<IReadOnlyList<ProductBrand>, IReadOnlyList<BrandDto>>(Brands);

            return Result<IReadOnlyList<BrandDto>>.Ok(mappedBrands);
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams productQuery, CancellationToken ct = default)
        {
            var spec = new ProductSpecifications(productQuery);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecificationsAsync(spec,ct);

            var mappedproducts = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            var countSpec = new ProductCountSpecifications(productQuery); // count with Get all or Filter
            var totalCount = await unitOfWork.GetRepository<Product , int>().GetProductCountWithSpecificationsAsync(countSpec, ct);

            return Result<PaginatedResult<ProductDto>>.Ok(new PaginatedResult<ProductDto>(mappedproducts , productQuery.PageIndex , products.Count, totalCount));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllProductTypesAsync(CancellationToken ct = default)
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct);

            var mappedTypes = mapper.Map<IReadOnlyList<ProductType>, IReadOnlyList<TypeDto>>(Types);

            return Result<IReadOnlyList<TypeDto>>.Ok(mappedTypes);
        }

        public async Task<Result<ProductDto>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var spec = new ProductSpecifications(id);

            var product = await unitOfWork.GetRepository<Product , int>().GetByIdWithSpecificationsAsync(spec, ct);

            if (product == null)
                return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound", $"Product With Id: {id} is not found"));

            var mappedProduct = mapper.Map<Product, ProductDto>(product);

            return Result<ProductDto>.Ok(mappedProduct);
        }
    }
}
