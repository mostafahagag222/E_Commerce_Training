using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Repositories
{
    public interface ITypeRepository
    {
        Task<bool> CheckExistenceByIDAsync(int? id);
    }
}
