using Application.Exceptions;
using Application.Interfaces.Repositories.AppTroopers.Comments;
using Application.Services.Interfaces.Comments;
using AutoMapper;
using Domain.Entities.AppTroopers.SecurityTips;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services.Implementations.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepositoryAsync commentRepository;
        private readonly IMapper mapper;

        public CommentService(
            ICommentRepositoryAsync commentRepository,
            IMapper mapper
            )
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment, string userId)
        {
            return await this.commentRepository.AddAsync(comment, userId);
        }

        public async Task<ICollection<Comment>>
            GetAllCommentAsync(int securityTipId, int pageNumber, int pageSize)
        {
            var comments = await this.commentRepository
                .GetAllCooments(securityTipId, pageNumber, pageSize);
            return comments;
        }

        public async Task<Comment> GetCommentAsync(int commentId)
        {
            var comment = await this.commentRepository.GetByIdAsync(commentId);
            if (comment == null) throw new ApiException($"Comment not found.");
            return comment;
        }

        public async Task<Comment> UpdateCommentAsync(int commentId, Comment comment)
        {
            var commentInDb = await this.GetCommentAsync(commentId);

            commentInDb.UserComment = comment.UserComment;
            commentInDb.CommenterId = comment.CommenterId;

            await this.commentRepository.UpdateAsync(commentInDb);

            return commentInDb;
        }

        public async Task<Comment> DeleteCommentAsync(int commentId)
        {
            var commentInDb = await this.GetCommentAsync(commentId);
            if (commentInDb == null) throw new ApiException($"Comment not found.");

            await this.commentRepository.DeleteAsync(commentInDb);
            return commentInDb;
        }
    }
}
