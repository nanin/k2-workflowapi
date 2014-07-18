using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider;
using SourceCode.Workflow.Client;
using DianPing.WorkFlow.Common.Models;
using System.Configuration;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using Newtonsoft.Json;
using DianPing.WorkFlow.Common.Enum;
using AutoMapper;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider.Entity;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Infrastructure.Interception;

namespace DianPing.WorkFlow.ServiceProvider
{
    public class K2ServiceProvider : IK2ServiceProvider
    {
        private static string REJECTACTION = "拒绝";
        private static string UNDOACTION = "结束流程";

        /// <summary>
        /// K2服务器的安全标签（SecurityLable），前置于帐号，用于区分用户验证方式，当前固定为SQLUM方式。
        /// </summary>
        private const string K2LABLE = "K2SQL:";


        [Cat]
        public ResultModel ReAssign(string sn, int assignFrom, int assignTo, out string activityName, out string processCode, out int procInstID)
        {
            Connection k2Connect = null;
            WorklistItem workList = null;
            ResultModel jr = new ResultModel() { Code = ResultCode.Fail };
            activityName = string.Empty;
            processCode = string.Empty;
            procInstID = 0;
            try
            {
                k2Connect = new Connection();
                k2Connect.Open(ConfigurationManager.AppSettings["K2Server"], ConfigurationManager.AppSettings["K2LoginString"]);
                k2Connect.ImpersonateUser(SecurityLable(assignFrom.ToString()));
                workList = k2Connect.OpenWorklistItem(sn);
                if (workList != null )
                {
                    workList.Redirect(SecurityLable(assignTo.ToString()));
                    jr.Code = ResultCode.Sucess;
                    jr.Msg = "";
                    activityName = workList.ActivityInstanceDestination.Name;
                    processCode = workList.ProcessInstance.DataFields["ProcessCode"].Value.ToString();
                    procInstID = workList.ProcessInstance.ID;

                }

            }
            catch (Exception ex)
            {
                jr.Msg = ex.Message;
            }
            finally
            {
                if (workList != null)
                {
                    if (workList.Status == WorklistStatus.Open)
                    {
                        try
                        {
                            k2Connect.RevertUser();
                            workList.Release();
                        }
                        catch { }
                    }
                }
                if (k2Connect != null)
                    k2Connect.Close();
            }
            return jr;
        }

        [Cat]
        public ResultModel StartProcess(string processName, int loginId, string objectId, string folio, Dictionary<string, string> dataFields,out int procInstId)
        {
            Connection k2Connect = null;
            WorklistItem workList = null;
            ResultModel jr = new ResultModel() { Code = ResultCode.Fail };
            procInstId = 0;
            try
            {
                k2Connect = new Connection();
                k2Connect.Open(ConfigurationManager.AppSettings["K2Server"], ConfigurationManager.AppSettings["K2LoginString"]);
                k2Connect.ImpersonateUser(SecurityLable(loginId.ToString()));
                //创建实例
                ProcessInstance ProcessInst = k2Connect.CreateProcessInstance(processName);
                if (!string.IsNullOrEmpty(folio))
                {
                    ProcessInst.Folio = folio;
                }
                #region //赋值datafields
                foreach (string key in dataFields.Keys)
                {
                    if (ProcessInst.DataFields[key] != null)
                    {
                        ProcessInst.DataFields[key].Value = dataFields[key];
                    }
                }
                #endregion

                k2Connect.StartProcessInstance(ProcessInst);

                procInstId = ProcessInst.ID;
                jr.Code = ResultCode.Sucess;
                jr.Msg = procInstId.ToString();
            }
            catch (Exception ex)
            {
                jr.Msg = ex.Message;
            }
            finally
            {
                if (workList != null)
                {
                    if (workList.Status == WorklistStatus.Open)
                    {
                        try
                        {
                            k2Connect.RevertUser();
                            workList.Release();
                        }
                        catch { }
                    }
                }
                if (k2Connect != null)
                    k2Connect.Close();
            }
            return jr;
            //var result = (new K2Service.K2Service()).StartProcess(processCode, loginId, objectId, folio, jsonData, ConfigurationManager.AppSettings["APIKEY"]);

        }

        [Cat]
        public ResultModel ApproveK2Process(string sn, int loginId, string actionString, string memo, Dictionary<string, string> dataFields, out string activityName, out string processCode, out int procInstID)
        {
            Connection k2Connect = null;
            WorklistItem workList = null;
            ResultModel jr = new ResultModel() { Code = ResultCode.Fail };
            activityName = string.Empty;
            processCode = string.Empty;
            procInstID = 0;
             try
            {
                k2Connect = new Connection();
                k2Connect.Open(ConfigurationManager.AppSettings["K2Server"], ConfigurationManager.AppSettings["K2LoginString"]);
                k2Connect.ImpersonateUser(SecurityLable(loginId.ToString()));
                
                workList = k2Connect.OpenWorklistItem(sn);
                if (workList != null)
                {
                    #region 更新Datafield
                    if (dataFields != null && dataFields.Count > 0)
                    {
                        ProcessInstance CurrentProcessInst = workList.ProcessInstance;
                        //更新Datafields
                        foreach (string key in dataFields.Keys)
                        {
                            if (CurrentProcessInst.DataFields[key] != null)
                            {
                                if (CurrentProcessInst.DataFields[key].Value.ToString() != dataFields[key])
                                {
                                    CurrentProcessInst.DataFields[key].Value = dataFields[key];
                                }
                            }
                        }
                    }
                    #endregion

                    #region 审批任务
                    //批量审批没有actionString，默认第一个操作
                    if (string.IsNullOrEmpty(actionString))
                    {
                        if (workList.Actions[0].Name == REJECTACTION)
                        {
                            workList.GotoActivity("流程未通过");
                        }
                        else if (workList.Actions[0].Name == UNDOACTION)
                        {
                            workList.GotoActivity("流程撤销");
                        }
                        else
                        {
                            workList.Actions[0].Execute();
                        }
                    }
                    else
                    {
                        //执行匹配的操作
                        if (actionString == UNDOACTION)
                        {
                            workList.GotoActivity("流程撤销");
                        }
                        else if (actionString == REJECTACTION)
                        {
                            workList.GotoActivity("流程未通过");
                        }
                        else
                        {
                            bool isExcute = false;
                            for (int i = 0; i < workList.Actions.Count; i++)
                            {
                                if (workList.Actions[i].Name == actionString)
                                {
                                    workList.Actions[i].Execute();
                                    isExcute = true;
                                    break;
                                }
                            }
                            if (!isExcute)
                            {
                                jr.Code = ResultCode.Fail;
                                jr.Msg = string.Format("Action:{0}错误", actionString);
                                return jr;
                            }
                        }
                    }
                    #endregion

                    jr.Code = ResultCode.Sucess;
                    jr.Msg = "";
                    activityName = workList.ActivityInstanceDestination.Name;
                    processCode = workList.ProcessInstance.DataFields["ProcessCode"].Value.ToString();
                    procInstID = workList.ProcessInstance.ID;
                }
            }
             catch (Exception ex)
             {
                 jr.Msg = ex.Message;
             }
             finally
             {
                 if (workList != null)
                 {
                     if (workList.Status == WorklistStatus.Open)
                     {
                         try
                         {
                             k2Connect.RevertUser();
                             workList.Release();
                         }
                         catch { }
                     }
                 }
                 if (k2Connect != null)
                     k2Connect.Close();
             }

             return jr;
        }

        [Cat]
        public ResultModel Involve(string sn, int assignFrom, int assignTo, out string activityName, out string processCode, out int procInstID)
        {
            Connection k2Connect = null;
            WorklistItem workList = null;
            ResultModel jr = new ResultModel() { Code = ResultCode.Fail };
            activityName = string.Empty;
            processCode = string.Empty;
            procInstID = 0;
            try
            {
                k2Connect = new Connection();
                k2Connect.Open(ConfigurationManager.AppSettings["K2Server"], ConfigurationManager.AppSettings["K2LoginString"]);
                k2Connect.RevertUser();
                workList = k2Connect.OpenWorklistItem(sn);
                if (workList != null)
                {
                    foreach (Destination ds in workList.DelegatedUsers)
                    {
                        if (ds.Name.Contains(assignFrom.ToString()))
                        {
                            workList.Redirect(SecurityLable(assignTo.ToString()));
                            jr.Code = ResultCode.Sucess;
                            jr.Msg = "";
                            activityName = workList.ActivityInstanceDestination.Name;
                            processCode = workList.ProcessInstance.DataFields["ProcessCode"].Value.ToString();
                            procInstID = workList.ProcessInstance.ID;
                            break;
                        }
                    }
                    if (procInstID == 0)
                    {
                        jr.Msg = string.Format("{0} have not rights to open {1}", assignFrom.ToString(), sn);
                    }
                }

            }
            catch (Exception ex)
            {
                jr.Msg = ex.Message;
            }
            finally
            {
                if (workList != null)
                {
                    if (workList.Status == WorklistStatus.Open)
                    {
                        try
                        {
                            k2Connect.RevertUser();
                            workList.Release();
                        }
                        catch { }
                    }
                }
                if (k2Connect != null)
                    k2Connect.Close();
            }
            return jr;
        }


        /// <summary>
        /// 添加SecurityLable，默认添加"k2sql:"
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private string SecurityLable(string val)
        {
            if (val.ToLower().IndexOf(K2LABLE) < 0)
            {
                val = string.Format("{0}{1}", K2LABLE, val);
            }
            return val;
        }

    }
}
