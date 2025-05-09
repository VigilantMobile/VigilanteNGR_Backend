﻿using Application.Interfaces.Repositories;
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
    public class EscalatedTipsRepositoryAsync : GenericRepositoryAsync<EscalatedTip>, IEscalatedTipsRepositoryAsync
    {
        private readonly DbSet<EscalatedTip> _escalatedTips;

        public EscalatedTipsRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _escalatedTips = dbContext.Set<EscalatedTip>();
        }
    }
}
