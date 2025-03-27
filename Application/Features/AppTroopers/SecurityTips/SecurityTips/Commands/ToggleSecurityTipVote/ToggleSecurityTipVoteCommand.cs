using Application.Interfaces;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Wrappers;
using Domain.Common.Enums;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands.ToggleSecurityTipVote
{
    public class ToggleSecurityTipVoteCommand : IRequest<Response<GetSecurityTipResponse>>
    {
        public string SecurityTipId { get; set; }
        public CommentVoteEnum VoteType { get; set; }
    }

    public class ToggleSecurityTipVoteCommandHandler : IRequestHandler<ToggleSecurityTipVoteCommand, Response<GetSecurityTipResponse>>
    {
        private readonly ISecurityTipService _securityTipService;
        private readonly IUserAccessor _userAccessor;
        private readonly ILogger<ToggleSecurityTipVoteCommandHandler> _logger;

        public ToggleSecurityTipVoteCommandHandler(
            ISecurityTipService securityTipService,
            IUserAccessor userAccessor,
            ILogger<ToggleSecurityTipVoteCommandHandler> logger)
        {
            _securityTipService = securityTipService;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Response<GetSecurityTipResponse>> Handle(ToggleSecurityTipVoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userAccessor.GetUserId();
                var result = await _securityTipService.ToggleSecurityTipVoteAsync(userId, request.SecurityTipId, request.VoteType);
                return new Response<GetSecurityTipResponse>(result, responsestatus: APIResponseStatus.success.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling security tip vote for tip {SecurityTipId}", request.SecurityTipId);
                return new Response<GetSecurityTipResponse>(null, "An error occurred while toggling the vote.");
            }
        }
    }
}
