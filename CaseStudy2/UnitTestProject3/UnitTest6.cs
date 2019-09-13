using CodeMetricsDBRepositoryContractsLib;
using GitRepositoryHandlerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimianReportParserLib;
using System.IO;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest6
    {
        private DatabaseTestHelper helper;
        [TestInitialize]
        public void TestInitialize()
        {

          helper = new DatabaseTestHelper();
        }
        [TestMethod]
        public void TestMethod1()
        {
            int b = 35;
            int a = helper.GetSimianDuplicates("1");
            Assert.AreEqual(a,b);
        }
        [TestMethod]
        public void TestMethod2()
        {
            int b = 30;
            int a = helper.GetTicsErrors("1");
            Assert.AreEqual(a, b);
        }
        [TestMethod]
        public void TestMethod3()
        {
            
            helper.UpdateTicsErrors("1",30);
            int b = helper.GetTicsErrors("1");
            Assert.AreEqual(30, b);
        }
        [TestMethod]
        public void TestMethod4()
        {

            helper.UpdateSimianDuplicates("1", 35);
            int b = helper.GetSimianDuplicates("1");
            Assert.AreEqual(35, b);
        }
        [TestMethod]
        public void TestMethod5()
        {
           helper.PersistToDatabase("15");
           int b = helper.GetTicsErrors("15");
            Assert.AreEqual(50, b);
        }
        [TestCleanup]
        public void TestCleanUp()
        {
            helper = null;
        }
    }
}
