﻿using Application.Exceptions;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Wrappers;
using Domain.Entities.AppTroopers.SecurityTips;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands.DeleteProductById
{
    public class DeleteSecurityTipCategoryByIdCommand : IRequest<Response<SecurityTipCategory>>
    {
        public string Id { get; set; }
        public class DeleteSecurityTipCategoryByIdCommandHandler : IRequestHandler<DeleteSecurityTipCategoryByIdCommand, Response<SecurityTipCategory>>
        {
            private readonly ISecurityTipCategoryRepositoryAsync _securityTipCategoryRepository;
            public DeleteSecurityTipCategoryByIdCommandHandler(ISecurityTipCategoryRepositoryAsync securityTipCategoryRepository)
            {
                _securityTipCategoryRepository = securityTipCategoryRepository;
            }
            public async Task<Response<SecurityTipCategory>> Handle(DeleteSecurityTipCategoryByIdCommand command, CancellationToken cancellationToken)
            {
                var securityTipCategory = await _securityTipCategoryRepository.GetByIdAsync(command.Id);
                if (securityTipCategory == null) throw new ApiException($"Security tip category not found.");
                await _securityTipCategoryRepository.DeleteAsync(securityTipCategory);

                //return new Response<Product>(product.Id);

                return new Response<SecurityTipCategory>(securityTipCategory, $"Security tip category successfully deleted");

            }
        }
    }
}
