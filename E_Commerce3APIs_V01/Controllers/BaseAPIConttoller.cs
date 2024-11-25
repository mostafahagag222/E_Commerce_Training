using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace E_Commerce3APIs_V01.Controllers
{
    [ApiController]
    public class BaseAPIController :ControllerBase
    {
        protected string ExtractToken ()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString();
            return token.Split(" ")[1];
        }
        protected int ExtractIdFromToken ()
        {
            var token = ExtractToken();
            var tokenHandler = new JwtSecurityTokenHandler();
            var id = int.Parse(tokenHandler.ReadJwtToken(token).Claims.FirstOrDefault(c => c.Type == "id").Value);
            return id;
        }

    }
}
