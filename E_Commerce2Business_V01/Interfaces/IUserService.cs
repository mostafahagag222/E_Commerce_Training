using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Payloads;
using E_Commerce2Business_V01.DTOs;
using E_Commerce2Business_V01.Payloads;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface IUserService
    {
        Task AddAddressAsync(AddAddressPayload payload, int userId);
        Task<bool> CheckEmailExists(string email);
        Task<LoginDTO> CreateAccountAsync(RegistrationPayload payload);
        LoginDTO GetLogInDTOFromTokenAsync(string token);
        Task<AddressDTO> GetUserAddressAsync(int userId);
        Task<LoginDTO> Login(LoginPayload payload);
    }
}
