namespace Application.Features.AppTroopers.SecurityTips.Commands
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
}
