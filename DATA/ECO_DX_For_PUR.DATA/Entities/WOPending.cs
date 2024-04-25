using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECO_DX_For_PUR.DATA.Entities
{
    public class WOPending
    {
        public string ORDER_NO { get; set; }
        public string ECO_NO { get; set; }
        public int DEPT_ORD { get; set; }
        public string DEPT_NAME { get; set; }
        public string TYPE_NAME { get; set; }
        public DateTime DUE_DATE { get; set; }
        public int TYPE_ID { get; set; }
        public DateTime UPD_DATE { get; set; }
        public string MODEL { get; set; }
        public string CUSTOMER { get; set; }
    }
}
