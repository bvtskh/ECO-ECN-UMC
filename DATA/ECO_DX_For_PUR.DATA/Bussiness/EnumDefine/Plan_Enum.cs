using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECO_DX_For_PUR.DATA.Bussiness.EnumDefine
{
    public class Plan_Enum
    {
        public enum WO_Select
        {
            All ,
            Pending,
        };
        public enum ButtonTag
        {
            customer,
            model,
            eco,
            Wo
        };
        public enum TextboxTag
        {
            Model,
            ECO_No,
            Customer,
            Wo
        };
    }
}
