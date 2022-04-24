using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Enums
{
    public enum SecurityTipStatusEnum
    {
        PendingApproval,
        Approved,
        Rejected,
        Broadcasted,
        PendingLocationLevelEscalation
    }
}
