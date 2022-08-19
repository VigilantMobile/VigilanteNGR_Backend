using Application.Features.Location;
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
        public string StateName { get; set; }
        public string LGAName { get; set; }
        public string DistrictName { get; set; }
        public string FormattedAddress { get; set; }
    }

    public class GetSecurityTipResponse
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string TipStatus { get; set; }
        public string BroadcasterName { get; set; }
        public string SecurityTipCategory { get; set; }
        public string AlertLevel { get; set; }
        public string BroadcastLevel { get; set; }
        public bool IsBroadcasted { get; set; }
        public int BroadcastLevelId { get; set; }
        public int BroadcastLocationId { get; set; }
        public string BroadcastLocation { get; set; }
        //Broadcaster
        public int BroadcasterLocationLevelId { get; set; }
        public int BroadcasterLocationId { get; set; }
        public string BroadcasterLocationLevel { get; set; }
        public string BroadcasterLocation { get; set; }

    }

    public class BroadcasterandTipLocations
    {
        public string BroadcastLocationLevel { get; set; }
        public string BroadcastLocation { get; set; }
        public string BroadcasterLocationLevel { get; set; }
        public string BroadcasterLocation { get; set; }
    }

    public class GetSecurityTipsListResponse
    {
       public List<GetSecurityTipResponse> SecurityTipsList { get; set; }
    }
}
