﻿using E_Commerce2Business_V01.Interfaces;
using E_Commerce2Business_V01.Payloads;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce3APIs_V01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ITypeService _typeService;
        private readonly IBrandService _brandService;
        public ProductsController(IProductService productService, ITypeService typeService, IBrandService brandService)
        {
            _productService = productService;
            _typeService = typeService;
            _brandService = brandService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsPage([FromQuery] GetProductsPagePayload payload)
        {
            return Ok(await _productService.GetProductsPageAsync(payload));
        }
        [HttpGet("Brands")]
        public async Task<IActionResult> GetBrands() => Ok(await _brandService.GetBrandsDTOAsync());
        [HttpGet("Types")]
        public async Task<IActionResult> GetTypes() => Ok(await _typeService.GetTypesDTOAsync());
    }
}
