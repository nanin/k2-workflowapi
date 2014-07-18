using System.Collections.Generic;
using DianPing.WorkFlow.Domain.Implementation;
using DianPing.WorkFlow.Domain.Interface;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Domain.Interface.Ddo;
using Moq;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using Newtonsoft.Json;
using DianPing.WorkFlow.Common.Enum;


namespace DianPing.WorkFlow.Test.Domain
{
    
    
    /// <summary>
    ///这是 MyTaskDomainTest 的测试类，旨在
    ///包含所有 MyTaskDomainTest 单元测试
    ///</summary>
    [TestClass()]
    public class MyTaskDomainTest 
    {
        private class MyTaskDomainTestMock
        {
            public static QueryListResultBase<MyTaskDdo> MyTaskDdo
            {
                get
                {
                    return new QueryListResultBase<MyTaskDdo>()
                    {
                        PagingInfo = new PaginationModel() { },
                        ResultList = new List<MyTaskDdo>() {
                         new MyTaskDdo(){  Folio="test111", SN="123-256"}
                        }
                    };
                }
            }

            public static QueryListResultBase<Worklist> Worklist
            {
                get
                {
                    return new QueryListResultBase<Worklist>()
                    {
                        PagingInfo = new PaginationModel() { },
                        ResultList = new List<Worklist>() {
                         new Worklist(){ Destination="K2SQL:-16740", DestType=1}
                        }
                    };
                }
            }

            public static ResultModel succussReAssignResult
            {
                get {
                    return new ResultModel()
                    {
                        Code = ResultCode.Sucess,
                        Msg = JsonConvert.SerializeObject( new K2CommentPO()
                        {
                            CommentID = 1,
                            LoginID = -16740
                        })
                    };
                }
            }

            public static ResultModel failReAssignResult
            {
                get
                {
                    return new ResultModel()
                    {
                        Code = ResultCode.Fail
                    };
                }
            }
        }
        
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
        ///GetMyTaskList 的测试
        ///</summary>
        [TestMethod()]
        public void GetMyTaskListTest()
        {
            var mock = new Mock<TaskDomain>() { CallBase = true };
            mock.Setup(_ => _.WorklistRepostories.GetWorkList(It.IsAny<QueryCriteriaBase<QueryWorkList>>())).Returns(MyTaskDomainTestMock.Worklist);
            Assert.AreEqual(MyTaskDomainTestMock.Worklist.ResultList.Count, mock.Object.GetMyTaskList(new QueryCriteriaBase<QueryWorkList>()).ResultList.Count);            
        }

        [TestMethod()]
        public void ReAssignTest()
        {

            string activityName = string.Empty;
            string processCode = string.Empty;
            int procInstID = 0;
            var mock = new Mock<TaskDomain>() { CallBase = true };
            mock.Setup(_ => _.K2ServiceProvider.ReAssign(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), out activityName, out processCode, out procInstID)).Returns(MyTaskDomainTestMock.failReAssignResult);
            Assert.AreEqual(ResultCode.Fail, mock.Object.ReAssign("12_12", -16740, "", -16740, "", false).Code);

            mock.Setup(_ => _.K2ServiceProvider.ReAssign(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), out activityName, out processCode, out procInstID)).Returns(MyTaskDomainTestMock.succussReAssignResult);
            mock.Setup(_ => _.K2CommentRepostories.Save(JsonConvert.DeserializeObject<K2CommentPO>(MyTaskDomainTestMock.succussReAssignResult.Msg)));
            Assert.AreEqual(ResultCode.Sucess, mock.Object.ReAssign("12_12", -16740, "", -16740, "", true).Code);
            Assert.AreEqual(ResultCode.Sucess, mock.Object.ReAssign("12_12", -16740, "", -16740, "", false).Code);
        }
    }
}
