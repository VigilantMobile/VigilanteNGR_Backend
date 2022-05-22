using Application.Services.Interfaces.Comments;
using Application.Wrappers;
using Domain.Entities.AppTroopers.SecurityTips;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<Response<Comment>>
    {
        public int Id { get; set; }

        public class DeleteCommentByIdCommandHandler : IRequestHandler<DeleteCommentCommand, Response<Comment>>
        {
            private readonly ICommentService commentService;

            public DeleteCommentByIdCommandHandler(ICommentService commentService)
            {
                this.commentService = commentService;
            }

            public async Task<Response<Comment>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var comment = await this.commentService.DeleteCommentAsync(request.Id);

                return new Response<Comment>(comment, $"Comment successfully deleted");
            }
        }
    }
}
