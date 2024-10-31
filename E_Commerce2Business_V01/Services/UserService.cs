using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Payloads;
using E_Commerce1DB_V01.Repositories;
using E_Commerce2Business_V01.DTOs;
using E_Commerce2Business_V01.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<LoginDTO> CreateAccountAsync(RegistrationPayload registrationPayload)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registrationPayload.Password);
            var user = new User()
            {
                Email = registrationPayload.Email.ToLower(),
                Name = registrationPayload.DisplayName,
                HashedPassword = hashedPassword
            };
            var createdUser = await CreateUserAsync(user);
            var logInDTO = CreateLoginDTO(createdUser);
            _logger.LogInformation($"Account With ID {createdUser.Id} was successfully created for Email : {createdUser.Email}  at : {DateTime.Now}");
            return logInDTO;
        }
        public async Task<LoginDTO> Login(LoginPayload loginPayload)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(loginPayload.Email);
            if (user == null)
                throw new BadRequestException("user not found");
            if (!BCrypt.Net.BCrypt.Verify(loginPayload.Password, user.HashedPassword))
                throw new BadRequestException("invalid password", user.Id);
            LoginDTO logInDTO = CreateLoginDTO(user);
            _logger.LogInformation($"Successful Login for user with id : {user.Id} at : {DateTime.Now}");
            return logInDTO;
        }
        public async Task<bool> CheckEmailExists(string email)
        {
            var doesEmailExist = await _unitOfWork.UserRepository.CheckEmailExistedAsync(email);
            return doesEmailExist;
        }
        public LoginDTO GetLogInDTOFromTokenAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var logInDTO = new LoginDTO()
            {
                DisplayName = jsonToken.Claims.FirstOrDefault(c => c.Type == "name").Value,
                Email = jsonToken.Claims.FirstOrDefault(c => c.Type == "email").Value,
                Token = token
            };
            return logInDTO;
        }
        public async Task<AddressDTO> GetUserAddressAsync(int userId)
        {
            var userAddressDTO = await _unitOfWork.UserRepository.GetUserAddress(userId);
            return userAddressDTO;
        }
        public async Task AddAddressAsync(AddAddressPayload payload, int userId)
        {
            await _unitOfWork.UserRepository.AddAddressAsync(payload, userId);
            var saveChanges = await _unitOfWork.SaveChangesAsync();
            if (saveChanges < 1)
                throw new InternalServerErrorException("couldn't add Address");
        }
        private string GenerateJWTToken(List<Claim> c)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddYears(1),
                signingCredentials: credentials,
                claims: c
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private async Task<User> CreateUserAsync(User user)
        {
            await _unitOfWork.UserRepository.AddAsync(user);
            var saveChanges = await _unitOfWork.SaveChangesAsync();
            if (saveChanges < 0)
                throw new InternalServerErrorException("something went Wrong Couldn't create account");
            return user;
        }
        private LoginDTO CreateLoginDTO(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("name", user.Name));
            claims.Add(new Claim("email", user.Email));
            claims.Add(new Claim("id", user.Id.ToString()));
            var loginDTO = new LoginDTO()
            {
                DisplayName = user.Name,
                Email = user.Email,
                Token = GenerateJWTToken(claims)
            };
            return loginDTO;
        }
    }
}
