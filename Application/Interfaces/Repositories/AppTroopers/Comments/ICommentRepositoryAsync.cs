using Domain.Entities.AppTroopers.SecurityTips;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.AppTroopers.Comments
{
    public interface ICommentRepositoryAsync : IGenericRepositoryAsync<Comment>
    {
        Task<ICollection<Comment>>
            GetAllCooments(int securityTipId, int pageNumber, int pageSize);
    }
}