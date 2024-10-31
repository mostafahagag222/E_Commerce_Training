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
    public static class IQueryableExtensions
    {
        public static async Task<PaginationDTO<T>> ToPaginationAsync<T>(this IQueryable<T> values, int pageIndex, int pageSize)
        {
            var count = await values.CountAsync();
            var pageItems = await values
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var paginationDTO = new PaginationDTO<T>()
            {
                PageNumber = pageIndex,
                Count = count,
                Data = pageItems,
                PageSize = pageSize
            };
            return paginationDTO;
        }
    }
}
