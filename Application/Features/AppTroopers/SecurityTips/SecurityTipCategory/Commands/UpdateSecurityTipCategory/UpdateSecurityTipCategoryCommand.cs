using Application.Exceptions;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Wrappers;
using Domain.Entities.AppTroopers.SecurityTips;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands.UpdateSecurityTipCategory
{
    public class UpdateSecurityTipCategoryCommand : IRequest<Response<SecurityTipCategory>>
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public class UpdateSecurityTipCategoryCommandHandler : IRequestHandler<UpdateSecurityTipCategoryCommand, Response<SecurityTipCategory>>
        {
            private readonly ISecurityTipCategoryRepositorysync _securityTipCategoryRepository;
            public UpdateSecurityTipCategoryCommandHandler(ISecurityTipCategoryRepositorysync securityTipCategoryRepository)
            {
                _securityTipCategoryRepository = securityTipCategoryRepository;
            }
            public async Task<Response<SecurityTipCategory>> Handle(UpdateSecurityTipCategoryCommand command, CancellationToken cancellationToken)
            {
                var securityTipCategory = await _securityTipCategoryRepository.GetByIdAsync(command.Id);

                if (securityTipCategory == null)
                {
                    throw new ApiException($"Security Tip Category Not Found.");
                }
                else
                {
                    securityTipCategory.CategoryName = command.CategoryName;
                    securityTipCategory.Description = command.Description;
                    securityTipCategory.LastModified = DateTime.UtcNow.AddHours(1);
                    await _securityTipCategoryRepository.UpdateAsync(securityTipCategory);

                    //return new Response<int>(product.Id); 
                    return new Response<SecurityTipCategory>(securityTipCategory, $"Security tip category successfully updated");
                }
            }
        }
    }
}
