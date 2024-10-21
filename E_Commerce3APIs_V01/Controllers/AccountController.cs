using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Payloads;
using E_Commerce1DB_V01.Repositories;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce3APIs_V01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseAPIController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegistrationPayload payload)
        {
            return Ok(await _userService.CreateAccount(payload));
        }
        [HttpGet("EmailExists")]
        public async Task<bool> EmailExists(string email)
        {
            return await _userService.CheckEmailExisted(email);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginPayload payload)
        {
            return Ok(await _userService.Login(payload));
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetDataFromToken ()
        {
            var token = ExtractToken();
            return Ok(_userService.ExtractDataFromTokenAsync(token));
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<IActionResult> GetAddress()
        {
            AddressDTO address = await _userService.GetAddress(ExtractIdFromToken());
            return Ok(address);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<IActionResult> AddAddress(AddressPayload payload)
        {
            await _userService.AddAddressAsync(payload, ExtractIdFromToken());
            return NoContent();
        }
    }
}
