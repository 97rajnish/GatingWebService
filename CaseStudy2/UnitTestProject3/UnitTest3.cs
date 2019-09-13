using CodeMetricsDBRepositoryContractsLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaticAnalysisReportParserContractsLib;
using System.IO;
using TicsReportParserLib;
using TicsToolExecutorLib;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest3
    {
        private TicsToolExecutor ticsExecutor;
        private TicsReportParser ticsReportParser;
        private Moq.Mock<IReportParser> _mockWrapper;
        private Moq.Mock<ICodeMetricsDbRepository> _mockWrapper1;
        [TestInitialize]
        public void TestInitialize()
        {

            _mockWrapper = new Moq.Mock<IReportParser>();
            _mockWrapper1 = new Moq.Mock<ICodeMetricsDbRepository>();
            ticsReportParser = new TicsReportParser(_mockWrapper1.Object);
            ticsExecutor = new TicsToolExecutor(ticsReportParser);
        }
        [TestMethod]
        public void TestMethod1()
        {
            ticsExecutor.ExecuteTicsTool();
            Assert.IsTrue(File.Exists("C:\\Users\\320053937\\Documents\\TicsReport.txt"));
        }


        [TestCleanup]
        public void TestCleanUp()
        {
            ticsExecutor = null;
            _mockWrapper = null;
        }
    }
}
