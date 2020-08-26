namespace eCommerceRestAPI.Controllers
{
    using eCommerceRestAPI.Dtos.Input.Products;
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("All")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await this.productsService.GetAllProductsAsync();
            return this.Ok(products);
        }

        [HttpGet("byId/{id}")]
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

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody]ProductCreationDto productInfo)
        {
            //TODO: Create Product Validation
            await this.productsService.CreateProductAsync(productInfo);
            return this.Ok();
        }
    }
}
