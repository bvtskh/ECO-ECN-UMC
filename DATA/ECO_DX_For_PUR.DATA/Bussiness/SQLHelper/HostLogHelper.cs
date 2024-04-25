using ECO_DX_For_PUR.DATA.Entities.ECN_ECO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECO_DX_For_PUR.DATA.Bussiness.SQLHelper
{
    public class HostLogHelper
    {
        public Guid CreateLog()
        {
            Guid guid = Guid.NewGuid();
            using(var context = new DBContext())
            {
                try
                {
                    HostLog hostLog = new HostLog();
                    hostLog.SESSION_KEY = guid;
                    hostLog.HOST_NAME = System.Environment.MachineName;
                    hostLog.LOG_TIME = DateTime.Now;
                    context.HostLogs.Add(hostLog);
                    context.SaveChanges();
                    return guid;
                }
                catch (Exception)
                {
                    throw;
                }               
            }
        }

        public void RemoveSession(Guid sessionKey)
        {
            using(var context = new DBContext())
            {
                try
                {
                    var log = context.HostLogs.Where(w => w.SESSION_KEY == sessionKey).FirstOrDefault();
                    if (log != null)
                    {
                        context.HostLogs.Remove(log);
                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    throw;
                }               
            }
        }
    }
}
