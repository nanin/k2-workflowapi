using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;
using System.Data.Entity;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity;

namespace DianPing.WorkFlow.Repositories.Implementation.DianPingK2Sln
{
    public class ProcessInfoRepostories : IProcessInfoRepostories
    {
        public IList<ProcessInfo> GetByProcessCode(IList<string> processCode)
        {
            var edm = new DianPingK2SlnContext();
            return edm.ProcessInfo.
                Where(_ => processCode.Contains(_.ProcessCode)).ToList();
        }

        /// <summary>
        /// 保存流程实例
        /// </summary>
        /// <param name="procInst">
        /// 流程实例持久化对象
        /// <see cref="DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity.ProcInstBasicInfo"/>
        /// </param>
        public void SaveProcInst(ProcInstBasicInfo procInst)
        {
            var edm = new DianPingK2SlnContext();
            edm.ProcInstBasicInfo.Add(procInst);
        }

        /// <summary>
        /// 获取流程实例基本信息
        /// </summary>
        /// <param name="procInstId"></param>
        /// <returns></returns>
        public ProcInstBasicInfo GetProcInstBasicInfo(int procInstId)
        {
            var edm = new DianPingK2SlnContext();
            return edm.ProcInstBasicInfo.Where(p => p.ProcInstID == procInstId).FirstOrDefault<ProcInstBasicInfo>();
        }

        public ProcInst GetProcInstById(int Id)
        {
            var edm = new DianPingK2ServerLogContext();
            return edm.ProcInst.Where(_ => _.ID == Id).FirstOrDefault();
        }


        public ProcInst GetProcInstByFolio(string Folio)
        {
            var edm = new DianPingK2ServerLogContext();
            return edm.ProcInst.Where(_ => _.Folio == Folio).FirstOrDefault<ProcInst>();        
        }
        
        public List<ActInst> GetProcessStatusByProcInstId(int procInstId)
        {
            List<ActInst> result = new List<ActInst>();
            //var transactionOptions = new System.Transactions.TransactionOptions();
            //transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            //using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
            //{
            var edm = new DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity.DianPingK2ServerLogContext();
            result = edm.ActInst.Include("ProcInst").Include("Act")
                .Where(_ => _.Status == 2)
                .Where(_ => _.ProcInst.ID == procInstId)
                .Where(_ => _.Act.Type == 1)
                .ToList();
            //    transactionScope.Complete();
            //}
            //----------------------
                //var edm = new DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity.DianPingK2ServerLogContext();
                //var result =
                //     (from _ in edm.ProcInst
                //      from ai in _.ActInst.Where(a => a.Status == 2 && a.Act.Type == 1).DefaultIfEmpty()
                //      from ac in edm.Act
                //      //join ac in edm.Act on ai.Act.ID equals ac.ID into acgroup
                //      where _.ID == procInstId
                      
                //      select new
                //      {
                //          ProcInstId = _.ID,
                //          Folio = _.Folio,
                //          ActInst = ai,
                //          WorkList = _.Worklist,
                //          Status = _.Status
                //      }
                //          ).ToList();

                //List<K2Status> list = new List<K2Status>();
                //foreach (var item in result)
                //{
                //    K2Status status = new K2Status()
                //    {
                //        Folio = item.Folio,
                //        ProcInstId = item.ProcInstId,
                //        LoginIds = item.WorkList.Where(_ => _.Status == 0).Select(t => Convert.ToInt32(t.Destination.Replace("K2SQL:", ""))).ToList(),
                //        Activity = item.Status.ToString()
                //    };
                //    if (item.ActInst != null)
                //    {
                //        status.Activity = item.ActInst.Act.Name;
                //        status.StartDate = item.ActInst.StartDate;
                //    }
                //    list.Add(status);
                //}
            //----------------------
           //    foreach (var item in result.GroupBy(_ => _.ActName))
           //    {
           //           list.Add(new K2Status
           //           { 
           //                Activity = item.Key,
           //                ProcInstId = item.Select(_ => _.Folio).FirstOrDefault(),
           //                  Folio = item.Select(_=>_.Folio).FirstOrDefault(),
           //                   StartDate = item.Select(_=>_.StartDate).FirstOrDefault(),
           //LoginIds = item.Select
                              
           //            ID = item.Key,
           //            ActInst = item.Select(_=> new ActInst{ ID = _.Act}).ToList(),
   
           //           });
           //       }

                //var aa =  edm.ProcInst.Include(_ => _.ActInst).Include(_ => _.ActInst.Select(y => y.Act))
                //.Where(_ => _.ID == procInstId)
                //.Where(_ => _.ActInst.Any(t => t.Act.Type == 1))
                
                //.ToList();
            //edm.Database.SqlQuery(K2Status," SELECT  ","")
            return result;
        }

        public List<ActInst> GetProcessStatusByFolio(string folio)
        {
            List<ActInst> result = new List<ActInst>();
            //var transactionOptions = new System.Transactions.TransactionOptions();
            //transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;
            //using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
            //{
                var edm = new DianPingK2ServerLogContext();
                result = edm.ActInst.Include("ProcInst").Include("Act")
                    .Where(_ => _.Status == 2)
                    .Where(_ => _.ProcInst.Folio == folio)
                    .Where(_ => _.Act.Type == 1)
                    .ToList();
            //    transactionScope.Complete();
            //}
            return result;
        }      
    }
}
