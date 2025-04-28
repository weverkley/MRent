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
    }
}

