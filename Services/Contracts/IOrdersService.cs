namespace eCommerceRestAPI.Services.Contracts
{
    using eCommerceRestAPI.Dtos.Input.Orders;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<bool> ValidateProductsAsync(CreateOrderDto orderProducts);

        Task CreateOrderAsync(int userId, CreateOrderDto orderProducts);
    }
}
