using StaticAnalysisReportParserContractsLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimianToolExecutorLib
{
    public class SimianToolExecutor
    {
        readonly string simianReportPath = "C:" + "\\Users\\320053937" + "\\Documents\\SimianReport.txt";
        readonly IReportParser _reportParser;

        public SimianToolExecutor(IReportParser reportParser)
        {
            _reportParser = reportParser;
        }

        public int ExecuteSimianTool(string simianOptions)
        {
            Process simianProcess = new Process();
            simianProcess.StartInfo.FileName = @"C:\Users\320053937\Downloads\bin\simian-2.5.10.exe";
            simianProcess.StartInfo.Arguments = simianOptions + "C:\\Temp\\CaseStudy1\\**\\*.cs";
            simianProcess.StartInfo.UseShellExecute = false;
            simianProcess.StartInfo.RedirectStandardOutput = true;
            simianProcess.Start();
            string output = simianProcess.StandardOutput.ReadToEnd(); //The output result
            simianProcess.WaitForExit();
            if (!File.Exists(simianReportPath))
                File.Create(simianReportPath).Dispose();
            StreamWriter tw = new StreamWriter(simianReportPath);
            tw.WriteLine(output);
            tw.Close();
            return simianProcess.ExitCode;
        }
        public int ParseReport()
        {
           
            int simianDuplicates = _reportParser.Parse(simianReportPath);
            return simianDuplicates;
        }
    }
}
