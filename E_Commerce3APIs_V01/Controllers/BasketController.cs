using E_Commerce2Business_V01.Interfaces;
using E_Commerce2Business_V01.Payloads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce3APIs_V01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ICartServices _cartServices;

        public BasketController(ICartServices cartServices)
        {
            _cartServices = cartServices;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToBasketPayload payload)
        {
            return Ok(await _cartServices.UpdateCartItems(payload));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket (string id)
        {
            await _cartServices.DeleteCartAsync(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetBasketDetails (string id)
        {
            return Ok(await _cartServices.GetBasketDetailsAsync(id));
        }
    }
}
