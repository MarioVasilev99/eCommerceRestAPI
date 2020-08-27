namespace eCommerceRestAPI.Controllers
{
    using eCommerceRestAPI.Dtos.Input.Products;
    using eCommerceRestAPI.Helpers;
    using eCommerceRestAPI.Models;
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route(RoutesHelper.ProductsController)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;
        private readonly UserManager<User> userManager;

        public ProductsController(IProductsService productsService, UserManager<User> userManager)
        {
            this.productsService = productsService;
            this.userManager = userManager;
        }

        // This method returns all of the products in the database.
        [HttpGet(RoutesHelper.GetAllProducts)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await this.productsService.GetAllProductsAsync();
            return this.Ok(products);
        }

        // This method checks if a product with the passed id exists and returns it to the user.
        [HttpGet(RoutesHelper.GetProductById)]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await this.productsService.GetProductByIdAsync(id);
            if (product == null)
            {
                return this.NotFound();
            }

            return this.Ok(product);
        }

        // This method creates a new product.
        [HttpPost(RoutesHelper.CreateProduct)]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody]ProductCreationDto productInfo)
        {
            //TODO: Create Product Validation
            await this.productsService.CreateProductAsync(productInfo);
            return this.Ok();
        }


        // This method checks if a product with the passed id exists and deletes it.
        // Returns: OK if a product is found. Not Found if product does not exist.
        [HttpDelete(RoutesHelper.DeleteProduct)]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            Product productToDelete = await this.productsService.GetProductByIdAsync(productId);

            if (productToDelete == null)
            {
                return this.NotFound();
            }

            await this.productsService.DeleteProductAsync(productToDelete);
            return this.Ok();
        }
    }
}
