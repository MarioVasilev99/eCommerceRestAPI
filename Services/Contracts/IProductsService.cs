namespace eCommerceRestAPI.Services.Contracts
{
    using eCommerceRestAPI.Models;
    using System.Collections.Generic;

    public interface IProductsService
    {
        List<Product> GetAllProducts();
    }
}
