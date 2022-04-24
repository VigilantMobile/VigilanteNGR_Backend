using Application.Features.AppTroopers.SecurityTip.SecurityTip.Commands;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.SecurityTip;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTip;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTip.Commands.CreateSecurityTipCategory
{
    public partial class CreateSecurityTipCommand : IRequest<Response<CreateSecurityTipResponse>>
    {
        public string CustomerId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Description { get; set; }
        public int Locationid { get; set; }
        public int CategoryId { get; set; }
        public int alertLevel { get; set; }
        public int BroadcastLevelId { get; set; }
    }
    public class CreateSecurityTipCommandCommandHandler : IRequestHandler<CreateSecurityTipCommand, Response<CreateSecurityTipResponse>>
    {
        private readonly ISecurityTipCategoryRepositorysync _securityTipCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public CreateSecurityTipCommandCommandHandler(ISecurityTipCategoryRepositorysync securityTipCategoryRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            _securityTipCategoryRepository = securityTipCategoryRepository;
            _mapper = mapper;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

        public async Task<Response<CreateSecurityTipResponse>> Handle(CreateSecurityTipCommand request, CancellationToken cancellationToken)
        {
            var securityTipCategory = _mapper.Map<SecurityTipCategory>(request);
            securityTipCategory.CreatedBy = _userAccessor.GetUserId();
            securityTipCategory.Created = DateTime.UtcNow.AddHours(1);
            await _securityTipCategoryRepository.AddAsync(securityTipCategory, _userAccessor.GetUserId());
           
           // return new Response<int>(product.Id);
            return new Response<CreateSecurityTipResponse>(securityTipCategory, $"Security tip category successfully Created");

        }
    }
}
