using CodeMetricsDBRepositoryContractsLib;
using StaticAnalysisReportParserContractsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TicsReportParserLib
{
    public class TicsReportParser : IReportParser
    {
        private readonly ICodeMetricsDbRepository _repository;
        int ticsErrors;

        public TicsReportParser(ICodeMetricsDbRepository repository)
        {
            _repository = repository;
        }
        public void Persist(string gitRepo)
        {
            _repository.PersistToDatabase(gitRepo);
        }

        public int Parse(string reportPath)
        {
            StreamReader reader = File.OpenText(reportPath);
            string line;
            string result;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("TOTAL |"))
                {
                    result = Regex.Match(line, @"\d+").Value;
                    ticsErrors += Int32.Parse(result);
                }
            }

            return ticsErrors;
        }
        public int GateOnPreviousRun(string gitRepo)
        {
            int previousRunErrors = _repository.GetTicsErrors(gitRepo);
            if (previousRunErrors < 0)
            {
                _repository.UpdateTicsErrors(gitRepo, ticsErrors);
                return -1;
            }
            else if (ticsErrors < previousRunErrors)
            {
                _repository.UpdateTicsErrors(gitRepo, ticsErrors);
                return 0;
            }
            else
                return 1;
        }
    }
}
