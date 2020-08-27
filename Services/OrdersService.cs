namespace eCommerceRestAPI.Services
{
    using eCommerceRestAPI.Dtos.Input.Orders;
    using eCommerceRestAPI.Dtos.Output;
    using eCommerceRestAPI.Models;
    using eCommerceRestAPI.Models.Enums;
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

            // Assign the calcuated total price to the newly created order.
            newOrder.TotalPrice = orderTotalPrice;

            // Save changes to db.
            await this.dbContext.SaveChangesAsync();
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
                    TotalPrice = o.TotalPrice,
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
    }
}
