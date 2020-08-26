namespace eCommerceRestAPI.Services
{
    using eCommerceRestAPI.Dtos.Input.Products;
    using eCommerceRestAPI.Models;
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateProductAsync(ProductCreationDto productInfo)
        {
            Product newProduct = new Product()
            {
                Name = productInfo.Name,
                Price = productInfo.Price,
                ImageUrl = productInfo.ImageUrl,
            };

            await this.dbContext.AddAsync(newProduct);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product productToDelete)
        {
            this.dbContext.Products.Remove(productToDelete);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync() => await this.dbContext.Products.ToListAsync();

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await this.dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<decimal> GetProductPriceAsync(int productId)
        {
            var productPrice = await this.dbContext
                .Products
                .Where(p => p.Id == productId)
                .Select(p => p.Price)
                .FirstOrDefaultAsync();

            return productPrice;
        }
    }
}
