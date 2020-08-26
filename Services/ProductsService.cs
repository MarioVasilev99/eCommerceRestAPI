namespace eCommerceRestAPI.Services
{
    using eCommerceRestAPI.Models;
    using eCommerceRestAPI.Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Product> GetAllProducts() => this.dbContext.Products.ToList();
    }
}
