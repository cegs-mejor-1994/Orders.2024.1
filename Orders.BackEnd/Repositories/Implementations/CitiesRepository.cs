﻿using Microsoft.EntityFrameworkCore;
using Orders.BackEnd.Data;
using Orders.BackEnd.Helpers;
using Orders.BackEnd.Repositories.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.BackEnd.Repositories.Implementations
{
    public class CitiesRepository : GenericRepository<City>, ICitiesRepository
    {
        private readonly DataContext _context;

        public CitiesRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Cities
                .Where(c => c.State!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<City>>
            {
                WasSuccess = true,
                Result = await queryable.
                OrderBy(c => c.Name)
                .Paginate(pagination)
                .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Cities
                .Where(c => c.State!.Id == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

    }
}
