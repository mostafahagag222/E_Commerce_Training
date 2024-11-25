using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Payloads;

namespace E_Commerce1DB_V01.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task AddAddressAsync(AddAddressPayload payload, int userId);
        Task<bool> CheckEmailExistedAsync(string email);
        Task<User> GetUserByEmailAsync(string email);
        Task<AddressDTO> GetUserAddress(int id);
    }
}
