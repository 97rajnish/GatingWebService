using GitRepositoryHandlerLib;
using CodeMetricsSQLDBRepositoryLib;
using SimianToolExecutorLib;
using SimianReportParserLib;
using System;

namespace GatingWebService
{

    public class SimianService : ISimianService
    {

        string simianOptions;
        public string ConfigureSimian(SimianOptions options)
        {
            simianOptions = options.ToString();
            return simianOptions;
        }

        public string GateSimianReport(InputRepoModel inputModel)
        {
            CodeMetricsSqldbRepository sqldbRepository = new CodeMetricsSqldbRepository();
            SimianReportParser simianReportParser = new SimianReportParser(sqldbRepository);
            GitRepositoryHandler gitRepositoryHandler = new GitRepositoryHandler();
            SimianToolExecutor simianToolExecutor = new SimianToolExecutor(simianReportParser);
            string inputRepo = inputModel.gitRepo;
            simianReportParser.Persist(inputRepo);
            gitRepositoryHandler.CloneGithubRepository(inputRepo);
            int simianExitCode = simianToolExecutor.ExecuteSimianTool(simianOptions);
            int simianDuplicates = simianToolExecutor.ParseReport();
            int gatingStatusOnPreviousRun = simianReportParser.GateOnPreviousRun(inputRepo);
            string result;
            if (gatingStatusOnPreviousRun == -1)
                result = CheckGatingCondition(simianExitCode, simianDuplicates, inputModel.SimianDuplicatesThreshold);
            else if(gatingStatusOnPreviousRun == 0)
                result = CheckGatingCondition(simianExitCode, simianDuplicates, inputModel.SimianDuplicatesThreshold) + " Less Duplicates than Previous Run";
            else
                result = CheckGatingCondition(simianExitCode, simianDuplicates, inputModel.SimianDuplicatesThreshold) + "More Duplicates than Previous Run";
            return result;
        }

        private string CheckGatingCondition(int simianExitCode, int simianDuplicates, int simianDuplicatesThreshold)
        {
            if (simianExitCode == 0 && simianDuplicates < simianDuplicatesThreshold)
                return "Go";
            else
                return "No-Go";
        }
    }
}
