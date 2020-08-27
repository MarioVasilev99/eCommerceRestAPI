namespace eCommerceRestAPI.Services
{
    using eCommerceRestAPI.Dtos.Input.Orders;
    using eCommerceRestAPI.Dtos.Output;
    using eCommerceRestAPI.Models;
    using eCommerceRestAPI.Models.Enums;
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IProductsService productsService;

        public OrdersService(ApplicationDbContext dbContext, IProductsService productsService)
        {
            this.dbContext = dbContext;
            this.productsService = productsService;
        }

        public async Task CreateOrderAsync(int userId, CreateOrderDto orderProducts)
        {
            // Create a new order
            var newOrder = new Order()
            {
                Status = StatusEnum.New,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            // Add the order to DB in order to be able to access its newly assigned id.
            await this.dbContext.Orders.AddAsync(newOrder);

            var orderTotalPrice = 0.0m;
            foreach (var productId in orderProducts.Products)
            {
                //Create new OrderProduct and add it to Order Products.
                var newProduct = new OrderProduct()
                {
                    OrderId = newOrder.Id,
                    ProductId = productId,
                };

                newOrder.Products.Add(newProduct);

                //Add current product price to the total price of the order.
                orderTotalPrice += await this.productsService.GetProductPriceAsync(productId);
            }

            // Converts the total sum from BGN currency to the users local currency.
            var convertedTotalSum = await this.GetConvertedLocalCurrencySum(orderTotalPrice, userId);

            // Assign the calculated and converted to local currency total price to the newly created order.
            newOrder.TotalPrice = convertedTotalSum;

            // Save changes to db.
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<decimal> GetConvertedLocalCurrencySum(decimal orderTotalPriceInBgn, int userId)
        {
            // Gets the user currency code
            var userCurrencyInfo = await this.dbContext
                .Users
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    Code = u.CurrencyCode
                })
                .FirstOrDefaultAsync();

            // If the users local currency is BGN then conversion is not needed.
            if (userCurrencyInfo.Code == "BGN")
            {
                return orderTotalPriceInBgn;
            }

            // Gets the users currency current rate and converts the order total sum.
            var convertedTotalSum = 0.0m;
            var getCurrencyRatesUrl = "https://api.exchangeratesapi.io/latest?base=BGN";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(getCurrencyRatesUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        var currencyRates = JsonConvert.DeserializeObject<CurrencyRatesResponseDto>(responseJson);
                        var localCurrencyRate = currencyRates.Rates[userCurrencyInfo.Code];
                        convertedTotalSum = orderTotalPriceInBgn * localCurrencyRate;
                    }
                }
            }

            return convertedTotalSum;
        }

        public async Task<List<OrderOutputDto>> GetUserOrdersAsync(int userId)
        {
            var userOrders = await this.dbContext
                .Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderOutputDto()
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    UserName = o.User.Username,
                    ProductNames = o.Products.Select(p => p.Product.Name).ToList(),
                    Status = o.Status.ToString(),
                    TotalPrice = $"{o.TotalPrice} {o.User.CurrencyCode}",
                    CreatedAt = o.CreatedAt.ToString("f"),
                })
                .ToListAsync();
            return userOrders;
        }

        public async Task<bool> ValidateProductsAsync(CreateOrderDto orderProducts)
        {
            bool productsValid = true;
            int productsCount = await this.dbContext.Products.CountAsync();

            foreach (var productId in orderProducts.Products)
            {
                bool currentProductExist = await this.dbContext.Products.Where(p => p.Id == productId).AnyAsync();
                if (!currentProductExist)
                {
                    productsValid = false;
                    break;
                }
            }

            return productsValid;
        }

        public async Task<bool> ValidateOrderIdAsync(int orderId)
        {
            return await this.dbContext.Orders.AnyAsync(o => o.Id == orderId);
        }

        public async Task<bool> ValidateUserAsync(int userId, int orderId)
        {
            var orderUserId = await this.dbContext
                .Orders
                .Where(o => o.Id == orderId)
                .Select(o => o.UserId)
                .FirstOrDefaultAsync();

            return orderUserId == userId;
        }

        public bool ValidateOrderStatus(string newOrderStatus)
        {
            bool isValid = Enum.IsDefined(typeof(StatusEnum), newOrderStatus);
            return isValid;
        }

        public async Task ChangeOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await this.dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            var statusEnum = (StatusEnum)Enum.Parse(typeof(StatusEnum), newStatus);

            order.Status = statusEnum;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
