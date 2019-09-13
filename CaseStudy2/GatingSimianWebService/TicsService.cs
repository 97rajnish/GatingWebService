using CodeMetricsSQLDBRepositoryLib;
using GitRepositoryHandlerLib;
using SimianReportParserLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicsReportParserLib;
using TicsToolExecutorLib;

namespace GatingWebService
{
    public class TicsService : ITicsService
    {
        public string GateTicsReport(TicsInputRepoModel inputModel)
        {
            
            CodeMetricsSqldbRepository sqldbRepository = new CodeMetricsSqldbRepository();
            TicsReportParser ticsReportParser = new TicsReportParser(sqldbRepository);
            GitRepositoryHandler gitRepositoryHandler = new GitRepositoryHandler();
            TicsToolExecutor ticsToolExecutor = new TicsToolExecutor(ticsReportParser);
            string inputRepo = inputModel.gitRepo;
            ticsReportParser.Persist(inputRepo);
            gitRepositoryHandler.CloneGithubRepository(inputRepo);
            ticsToolExecutor.ExecuteTicsTool();
            int ticsErrors = ticsToolExecutor.ParseReport();
            int gatingStatusOnPreviousRun = ticsReportParser.GateOnPreviousRun(inputRepo);
            string result;
            if (gatingStatusOnPreviousRun == -1)
                result = CheckGatingCondition(ticsErrors, inputModel.TicsErrorsThreshold);
            else if (gatingStatusOnPreviousRun == 0)
                result = CheckGatingCondition(ticsErrors, inputModel.TicsErrorsThreshold) + " Less Errors than Previous Run";
            else
                result = CheckGatingCondition(ticsErrors, inputModel.TicsErrorsThreshold) + " Same Or More Errors than Previous Run";
            return result;
        }
        private string CheckGatingCondition(int ticsErrors, int ticsErrorsThreshold)
        {
            if (ticsErrors < ticsErrorsThreshold)
                return "Go";
            else
                return "No-Go";
        }
    }
}