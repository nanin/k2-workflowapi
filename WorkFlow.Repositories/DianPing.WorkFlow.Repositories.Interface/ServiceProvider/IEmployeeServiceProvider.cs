using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Common.Enum;

namespace DianPing.WorkFlow.Repositories.Interface.ServiceProvider
{
    public interface IEmployeeServiceProvider
    {
        string[] GetUsersBySalesCRMRoleType(Common.Enum.TGContractRoles roletype, int loginID);
        /// <summary>
        /// 根据LoginId获得其所在部门的head的LoginId
        /// </summary>
        string[] GetDepartmentHeadLoginIdByLoginId(int loginId);

        /// <summary>
        /// 根据LoginId获得其reportto的LoginId
        /// </summary>
        string[] GetReportToLoginIdByLoginId(int loginId);

        /// <summary>
        /// 根据部门ID获取其所在一级部门的ID
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        string[] GetFirstLevelDepartmentIdByDepartment(int departmentId);
    }
}
