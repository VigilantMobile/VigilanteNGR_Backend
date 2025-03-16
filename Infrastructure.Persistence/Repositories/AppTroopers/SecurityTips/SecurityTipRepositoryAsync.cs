using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.AppTroopers.SecurityTips;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Features.AppTroopers.SecurityTips;
using System.Linq;
using Domain.Entities.LocationEntities;

namespace Infrastructure.Persistence.Repositories.SecurityTips
{
    public class SecurityTipRepositoryAsync : GenericRepositoryAsync<SecurityTip>, ISecurityTipRepositoryAsync
    {
        private readonly DbSet<SecurityTip> _securityTip;
        private readonly ApplicationDbContext _context;
        public SecurityTipRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _securityTip = dbContext.Set<SecurityTip>();
        }

        public async Task<GetLiveLocationSecurityTipResponse> GetSecurityTipDataForTown(string Townid, int pageNumber, int pageSize)
        {
            GetLiveLocationSecurityTipResponse SecurityTipsListResponse = new GetLiveLocationSecurityTipResponse();

            var SecurityTipsForLiveLocation = await (from securityTip in _context.SecurityTips
                                                     join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                                     join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                                     join categoryType in _context.SecurityTipCategoryTypes on category.CategoryTypeId equals categoryType.Id
                                                     join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                                     join town in _context.Towns on securityTip.TownId equals town.Id
                                                     join lga in _context.LGAs on town.LGAId equals lga.Id
                                                     join state in _context.States on lga.StateId equals state.Id
                                                     join country in _context.Countries on state.CountryId equals country.Id
                                                     where securityTip.TownId.ToString() == Townid
                                                     && securityTip.IsBroadcasted == true

                                                     select new GetSecurityTipResponse
                                                     {
                                                         Id = securityTip.Id.ToString(),
                                                         Subject = securityTip.Subject,
                                                         Description = securityTip.Body,
                                                         Coordinates = securityTip.Coordinates,
                                                         SecurityTipStatus = securityTip.Status.ToString(),
                                                         BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                         AlertLevel = alertLevel.Name,
                                                         SecurityTipCategory = new AlertCategory
                                                         {
                                                             Id = category.Id.ToString(),
                                                             Name = category.Name,
                                                             Description = category.Description
                                                         },
                                                         AlertCategoryType = new AlertCategoryType
                                                         {
                                                             Id = categoryType.Id.ToString(),
                                                             Name = categoryType.Name,
                                                             Description = categoryType.Description
                                                         },
                                                         AlertLocation = new AlertLocation
                                                         {
                                                             City = town.Name,
                                                             StateOrProvince = state.Name,
                                                             Country = country.Name,
                                                         },
                                                         IsBroadcasted = securityTip.IsBroadcasted,
                                                         Broadcaster = new Broadcaster
                                                         {
                                                             Id = broadcaster.Id,
                                                             FullName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                             ProfilePhotoUrl = broadcaster.CustomerProfileUrl
                                                         }
                                                     }).Skip((pageNumber - 1) * pageSize)
                                               .Take(pageSize)
                                               .AsNoTracking()
                                               .ToListAsync();

            SecurityTipsListResponse.SecurityTipsList = SecurityTipsForLiveLocation;
            return SecurityTipsListResponse;
        }



        public async Task<GetLiveLocationSecurityTipResponse> GetSecurityTipDataForLGA(string LGAId, int pageNumber, int pageSize)
        {
            GetLiveLocationSecurityTipResponse SecurityTipsListResponse = new GetLiveLocationSecurityTipResponse();

            var SecurityTipsForLiveLocation = await (from securityTip in _context.SecurityTips
                                                     join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                                     join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                                     join categoryType in _context.SecurityTipCategoryTypes on category.CategoryTypeId equals categoryType.Id
                                                     join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                                     join lga in _context.LGAs on securityTip.Coordinates equals LGAId
                                                     join town in _context.Towns on broadcaster.TownId equals town.Id
                                                     where securityTip.Coordinates == LGAId
                                                     && securityTip.IsBroadcasted == true

                                                     select new GetSecurityTipResponse
                                                     {
                                                         Id = securityTip.Id.ToString(),
                                                         Subject = securityTip.Subject,
                                                         Description = securityTip.Body,
                                                         Coordinates = securityTip.Coordinates,
                                                         SecurityTipStatus = securityTip.Status.ToString(),
                                                         BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                         AlertLevel = alertLevel.Name,
                                                         SecurityTipCategory = new AlertCategory
                                                         {
                                                             Id = category.Id.ToString(),
                                                             Name = category.Name,
                                                             Description = category.Description
                                                         },
                                                         AlertCategoryType = new AlertCategoryType
                                                         {
                                                             Id = categoryType.Id.ToString(),
                                                             Name = categoryType.Name,
                                                             Description = categoryType.Description
                                                         },
                                                         AlertLocation = new AlertLocation
                                                         {
                                                             City = town.Name,
                                                             StateOrProvince = lga.Name,
                                                         },
                                                         IsBroadcasted = securityTip.IsBroadcasted,
                                                         Broadcaster = new Broadcaster
                                                         {
                                                             Id = broadcaster.Id,
                                                             FullName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                             ProfilePhotoUrl = broadcaster.CustomerProfileUrl
                                                         }
                                                     })
                                                   .Skip((pageNumber - 1) * pageSize)
                                                   .Take(pageSize)
                                                   .AsNoTracking()
                                                   .ToListAsync();

            SecurityTipsListResponse.SecurityTipsList = SecurityTipsForLiveLocation;
            return SecurityTipsListResponse;
        }


        public async Task<GetLiveLocationSecurityTipResponse> GetSecurityTipDataForState(string StateId, int pageNumber, int pageSize)
        {
            GetLiveLocationSecurityTipResponse SecurityTipsListResponse = new GetLiveLocationSecurityTipResponse();

            var SecurityTipsForLiveLocation = await (from securityTip in _context.SecurityTips
                                                     join broadcaster in _context.Users on securityTip.BroadcasterId equals broadcaster.Id
                                                     join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                                     join categoryType in _context.SecurityTipCategoryTypes on category.CategoryTypeId equals categoryType.Id
                                                     join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                                     join state in _context.States on securityTip.Coordinates equals StateId
                                                     join town in _context.Towns on broadcaster.TownId equals town.Id
                                                     join lga in _context.LGAs on town.LGAId equals lga.Id
                                                     where securityTip.Coordinates == StateId
                                                     && securityTip.IsBroadcasted == true

                                                     select new GetSecurityTipResponse
                                                     {
                                                         Id = securityTip.Id.ToString(),
                                                         Subject = securityTip.Subject,
                                                         Description = securityTip.Body,
                                                         Coordinates = securityTip.Coordinates,
                                                         SecurityTipStatus = securityTip.Status.ToString(),
                                                         BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                         AlertLevel = alertLevel.Name,
                                                         SecurityTipCategory = new AlertCategory
                                                         {
                                                             Id = category.Id.ToString(),
                                                             Name = category.Name,
                                                             Description = category.Description
                                                         },
                                                         AlertCategoryType = new AlertCategoryType
                                                         {
                                                             Id = categoryType.Id.ToString(),
                                                             Name = categoryType.Name,
                                                             Description = categoryType.Description
                                                         },
                                                         AlertLocation = new AlertLocation
                                                         {
                                                             City = town.Name,
                                                             StateOrProvince = state.Name
                                                         },
                                                         IsBroadcasted = securityTip.IsBroadcasted,
                                                         Broadcaster = new Broadcaster
                                                         {
                                                             Id = broadcaster.Id,
                                                             FullName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                             ProfilePhotoUrl = broadcaster.CustomerProfileUrl
                                                         }
                                                     })
                                                   .Skip((pageNumber - 1) * pageSize)
                                                   .Take(pageSize)
                                                   .AsNoTracking()
                                                   .ToListAsync();

            SecurityTipsListResponse.SecurityTipsList = SecurityTipsForLiveLocation;
            return SecurityTipsListResponse;
        }
    }
}