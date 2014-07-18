using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Server.Entity;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2Server
{
    public interface IWorklistHeaderRepositories
    {
        IList<WorklistHeader> GetListBySnList(List<string> snList);
    } 
}
