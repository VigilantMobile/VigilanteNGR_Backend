using Domain.DTOs.Comments;
using Domain.Entities.AppTroopers.SecurityTips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Features.Comments.CommentViewModel;

namespace Application.Services.Interfaces.Comments
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(Comment comment, string userId);
        Task<Comment> GetCommentAsync(int commentId);
        Task<List<Comment>> GetAllCommentAsync(int securityTipId);
        Task<Comment> UpdateCommentAsync(int commentId, Comment comment);
        Task<Comment> DeleteCommentAsync(int commentId);
    }
}
