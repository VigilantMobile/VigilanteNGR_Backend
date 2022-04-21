//using Domain.Common;
//using Domain.Entities.Identity;
//using Domain.Entities.LocationEntities;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Entities.AppTroopers.Curfew
//{
//    public class TownCurfew : Curfew
//    {
//        [Required]
//        [ForeignKey("Town")]
//        public int TownId { get; set; }
//        public virtual Town Town { get; set; }

//        [Required]
//        [ForeignKey("VGNGAStaff")]
//        public string AdminAuthorizerID { get; set; }
//        public virtual VGNGAStaff VGNGAStaff { get; set; }
//    }
//}
