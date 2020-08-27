namespace eCommerceRestAPI.Services.Contracts
{
    using eCommerceRestAPI.Dtos.Input.Orders;
    using eCommerceRestAPI.Dtos.Output;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<bool> ValidateProductsAsync(CreateOrderDto orderProducts);

        Task CreateOrderAsync(int userId, CreateOrderDto orderProducts);

        Task<List<OrderOutputDto>> GetUserOrdersAsync(int userId);

        Task<decimal> GetConvertedLocalCurrencySum(decimal orderTotalPriceInBgn, int userId);

        Task<bool> ValidateUserAsync(int userId, int orderId);

        Task<bool> ValidateOrderIdAsync(int orderId);

        bool ValidateOrderStatus(string newOrderStatus);

        Task ChangeOrderStatusAsync(int orderId, string newStatus);
    }
}
