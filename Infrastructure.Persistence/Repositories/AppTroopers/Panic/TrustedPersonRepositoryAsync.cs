using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.AppTroopers.Panic;
using System.Linq;
using Application.Interfaces.Repositories.AppTroopers.Panic;

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
