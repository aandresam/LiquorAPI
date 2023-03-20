using liquorApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using liquorApi.Context.Entities;
using liquorApi.Models;

namespace liquorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;

        public ProductsController(ProductsService productsService)
        {
            this.productsService = productsService;
        }


        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await this.productsService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            return await this.productsService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductDtoIn product)
        {
            var newProduct = await this.productsService.Create(product);

            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id}, newProduct);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDto product)
        {
            await this.productsService.Update(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await this.productsService.Delete(id);
            return NoContent();
        }
    }
}