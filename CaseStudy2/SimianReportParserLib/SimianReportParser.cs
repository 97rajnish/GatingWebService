using CodeMetricsDBRepositoryContractsLib;
using StaticAnalysisReportParserContractsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimianReportParserLib
{
    public class SimianReportParser : IReportParser
    {
        private readonly ICodeMetricsDbRepository _repository;
#pragma warning disable S1104 // Fields should not have public accessibility
        public int simianDuplicates;
#pragma warning restore S1104 // Fields should not have public accessibility
        public SimianReportParser(ICodeMetricsDbRepository repository)
        {
            _repository = repository;
        }
        public int Parse(string reportPath)
        {
            var data = File.ReadAllLines(reportPath);
            string line = data[data.Length - 4];
            string result = Regex.Match(line, @"\d+").Value;
            simianDuplicates = Int32.Parse(result);
            return simianDuplicates;
        }
        public void Persist(string gitRepo)
        {
            _repository.PersistToDatabase(gitRepo);
        }
        public int GateOnPreviousRun(string gitRepo)
        {
            int previousRunDuplicates = _repository.GetSimianDuplicates(gitRepo);
            if (previousRunDuplicates < 0)
            {
                _repository.UpdateSimianDuplicates(gitRepo, simianDuplicates);
                return -1;
            }
            else if (simianDuplicates < previousRunDuplicates)
            {
                _repository.UpdateSimianDuplicates(gitRepo, simianDuplicates);
                return 0;
            }
            else
                return 1;
            
        }
    }
}
