using Application.Services.Interfaces.Comments;
using Application.Wrappers;
using Domain.Entities.AppTroopers.SecurityTips;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Comments.Queries.GetCommentById
{
    public class GetCommentByIdQuery : IRequest<Response<Comment>>
    {
        public int Id { get; set; }

        public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, Response<Comment>>
        {
            private readonly ICommentService commentService;

            public GetCommentByIdQueryHandler(ICommentService commentService)
            {
                this.commentService = commentService;
            }

            public async Task<Response<Comment>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
            {
                var comment = await this.commentService.GetCommentAsync(request.Id);

                return new Response<Comment>(comment, $"Comment retrieval successful");
            }
        }
    }
}
