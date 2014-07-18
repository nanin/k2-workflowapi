using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog
{
    public interface IPorcessInstRepostories
    {
        ProcInst GetProcInst(int loginId);
    }
}
