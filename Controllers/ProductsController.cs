namespace eCommerceRestAPI.Controllers
{
    using eCommerceRestAPI.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

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
        public IActionResult GetAllProducts()
        {
            var products = this.productsService.GetAllProducts();
            return this.Ok(products);
        }
    }
}
