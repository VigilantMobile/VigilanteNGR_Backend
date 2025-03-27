using Application.Interfaces;
using Application.Services.Interfaces.AppTroopers.SecurityTips;
using Application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Application.Features.AppTroopers.SecurityTips.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Response<GetSecurityTipResponse>>
    {
        public string SecurityTipId { get; set; }
        public string CommentText { get; set; }

        public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<GetSecurityTipResponse>>
        {
            private readonly ISecurityTipService _securityTipService;
            private readonly IUserAccessor _userAccessor;
            private readonly ILogger<CreateCommentCommandHandler> _logger;

            public CreateCommentCommandHandler(
                ISecurityTipService securityTipService,
                IUserAccessor userAccessor,
                ILogger<CreateCommentCommandHandler> logger)
            {
                _securityTipService = securityTipService;
                _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<Response<GetSecurityTipResponse>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUserId();
                var result = await _securityTipService.CreateCommentAsync(userId, request.SecurityTipId, request.CommentText);
                return new Response<GetSecurityTipResponse>(result, "Comment created successfully");
            }
        }
    }
}