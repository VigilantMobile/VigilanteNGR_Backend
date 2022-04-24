using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.AppTroopers.SecurityTip.Queries.GetAllSecurityTipCategories
{
    public class GetAllSecurityTipCategoriesViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}
