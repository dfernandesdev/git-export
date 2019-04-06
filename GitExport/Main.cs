using CliWrap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//git archive -o patch.zip $commit-id $(git diff-tree -r --no-commit-id --name-only --diff-filter= ACMRT $commit-id)

namespace GitExport
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            //txtRepoPath.Text = @"C:\Users\Diogo\Source\repos2\LDSoft.Aquiles\";
            //txtCommitFrom.Text = "fb947fa9dca34a2af89e9493dd079b63873da224";
            txtCommitTo.Text = "HEAD";
        }

        public void Export(string oldCommit, string newCommit)
        {
            var git = new GitManager(txtRepoPath.Text);

            var args = $@"diff -r --name-only --diff-filter=ACMRT {oldCommit}..{newCommit}";

            var files = git.InternalProceed(args).StandardOutput.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            string filesFlat = string.Empty;

            foreach (var file in files)
                filesFlat += $"\"{file}\" ";

            var args2 = $@"archive -o ""{AppDomain.CurrentDomain.BaseDirectory}{DateTime.Now.ToString("yyyyMMdd-HHmmss")}.zip"" HEAD {filesFlat}";
            git.InternalProceed(args2);




            //var args = $@"diff --name-only -z --diff-filter=ACMRT {oldCommit}..{newCommit}";
            //var args2 = $@"-0 git archive -o update.zip {newCommit}";
            ////var args = $@"-c 'git diff --name-only -z --diff-filter=ACMRT {oldCommit}..{newCommit}' | xargs -0 bash -c '""$@""; sleep 10' _ git archive -o update.zip {newCommit}";

            //var retorno = new Cli(@"git.exe")
            //    .EnableStandardErrorValidation(false)
            //    .EnableExitCodeValidation(true)
            //    .SetWorkingDirectory(@"C:\Users\Diogo\Source\repos2\LDSoft.Aquiles\")
            //    .SetArguments(args)
            //    .Execute();


            //var retorno2 = new Cli(@"C:\Program Files\Git\usr\bin\xargs.exe")
            //    .EnableStandardErrorValidation(false)
            //    .EnableExitCodeValidation(true)
            //    .SetWorkingDirectory(@"C:\Users\Diogo\Source\repos2\LDSoft.Aquiles\")
            //    .SetStandardInput(retorno.StandardOutput)
            //    .SetArguments(args2)
            //    .Execute();


            //var proc = new Process
            //{
            //    StartInfo = new ProcessStartInfo
            //    {
            //        FileName = "git.exe",
            //        Arguments = args,
            //        UseShellExecute = false,
            //        RedirectStandardOutput = true,
            //        CreateNoWindow = true,
            //        WorkingDirectory = @"C:\Users\Diogo\Source\repos2\LDSoft.Aquiles\"
            //    }
            //};

            //proc.Start();

            //Process cmd = new Process();
            //cmd.StartInfo.FileName = "cmd.exe";
            //cmd.StartInfo.Verb = "runas";
            //cmd.StartInfo.RedirectStandardInput = true;
            //cmd.StartInfo.RedirectStandardOutput = true;
            //cmd.StartInfo.CreateNoWindow = true;
            //cmd.StartInfo.UseShellExecute = false;
            //cmd.Start();

            //cmd.StandardInput.WriteLine("notepad.exe >"+ commitId);
            //cmd.StandardInput.Flush();
            //cmd.StandardInput.Close();
            //cmd.WaitForExit();
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd());




            //var startInfo = new System.Diagnostics.ProcessStartInfo
            //{
            //    WorkingDirectory = @"The\Process\Working\Directory",
            //    WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
            //    FileName = "cmd.exe",
            //    RedirectStandardInput = true,
            //    UseShellExecute = false
            //};



            //System.Diagnostics.Process p = new System.Diagnostics.Process();
            //p.StartInfo.FileName = $"git archive -o patch.zip {commitId} $(git diff-tree -r --no-commit-id --name-only --diff-filter= ACMRT {commitId})";
            //p.StartInfo.UseShellExecute = true;
            //p.Start();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export(txtCommitFrom.Text, txtCommitTo.Text);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRepoPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
