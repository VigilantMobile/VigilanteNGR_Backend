using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.LocationEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location
{
    public partial class CreateDistrictCommand : IRequest<Response<Town>>
    {
        public string Name { get; set; }
        public int LGAId { get; set; }
    }
    public class CreateDistrictCommandHandler : IRequestHandler<CreateDistrictCommand, Response<Town>>
    {
        private readonly ITownRepositoryAsync _townRepositoryAsync;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public CreateDistrictCommandHandler(ITownRepositoryAsync townRepositoryAsync, IMapper mapper, IUserAccessor userAccessor)
        {
            _townRepositoryAsync = townRepositoryAsync;
            _mapper = mapper;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

        public async Task<Response<Town>> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
        {
            string CreatedBy = _userAccessor.GetUserId();

            var isUniqueTownInLGA = await _townRepositoryAsync.IsUniqueTowninLGA(request.LGAId, request.Name);

            if(!isUniqueTownInLGA)
            {
                throw new ApiException($"District already exists.");
            }

            var town = _mapper.Map<Town>(request);
            town.CreatedBy = CreatedBy;
            town.Created = DateTime.UtcNow.AddHours(1);
            await _townRepositoryAsync.AddAsync(town, _userAccessor.GetUserId());
           
            return new Response<Town>(town, message: $"District successfully created.", successStatus: true);
        }
    }
}
