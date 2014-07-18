using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCode.Workflow.Client;
using DianPing.BPM.Common.AppSettings;

namespace DianPing.WorkFlow.Infrastructure.K2
{
    /// <summary>
    /// 调用k2接口帮助类
    /// </summary>
    public class K2Helper
    {
        private static string REJECTACTION = "拒绝";
        private static string UNDOACTION = "结束流程";

        /// <summary>
        /// 调用k2的dll，生成流程
        /// <param name="dataFields">开启流程所需数据</param>
        /// <param name="processName">流程名称</param>
        /// </summary>
        public static int StartProcess(string processName, Dictionary<string, string> dataFields, string objectId, string folio, string userName)
        {
            int processInstId = 0;
            Connection k2Connection = new Connection();

            try
            {
                k2Connection.Open(ConfigurationBase.Web.K2Server, ConfigurationBase.Web.K2LoginString);
                k2Connection.ImpersonateUser(userName);

                //创建实例
                ProcessInstance processInst = k2Connection.CreateProcessInstance(processName);
                processInstId = processInst.ID;

                if (!string.IsNullOrEmpty(folio))
                {
                    processInst.Folio = folio;
                }
                #region 赋值datafields
                foreach (string key in dataFields.Keys)
                {
                    if (processInst.DataFields[key] != null)
                    {
                        processInst.DataFields[key].Value = dataFields[key];
                    }
                }
                #endregion
            }
            finally
            {
                if (k2Connection != null)
                {
                    k2Connection.Close();
                }
            }

            return processInstId;
        }

        /// <summary>
        /// 审批流程
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="sn"></param>
        /// <param name="actionString"></param>
        /// <param name="memo"></param>
        /// <param name="dataFields"></param>
        public WorklistItem ApprovalProcess(string userName, string sn, string actionString, string memo, Dictionary<string, string> dataFields)
        {
            Connection k2Connection = new Connection();

            try
            {
                k2Connection.Open(ConfigurationBase.Web.K2Server, ConfigurationBase.Web.K2LoginString);
                k2Connection.ImpersonateUser(userName);

                //根据SN打开工作项
                WorklistItem workList = k2Connection.OpenWorklistItem(sn);

                if (workList != null)
                {
                    #region 更新Datafield
                    if (dataFields != null && dataFields.Count > 0)
                    {
                        ProcessInstance currentProcessInst = k2Connection.OpenProcessInstance(workList.ProcessInstance.ID);
                        //更新Datafields
                        foreach (string key in dataFields.Keys)
                        {
                            if (currentProcessInst.DataFields[key] != null)
                            {
                                if (currentProcessInst.DataFields[key].Value!= dataFields[key])
                                {
                                    currentProcessInst.DataFields[key].Value = dataFields[key];
                                }
                            }
                        }
                        currentProcessInst.Update();
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
                            bool isExcuted = false;
                            for (int i = 0; i < workList.Actions.Count; i++)
                            {
                                if (workList.Actions[i].Name == actionString)
                                {
                                    workList.Actions[i].Execute();
                                    isExcuted = true;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion
                }

                return workList;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (k2Connection != null)
                {
                    k2Connection.Close();
                }
            }
        }

    }
}
