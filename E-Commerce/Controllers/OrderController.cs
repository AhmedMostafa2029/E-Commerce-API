using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    public class OrderController : ApiBaseController
    {
        private readonly IOrderServices orderServices;

        public OrderController(IOrderServices orderServices)
        {
            this.orderServices = orderServices;
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder([FromBody] OrderDto orderDto , [FromQuery] string email , CancellationToken ct)
        {
            return ToActionResult(await orderServices.CreateOrderAsync(orderDto, email, ct));
        }


        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethods(CancellationToken ct)
        {
            return ToActionResult(await orderServices.GetAllDeliveryMethodsAsync(ct));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersByEmail(string email ,CancellationToken ct)
        {
            return ToActionResult(await orderServices.GetAllOrdersByEmailAsync(email , ct));
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdAndEmail(Guid id , string email, CancellationToken ct)
        {
            return ToActionResult(await orderServices.GetOrderByIdAndEmailAsync(id ,email, ct));
        }





    }
}
