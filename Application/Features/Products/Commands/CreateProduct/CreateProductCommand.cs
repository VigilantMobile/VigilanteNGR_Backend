using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.CreateProduct
{
    public partial class CreateProductCommand : IRequest<Response<Product>>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<Product>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public CreateProductCommandHandler(IProductRepositoryAsync productRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

        public async Task<Response<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(product, _userAccessor.GetUserId());
           
           // return new Response<int>(product.Id);
            return new Response<Product>(product, $"Product successfully created");

        }
    }
}
