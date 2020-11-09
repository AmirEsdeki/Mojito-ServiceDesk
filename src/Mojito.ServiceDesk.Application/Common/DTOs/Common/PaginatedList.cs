using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Common
{
    public class PaginatedList<TDTO>
    {
        public List<TDTO> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public PaginatedList(List<TDTO> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;
    }

    public class PaginatedListBuilder<T, TDTO> 
        where T : class 
        where TDTO : class
    {
        private readonly IMapper mapper;

        public PaginatedListBuilder(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public async Task<PaginatedList<TDTO>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            try
            {
                List<T> items;

                if (pageIndex != 0 && pageSize != 0)
                    items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                else
                    items = await source.ToListAsync();

                var mappedItems = mapper.Map<List<TDTO>>(items ??= new List<T>());

                return new PaginatedList<TDTO>(mappedItems, count, pageIndex, pageSize);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
