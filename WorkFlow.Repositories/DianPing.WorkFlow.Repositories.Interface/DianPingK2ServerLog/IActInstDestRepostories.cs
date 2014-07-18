using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog
{
    public interface IActInstDestRepostories
    {
        List<ActInstDest> queryByPorcInstIdAndActInstId(int procInstId, int actInstId);
    }
}
