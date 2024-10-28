using E_Commerce1DB_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce3APIs_V01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BaseAPIController
    {
        //private readonly 
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;
        public PaymentsController(IPaymentService paymentService, IOrderService orderService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
        }
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentRequest([FromRoute] string basketId)
        {
            var userId = ExtractIdFromToken();
            RedirectionUrlDTO redirectionUrlDTO = await _paymentService.CreatePaymentRequest(basketId,userId);
            return Ok(redirectionUrlDTO);
            // Knet test credentials
            // KNET TEST CARD 1
            // 000000000001
            // 09/25
            // 1111
        }

        [HttpPost("webhook/capture")]
        public async Task<IActionResult> TestCapture()
        {
            var formData = await Request.ReadFormAsync();
            await _orderService.HandlePaymentResultAsync(formData);
            // Convert form data to a dictionary
            var result = formData.ToDictionary(k => k.Key, v => v.Value.ToString());
            // Process the received data (log it, save it to a database, etc.)
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            // Return the form data as JSON
            return Ok(result);
        }
        [HttpGet("webhook/redirect")]
        public async Task<IActionResult> TestRedirect()
        {
            if (!(Request.Query["result"] == "CAPTURED"))
                return Redirect("https://google.com");
            return Redirect("https://facebook.com");
        }

    }
}
