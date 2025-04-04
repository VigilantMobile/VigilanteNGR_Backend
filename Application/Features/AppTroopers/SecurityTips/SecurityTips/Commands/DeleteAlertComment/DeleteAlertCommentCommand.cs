using Application.Interfaces;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<Response<GetSecurityTipResponse>>
    {
        public string SecurityTipId { get; set; }
        public string CommentId { get; set; }

        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Response<GetSecurityTipResponse>>
        {
            private readonly ISecurityTipService _securityTipService;
            private readonly IUserAccessor _userAccessor;
            private readonly ILogger<DeleteCommentCommandHandler> _logger;

            public DeleteCommentCommandHandler(
                ISecurityTipService securityTipService,
                IUserAccessor userAccessor,
                ILogger<DeleteCommentCommandHandler> logger)
            {
                _securityTipService = securityTipService;
                _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<Response<GetSecurityTipResponse>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUserId();
                var result = await _securityTipService.DeleteCommentAsync(userId, request.SecurityTipId, request.CommentId);
                return new Response<GetSecurityTipResponse>(result, "Comment deleted successfully");
            }
        }
    }
}
