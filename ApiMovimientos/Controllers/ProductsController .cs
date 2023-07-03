using ApiMovimientos.Contracts;
using ApiMovimientos.Model;
using ApiMovimientos.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMovimientos.Controllers
{
    [ApiController]
    //[Route("api/v1/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    //[ApiVersion("1")]
    [Route("[controller]")]

    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) => _productService = productService;
        
        [HttpGet]
        public async Task<IList<ProductDetails>> Get() => await _productService.ProductListAsync();
        
        [HttpGet("{productId:length(24)}")]
        public async Task<ActionResult<ProductDetails>> Get(string productId)
        {
            var productDetails = await _productService.GetProductDetailByIdAsync(productId);
            if (productDetails is null)
            {
                return NotFound($"sin datos para { productId }");
            }
            return Ok(productDetails);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(ProductDetails productDetails)
        {
            await _productService.AddProductAsync(productDetails);
            return CreatedAtAction(nameof(Get), new { id = productDetails.Id }, productDetails);
        }
        [HttpPut("{productId:length(24)}")]
        public async Task<IActionResult> Update(string productId, ProductDetails productDetails)
        {
            var productDetail = await _productService.GetProductDetailByIdAsync(productId);
            if (productDetail is null)
            {
                return NotFound();
            }
            productDetails.Id = productDetail.Id;
            await _productService.UpdateProductAsync(productId, productDetails);
            return Ok();
        }
        [HttpDelete("{productId:length(24)}")]
        public async Task<IActionResult> Delete(string productId)
        {
            var productDetails = await _productService.GetProductDetailByIdAsync(productId);
            if (productDetails is null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(productId);
            return Ok();
        }
    }
}
