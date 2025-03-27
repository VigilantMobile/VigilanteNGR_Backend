using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Wrappers;
using Domain.Common.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands.ToggleCommentVote
{
    public class ToggleCommentVoteCommand : IRequest<Response<GetSecurityTipResponse>>
    {
        public string CommentId { get; set; }
        public CommentVoteEnum VoteType { get; set; }
    }

    public class ToggleCommentVoteCommandHandler : IRequestHandler<ToggleCommentVoteCommand, Response<GetSecurityTipResponse>>
    {
        private readonly ISecurityTipService _securityTipService;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<ToggleCommentVoteCommandHandler> _logger;

        public ToggleCommentVoteCommandHandler(
            ISecurityTipService securityTipService,
            IUserAccessor userAccessor,
            ILogger<ToggleCommentVoteCommandHandler> logger)
        {
            _securityTipService = securityTipService;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response<GetSecurityTipResponse>> Handle(ToggleCommentVoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userAccessor.GetUserId();
                var result = await _securityTipService.ToggleCommentVoteAsync(userId, request.CommentId, request.VoteType);
                return new Response<GetSecurityTipResponse>(result, responsestatus: APIResponseStatus.success.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling comment vote for comment {CommentId}", request.CommentId);
                return new Response<GetSecurityTipResponse>(null, "An error occurred while toggling the vote.");
            }
        }
    }
}
