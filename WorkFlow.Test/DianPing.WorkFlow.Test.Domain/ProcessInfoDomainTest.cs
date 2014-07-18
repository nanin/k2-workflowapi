using DianPing.WorkFlow.Domain.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using DianPing.WorkFlow.Application.Implementation.UnityHelpers;
using Microsoft.Practices.Unity;
using Moq;
using DianPing.WorkFlow.Domain.Interface;
using DianPing.WorkFlow.Common.Models;
using DianPing.WorkFlow.Domain.Interface.Ddo;

namespace DianPing.WorkFlow.Test.Domain
{
    /// <summary>
    ///这是 ProcessInfoDomainTest 的测试类，旨在
    ///包含所有 ProcessInfoDomainTest 单元测试
    ///</summary>
    [TestClass()]
    public class ProcessInfoDomainTest
    {
        public ProcessInfoDomainTest()
        {
            RegisteUnity.RegisterForDefault();
            RegisteUnity.Current.BuildUp<ProcessInfoDomainTest>(this as ProcessInfoDomainTest);
        }

        private class ProcessInfoDomainTestMock
        {
            public static IList<ProcessInfo> ProcessInfoList
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
        ///GetByProcessCode 的测试
        ///</summary>
        [TestMethod()]
        public void GetByProcessCodeTest()
        {
            var mock = new Mock<ProcessInfoDomain>(){ CallBase=true};
            mock.Setup(_ => _.ProcessInfoRepostories.GetByProcessCode(It.IsAny<IList<string>>())).Returns(ProcessInfoDomainTestMock.ProcessInfoList);
            var actual = mock.Object.GetByProcessCode(new List<string>() { "aasf" });
            Assert.IsTrue(actual.Count > 0);
        }
    }
}
