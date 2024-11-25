using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Payloads;
using E_Commerce1DB_V01.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce1DB_V01.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ECPContext context;
        public UserRepository(ECPContext context) : base(context)
        {
            this.context = context;
        }
        public async Task AddAddressAsync(AddAddressPayload addAddressPayload, int userId)
        {
            var toBeAddedAddress = new Address()
            {
                City = addAddressPayload.City,
                FirstName = addAddressPayload.FirstName,
                LastName = addAddressPayload.LastName,
                State = addAddressPayload.State,
                Street = addAddressPayload.Street,
                ZipCode = addAddressPayload.zipcode,
                UserId = userId
            };
            await context.Addresses.AddAsync(toBeAddedAddress);
        }
        public async Task<bool> CheckEmailExistedAsync(string email) => await context.Users
            .AnyAsync(u => u.Email == email);
        public async Task<User> GetUserByEmailAsync(string email) => await context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();
        public async Task<AddressDTO> GetUserAddress(int id) => await context.Addresses
                .Where(a => a.UserId == id)
                .Select(a => new AddressDTO()
                {
                    City = a.City,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    State = a.State,
                    Street = a.Street,
                    ZipCode = a.ZipCode
                }).FirstOrDefaultAsync();
    }
}
