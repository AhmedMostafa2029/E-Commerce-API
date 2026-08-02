using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{

    public class CartController : ApiBaseController
    {
        private readonly ICartServices cartServices;

        public CartController(ICartServices cartServices)
        {
            this.cartServices = cartServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetCart(string id , CancellationToken ct)
        {
            var cart = await cartServices.GetCartAsync(id , ct);

            return ToActionResult(cart);
        }


        [HttpPost]
        public async Task<ActionResult<CartDto>> CreateOrUpdateCart(CartDto cart, CancellationToken ct)
        {
            var resultCart = await cartServices.CreateOrUpdateAsync(cart, ct);

            return ToActionResult(resultCart);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCart(string id, CancellationToken ct)
        {
            var resultCart = await cartServices.DeleteCartAsync(id, ct);

            return ToActionResult(resultCart);
        }

    }
}
