using Application.Services.Interfaces.Comments;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Comments.Queries.GetComments
{
    public class GetCommentsQuery : IRequest<PagedResponse<IEnumerable<CommentViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int SecurityTipId { get; set; }
    }

    public class GetAllCommentsQueryHandler : IRequestHandler<GetCommentsQuery, PagedResponse<IEnumerable<CommentViewModel>>>
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;

        public GetAllCommentsQueryHandler(
            ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService;
            this.mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CommentViewModel>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await this.commentService.GetAllCommentAsync(request.SecurityTipId, request.PageNumber, request.PageSize);
            var commentsViewModel = this.mapper.Map<IEnumerable<CommentViewModel>>(comments);

            return new PagedResponse<IEnumerable<CommentViewModel>>(commentsViewModel, request.PageNumber, request.PageSize);
        }
    }
}
