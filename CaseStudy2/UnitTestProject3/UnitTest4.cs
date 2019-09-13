using CodeMetricsDBRepositoryContractsLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimianReportParserLib;
using SimianToolExecutorLib;
using StaticAnalysisReportParserContractsLib;
using System.IO;
using TicsReportParserLib;
using TicsToolExecutorLib;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest4
    {
        private SimianToolExecutor simianExecutor;
        private SimianReportParser simianReportParser;
        private Moq.Mock<ICodeMetricsDbRepository> _mockWrapper;
        [TestInitialize]
        public void TestInitialize()
        {

            _mockWrapper = new Moq.Mock<ICodeMetricsDbRepository>();
            simianReportParser = new SimianReportParser(_mockWrapper.Object);
            simianExecutor = new SimianToolExecutor(simianReportParser);
        }
        [TestMethod]
        public void TestMethod1()
        {
            string simianOptions = " -ignoreCurlyBraces+ ";
            simianExecutor.ExecuteSimianTool(simianOptions);
            Assert.IsTrue(File.Exists("C:\\Users\\320053937\\Documents\\SimianReport.txt"));
        }


        [TestCleanup]
        public void TestCleanUp()
        {
            simianExecutor = null;
            _mockWrapper = null;
        }
    }
}
