using E_Commerce1DB_V01.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Interfaces
{
    public interface IBrandService
    {
        Task<List<GetBrandsDTO>> GetBrandsDTOAsync();
    }
}
