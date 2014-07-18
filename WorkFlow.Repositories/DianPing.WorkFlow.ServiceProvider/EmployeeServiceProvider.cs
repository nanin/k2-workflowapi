using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.ServiceProvider;
using DianPing.WorkFlow.ServiceProvider.JsonModel;
using Newtonsoft.Json;

namespace DianPing.WorkFlow.ServiceProvider
{
    public class EmployeeServiceProvider : IEmployeeServiceProvider
    {

        private readonly string ORGANIZATIONAL_STRUCTURE_PIGEON =
            ConfigurationManager.AppSettings["OrganizationalStructurePigeon"] + "/invoke.json";

        public string[] GetUsersBySalesCRMRoleType(Common.Enum.TGContractRoles roletype, int loginID)
        {
            var client = new EmployeeService.HRServiceClient();
            return client.GetUsersBySalesCRMRoleType((EmployeeService.CRMRoleType)((int)roletype), loginID.ToString())
                .Select(_ => _.LoginId).ToArray();
        }

        public string[] GetDepartmentHeadLoginIdByLoginId(int loginId)
        {

            string postString = string.Join("&", "url=http://service.dianping.com/ba/base/organizationalstructure/UserService_1.0.0"
                                               , "method=getOrganizationHierarchy"
                                               , "parameterTypes=int"
                                               , "parameters=" + loginId.ToString());
            byte[] postData = Encoding.UTF8.GetBytes(postString);
            List<Department> departmentList = new List<Department>();
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("serialize", 7.ToString());
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] responseData = client.UploadData(ORGANIZATIONAL_STRUCTURE_PIGEON, "POST", postData);//得到返回字符流  
                string result = Encoding.UTF8.GetString(responseData);//解码  
                departmentList = JsonConvert.DeserializeObject<List<Department>>(result);
            }
            if (departmentList == null)
            {
                return new List<string>().ToArray();
            }
            else
            {
                var firstLevelDepartment = departmentList.SingleOrDefault(_ => _.Level == 1);
                if (firstLevelDepartment == null)
                {
                    return new List<string>().ToArray();
                }
                else
                {
                    return new string[] { firstLevelDepartment.LeaderId.ToString() };
                }
            }
        }

        public string[] GetReportToLoginIdByLoginId(int loginId)
        {
            string postString = string.Join("&", "url=http://service.dianping.com/ba/base/organizationalstructure/UserService_1.0.0"
                                              , "method=queryUserByLoginID"
                                              , "parameterTypes=int"
                                              , "parameters=" + loginId.ToString());
            byte[] postData = Encoding.UTF8.GetBytes(postString);
            Dper dper = new Dper();
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("serialize", 7.ToString());
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] responseData = client.UploadData(ORGANIZATIONAL_STRUCTURE_PIGEON, "POST", postData);//得到返回字符流  
                string result = Encoding.UTF8.GetString(responseData);//解码  
                dper = JsonConvert.DeserializeObject<Dper>(result);
            }

            if (dper == null)
            {
                return new List<string>().ToArray();
            }
            else
            {
                return new string[] { dper.ReportToLoginId.ToString() };
            }

        }

        public string[] GetFirstLevelDepartmentIdByDepartment(int departmentId)
        {
            string postString = string.Join("&", "url=http://service.dianping.com/ba/base/organizationalstructure/OrganizationService_1.0.0"
                                                , "method=getDepartmentHierarchy"
                                                , "parameterTypes=int"
                                                , "parameters=" + departmentId.ToString());
            byte[] postData = Encoding.UTF8.GetBytes(postString);
            List<Department> departmentList = new List<Department>();
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("serialize", 7.ToString());
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] responseData = client.UploadData(ORGANIZATIONAL_STRUCTURE_PIGEON, "POST", postData);//得到返回字符流  
                string result = Encoding.UTF8.GetString(responseData);//解码  
                departmentList = JsonConvert.DeserializeObject<List<Department>>(result);
            }
            if (departmentList == null)
            {
                return new List<string>().ToArray();
            }
            else
            {
                var firstLevelDepartment = departmentList.SingleOrDefault(_ => _.Level == 1);
                if (firstLevelDepartment == null)
                {
                    return new List<string>().ToArray();
                }
                else
                {
                    return new string[] { firstLevelDepartment.DepartmentId.ToString() };
                }
            }
        }
    }



}
