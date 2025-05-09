﻿using Application.Features.Location;
using Domain.Entities.AppTroopers.SecurityTips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips
{
    public class CreateSecurityTipResponse
    {
        public bool Succeeded { get; set; }
        public string SecurityTipStatus { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsCreated { get; set; }
        public string Message { get; set; }
    }

    public class CreateSecurityTipEligibilityResponse
    {
        public bool CanPostTip { get; set; }
        public bool CanBroadcastImmediately { get; set; }
        public bool EscalationRequested { get; set; }
        public string FailureReason { get; set; }
    }

    public class CreateSecurityTipRequest
    {
        public string Subject { get; set; }
        public string BroadcasterId { get; set; }
        public int CategoryId { get; set; }
        public int Casualties { get; set; }
        public int AlertLevelId { get; set; }
        public int BroadcastLevelId { get; set; } // 1,2,3 for state, lga and 
        public int LocationId { get; set; }
        public int SourceId { get; set; }
        public string Body { get; set; }
        public string TipStatus { get; set; }
        public string coordinates { get; set; } 
    }

    public class CustomerPreciseLocation
    {
        public string Country { get; set; }
        public string StateOrProvinceOrRegion { get; set; }
        public string CountryOrDistrictOrLGA { get; set; }
        public string TownOrDistrict { get; set; }
        public string FormattedAddress { get; set; }
    }

    public class GetSecurityTipResponse
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Coordinates { get; set; }
        public string SecurityTipStatus { get; set; }
        public DateTime Created { get; set; }
        public string UpvoteCount { get; set; }
        public string DownvoteCount { get; set; }
        public string ViewCount { get; set; }
        public string BroadcasterName { get; set; }
        public AlertCategoryType AlertCategoryType { get; set; }
        public AlertCategory SecurityTipCategory { get; set; }
        public AlertLocation AlertLocation { get; set; }
        public string AlertLevel { get; set; }
        public bool IsBroadcasted { get; set; }
        //Broadcaster
        public Broadcaster Broadcaster { get; set; }
        public List<CommentViewModel> Comments { get; set; }

    }

    public class CommentViewModel
    {
        public string Id { get; set; }
        public string Comment { get; set; }
        public string CommenterId { get; set; }
        public string CommenterName { get; set; }
        public string CommenterProfileUrl { get; set; }
        public DateTime Created { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }
    }

    public class AlertLocation
    {
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string Country { get; set; }
    }

    public class AlertCategoryType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AlertCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Broadcaster
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string ProfilePhotoUrl { get; set; }
    }

    public class BroadcasterandTipLocations
    {
        public string BroadcastLocationLevel { get; set; }
        public string BroadcastLocation { get; set; }
        public string BroadcasterFullLocation { get; set; }
    }

    public class GetSecurityTipsListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<GetSecurityTipResponse> SecurityTipsList { get; set; }
    }

    //Live Location

    public class GetLiveLocationSecurityTipResponse
    {
        public bool Success { get; set; }
        public string  Message { get; set; }
        public List<GetSecurityTipResponse> SecurityTipsList { get; set; }
    }

    public class GetSecurityTipsForUserTownLGAandStateResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<GetSecurityTipResponse> SecurityTipsListforUserTown { get; set; }
        public List<GetSecurityTipResponse> SecurityTipsListforUserLGA { get; set; }
        public List<GetSecurityTipResponse> SecurityTipsListforUserState { get; set; }
    }

    public class LiveLocationSecurityTipResponse
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string TipStatus { get; set; }
        public string BroadcasterName { get; set; }
        public string SecurityTipCategory { get; set; }
        public string AlertLevel { get; set; }
        public string BroadcastLevel { get; set; }
        public bool IsBroadcasted { get; set; }
        public string BroadcastLevelId { get; set; }
        public string BroadcastLocationId { get; set; }
        public string BroadcastLocation { get; set; }
        public string BroadcasterTownId { get; set; }
    }
}
