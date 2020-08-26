namespace eCommerceRestAPI.Services.Contracts
{
    using eCommerceRestAPI.Dtos.Input.Products;
    using eCommerceRestAPI.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductsService
    {
        Task<List<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int productId);

        Task CreateProductAsync(ProductCreationDto productInfo);

        Task DeleteProductAsync(Product product);

        Task<decimal> GetProductPriceAsync(int productId);
    }
}
