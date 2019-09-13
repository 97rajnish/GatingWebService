using CodeMetricsDBRepositoryContractsLib;
using GitRepositoryHandlerLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimianReportParserLib;
using System.IO;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest5
    {
        private GitRepositoryHandler gitHandler;
        [TestInitialize]
        public void TestInitialize()
        {

            gitHandler = new GitRepositoryHandler();
        }
        [TestMethod]
        public void TestMethod1()
        {
            gitHandler.CloneGithubRepository(@"https://github.com/97rajnish/CaseStudy");
            Assert.IsTrue(Directory.Exists(@"C:\Temp\CaseStudy1"));
        }
       
        [TestCleanup]
        public void TestCleanUp()
        {
            gitHandler = null;
        }
    }
}
