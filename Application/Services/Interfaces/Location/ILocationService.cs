using Application.DTOs.Location;
using Application.Features.AppTroopers.SecurityTips;
using Application.Features.UserProfile;
using Application.Wrappers;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTips;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.Location
{
    public interface ILocationService : IAutoDependencyService
    {
        Task<Response<string>> SaveCustomerLocationInfo(string Coordinates);
    }
}
