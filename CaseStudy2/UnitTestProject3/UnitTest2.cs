using CodeMetricsDBRepositoryContractsLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicsReportParserLib;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest2
    {
        private TicsReportParser ticsReportParser;
        private Moq.Mock<ICodeMetricsDbRepository> _mockWrapper;
        [TestInitialize]
        public void TestInitialize()
        {

            _mockWrapper = new Moq.Mock<ICodeMetricsDbRepository>();
            ticsReportParser = new TicsReportParser(_mockWrapper.Object);
        }
        [TestMethod]
        public void TestMethod1()
        {
            string reportPath = "C:\\Users\\320053937\\Documents\\TicsReportTest.txt";
            int i = ticsReportParser.Parse(reportPath);
            int expecedValue = 14;
            Assert.AreEqual(expecedValue, i);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string input = "Hi";
            ticsReportParser.Persist(input);
            _mockWrapper.Verify(fakeneighbour => fakeneighbour.PersistToDatabase(input), Moq.Times.Exactly(1));
        }
        [TestMethod]
        public void TestMethod3()
        {
            string input = "HiTest";
            ticsReportParser.GateOnPreviousRun(input);
            _mockWrapper.Verify(fakeneighbour => fakeneighbour.GetTicsErrors(input), Moq.Times.Exactly(1));
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            ticsReportParser = null;
            _mockWrapper = null;
        }
    }
}
