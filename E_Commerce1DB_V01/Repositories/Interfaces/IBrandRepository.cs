using E_Commerce1DB_V01.DTOs;

namespace E_Commerce1DB_V01.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task<bool> CheckExistenceByIDAsync(int? id);
        Task<List<GetBrandsDTO>> GetBrandsDTOAsync();
        ECPContext EcpContext { get; }
    }
}
