using Application.Filters;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.AppTroopers.SecurityTips
{
    public class GetSecurityTipsListQueryParameter : PaginationRequestParameter
    {

    }

    public class GetLiveLocationSecurityTipsQueryParameter : PaginationRequestParameter
    {
        [Required]
        public string BroadcastLevel { get; set; }

        [Required]
        public string Coordinates { get; set; }
    }
}
