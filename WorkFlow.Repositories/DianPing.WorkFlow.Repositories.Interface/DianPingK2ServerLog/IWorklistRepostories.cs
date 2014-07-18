using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;
using DianPing.WorkFlow.Common.Models;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog
{
    public interface IWorklistRepostories
    {
        //IList<Worklist> GetWorkList(List<int> procInstIds, string folio, List<int> loginIds, DateTime? startTime, DateTime? endTime);
        QueryListResultBase<Worklist> GetWorkList(QueryCriteriaBase<QueryWorkList> queryPara);
    }

}
