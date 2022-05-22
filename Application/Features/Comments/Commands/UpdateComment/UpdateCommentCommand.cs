using Application.Services.Interfaces.Comments;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.AppTroopers.SecurityTips;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Comments.Commands.UpdateComment
{
    public partial class UpdateCommentCommand : IRequest<Response<Comment>>
    {
        public int Id { get; set; }
        public string UserComment { get; set; }
        public int SecurityTipId { get; set; }
        public string CommenterId { get; set; }

        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response<Comment>>
        {
            private readonly ICommentService commentService;
            private readonly IMapper mapper;

            public UpdateCommentCommandHandler(ICommentService commentService, IMapper mapper)
            {
                this.commentService = commentService;
                this.mapper = mapper;
            }

            public async Task<Response<Comment>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = this.mapper.Map<Comment>(request);
                var updatedComment = await this.commentService.UpdateCommentAsync(comment.Id, comment);

                return new Response<Comment>(updatedComment, $"Comment updated successfully");
            }
        }
    }
}
