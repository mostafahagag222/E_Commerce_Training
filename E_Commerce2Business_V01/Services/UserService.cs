using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Payloads;
using E_Commerce1DB_V01.Repositories;
using E_Commerce2Business_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace E_Commerce2Business_V01.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<LoginDTO> CreateAccount(RegistrationPayload payload)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(payload.Password);
            var user = new User()
            {
                Email = payload.Email.ToLower(),
                Name = payload.DisplayName,
                HashedPassword = hashedPassword

            };
            await _unitOfWork.UserRepository.AddAsync(user);
            var r = await _unitOfWork.SaveChangesAsync();
            if (r > 0)
            {
                var DTO = new LoginDTO()
                {
                    DisplayName = payload.DisplayName,
                    Email = payload.Email,
                    Token = GenerateJWTToken(null)
                };
                return DTO;
            }
            else
                throw new InternalServerErrorException("something went Wrong Couldn't create account");
        }
        public async Task<LoginDTO> Login(LoginPayload payload)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(payload.Email);
            if (user == null)
                throw new BadRequestException("user not found");
            if (!BCrypt.Net.BCrypt.Verify(payload.Password, user.HashedPassword))
                throw new BadRequestException("invalid password");
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("name", user.Name));
            claims.Add(new Claim("email", user.Name));
            claims.Add(new Claim("id", user.Id.ToString()));
            return new LoginDTO()
            {
                DisplayName = user.Name,
                Email = user.Email,
                Token = GenerateJWTToken(claims)
            };
        }
        public async Task<bool> CheckEmailExisted(string email)
        {
            return await _unitOfWork.UserRepository.CheckEmailExistedAsync(email);
        }
        public LoginDTO ExtractDataFromTokenAsync(string token)
        {
            if (!IsTokenValidAndNotExpired(token))
                throw new UnAuthorizedException("Token Is invalid or expired");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            return new LoginDTO()
            {
                DisplayName = jsonToken.Claims.FirstOrDefault(c => c.Type == "name").Value,
                Email = jsonToken.Claims.FirstOrDefault(c => c.Type == "email").Value,
                Token = token
            };
        }
        private string GenerateJWTToken(List<Claim> c)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A7A123A7tin2223A7a3jsadfbfsdhbjidsakjdsankasdjbkjafbfakjsbjkasdbjklasdfbkajskd"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddYears(1),
                signingCredentials: credentials,
                claims: c
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool IsTokenValidAndNotExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("A7A123A7tin2223A7a3jsadfbfsdhbjidsakjdsankasdjbkjafbfakjsbjkasdbjklasdfbkajskd");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = "yourIssuer",
                    ValidAudience = "yourAudience",
                    ValidateLifetime = true, // Check for expiration
                    ClockSkew = TimeSpan.Zero // Optional: reduce clock skew if needed
                }, out _);

                // If no exception was thrown, the token is valid and not expired
                return true;
            }
            catch (SecurityTokenExpiredException)
            {
                // Token is expired
                return false;
            }
            catch (Exception)
            {
                // Token is invalid (e.g., signature mismatch, wrong issuer/audience)
                return false;
            }
        }

        public Task<AddressDTO> GetAddress(int userId)
        {
            return _unitOfWork.UserRepository.GetUserAddress(userId);
        }

        public async Task AddAddressAsync(AddressPayload payload, int userId)
        {
            await _unitOfWork.UserRepository.AddAddressAsync(payload, userId);
            if (await _unitOfWork.SaveChangesAsync() < 1)
                throw new InternalServerErrorException("couldn't add Address");
        }
    }
}
