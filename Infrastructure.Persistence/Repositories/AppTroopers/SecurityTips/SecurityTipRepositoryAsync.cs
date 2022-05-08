using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AppTroopers.SecurityTips;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;

namespace Infrastructure.Persistence.Repositories.SecurityTips
{
    public class SecurityTipRepositoryAsync : GenericRepositoryAsync<SecurityTip>, ISecurityTipRepositoryAsync
    {
        private readonly DbSet<SecurityTip> _securityTip;

        public SecurityTipRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _securityTip = dbContext.Set<SecurityTip>();
        }
    }
}
