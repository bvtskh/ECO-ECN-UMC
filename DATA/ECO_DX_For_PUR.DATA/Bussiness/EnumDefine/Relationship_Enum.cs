using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECO_DX_For_PUR.DATA.Bussiness.EnumDefine
{
    public class Relationship_Enum
    {
        public enum WOTYPE
        {
            AI,
            SMTA,
            SMTB,
            SMT,
            FAT
        };
        public enum DataStatus
        {
            Success,
            Pending
        };
    }
}
