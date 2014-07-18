using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;
using DianPing.WorkFlow.Common.Models;

namespace DianPing.WorkFlow.Repositories.Implementation.DianPingK2ServerLog
{
    public class WorklistRepostories : IWorklistRepostories
    {
        //public IList<Models.Worklist> GetWorkList(string destination, DateTime startDate, DateTime endDate)
        //{
        //    using (var edm = new Models.DianPingK2ServerLogContext())
        //    {
        //        //return edm.Worklist.Select(_ =>
        //        //    new Models.Worklist
        //        //    {
        //        //        DestType = _.DestType,
        //        //        ProcInst = new Models.ProcInst
        //        //        {
        //        //            Folio = _.ProcInst.Folio
        //        //        }
        //        //    }).Where(_ => _.Destination == destination && _.ProcInst.StartDate > startDate).ToList();
        //        return edm.Worklist.Include("ProcInst")
        //            .Where(_ => _.Destination == destination && _.ProcInst.StartDate > startDate).ToList();
        //    }

        //}

        public QueryListResultBase<Worklist> GetWorkList(QueryCriteriaBase<QueryWorkList> queryPara)
        {
            var result = new QueryListResultBase<Worklist>();

            var transactionOptions = new System.Transactions.TransactionOptions();
            transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted;

            var edm = new DianPingK2ServerLogContext();
            //edm.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
            
            var query = edm.Worklist.Include("ProcInst").AsQueryable().Where(_ => _.Status == 0);

            if (queryPara != null)
            {
                var criteria = queryPara.QueryCriteria;
                if (criteria != null)
                {
                    //if (criteria.ProcessFullName != null && criteria.ProcessFullName.Count > 0)
                    //{
                    //    query = query.Where(_ => criteria.ProcessFullName.Contains(_.ProcInst.proc.ProcSet.FullName));
                    //}
                    if (criteria.ProcessCodes != null && criteria.ProcessCodes.Count > 0)
                    {
                        query = query.Where(_ => criteria.ProcessCodes.Contains(_.ProcInst.proc.ProcSet.Descr));
                    }
                    if (criteria.LoginIds != null && criteria.LoginIds.Count > 0)
                    {
                        IList<string> loginIdsStr = criteria.LoginIds.Select(_ => string.Format("K2SQL:{0}", _)).ToList();
                        query = query.Where(_ => loginIdsStr.Contains(_.Destination));
                    }
                    if (criteria.OriginatorLoginIds != null && criteria.OriginatorLoginIds.Count > 0)
                    {
                        IList<string> loginIdsStr = criteria.OriginatorLoginIds.Select(_ => string.Format("K2SQL:{0}", _)).ToList();
                        query = query.Where(_ => loginIdsStr.Contains(_.ProcInst.Originator));
                    }
                    if (criteria.ProcInstIds != null && criteria.ProcInstIds.Count > 0)
                    {
                        query = query.Where(_ => criteria.ProcInstIds.Contains(_.ProcInst.ID));
                    }
                    if (!string.IsNullOrEmpty(criteria.Folio))
                    {
                        query = query.Where(_ => _.ProcInst.Folio.StartsWith(criteria.Folio));
                    }
                    if (criteria.TaskStartDate != null)
                    {
                        if (criteria.TaskStartDate.DateFrom.HasValue)
                        {
                            query = query.Where(_ => _.StartDate >= criteria.TaskStartDate.DateFrom.Value);
                        }
                        if (criteria.TaskStartDate.DateTo.HasValue)
                        {
                            query = query.Where(_ => _.StartDate < criteria.TaskStartDate.DateTo.Value);
                        }
                    }
                    if (criteria.ProcessStartDate != null)
                    {
                        if (criteria.ProcessStartDate.DateFrom.HasValue)
                        {
                            query = query.Where(_ => _.ProcInst.StartDate >= criteria.ProcessStartDate.DateFrom.Value);
                        }
                        if (criteria.ProcessStartDate.DateTo.HasValue)
                        {
                            query = query.Where(_ => _.ProcInst.StartDate < criteria.ProcessStartDate.DateTo.Value);
                        }
                    }
                    if (queryPara.PagingInfo != null)
                    {
                        using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
                        {
                            queryPara.PagingInfo.ItemCount = query.Count();
                            transactionScope.Complete();
                        }
                        if (queryPara.PagingInfo.SortField != null)
                        {
                            switch (queryPara.PagingInfo.SortField.ToLower())
                            {
                                case "folio":
                                    if (queryPara.PagingInfo.SortOrder == Common.Enum.SortOrder.Descending)
                                    {
                                        query = query.OrderByDescending(_ => _.ProcInst.Folio);
                                    }
                                    else
                                    {
                                        query = query.OrderBy(_ => _.ProcInst.Folio);
                                    }
                                    break;
                                case "worklisttime":
                                    if (queryPara.PagingInfo.SortOrder == Common.Enum.SortOrder.Descending)
                                    {
                                        query = query.OrderByDescending(_ => _.StartDate);
                                    }
                                    else
                                    {
                                        query = query.OrderBy(_ => _.StartDate);
                                    }
                                    break;
                                case "procstarttime":
                                    if (queryPara.PagingInfo.SortOrder == Common.Enum.SortOrder.Descending)
                                    {
                                        query = query.OrderByDescending(_ => _.ProcInst.StartDate);
                                    }
                                    else
                                    {
                                        query = query.OrderBy(_ => _.ProcInst.StartDate);
                                    }
                                    break;
                                default:
                                    query = query.OrderByDescending(_ => _.StartDate);
                                    break;
                            }
                        }
                        else
                        {
                            query = query.OrderByDescending(_ => _.StartDate);
                        }
                        query = query
                            .Skip(queryPara.PagingInfo.PageIndex == 0 ? 0 : (queryPara.PagingInfo.PageIndex - 1) * queryPara.PagingInfo.PageSize)
                            .Take(queryPara.PagingInfo.PageSize);
                    }
                }
                result.PagingInfo = queryPara.PagingInfo;
            }
            //result.PagingInfo = null;

            using (var transactionScope = new System.Transactions.TransactionScope(System.Transactions.TransactionScopeOption.Required, transactionOptions))
            {
                result.ResultList = query.ToList();
                transactionScope.Complete();
            }
            return result;
        }
    }
}
