using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;

namespace DianPing.WorkFlow.Repositories.Implementation.DianPingK2ServerLog
{
    public class PorcessInstRepostories : IPorcessInstRepostories
    {
        public ProcInst GetProcInst(int loginId)
        {
            ProcInst procInst = new ProcInst();
            using (var edm = new DianPingK2ServerLogContext())
            {
                procInst = edm.ProcInst.AsQueryable()
                    .Where(_ => _.ID == loginId).FirstOrDefault();
            }
            return procInst;
        }
    }
}
