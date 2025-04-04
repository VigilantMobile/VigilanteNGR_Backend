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

namespace Application.Features.AppTroopers.SecurityTips.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<Response<GetSecurityTipResponse>>
    {
        public string CommentId { get; set; }
        public string UpdatedComment { get; set; }

        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response<GetSecurityTipResponse>>
        {
            private readonly ISecurityTipService _securityTipService;
            private readonly IUserAccessor _userAccessor;
            private readonly ILogger<UpdateCommentCommandHandler> _logger;

            public UpdateCommentCommandHandler(
                ISecurityTipService securityTipService,
                IUserAccessor userAccessor,
                ILogger<UpdateCommentCommandHandler> logger)
            {
                _securityTipService = securityTipService;
                _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<Response<GetSecurityTipResponse>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUserId();
                var result = await _securityTipService.UpdateCommentAsync(userId, request.CommentId, request.UpdatedComment);
                return new Response<GetSecurityTipResponse>(result, "Comment updated successfully");
            }
        }
    }
}
