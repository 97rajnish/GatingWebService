using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepositoryHandlerLib
{
    public class GitRepositoryHandler
    {
        public void CloneGithubRepository(string gitRepo)
        {
            DeleteClonedFiles();
            ProcessStartInfo cmdStartInfo = new ProcessStartInfo();
            cmdStartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            cmdStartInfo.RedirectStandardInput = true;
            cmdStartInfo.UseShellExecute = false;
            cmdStartInfo.CreateNoWindow = true;
            string command = "git clone " + gitRepo + " C://Temp//CaseStudy1";
            Process cmdProcess = new Process();
            cmdProcess.StartInfo = cmdStartInfo;
            cmdProcess.Start();
            cmdProcess.StandardInput.WriteLine(command);
            cmdProcess.StandardInput.WriteLine("exit");
            cmdProcess.WaitForExit();
        }

        private void DeleteClonedFiles()
        {
            var path = @"C" + ":\\Temp\\CaseStudy1";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            System.IO.DirectoryInfo di = new DirectoryInfo(path);
            foreach (var file in di.GetFiles("*", SearchOption.AllDirectories))
                file.Attributes &= ~FileAttributes.ReadOnly;
            foreach (FileInfo file in di.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }
        }
    }
}
