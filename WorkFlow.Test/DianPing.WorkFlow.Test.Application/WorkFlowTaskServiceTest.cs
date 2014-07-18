using DianPing.WorkFlow.Application.Implementation.Task;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Application.Interface.Dto;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using Microsoft.Practices.Unity;
using Moq;
using System.Collections.Generic;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2ServerLog.Entity;
using DianPing.WorkFlow.Domain.Interface.Ddo;
using Newtonsoft.Json;
using DianPing.WorkFlow.Common.Enum;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Server.Entity;


namespace DianPing.WorkFlow.Test.Application1
{
    
    /// <summary>
    ///这是 WorkFlowTaskServiceTest 的测试类，旨在
    ///包含所有 WorkFlowTaskServiceTest 单元测试
    ///</summary>
    [TestClass()]
    public class WorkFlowTaskServiceTest
    {
        public WorkFlowTaskServiceTest()
        {
            RegisteUnity.RegisterForDefault();
            RegisteUnity.Current.BuildUp<WorkFlowTaskServiceTest>(this as WorkFlowTaskServiceTest);
        }

        private class WorkFlowTaskServiceTestMock
        {
            public static List<ProcessInfo> processInfoList
            {
                get
                {
                    return new List<ProcessInfo>()
                    {
                        new ProcessInfo(){
                         ProcessFullName="测试测试流程",
                          ProcessCode="tttst"
                        }
                    };
                }
            }
            public static List<WorklistHeader> WorkListHeader
            {
                get {
                    return new List<WorklistHeader>()
                    {
                        new WorklistHeader(){
                         ActInstDestId=256,
                          ProcInstID=123
                        }
                    };
                }
            }

            public static QueryListResultBase<MyTaskDdo> MyTaskDdo
            {
                get
                {
                    return new QueryListResultBase<MyTaskDdo>()
                    {
                        PagingInfo = new PaginationModel() { },
                        ResultList = new List<MyTaskDdo>() {
                         new MyTaskDdo(){ Folio="test111", SN="123_256"}
                        }
                    };
                }
            }
            
            public static QueryListResultBase<MyTaskDto> MyTaskDto
            {
                get
                {
                    return new QueryListResultBase<MyTaskDto>()
                    {
                        PagingInfo = new PaginationModel() { },
                        ResultList = new List<MyTaskDto>() {
                         new MyTaskDto(){ Folio="test111", SN="123_256"} }
                    };
                }
            }

            
            public static ResultModel succussReAssignResult
            {
                get
                {
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
        ///WorkFlowTaskService 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void WorkFlowTaskServiceConstructorTest()
        {
            WorkFlowTaskService target = new WorkFlowTaskService();
        }
        
        /// <summary>
        ///GetMyTaskList 的测试
        ///</summary>
        [TestMethod()]
        public void GetMyTaskListTest()
        {
            var mock = new Mock<WorkFlowTaskService>() { CallBase = true };
            mock.Setup(_ => _.ProcessInfoDomain.GetByProcessCode(It.IsAny<IList<string>>())).Returns(WorkFlowTaskServiceTestMock.processInfoList);
            mock.Setup(_ => _.MyTaskDomain.GetMyTaskList(It.IsAny<QueryCriteriaBase<QueryWorkList>>())).Returns(WorkFlowTaskServiceTestMock.MyTaskDdo);
            mock.Setup(_ => _.WorklistHeaderRepositories.GetListBySnList(It.IsAny<List<string>>())).Returns(WorkFlowTaskServiceTestMock.WorkListHeader);
            var query = new QueryCriteriaBase<MyTaskCriteria>()
            {
                PagingInfo = new PaginationModel()
            };
            var actual = mock.Object.GetMyTaskList(null);
            Assert.AreEqual(0, actual.ResultList.Count);
            query.QueryCriteria = new MyTaskCriteria()
            {
                Folio = "",
                ProcessStartDate = new DatePeriodModel
                {
                    DateFrom = DateTime.Now,
                    DateTo = DateTime.Now,
                }
            };
            Assert.AreEqual(0, actual.ResultList.Count);

            query.QueryCriteria.ProcessCode = null;
            var actual2 = mock.Object.GetMyTaskList(query);
            Assert.IsTrue(actual2.ResultList.Count == WorkFlowTaskServiceTestMock.MyTaskDto.ResultList.Count);
            query.QueryCriteria.ProcessCode = new List<string>() { };
            actual2 = mock.Object.GetMyTaskList(query);
            Assert.IsTrue(actual2.ResultList.Count == WorkFlowTaskServiceTestMock.MyTaskDto.ResultList.Count);
            query.QueryCriteria.ProcessCode = new List<string>() { "aaa" };
            actual2 = mock.Object.GetMyTaskList(query);
            Assert.IsTrue(actual2.ResultList.Count == WorkFlowTaskServiceTestMock.MyTaskDto.ResultList.Count);

            query.QueryCriteria.ProcInstId = null;
            var actual3 = mock.Object.GetMyTaskList(query);
            Assert.IsTrue(actual3.ResultList.Count == WorkFlowTaskServiceTestMock.MyTaskDto.ResultList.Count);
            query.QueryCriteria.ProcInstId = new List<int>() { };
            actual3 = mock.Object.GetMyTaskList(query);
            Assert.IsTrue(actual3.ResultList.Count == WorkFlowTaskServiceTestMock.MyTaskDto.ResultList.Count);
            query.QueryCriteria.ProcInstId = new List<int>() { 12345 };
            actual3 = mock.Object.GetMyTaskList(query);
            Assert.IsTrue(actual3.ResultList.Count == WorkFlowTaskServiceTestMock.MyTaskDto.ResultList.Count);
        }

        [TestMethod()]
        public void ReAssignTest()
        {
            var mock = new Mock<WorkFlowTaskService>() { CallBase = true };
            mock.Setup(_ => _.MyTaskDomain.ReAssign(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(WorkFlowTaskServiceTestMock.succussReAssignResult);

            Assert.AreEqual(ResultCode.Fail, mock.Object.ReAssign("123", 1, "", 2, "", true).Code);
            Assert.AreEqual(ResultCode.Fail, mock.Object.ReAssign("123_2", 0, "", 2, "", true).Code);
            Assert.AreEqual(ResultCode.Fail, mock.Object.ReAssign("123_2", 1, "", 0, "", true).Code);
            Assert.AreEqual(ResultCode.Sucess, mock.Object.ReAssign("123_2", 1, "", 2, "", true).Code);
            
        }
    }
}
