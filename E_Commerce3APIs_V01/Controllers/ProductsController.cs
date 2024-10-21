using E_Commerce2Business_V01.Interfaces;
using E_Commerce2Business_V01.Payloads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce3APIs_V01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsPage ([FromQuery]GetProductsPayload payload)
        {
            return Ok(await _productService.GetProductsPageAsync(payload));
        }
        [HttpGet("Brands")]
        public async Task<IActionResult> GetBrands()
        {
            return Ok (await _productService.GetBrands());
        }
        [HttpGet("Types")]
        public async Task<IActionResult> GetTypes()
        {
            return Ok (await _productService.GetTypes());
        }
    }
}
