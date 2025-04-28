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
    }
}

