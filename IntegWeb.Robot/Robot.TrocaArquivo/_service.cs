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
using IntegWeb.Previdencia.Aplicacao.BLL;

namespace Robot.TrocaArquivo
{
    public partial class _service : ServiceBase
    {
        int RunJobs_count = 0;
        System.Timers.Timer timer;
        CancellationTokenSource cts;
        byte cont = 1;

        public _service()
        {
            //InitializeComponent();
            timer = new System.Timers.Timer();
            //When autoreset is True there are reentrancy problem 
            timer.AutoReset = false;
            timer.Interval = 6000;
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
                switch (cont)
                {
                    case 1:
                        await ProcessaTrocaDeArquivos(cts.Token, 4); //Validando
                        break;
                    case 2:
                        await ProcessaTrocaDeArquivos(cts.Token, 3); //Validação solicitada
                        break;
                    case 3:
                        await ProcessaTrocaDeArquivos(cts.Token, 7); //Carregando
                        break;
                    case 4:
                    default:
                       await  ProcessaTrocaDeArquivos(cts.Token, 6); //Carregamento solicitado       
                       cont = 0;
                       break;
                }
                cont++;
                //resultsTextBox.Text += "\r\nDownloads complete.";
                if (RunJobs_count >= 1800 || RunJobs_count == 0)
                //if (RunJobs_count >= 60 || RunJobs_count == 0)
                {
                    Util.Log("RunJobs ", "RunJobs complete. [cont=" + cont.ToString() + "] [" + RunJobs_count.ToString() + "x] ");
                    RunJobs_count = 0;
                }
                //RunJobs_count++;
                //timer.Start();
            }
            catch (OperationCanceledException ocEx)
            {
                //resultsTextBox.Text += "\r\nDownloads canceled.\r\n";
                //Util.LogError(ocEx);
                if (RunJobs_count >= 60 || RunJobs_count == 0)
                {
                    string _log = Util.LogError(ocEx);
                    _log = "RunJobs [OperationCanceledException]: <br><br>" + _log;
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
                    _log = "RunJobs [Exception]: <br><br>" + _log;
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

        async Task ProcessaTrocaDeArquivos(CancellationToken ct, short pStatus)
        {
            ArqPatrocinadoraBLL ArqPatBLL = new ArqPatrocinadoraBLL();
            String ret = await ArqPatBLL.Processar_Todos_Arquivos_Por_Status(pStatus);
            if (!String.IsNullOrEmpty(ret)) {
                switch (pStatus)
                {
                    case 3:
                    default:
                        Util.Log("Validação processada:", ret);
                        break;
                    case 4:
                        Util.Log("Validação particial:", ret);
                        break;
                    case 6:
                        Util.Log("Carregamento processado:", ret);
                        break;
                    case 7:
                        Util.Log("Carregamento particial:", ret);
                        break;
                }
            }
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
    }
}
