using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WinTriage
{
    class Cmd
    {
        public Cmd()
        {
            this.proc = new Process();
        }

		public string RunCmd(string cmd)
		{
			this.proc.StartInfo.CreateNoWindow = true;
			this.proc.StartInfo.FileName = "cmd.exe";
			this.proc.StartInfo.UseShellExecute = false;
			this.proc.StartInfo.RedirectStandardError = true;
			this.proc.StartInfo.RedirectStandardInput = true;
			this.proc.StartInfo.RedirectStandardOutput = true;
			this.proc.Start();
			this.proc.StandardInput.WriteLine(cmd);
			this.proc.StandardInput.WriteLine("exit");
			string text = this.proc.StandardOutput.ReadToEnd();
			this.proc.Close();
			return text;
		}

		public void RunProgram(string programName, string cmd)
		{
			Process process = new Process();
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.FileName = programName;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.RedirectStandardInput = true;
			process.StartInfo.RedirectStandardOutput = true;
			process.Start();
			bool flag = cmd.Length != 0;
			if (flag)
			{
				process.StandardInput.WriteLine(cmd);
			}
			process.Close();
		}

		public void RunProgram(string programName)
        {
			this.RunProgram(programName, "");
        }


		private Process proc = null;
    }

}
