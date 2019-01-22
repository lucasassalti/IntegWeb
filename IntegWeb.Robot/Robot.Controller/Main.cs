using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.ServiceProcess;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using Robot.Framework;
using Robot.Entidades;

namespace Robot
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadGrid();
            if (btnInstall.Enabled)
            {
                InstallServices();
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            List<ServiceController> lsServices = Util.GetAllServices("IntegWeb");
            dataGridView1.DataSource = lsServices;

            if (lsServices.Count < 2)
            {
                btnInstall.Enabled = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Resultado res = Util.StartService("Robot.Schedule", 2);
            if (!res.Ok)
            {
                MessageBox.Show(res.Mensagem, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadGrid();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Resultado res = Util.StopService("Robot.Schedule", 1);
            if (!res.Ok)
            {
                MessageBox.Show(res.Mensagem, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadGrid();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            InstallServices();
        }

        private void InstallServices()
        {

            var appSettings = ConfigurationManager.AppSettings;

            string path_InstallUtil = appSettings["path_InstallUtil"];
            string path_Schedule = appSettings["path_Schedule"];
            string path_TrocaArquivo = appSettings["path_TrocaArquivo"];

            string app_path = Path.GetDirectoryName(Assembly.GetAssembly(GetType()).Location);

            Verify_File_Path(app_path, ref path_Schedule);
            Verify_File_Path(app_path, ref path_TrocaArquivo);

            //System.Diagnostics.Process.Start(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe", app_path + @"Robot.Schedule.exe");
            //System.Diagnostics.Process.Start(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe", app_path + @"Robot.TrocaArquivo.exe");

            Start_Process(@path_InstallUtil, @path_Schedule);
            Start_Process(@path_InstallUtil, @path_TrocaArquivo);
        }

        private void Verify_File_Path(string app_path, ref string path_service)
        {
            if (!File.Exists(@path_service))
            {
                string FileName = Path.GetFileName(@path_service);
                if (File.Exists(app_path + "\\" + FileName))
                {
                    path_service = app_path + "\\" + FileName;
                }
            }
        }

        private void Start_Process(string FileName, string Arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();             
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = FileName; //Application.ExecutablePath;
            startInfo.Verb = "runas";
            startInfo.Arguments = Arguments;
            startInfo.ErrorDialog = true;
            Process p = Process.Start(startInfo);
            p.WaitForExit();
            //Application.Exit();
        }
    }
}
