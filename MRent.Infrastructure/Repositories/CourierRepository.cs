using Microsoft.EntityFrameworkCore;
using MRent.Domain.Entities;
using MRent.Domain.Repositories;
using MRent.Infrastructure.Contexts;

namespace MRent.Infrastructure.Repositories
{
    public class CourierRepository : BaseRepository<CourierEntity>, ICourierRepository
    {
        public CourierRepository(PostgresContext context) : base(context)
        {
        }

        public async Task<bool> IsCNPJUniqueAsync(string cnpj)
        {
            var entity = await _context.Couriers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CNPJ == cnpj);
            return entity == null;
        }

        public async Task<bool> IsCNHUniqueAsync(string cnh)
        {
            var entity = await _context.Couriers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CNH == cnh);
            return entity == null;
        }
    }
}

