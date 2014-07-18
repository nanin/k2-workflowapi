using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;

namespace DianPing.WorkFlow.Repositories.Implementation.DianPingK2ServerLog
{
    public class ActInstDestRepostories : IActInstDestRepostories
    {
        public List<ActInstDest> queryByPorcInstIdAndActInstId(int procInstId, int actInstId)
        {
            List<ActInstDest> list = new List<ActInstDest>();
            using (var edm = new DianPingK2ServerLogContext())
            {
                list = edm.ActInstDest.AsQueryable()
                    .Where(_ => _.ProcInstID == procInstId)
                    .Where(_ => _.ActInstID == actInstId)
                    .Where(_ => _.Status == 0).ToList(); ;
            }
            return list;
        }
    }
}
