namespace eCommerceRestAPI.Controllers
{
    using eCommerceRestAPI.Dtos.Input.Orders;
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDto orderProductsInfo)
        {
            int userId = this.GetUserId();
            bool productsValid = await this.ordersService.ValidateProductsAsync(orderProductsInfo);

            if (!productsValid)
            {
                return this.BadRequest("Product Id does not exist.");
            }

            await this.ordersService.CreateOrderAsync(userId, orderProductsInfo);
            return this.Ok();
        }
    }
}
