using Application.Interfaces.Repositories.AppTroopers.Comments;
using Domain.Entities.AppTroopers.SecurityTips;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.AppTroopers.Comments
{
    public class CommentRepositoryAsync : GenericRepositoryAsync<Comment>, ICommentRepositoryAsync
    {
        private readonly DbSet<Comment> _comments;
        public CommentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _comments = dbContext.Set<Comment>();
        }
    }
}
