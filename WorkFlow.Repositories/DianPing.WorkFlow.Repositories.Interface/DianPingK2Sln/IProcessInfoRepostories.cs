using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln
{
    public interface IProcessInfoRepostories
    {
        IList<ProcessInfo> GetByProcessCode(IList<string> processCode);

        /// <summary>
        /// 保存流程实例
        /// </summary>
        /// <param name="procInst">
        /// 流程实例持久化对象
        /// <see cref="DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity.ProcInstBasicInfo"/>
        /// </param>
        void SaveProcInst(ProcInstBasicInfo procInst);

                /// <summary>
        /// 获取流程实例基本信息
        /// </summary>
        /// <param name="procInstId"></param>
        /// <returns></returns>
        ProcInstBasicInfo GetProcInstBasicInfo(int procInstId);

        ProcInst GetProcInstById(int Id);
        ProcInst GetProcInstByFolio(string Folio);

        List<ActInst> GetProcessStatusByProcInstId(int procInstId);

        List<ActInst> GetProcessStatusByFolio(string folio);
    }
}
