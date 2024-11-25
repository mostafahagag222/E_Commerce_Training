using E_Commerce1DB_V01.Entities;
using E_Commerce1DB_V01.Repositories.Interfaces;

namespace E_Commerce1DB_V01.Repositories;

public class UserTokenRepository : GenericRepository<UserToken> , IUserTokenRepository
{
    private readonly ECPContext _context;
    public UserTokenRepository(ECPContext context) : base(context)
    {
        _context = context;
    }
}