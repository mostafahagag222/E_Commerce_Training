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
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBasket(UpdateBasketPayload payload) => Ok(await _basketService.UpdateBasketAsync(payload));
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _basketService.DeleteBasketAsync(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetBasketDetails(string id)
        {
            return Ok(await _basketService.GetBasketDetailsAsync(id));
        }
    }
}
