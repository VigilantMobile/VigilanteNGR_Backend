using Application.Features.AppTroopers.SecurityTips.Commands;
using Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTip;
using Application.Features.Location;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.SecurityTips;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces.AppTroopers.SecurityTips
{
    public interface ISecurityTipService : IAutoDependencyService
    {
        Task<CreateSecurityTipResponse> CreateSecurityTipAsync(CreateSecurityTipCommand securityTipRequest);
    }
}
