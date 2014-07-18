using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Domain.Interface;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity;
using DianPing.WorkFlow.Common.Enum;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider;
using DianPing.WorkFlow.Infrastructure.Interception;
using System.Web;
using System.Web.Caching;
using System.IO;
using Com.Dianping.Cat;


namespace DianPing.WorkFlow.Domain.Implementation
{
    public class K2UserDomain : IK2UserDomain
    {
        [Dependency]
        public virtual IK2UserRepostories k2UserRepostories { get; set; }

        [Dependency]
        public virtual IEmployeeServiceProvider employeeServiceProvider { get; set; }

        // todo: 重构，根据 http://www.dzone.com/snippets/get-appsettings-webconfig
        private readonly string DEST_ACL_SYSTEM = ConfigurationManager.AppSettings["dest_acl_system"];

        public K2UserPO GetK2User(int loginID)
        {
            return k2UserRepostories.GetK2User(loginID);
        }

        public void GenerateApprovalUser(int loginID, Dictionary<string, string> dataFields, string processCode, string actName)
        {
            //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            //var fileMap = Path.Combine(path, "CatConfig.xml");
            //Cat.Initialize(fileMap);
            Cat.GetProducer().LogEvent("GenerateApprovalUser", "Arguments", "0", string.Format("loginId:{0},processCode:'{1}'", loginID, processCode));
            Cat.GetProducer().NewTransaction("GenerateApprovalUser", processCode);
            var b = Cat.GetManager().PeekTransaction;

            try
            {
                switch (processCode)
                {
                    #region 合同审批流程
                    case "TGContract":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_ae);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_managerlevel1);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_riskcontrol);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_vp);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_finance);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_contract);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            
                            break;
                        }
                    #endregion
                    #region 团购合同变更流程
                    case "TGContractModify":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_ae);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_managerlevel1);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_vp);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_finance);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_contract);
                        }
                        break;
                    #endregion
                    #region 备单变更流程
                    case "TGDealGroup":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_projecteam);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_managerlevel1);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_riskcontrol);
                        }
                        break;
                    #endregion
                    #region 保补审批流程
                    case "TGAllowance":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_citymanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_allowancer);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_allowancemanagerlevel1);
                        }
                        break;
                    #endregion
                    #region 本地广告审批流程
                    case "ADContract":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_managerlevel1);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_citymanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_regionalmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_vp);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_finance_ad);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_contract);
                        }
                        break;
                    #endregion
                    #region 本地广告变更审批流程
                    case "ADContractModify":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_managerlevel1);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_finance_ad);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_contract);
                        }
                        break;
                    #endregion
                    #region 结婚广告审批流程
                    case "ADContractV2":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_managerlevel1);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_citymanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_regionalmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_finance_ad);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_contract);
                        }
                        break;
                    #endregion

                    #region 框架合同审批流程
                    case "TGContractV2":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_vp);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_finance);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_contract);
                            break;
                        }
                    #endregion
                    #region 框架合同变更审批流程
                    case "TGContractV2Modify":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_salesmanager);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_vp);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_finance);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_contract);
                            break;
                        }
                    #endregion
                    #region 框架合同的订单审批流程
                    case "TGOrder":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_managerlevel1);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_riskcontrol);
                            break;
                        }
                    #endregion
                    #region 框架合同的订单变更审批流程
                    case "TGOrderModify":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_managerlevel1);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_riskcontrol);
                            //GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_projecteam);
                            break;
                        }
                    #endregion
                    #region 订单审批流程
                    case "PCTGOrderApproval":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_managerlevel1);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_riskcontrol);
                            break;
                        }
                    #endregion
                    #region 订单变更流程
                    case "PCTGCardApproval":
                        {
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_managerlevel1);
                            GetUsersBySalesCRMRoleType(loginID, dataFields, TGContractRoles.dest_riskcontrol);
                            break;
                        }
                    #endregion

                    #region 加入安全角色申请流程
                    case "BIACLApplySecureRole":
                        {
                            GetUsersByACLApplyRoles(loginID, dataFields, BIACLApplyRoles.dest_reportto);
                            dataFields.Add(BIACLApplyRoles.dest_acl_system.ToString(), DEST_ACL_SYSTEM);
                            break;
                        }
                    #endregion
                    #region 长期权限申请流程
                    case "BIACLApplyPrivs":
                        {
                            GetUsersByACLApplyRoles(loginID, dataFields, BIACLApplyRoles.dest_reportto);
                            dataFields.Add(BIACLApplyRoles.dest_acl_system.ToString(), DEST_ACL_SYSTEM);
                            break;
                        }
                    #endregion
                    #region 安全角色管理流程
                    case "BIACLApplyRoleAdmin":
                        {
                            dataFields.Add(BIACLApplyRoles.dest_acl_system.ToString(), DEST_ACL_SYSTEM);
                            break;
                        }
                    #endregion
                    #region 故障排查紧急申请流程
                    case "BIACLApplyUrgent":
                        {
                            dataFields.Add(BIACLApplyRoles.dest_acl_system.ToString(), DEST_ACL_SYSTEM);
                            break;
                        }
                    #endregion
                }
                b.Status = "0";
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("501:"))
                {
                    b.Status = "0";
                }
                else
                {
                    Cat.GetProducer().LogError(ex);
                    b.SetStatus(ex);
                }
                throw ex;
            }
            finally
            {
                b.Complete();
            }

        }

        #region private
        //private void GetUsersBySalesCRMRoleType(int loginID, Dictionary<string, string> dataFields, TGContractRoles roletype)
        //{
        //    string user = string.Empty;

        //    user = string.Join(";", employeeServiceProvider.GetUsersBySalesCRMRoleType(roletype, loginID));
        //    if (string.IsNullOrEmpty(user))
        //    {
        //        throw new Exception(string.Format("501:{0}发起的流程未找到审批人[{1}],请联系CRM管理员", loginID.ToString(), roletype.ToString()));
        //    }
        //    dataFields.Add(roletype.ToString(), user);
        //}

        private void GetUsersBySalesCRMRoleType(int loginID, Dictionary<string, string> dataFields, TGContractRoles roletype)
        {
            string key = string.Format("{0}_{1}", loginID.ToString(), roletype.ToString());
            string user = string.Empty;

            var cache = HttpRuntime.Cache.Get(key);

            if (cache == null)
            {
                user = string.Join(";", employeeServiceProvider.GetUsersBySalesCRMRoleType(roletype, loginID));
                Cat.GetProducer().LogEvent("GetUsersBySalesCRMRoleType", "FromAPI", "0", user);
                try
                {
                    DateTime cacheExpiriation;
                    int timeSpan = 30 - DateTime.Now.Minute;

                    cacheExpiriation = timeSpan > 0
                        ? DateTime.Now.AddMinutes(timeSpan)
                        : DateTime.Now.AddHours(1).AddMinutes(timeSpan);

                    HttpRuntime.Cache.Add(key, user, null, cacheExpiriation, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

                }
                catch { }
            }
            else
            {
                user = cache.ToString();
                Cat.GetProducer().LogEvent("GetUsersBySalesCRMRoleType", "FromCache", "0", user);
            }
            if (string.IsNullOrEmpty(user))
            {
                //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
                //var fileMap = Path.Combine(path, "CatConfig.xml");
                //Cat.Initialize(fileMap);
                //var a = Cat.GetProducer().NewTransaction("WorkFlowService", input.MethodBase.Name);

                //Cat.GetProducer().LogEvent("GetUsersBySalesCRMRoleType", "Error", "0", user);
                throw new Exception(string.Format("501:{0}发起的流程未找到审批人[{1}],请联系CRM管理员", loginID.ToString(), roletype.ToString()));
            }

            dataFields.Add(roletype.ToString(), user);
        }

        private void GetUsersByACLApplyRoles(int loginID, Dictionary<string, string> dataFields, BIACLApplyRoles roletype)
        {
            string user = string.Empty;

            switch (roletype)
            {
                case BIACLApplyRoles.dest_reportto:
                    user = string.Join(";", employeeServiceProvider.GetReportToLoginIdByLoginId(loginID));
                    break;
            }

            if (string.IsNullOrEmpty(user))
            {
                throw new Exception(string.Format("501:{0}发起的流程未找到审批人[{1}],请联系ACL管理员", loginID.ToString(), roletype.ToString()));
            }

            dataFields.Add(roletype.ToString(), user);
        }
        #endregion
    }
}
