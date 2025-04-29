using Microsoft.EntityFrameworkCore;
using MRent.Domain.Entities;
using MRent.Domain.Repositories;
using MRent.Infrastructure.Contexts;

namespace MRent.Infrastructure.Repositories
{
    public class RentRepository : BaseRepository<RentEntity>, IRentRepository
    {
        public RentRepository(PostgresContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RentEntity>> GetByMotorcycleIdAsync(Guid motorcycleId)
        {
            return await _context.Rents
                .AsNoTracking()
                .Where(m => m.MotorcycleId == motorcycleId)
                .ToListAsync();
        }

        public override async Task<RentEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Rents
                .Include(i => i.Plan)
                .Include(i => i.Motorcycle)
                .Include(i => i.Courier)
                .AsNoTracking()
                .Where(m => m.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}

