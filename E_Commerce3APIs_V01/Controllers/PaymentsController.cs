using Microsoft.AspNetCore.Http;
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
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentRequest([FromRoute] string basketId)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer  jtest123");
            return Ok(new { threeDSecureUrl="https://facebook.com" });
        }

        [HttpGet("test/test")]
        public async Task<IActionResult> Test()
        {
            // HTTP client -> use it to send requests

            using var httpClient = new HttpClient();

            // Before sending to provider

            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer  jtest123");

            var request = new
            {
                products = new[]
    {
        new
        {
            name = "Logitech K380",
            description = "Logitech K380 / Easy-Switch for Upto 3 Devices, Slim Bluetooth Tablet Keyboard",
            price = 10,
            quantity = 1
        },
        new
        {
            name = "Logitech M171 Wireless Optical Mouse",
            description = "Logitech M171 Wireless Optical Mouse (2.4GHz Wireless, Blue Grey)",
            price = 10,
            quantity = 1
        }
    },
                order = new
                {
                    //id = "202210101255255144669",
                    id = "123",
                    reference = "11111991",
                    description = "Purchase order received for Logitech K380 Keyboard",
                    currency = "KWD",
                    amount = 20
                },
                paymentGateway = new
                {
                    src = "knet"
                },
                language = "en",
                reference = new
                {
                    id = "202210101202210101"
                },
                customer = new
                {
                    uniqueId = "2129879kjbljg767881",
                    name = "Dharmendra Kakde",
                    email = "kakde.dharmendra@upayments.com",
                    mobile = "+96566336537"
                },
                returnUrl = "https://3c03-154-182-87-107.ngrok-free.app/api/payments/webhook/redirect", // redirect url (first api)
                cancelUrl = "https://error.com",
                notificationUrl = "https://3c03-154-182-87-107.ngrok-free.app/api/payments/webhook/capture", // webhook url (second api)
                customerExtraData = "User define data"
            };

            var json = JsonConvert.SerializeObject(request);

            var content = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync("https://sandboxapi.upayments.com/api/v1/charge", content);

            // Knet test credentials
            // KNET TEST CARD 1
            // 000000000001
            // 09/25
            // 1111
            var resBody = response.Content.ReadAsStringAsync();
            return Ok(resBody);
        }

        [HttpPost("webhook/capture")]
        public async Task<IActionResult> TestCapture()
        {
            var formData = await Request.ReadFormAsync();

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
            var queryParams = Request.Query
            .ToDictionary(q => q.Key, q => q.Value.ToString());

            // Return the query parameters as JSON
            return Redirect("https://facebook.com");
        }

    }
}
