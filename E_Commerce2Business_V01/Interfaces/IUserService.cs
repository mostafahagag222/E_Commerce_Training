using E_Commerce1DB_V01;
using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Payloads;
using E_Commerce2Business_V01.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface IUserService
    {
        Task AddAddressAsync(AddressPayload payload, int userId);
        Task<bool> CheckEmailExisted(string email);
        Task<LoginDTO> CreateAccount(RegistrationPayload payload);
        LoginDTO ExtractDataFromTokenAsync(string token);
        Task<AddressDTO> GetAddress(int userId);
        Task<LoginDTO> Login(LoginPayload payload);
    }
}
