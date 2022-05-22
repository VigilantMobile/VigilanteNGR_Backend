using Domain.Entities.AppTroopers.SecurityTips;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.Comments
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(Comment comment, string userId);
        Task<Comment> GetCommentAsync(int commentId);

        Task<ICollection<Comment>>
            GetAllCommentAsync(int securityTipId, int pageNumber, int pageSize);
        Task<Comment> UpdateCommentAsync(int commentId, Comment comment);
        Task<Comment> DeleteCommentAsync(int commentId);
    }
}
