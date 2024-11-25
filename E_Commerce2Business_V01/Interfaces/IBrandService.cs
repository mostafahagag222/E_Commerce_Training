using E_Commerce1DB_V01.DTOs;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface IBrandService
    {
        Task<List<GetBrandsDTO>> GetBrandsDTOAsync();
    }
}
