using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comments.Queries.GetComments
{
    public class CommentViewModel
    {
        public string UserComment { get; set; }
        public int SecurityTipId { get; set; }
        public string CommenterId { get; set; }
    }
}
