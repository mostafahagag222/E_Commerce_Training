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
        public async Task<IActionResult> Register(RegistrationPayload registrationPayload)
        {
            var loginDTO = await _userService.CreateAccountAsync(registrationPayload);
            return Ok(loginDTO);
        }
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExisting(string email)
        {
            var doesEmailExist = await _userService.CheckEmailExists(email);
            return Ok(doesEmailExist);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginPayload logInPayload)
        {
            var loginDTO = await _userService.Login(logInPayload);
            return Ok(loginDTO);
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetDataFromToken ()
        {
            var token = ExtractToken();
            var loginDTO = _userService.GetLogInDTOFromTokenAsync(token);
            return Ok(loginDTO);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<IActionResult> GetUserAddress()
        {
            var userId = ExtractIdFromToken();
            AddressDTO address = await _userService.GetUserAddressAsync(userId);
            return Ok(address);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<IActionResult> AddAddress(AddAddressPayload addAddressPayload)
        {
            var userId = ExtractIdFromToken();
            await _userService.AddAddressAsync(addAddressPayload, userId);
            return NoContent();
        }
    }
}
