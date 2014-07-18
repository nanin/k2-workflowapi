using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using log4net;

using DianPing.WorkFlow.Infrastructure.Interception;

namespace DianPing.WorkFlow.Infrastructure
{
    public class APIKeyUtility
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool IsRightAPIKey(string apikey)
        {
            if (string.IsNullOrEmpty(apikey)) { return false; }
            if (apikey == ConfigurationManager.AppSettings["JavaAdminAPIKey"]) { return true; }
            try
            {
                string sql = "select * from HT_AdminAPIKey where IsValid = 1";
                var db = DatabaseFactory.CreateDatabase("DianPing_Main");
                DataSet ds = db.ExecuteDataSet(db.GetSqlStringCommand(sql));
                List<string> apikeylist = new List<string>();
                if (ds != null)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        apikeylist.Add(dr["APIKey"] + string.Empty);
                    }
                }
                return apikeylist.Exists(a => { return a == apikey; });
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return false;
            }
            //return true;
        }
    }
}
