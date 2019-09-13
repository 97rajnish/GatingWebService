using StaticAnalysisReportParserContractsLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicsToolExecutorLib
{
    public class TicsToolExecutor
    {
#pragma warning disable S1104 // Fields should not have public accessibility
        public string ticsReportPath = "C:" + "\\Users\\320053937" + "\\Documents\\TicsReport.txt";
#pragma warning restore S1104 // Fields should not have public accessibility
        readonly IReportParser _reportParser;

        public TicsToolExecutor(IReportParser reportParser)
        {
            _reportParser = reportParser;
        }

        public  void ExecuteTicsTool()
        {

            Process ticsProcess = new Process();
            ticsProcess.StartInfo.FileName = @"C:\Program Files\TIOBE\TICS\Client\Tics.exe";
            ticsProcess.StartInfo.Arguments = @"C:\Temp\CaseStudy1"; //argument
            ticsProcess.StartInfo.UseShellExecute = false;
            ticsProcess.StartInfo.RedirectStandardOutput = true;
            ticsProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            ticsProcess.StartInfo.CreateNoWindow = true; //not diplay a windows
            ticsProcess.Start();
            string output = ticsProcess.StandardOutput.ReadToEnd(); //The output result
            ticsProcess.WaitForExit();
            if (!File.Exists(ticsReportPath))
                File.Create(ticsReportPath).Dispose();  
            StreamWriter tw = new StreamWriter(ticsReportPath);
            tw.WriteLine(output);
            tw.Close();

        }
        public int ParseReport()
        {
            
            int ticsErrors = _reportParser.Parse(ticsReportPath);
            return ticsErrors;
        }
    }
}
