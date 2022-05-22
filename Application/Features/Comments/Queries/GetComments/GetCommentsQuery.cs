using Application.Services.Interfaces.Comments;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Comments.Queries.GetComments
{
    public class GetCommentsQuery : IRequest<PagedResponse<IEnumerable<CommentViewModel>>>
    {
        public int SecurityTipId { get; set; }
    }

    public class GetAllCommentsQueryHandler : IRequestHandler<GetCommentsQuery, Response<IEnumerable<CommentViewModel>>>
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;

        public GetAllCommentsQueryHandler(
            ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService;
            this.mapper = mapper;
        }

        public async Task<Response<IEnumerable<CommentViewModel>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await this.commentService.GetAllCommentAsync(request.SecurityTipId);
            var commentsViewModel = this.mapper.Map<IEnumerable<CommentViewModel>>(comments);

            return new Response<IEnumerable<CommentViewModel>>(commentsViewModel, $"Comment retrieved succesfully");
        }
    }
}
