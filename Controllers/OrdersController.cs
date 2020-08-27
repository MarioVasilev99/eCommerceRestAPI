namespace eCommerceRestAPI.Controllers
{
    using eCommerceRestAPI.Dtos.Input.Orders;
    using eCommerceRestAPI.Helpers;
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route(RoutesHelper.OrdersController)]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpPost(RoutesHelper.OrderCreate)]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDto orderProductsInfo)
        {
            int userId = this.GetUserId();

            // Validates the passed product ids whether they exist in the db.
            bool productsValid = await this.ordersService.ValidateProductsAsync(orderProductsInfo);

            if (!productsValid)
            {
                return this.BadRequest("Product Id does not exist.");
            }

            // Creates a new order.
            await this.ordersService.CreateOrderAsync(userId, orderProductsInfo);

            return this.Ok();
        }

        [HttpGet(RoutesHelper.OrderGetUserOrders)]
        [Authorize]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            // Gets all orders of the current user
            var orders = await this.ordersService.GetUserOrdersAsync(userId);

            return this.Ok(orders);
        }

        [HttpPost(RoutesHelper.OrderChangeStatus)]
        [Authorize]
        public async Task<IActionResult> ChangeOrderStatus([FromBody]ChangeOrderStatusDto orderStatusDto)
        {
            // Validates if the order id passed by the user exists is the db.
            var orderIdValid = await this.ordersService.ValidateOrderIdAsync(orderStatusDto.OrderId);

            if (!orderIdValid)
            {
                // Todo: Remove magic string
                return this.NotFound("An order with this id does not exist.");
            }

            // Validates if the currently logged user is the user who made the order.
            var userId = this.GetUserId();
            var userAbleToModifyOrder = await this.ordersService.ValidateUserAsync(userId, orderStatusDto.OrderId);

            if (!userAbleToModifyOrder)
            {
                // Todo: Remove magic string
                return this.BadRequest("You don't have permission to modify this order.");
            }

            // Validates if the new order status is valid.
            var orderStatusValid = this.ordersService.ValidateOrderStatus(orderStatusDto.Status);

            if (!orderStatusValid)
            {
                // Todo: Remove magic string
                return this.BadRequest("Order status is not valid.");
            }

            // Changes order status.
            await this.ordersService.ChangeOrderStatusAsync(orderStatusDto.OrderId, orderStatusDto.Status);
            return this.Ok();
        }
    }
}
