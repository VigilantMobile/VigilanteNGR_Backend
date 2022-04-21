using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.LocationEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Location
{
    public interface ITownRepositoryAsync : IGenericRepositoryAsync<Town>
    {
        Task<bool> IsUniqueTowninLGA(int LGAId, string townName);
        Task<Town> GetTownWithLGAAsync(int id);
    }
}
