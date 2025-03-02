using Microsoft.EntityFrameworkCore;
using Orders.BackEnd.Data;
using Orders.BackEnd.Repositories.Interfaces;
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
            var states = await _context.States.Include(c => c.Cities).ToListAsync();

            return new ActionResponse<IEnumerable<State>>
            {
                WasSuccess = true,
                Result = states
            };
        }
    }
}
