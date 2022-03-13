using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Enums
{
    public enum BroadcasterTypeEnum
    {
        OfficialSettlementVigilante,
        OfficialTownVigilante,
        OfficialLGAVigilante,
        OfficialStateVigilante,
        OfficialFederalVigilante,

        NPFSettlement,
        NPFTown,
        NPFLGA,
        NPFState,
        NPFFederal,

        VGNGAVerifiedUser,
        VGNGAUser,

        OfficialVGNGA
    }
}
