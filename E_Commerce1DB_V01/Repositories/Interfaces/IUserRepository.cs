using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Payloads;
using E_Commerce1DB_V01.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task AddAddressAsync(AddAddressPayload payload, int userId);
        Task<bool> CheckEmailExistedAsync(string email);
        Task<User> GetUserByEmailAsync(string email);
        Task<AddressDTO> GetUserAddress(int id);
    }
}
