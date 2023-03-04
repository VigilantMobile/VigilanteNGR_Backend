using Application.Filters;

namespace Application.Features.AppTroopers.SecurityTips
{
    public class GetSecurityTipsListQueryParameter : RequestParameter
    {

    }

    public class GetLiveLocationSecurityTipsQueryParameter : RequestParameter
    {
        public string UserId { get; set; }
        public string Coordinates { get; set; }
        public string BroadcastLevel { get; set; }
    }
}
