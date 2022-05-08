using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.LocationEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Location
{
    public interface ILGARepositoryAsync : IGenericRepositoryAsync<LGA>
    {
        //Task<bool> IsUniqueLGAinState(int StateId, string LGAName);
        Task<LGA> GetLGAWithStateAsync(int LGAid);
        Task<List<Town>> GetDistrictsinLGAAsync(int LGAid);
    }
}
