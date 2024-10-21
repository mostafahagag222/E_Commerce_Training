using E_Commerce1DB_V01.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Extensions
{
    public static class IQuerybleExtensions
    {
        public static async Task<PaginationDTO<T>> ToPaginationAsync<T>(this IQueryable<T> values, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                throw new ArgumentException("invalid Page index must be greater than zero");
            if (pageSize < 1)
                throw new ArgumentException("invalid Page size must be greater than zero");
            var count = await values.CountAsync();
            var r = await values
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginationDTO<T>()
            {
                PageNumber = pageIndex,
                Count = count,
                Data = r,
                PageSize = pageSize
            };
        }
    }
}
