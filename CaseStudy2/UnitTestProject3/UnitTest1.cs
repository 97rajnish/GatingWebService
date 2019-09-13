using CodeMetricsDBRepositoryContractsLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimianReportParserLib;
using System.Data.SqlClient;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest1
    {
        private SimianReportParser simianReportParser;
        private Moq.Mock<ICodeMetricsDbRepository> _mockWrapper2;
        [TestInitialize]
        public void TestInitialize()
        {
            
            _mockWrapper2 = new Moq.Mock<ICodeMetricsDbRepository>();
            simianReportParser = new SimianReportParser(_mockWrapper2.Object);
        }
        [TestMethod]
        public void TestMethod1()
        {
            string reportPath = "C:\\Users\\320053937\\Documents\\SimianReportTest.txt";
            int i = simianReportParser.Parse(reportPath);
            int expecedValue = 15;
            Assert.AreEqual(expecedValue, i);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string input = "Hi";
            simianReportParser.Persist(input);
            _mockWrapper2.Verify(fakeneighbour => fakeneighbour.PersistToDatabase(input), Moq.Times.Exactly(1));
        }
        [TestMethod]
        public void TestMethod3()
        {
            string input = "HiTest";
            simianReportParser.GateOnPreviousRun(input);
            _mockWrapper2.Verify(fakeneighbour => fakeneighbour.GetSimianDuplicates(input), Moq.Times.Exactly(1));
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            simianReportParser = null;
            _mockWrapper2 = null;
        }
    }
}
