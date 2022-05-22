using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.AppTroopers.SecurityTips;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTipCategory
{
    public partial class CreateSecurityTipCategoryCommand : IRequest<Response<SecurityTipCategory>>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
    public class CreateSecurityTipCategoryCommandHandler : IRequestHandler<CreateSecurityTipCategoryCommand, Response<SecurityTipCategory>>
    {
        private readonly ISecurityTipCategoryRepositorysync _securityTipCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public CreateSecurityTipCategoryCommandHandler(ISecurityTipCategoryRepositorysync securityTipCategoryRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            _securityTipCategoryRepository = securityTipCategoryRepository;
            _mapper = mapper;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

        public async Task<Response<SecurityTipCategory>> Handle(CreateSecurityTipCategoryCommand request, CancellationToken cancellationToken)
        {
            var securityTipCategory = _mapper.Map<SecurityTipCategory>(request);
            securityTipCategory.CreatedBy = _userAccessor.GetUserId();
            securityTipCategory.Created = DateTime.UtcNow.AddHours(1);
            await _securityTipCategoryRepository.AddAsync(securityTipCategory, _userAccessor.GetUserId());

            // return new Response<int>(product.Id);
            return new Response<SecurityTipCategory>(securityTipCategory, $"Security tip category successfully Created");

        }
    }
}
