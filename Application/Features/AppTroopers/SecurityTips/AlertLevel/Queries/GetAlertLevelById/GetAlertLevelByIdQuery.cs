using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Wrappers;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTips;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.GetSecurityTipCategoryById
{
    public class GetAlertLevelByIdQuery : IRequest<Response<AlertLevel>>
    {
        public string AlertLevelId { get; set; }
        public class GetAlertLevelByIdQueryHandler : IRequestHandler<GetAlertLevelByIdQuery, Response<AlertLevel>>
        {
            private readonly IAlertLevelRepositoryAsync _alertLevelRepository;
            public GetAlertLevelByIdQueryHandler(IAlertLevelRepositoryAsync alertLevelRepository)
            {
                _alertLevelRepository = alertLevelRepository;
            }
            public async Task<Response<AlertLevel>> Handle(GetAlertLevelByIdQuery query, CancellationToken cancellationToken)
            {
                var alertLevel = await _alertLevelRepository.GetByIdAsync(query.AlertLevelId);
                if (alertLevel == null) throw new ApiException($"Alert level not found.");

                // return new Response<Product>(product);

                return new Response<AlertLevel>(alertLevel, $"Alert level retrieval successful");
            }
        }
    }
}
