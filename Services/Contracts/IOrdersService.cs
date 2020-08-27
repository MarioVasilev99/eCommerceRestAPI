namespace eCommerceRestAPI.Services.Contracts
{
    using eCommerceRestAPI.Dtos.Input.Orders;
    using eCommerceRestAPI.Dtos.Output;
    using eCommerceRestAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<bool> ValidateProductsAsync(CreateOrderDto orderProducts);

        Task CreateOrderAsync(int userId, CreateOrderDto orderProducts);

        Task<List<OrderOutputDto>> GetUserOrdersAsync(int userId);
    }
}
