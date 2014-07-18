using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider;
using DianPing.WorkFlow.ServiceProvider;
using DianPing.WorkFlow.ServiceProvider.JsonModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace DianPing.WorkFlow.Test.Application
{

    [TestClass()]
    public class EmployeeServiceProviderTest
    {
        private IEmployeeServiceProvider employeeServiceProvider = new EmployeeServiceProvider();
        private readonly string ORGANIZATIONAL_STRUCTURE_PIGEON =
    ConfigurationManager.AppSettings["OrganizationalStructurePigeon"];

        [TestMethod()]
        public void GetFirstLevelDepartmentIdByDepartmentTest()
        {
            var result = employeeServiceProvider.GetFirstLevelDepartmentIdByDepartment(2133);
        }


        [TestMethod()]
        public void GetDepartmentHeadLoginIdByLoginIdTest()
        {
            var result = employeeServiceProvider.GetDepartmentHeadLoginIdByLoginId(-21292);
        }

        [TestMethod()]
        public void GetReportToLoginIdByLoginIdTest()
        {
            var result = employeeServiceProvider.GetReportToLoginIdByLoginId(-21292);
        }
    }
}
