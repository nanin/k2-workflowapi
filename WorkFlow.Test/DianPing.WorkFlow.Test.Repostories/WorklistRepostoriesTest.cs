using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog;
using DianPing.WorkFlow.Repositories.Implementation.DianPingK2ServerLog;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;

namespace DianPing.WorkFlow.Test.Repostories
{
    /// <summary>
    ///这是 WorklistRepostoriesTest 的测试类，旨在
    ///包含所有 WorklistRepostoriesTest 单元测试
    ///</summary>
    [TestClass()]
    public class WorklistRepostoriesTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///GetWorkList 的测试
        ///</summary>
        [TestMethod()]
        public void GetWorkListTest()
        {
            IWorklistRepostories target = new WorklistRepostories(); 

            QueryCriteriaBase<QueryWorkList> queryCriteriay = new QueryCriteriaBase<QueryWorkList>()
            {
                PagingInfo = new PaginationModel()
                {
                    PageSize = 0,
                    PageCount = 0
                },
                QueryCriteria = new QueryWorkList()
                {
                    ProcessStartDate = new DatePeriodModel
                    {
                        DateFrom = DateTime.Today.AddDays(-30),
                        DateTo = DateTime.Today,
                    },
                    Folio = string.Empty,
                    LoginIds = null,
                    ProcInstIds = null
                }
            };
            var actual = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual.PagingInfo.PageCount == 0);


            queryCriteriay.PagingInfo.PageSize = 3;
            actual = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual.ResultList.Count > 0);



            queryCriteriay.QueryCriteria.LoginIds = new List<int>();
            queryCriteriay.QueryCriteria.ProcInstIds = new List<int>();
            actual = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual.ResultList.Count > 0);


            queryCriteriay.QueryCriteria.ProcInstIds = new List<int> { actual.ResultList[0].ProcInst.ID };
            queryCriteriay.QueryCriteria.Folio = actual.ResultList[0].ProcInst.Folio;
            queryCriteriay.QueryCriteria.LoginIds = new List<int>() { Convert.ToInt32(actual.ResultList[0].Destination.Replace("K2SQL:", "")) };
            var actual2 = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual2.ResultList.Count > 0);

            queryCriteriay.QueryCriteria.ProcessCodes = new List<string> { };
            var actual9 = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual9.ResultList.Count > 0);

            queryCriteriay.QueryCriteria.ProcessCodes = new List<string> { actual.ResultList[0].ProcInst.proc.ProcSet.Descr };
            var actual10 = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual10.ResultList.Count > 0);

            var actual3 = target.GetWorkList(queryCriteriay);
            //Assert.AreEqual(actual2.ResultList.Count, actual3.PagingInfo.ItemCount);
            Assert.IsTrue(queryCriteriay.PagingInfo.PageSize >= actual3.ResultList.Count);
            Assert.IsTrue(actual3.PagingInfo.PageCount > 0);

            queryCriteriay.PagingInfo.SortField = "worklisttime";
            queryCriteriay.PagingInfo.SortOrder = Common.Enum.SortOrder.Descending;
            var actual4 = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual4.ResultList[0].StartDate >= actual4.ResultList[actual4.ResultList.Count - 1].StartDate);

            queryCriteriay.PagingInfo.SortField = "worklisttime";
            queryCriteriay.PagingInfo.SortOrder = Common.Enum.SortOrder.Ascending;
            var actual5 = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual5.ResultList[0].StartDate <= actual5.ResultList[actual5.ResultList.Count - 1].StartDate);

            queryCriteriay.PagingInfo.SortField = "folio";
            queryCriteriay.PagingInfo.SortOrder = Common.Enum.SortOrder.Unspecified;
            var actual6 = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual6.ResultList.Count > 0);

            queryCriteriay.PagingInfo.SortField = "folio";
            queryCriteriay.PagingInfo.SortOrder = Common.Enum.SortOrder.Descending;
            var actual7 = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual7.ResultList.Count > 0);

            queryCriteriay.PagingInfo.PageSize = 2000;
            var actual11 = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual11.ResultList.Count <=1000);

            queryCriteriay.PagingInfo.SortField = "";
            queryCriteriay.PagingInfo.PageIndex = queryCriteriay.PagingInfo.ItemCount + 1;
            var actual8 = target.GetWorkList(queryCriteriay);
            Assert.IsTrue(actual8.ResultList.Count == 0);


        }
    }
}
