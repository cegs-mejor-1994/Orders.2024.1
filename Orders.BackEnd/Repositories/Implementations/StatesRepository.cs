using Microsoft.EntityFrameworkCore;
using Orders.BackEnd.Data;
using Orders.BackEnd.Helpers;
using Orders.BackEnd.Repositories.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Entities;
using Orders.Shared.Responses;

namespace Orders.BackEnd.Repositories.Implementations
{
    public class StatesRepository : GenericRepository<State>, IStatesRepository
    {
        private readonly DataContext _context;

        public StatesRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<ActionResponse<State>> GetAsync(int id)
        {
            var state = await _context.States.Include(c => c.Cities).FirstOrDefaultAsync(c => c.Id == id);

            if (state == null)
            {
                return new ActionResponse<State>
                {
                    WasSuccess = false,
                    Message = "El Estado no existe"
                };
            }

            return new ActionResponse<State>
            {
                WasSuccess = true,
                Result = state
            };
        }

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync()
        {
            var states = await _context.States
                .OrderBy(s => s.Name)
                .ToListAsync();

            return new ActionResponse<IEnumerable<State>>
            {
                WasSuccess = true,
                Result = states
            };
        }

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.States
                .Include(c => c.Cities)
                .Where(c => c.Country!.Id == pagination.Id)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<State>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(s => s.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.States
                .Where(c => c.Country!.Id == pagination.Id)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }
                
            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            /*var count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling((double)count / pagination.RecordsNumber);*/

            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }
    }
}
