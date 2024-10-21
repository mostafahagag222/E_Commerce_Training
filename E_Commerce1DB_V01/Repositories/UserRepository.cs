using E_Commerce1DB_V01.DTOs;
using E_Commerce1DB_V01.Payloads;
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

        public async Task AddAddressAsync(AddressPayload payload, int userId)
        {
            var toBeAddedAddress = new Address()
            {
                City = payload.City,
                FirstName = payload.FirstName,
                LastName = payload.LastName,
                State = payload.State,
                Street = payload.Street,
                ZipCode = payload.zipcode,
                UserId = userId
            };
            await context.Addresses.AddAsync(toBeAddedAddress);
        }

        public async Task<bool> CheckEmailExistedAsync(string email)
        {
            return await context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<AddressDTO> GetUserAddress(int id)
        {
            return await context.Addresses
                .Where (a => a.UserId == id)
                .Select(a=>new AddressDTO()
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
}
