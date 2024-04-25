using ECO_DX_For_PUR.DATA.Bussiness.Interface;
using ECO_DX_For_PUR.DATA.Entities.PI_BASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECO_DX_For_PUR.DATA.Bussiness.SQLHelper
{
    public class PI_BASEHelper : IBASE
    {
        PI_BASE_DBContext _context = new PI_BASE_DBContext();
        public List<Customer> GetCustomer()
        {
            return _context.Customers.ToList();
        }
    }
}
