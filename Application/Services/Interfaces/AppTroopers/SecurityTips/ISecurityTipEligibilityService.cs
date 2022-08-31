using Application.Features.AppTroopers.SecurityTips;
using Application.Features.Location;
using Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.AppTroopers.SecurityTips
{
    public interface ISecurityTipEligibilityService : IAutoDependencyService
    {
        Task<CreateSecurityTipEligibilityResponse> GetSecurityTipPostEligibility(string CustomerId, int PostLocationId, int PostLocationLevel, int alertLevelId, string currentLocationCoordinates);
    }
}
