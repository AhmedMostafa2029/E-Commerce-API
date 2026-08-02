using E_Commerce.API.Attributes;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using E_Commerce.Application.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices) 
        {
            _productServices = productServices;
        }

        [HttpGet]
        [Authorize]
        [RedisCach(5000)]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams productQuery, CancellationToken ct)
        {
            var products = await _productServices.GetAllProductsAsync(productQuery , ct);

            return ToActionResult(products);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllProductBrands(CancellationToken ct)
        {
            var Brands = await _productServices.GetAllProductBrandsAsync(ct);

            return ToActionResult(Brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllProductTypes(CancellationToken ct)
        {
            var Types = await _productServices.GetAllProductTypesAsync(ct);

            return ToActionResult(Types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id , CancellationToken ct)
        {
            var product = await _productServices.GetByIdAsync(id , ct);

            return ToActionResult(product);
        }

    }
}
