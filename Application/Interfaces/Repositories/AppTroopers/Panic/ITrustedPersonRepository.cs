using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTip;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.Panic
{
    public interface ITrustedPersonRepositoryAsync : IGenericRepositoryAsync<TrustedPerson>
    {
        Task<TrustedPerson> IsOwnedByOwner(int Id, string ContactOwnerId);

    }
}
