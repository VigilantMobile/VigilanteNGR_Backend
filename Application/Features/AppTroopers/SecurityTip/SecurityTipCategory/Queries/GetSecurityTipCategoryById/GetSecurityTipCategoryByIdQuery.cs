using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.SecurityTip;
using Application.Wrappers;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTip;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTip.GetSecurityTipCategoryById
{
    public class GetSecurityTipCategoryByIdQuery : IRequest<Response<SecurityTipCategory>>
    {
        public int Id { get; set; }
        public class GetSecurityTipCategoryByIdQueryHandler : IRequestHandler<GetSecurityTipCategoryByIdQuery, Response<SecurityTipCategory>>
        {
            private readonly ISecurityTipCategoryRepositorysync _securityTipCategoryRepository;
            public GetSecurityTipCategoryByIdQueryHandler(ISecurityTipCategoryRepositorysync securityTipCategoryRepository)
            {
                _securityTipCategoryRepository = securityTipCategoryRepository;
            }
            public async Task<Response<SecurityTipCategory>> Handle(GetSecurityTipCategoryByIdQuery query, CancellationToken cancellationToken)
            {
                var securityTipCategory = await _securityTipCategoryRepository.GetByIdAsync(query.Id);
                if (securityTipCategory == null) throw new ApiException($"Security tip category not found.");
               
                // return new Response<Product>(product);

                return new Response<SecurityTipCategory>(securityTipCategory, $"Security tip category retrieval successful");

            }
        }
    }
}
