using Application.Filters;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.AppTroopers.SecurityTips
{
    public class GetSecurityTipsListQueryParameter : RequestParameter
    {

    }

    public class GetLiveLocationSecurityTipsQueryParameter : RequestParameter
    {
        [Required]
        public string BroadcastLevel { get; set; }

        [Required]
        public string Coordinates { get; set; }
    }
}
