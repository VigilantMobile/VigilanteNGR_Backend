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
                                                     join broadcastLevel in _context.BroadcastLevels on securityTip.BroadcastLevelId equals broadcastLevel.Id
                                                     join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                                     join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                                     join town in _context.Towns on securityTip.LocationId equals Townid
                                                     where securityTip.LocationId == Townid
                                                     && securityTip.IsBroadcasted == true

                                                     select new GetSecurityTipResponse
                                                     {
                                                        Id = securityTip.Id.ToString(),
                                                        Subject = securityTip.Subject,
                                                        Body = securityTip.Body,
                                                        BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                        TipStatus = securityTip.TipStatusString,
                                                        AlertLevel = alertLevel.Name,
                                                        SecurityTipCategory = category.CategoryName,
                                                        BroadcastLevelId = securityTip.BroadcastLevelId.ToString(),
                                                        BroadcastLocationId = securityTip.LocationId,
                                                        BroadcasterTownId = broadcaster.TownId.ToString(),
                                                        BroadcastLevel = broadcastLevel.Name,
                                                        BroadcastLocation = town.Name
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
                                                     join broadcastLevel in _context.BroadcastLevels on securityTip.BroadcastLevelId equals broadcastLevel.Id
                                                     join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                                     join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                                     join lga in _context.LGAs on securityTip.LocationId equals LGAId

                                                     where securityTip.LocationId == LGAId
                                                     && securityTip.IsBroadcasted == true

                                                     select new GetSecurityTipResponse
                                                     {
                                                         Id = securityTip.Id.ToString(),
                                                         Subject = securityTip.Subject,
                                                         Body = securityTip.Body,
                                                         BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                         TipStatus = securityTip.TipStatusString,
                                                         AlertLevel = alertLevel.Name,
                                                         SecurityTipCategory = category.CategoryName,
                                                         BroadcastLevelId = securityTip.BroadcastLevelId.ToString(),
                                                         BroadcastLocationId = securityTip.LocationId,
                                                         BroadcasterTownId = broadcaster.TownId.ToString(),
                                                         BroadcastLevel = broadcastLevel.Name,
                                                         BroadcastLocation = lga.Name
                                                     }).Skip((pageNumber - 1) * pageSize)
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
                                                     join broadcastLevel in _context.BroadcastLevels on securityTip.BroadcastLevelId equals broadcastLevel.Id
                                                     join category in _context.SecurityTipCategories on securityTip.SecurityTipCategoryId equals category.Id
                                                     join alertLevel in _context.AlertLevels on securityTip.AlertLevelId equals alertLevel.Id
                                                     join state in _context.States on securityTip.LocationId equals StateId

                                                     where securityTip.LocationId == StateId
                                                     && securityTip.IsBroadcasted == true

                                                     select new GetSecurityTipResponse
                                                     {
                                                         Id = securityTip.Id.ToString(),
                                                         Subject = securityTip.Subject,
                                                         Body = securityTip.Body,
                                                         BroadcasterName = $"{broadcaster.FirstName} {broadcaster.LastName}",
                                                         TipStatus = securityTip.TipStatusString,
                                                         AlertLevel = alertLevel.Name,
                                                         SecurityTipCategory = category.CategoryName,
                                                         BroadcastLevelId = securityTip.BroadcastLevelId.ToString(),
                                                         BroadcastLocationId = securityTip.LocationId,
                                                         BroadcasterTownId = broadcaster.TownId.ToString(),
                                                         BroadcastLevel = broadcastLevel.Name,
                                                         BroadcastLocation = state.Name
                                                     }).Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .AsNoTracking()
                                          .ToListAsync();

            SecurityTipsListResponse.SecurityTipsList = SecurityTipsForLiveLocation;
            return SecurityTipsListResponse;
        }
    }
}
