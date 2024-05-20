using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECO_DX_For_PUR.DATA.Connection
{
    public class CONNECTION
    {
        public const string COST_SYS = "data source=172.28.10.22;initial catalog=COST_SYSTEM;persist security info=True;user id=lca;password=umc@123;MultipleActiveResultSets=True;App=EntityFramework";
        public const string ECO = "data source=172.28.10.28;initial catalog=ECO_DXForPUR;user id=sa;password=umc@123;MultipleActiveResultSets=True;App=EntityFramework";
        public const string PI_BASE = "data source=172.28.10.22;initial catalog=PI_BASE;persist security info=True;user id=lca;password=umc@123;MultipleActiveResultSets=True;App=EntityFramework";
        public const string USAP = "data source=172.28.10.9;initial catalog=UMC3000;persist security info=True;user id=dev;password=umc@123;MultipleActiveResultSets=True;App=EntityFramework";
    }
}
