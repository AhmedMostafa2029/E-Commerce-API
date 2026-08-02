using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class PaymentController : ApiBaseController
    {
        private readonly IPaymentService paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<CartDto>> CreateOrUpdatePaymentIntent(string cartId, CancellationToken ct = default)
        {
            var result = await paymentService.CreateOrUpdatePaymentIntentAsync(cartId, ct);

            return ToActionResult(result);

        }
    }
}
