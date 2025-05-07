using Application.Services;
    using Core.Entities;
using Core.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    namespace WebAPI.Controllers {
        [Route("api/[controller]")]
        [ApiController]
        public class ProductController : ControllerBase {
            private readonly IProductService _productService;

            public ProductController(IProductService productService) {
                _productService = productService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll() {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id) {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null) {
                    return NotFound();
                }
                return Ok(product);
            }

            [HttpPost]
            public async Task<IActionResult> Post([FromBody] Product product) {
                await _productService.AddProductAsync(product);
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, [FromBody] Product product) {
            //if (id != product.Id) {
            //    return BadRequest();
            //}
                product.Id = id;
                await _productService.UpdateProductAsync(product);
                return Ok(product);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id) {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
        }
    }