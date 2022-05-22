using Application.Wrappers;
using MediatR;
using System;
using Domain.Entities.AppTroopers.SecurityTips;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories.AppTroopers.Comments;
using Application.Services.Interfaces.Comments;
using AutoMapper;
using Application.Interfaces;

namespace Application.Features.Comments.Commands.CreateComment
{
    public partial class CreateCommentCommand : IRequest<Response<Comment>>
    {
        public string UserComment { get; set; }
        public int SecurityTipId { get; set; }
        public string CommenterId { get; set; }
    }

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<Comment>>
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        private readonly IUserAccessor userAccessor;

        public CreateCommentCommandHandler(ICommentService commentService, IMapper mapper, IUserAccessor userAccessor)
        {
            this.commentService = commentService;
            this.mapper = mapper;
            this.userAccessor = userAccessor;
        }

        public async Task<Response<Comment>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = this.mapper.Map<Comment>(request);
            await this.commentService.CreateCommentAsync(comment, this.userAccessor.GetUserId());

            return new Response<Comment>(comment, $"Comment posted succesfully");
        }
    }
}
