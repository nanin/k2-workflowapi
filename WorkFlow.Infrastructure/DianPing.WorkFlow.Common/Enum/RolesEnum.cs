using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DianPing.WorkFlow.Common.Enum
{
    class RolesEnum
    {
    }

    public enum TGContractRoles
    {
        /// <summary>
        /// 制单人
        /// </summary>
        [Description("制单人")]
        dest_Creator = 0,
        /// <summary>
        /// AE
        /// </summary>
        [Description("AE")]
        dest_ae = 1,
        /// <summary>
        /// 销售一级主管
        /// </summary>
        [Description("销售一级主管")]
        dest_managerlevel1 = 2,
        /// <summary>
        /// 风控品控
        /// </summary>
        [Description("风控品控")]
        dest_riskcontrol = 3,
        /// <summary>
        /// 地区经理
        /// </summary>
        [Description("地区经理")]
        dest_salesmanager = 4,
        /// <summary>
        /// 销售VP
        /// </summary>
        [Description("销售VP")]
        dest_vp = 5,
        /// <summary>
        /// 财务
        /// </summary>
        [Description("团购财务")]
        dest_finance = 6,
        /// <summary>
        /// 合同审核员
        /// </summary>
        [Description("合同审核员")]
        dest_contract = 7,
        /// <summary>
        /// 项目组
        /// </summary>
        [Description("项目组")]
        dest_projecteam = 8,
        /// <summary>
        /// 大区经理
        /// </summary>
        [Description("大区经理")]
        dest_regionalmanager = 9,
        /// <summary>
        /// 保补审批员
        /// </summary>
        [Description("保补审批员")]
        dest_allowancer = 10,
        /// <summary>
        /// 保补一级主管
        /// </summary>
        [Description("保补一级主管")]
        dest_allowancemanagerlevel1 = 11,

        /// <summary>
        /// 城市经理
        /// </summary>
        [Description("城市经理")]
        dest_citymanager = 17,
        /// <summary>
        /// 推广财务
        /// </summary>
        [Description("推广财务")]
        dest_finance_ad = 18
    }

    /// <summary>
    /// BI ACL流程
    /// </summary>
    public enum BIACLApplyRoles
    {
        /// <summary>
        /// 制单人
        /// </summary>
        [Description("制单人")]
        dest_Creator = 0,
        /// <summary>
        /// 汇报线
        /// </summary>
        [Description("汇报线")]
        dest_reportto = 1,
        ///// <summary>
        ///// 部门负责人
        ///// </summary>
        //[Description("部门负责人")]
        //dest_department_head = 2,
        ///// <summary>
        ///// 安全角色的部门负责人
        ///// </summary>
        //[Description("安全角色的部门负责人")]
        //dest_role_department_head = 3,
        /// <summary>
        /// ACL系统
        /// </summary>
        [Description("ACL系统")]
        dest_acl_system = 4,

    }
}
