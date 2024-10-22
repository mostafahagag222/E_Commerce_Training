using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce3APIs_V01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseAPIController
    {
        private readonly IShippingMethodService _shippingMethodService;

        public OrdersController(IShippingMethodService orderService)
        {
            _shippingMethodService = orderService;
        }

        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            List<DeliveryMethodDTO> deliveryMethods = await _shippingMethodService.GetDeliveryMethodsDTOAsync();
            return Ok(deliveryMethods);
        }
    }
}
