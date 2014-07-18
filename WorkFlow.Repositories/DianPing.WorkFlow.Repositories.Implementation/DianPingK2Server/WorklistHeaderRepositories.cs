using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Server;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Server.Entity;
using System.Data.Objects.SqlClient;

namespace DianPing.WorkFlow.Repositories.Implementation.DianPingK2Server
{
    public class WorklistHeaderRepositories : IWorklistHeaderRepositories
    {
        public IList<WorklistHeader> GetListBySnList(List<string> snList)
        {
            List<int> procIntIdList = snList.Select(_=>Convert.ToInt32( _.Split('_')[0])).ToList();
            List<int> actInstDestIdList = snList.Select(_ => Convert.ToInt32(_.Split('_')[1])).ToList();
            IList<WorklistHeader> result = new List<WorklistHeader>();
            using (var edm = new DianPingK2ServerContext())
            {
                var sql = edm.WorklistHeader.AsQueryable()
                    .Where(_ => procIntIdList.Contains(_.ProcInstID))
                    .Where(_ => actInstDestIdList.Contains(_.ActInstDestId));
                result = sql.ToList();
                //var aa = edm.WorklistHeader.Where(_ => snList.Contains(SqlFunctions.StringConvert((decimal?)(_.ProcInstID)) + "_" + SqlFunctions.StringConvert((decimal?)(_.ActInstDestId)))).AsQueryable();
            }
            return result;
        }
    }
}
