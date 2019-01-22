using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Robot.Entidades;
using Robot.Framework;
using Robot.Schedule.DAL;
using Robot.Schedule.BLL;
using IntegWeb.Previdencia.Aplicacao.BLL;

// Add a using directive and a reference for System.Net.Http.  
using System.Net.Http;  

namespace Robot.Schedule
{
    public partial class _service : ServiceBase
    {
        
        System.Timers.Timer timer;
        CancellationTokenSource cts;
        int RunJobs_count = 0;
        byte cont = 1;

        public _service()
        {
            //InitializeComponent();
            timer = new System.Timers.Timer();
            //When autoreset is True there are reentrancy problem 
            timer.AutoReset = false;
            timer.Interval = 5000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(RunJobs);
            //RunJobs(new object { }, null);
            Util.Log("_service", "Service created");
        }

        private async void RunJobs(object sender, System.Timers.ElapsedEventArgs e)
        {
            // Instantiate the CancellationTokenSource.  
            cts = new CancellationTokenSource();

            try
            {
                //await AccessTheWebAsync(cts.Token);
                Processa_Schedule(cts.Token);
                //ProcessaTrocaDeArquivos_Validacao(cts.Token);
                //ProcessaTrocaDeArquivos_Carregamento(cts.Token);
                //resultsTextBox.Text += "\r\nDownloads complete.";
                cont++;
                if (RunJobs_count >= 1800 || RunJobs_count == 0)
                {
                    Util.Log("Schedule.RunJobs", "RunJobs complete. [cont=" + cont.ToString() + "] [" + RunJobs_count.ToString() + "x] ");
                    RunJobs_count = 0;
                }
            }
            catch (OperationCanceledException ocEx)
            {
                //resultsTextBox.Text += "\r\nDownloads canceled.\r\n";
                //Util.LogError(ocEx);
                if (RunJobs_count >= 60 || RunJobs_count == 0)
                {
                    string _log = Util.LogError(ocEx);
                    _log = "Schedule.RunJobs [OperationCanceledException]: <br><br>" + _log;
                    DisparaAlerta(_log);
                    RunJobs_count = 0;
                }
            }
            catch (Exception Ex)
            {
                // resultsTextBox.Text += "\r\nDownloads failed.\r\n";                
                if (RunJobs_count >= 60 || RunJobs_count == 0)
                {
                    string _log = Util.LogError(Ex);
                    _log = "Schedule.RunJobs [Exception]: <br><br>" + _log;
                    DisparaAlerta(_log);
                    RunJobs_count = 0;
                }
            }
            RunJobs_count++;
            timer.Start();
            cts = null;
        }

        private void DisparaAlerta(string _log)
        {
            Email mail_util = new Email();
            mail_util.EnviaEmail("guilherme.provenzano@funcesp.com.br", "Portal Funcesp <atendimento@funcesp.com.br>", "** Alerta de erro no processamento do Troca de Arquivos **", _log, "");
        }

        async Task Processa_Schedule(CancellationToken ct)
        {
            try{
                ScheduleBLL ArqPatBLL = new ScheduleBLL();
                Resultado res = ArqPatBLL.Processar();
                Util.Log("Processa_Schedule", res.Mensagem);
                cont++;
                if (RunJobs_count >= 1800 || RunJobs_count == 0)
                {
                    Util.Log("Schedule.Processa_Schedule", "Processa_Schedule complete. [cont=" + cont.ToString() + "] [" + RunJobs_count.ToString() + "x] ");
                    RunJobs_count = 0;
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                GravaLog("Schedule.Processa_Schedule [DbEntityValidationException]: <br><br>" + Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException DbEx)
            {
                GravaLog("Schedule.Processa_Schedule [DbUpdateException]: <br><br>" + Util.GetInnerException(DbEx));
            }
            catch (OperationCanceledException ocEx)
            {
                GravaLog("Schedule.Processa_Schedule [OperationCanceledException]: <br><br>" + Util.LogError(ocEx));
            }
            catch (Exception Ex)
            {
                GravaLog("Schedule.Processa_Schedule [Exception]: <br><br>" + Util.LogError(Ex));
            }
        }

        private void GravaLog(string _log)
        {
            if (RunJobs_count >= 60 || RunJobs_count == 0)
            {
                //string _log = Util.LogError(ocEx);
                //_log = "Processa_Schedule [OperationCanceledException]: <br><br>" + _log;
                DisparaAlerta(_log);
                RunJobs_count = 0;
            }
        }

        async Task ProcessaTrocaDeArquivos_Validacao(CancellationToken ct)
        {
            ArqPatrocinadoraBLL ArqPatBLL = new ArqPatrocinadoraBLL();
            String ret = ""; // ArqPatBLL.Processar_Todos_Arquivos_Por_Status(3); // Validação solicitada
            Util.Log("Schedule.ProcessaTrocaDeArquivos_Validacao", ret);
        }

        async Task ProcessaTrocaDeArquivos_Carregamento(CancellationToken ct)
        {
            ArqPatrocinadoraBLL ArqPatBLL = new ArqPatrocinadoraBLL();
            String ret = ""; // ArqPatBLL.Processar_Todos_Arquivos_Por_Status(6); // Carregamento solicitado
            Util.Log("Schedule.ProcessaTrocaDeArquivos_Validacao", ret);
        }

        async Task AccessTheWebAsync(CancellationToken ct)
        {
            HttpClient client = new HttpClient();

            // Make a list of web addresses.  
            List<string> urlList = SetUpURLList();

            // ***Create a query that, when executed, returns a collection of tasks.  
            IEnumerable<Task<int>> downloadTasksQuery =
                from url in urlList select ProcessURL(url, client, ct);

            // ***Use ToList to execute the query and start the tasks.   
            List<Task<int>> downloadTasks = downloadTasksQuery.ToList();

            // ***Add a loop to process the tasks one at a time until none remain.  
            while (downloadTasks.Count > 0)
            {
                // Identify the first task that completes.  
                Task<int> firstFinishedTask = await Task.WhenAny(downloadTasks);

                // ***Remove the selected task from the list so that you don't  
                // process it more than once.  
                downloadTasks.Remove(firstFinishedTask);

                // Await the completed task.  
                int length = await firstFinishedTask;
                //resultsTextBox.Text += String.Format("\r\nLength of the download:  {0}", length);
                Util.Log("AccessTheWebAsync", String.Format("\r\nLength of the download:  {0}", length));                
            }            
        }

        private List<string> SetUpURLList()
        {
            List<string> urls = new List<string>   
            {   
                "http://msdn.microsoft.com",  
                "http://msdn.microsoft.com/library/windows/apps/br211380.aspx",  
                "http://msdn.microsoft.com/en-us/library/hh290136.aspx",  
                "http://msdn.microsoft.com/en-us/library/dd470362.aspx",  
                "http://msdn.microsoft.com/en-us/library/aa578028.aspx",  
                "http://msdn.microsoft.com/en-us/library/ms404677.aspx",  
                "http://msdn.microsoft.com/en-us/library/ff730837.aspx"  
            };
            return urls;
        }

        async Task<int> ProcessURL(string url, HttpClient client, CancellationToken ct)
        {
            // GetAsync returns a Task<HttpResponseMessage>.   
            HttpResponseMessage response = await client.GetAsync(url, ct);

            // Retrieve the website contents from the HttpResponseMessage.  
            byte[] urlContents = await response.Content.ReadAsByteArrayAsync();

            return urlContents.Length;
        }  

        protected override void OnStart(string[] args)
        {
            this.timer.Start();
            Util.Log("OnStart", "Service Started");
        }

        protected override void OnStop()
        {
            this.timer.Stop();
            Util.Log("OnStop", "Service Stoped");
        }

        protected override void OnPause()
        {
            base.OnPause();
            this.timer.Stop();
            Util.Log("OnPause", "Service Paused");
            //this.timer.Stop();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            this.timer.Start();
            Util.Log("OnContinue", "Service Continued");
        }
    }
}
