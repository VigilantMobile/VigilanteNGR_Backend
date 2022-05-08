using Application.Interfaces.Repositories.AppTroopers.Panic;
using Domain.Entities.AppTroopers.Panic;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Panic
{
    public class TrustedPersonRepositoryAsync : GenericRepositoryAsync<TrustedPerson>, ITrustedPersonRepositoryAsync
    {
        private readonly DbSet<TrustedPerson> _trustedPerson;
        private readonly ApplicationDbContext _context;

        public TrustedPersonRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _trustedPerson = dbContext.Set<TrustedPerson>();
        }

        public async Task<TrustedPerson> IsOwnedByOwner(int Id, string ContactOwnerId)
        {
            return await _trustedPerson.Where(x => x.Id == Id && x.OwnerId == ContactOwnerId).FirstOrDefaultAsync();
        }
    }
}
