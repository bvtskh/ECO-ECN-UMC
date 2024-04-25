using ECO_DX_For_PUR.DATA.Entities;
using ECO_DX_For_PUR.DATA.Entities.ECN_ECO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ECO_DX_For_PUR.DATA.Bussiness.EnumDefine.Plan_Enum;

namespace ECO_DX_For_PUR.DATA.Bussiness.Interface
{
    public interface IWO
    {
        List<WoChanging> GetWOChanging();
        bool IsChangingWO(string woPending);
        bool InsertData(List<WO_Relationship> dataInsert);
        List<WO_Relationship> GetWORelationList();
        bool IsFinishWO(string woPending);
        List<WoChanging> GetPendingWO();
        List<Area> GetAreaList();
        DataTable GetAllWo(WO_Select wo);
        string GetFullpathPDFfile(string eco);
    }
}
