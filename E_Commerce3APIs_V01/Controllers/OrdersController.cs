using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IOrderService _orderService;
        public OrdersController(IShippingMethodService shippingMethodService, IOrderService orderService)
        {
            _shippingMethodService = shippingMethodService;
            _orderService = orderService;
        }
        [HttpGet("DeliveryMethods")]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            List<DeliveryMethodDTO> deliveryMethods = await _shippingMethodService.GetDeliveryMethodsDTOAsync();
            return Ok(deliveryMethods);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrders()
        {
            var userId = ExtractIdFromToken();
            List<GetAllOrdersDTO> orders = await _orderService.GetAllOrdersForUser(userId);
            return Ok(orders);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderByIdAsync(int orderId)
        {
            OrderDTO order = await _orderService.GetOrderDetailsById(orderId);
            return Ok(order);
        }
    }
}
