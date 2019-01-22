using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Servico_envio_email_aniversarios
{
    class Program : ServiceBase
    {        
        static private Timer timer;
        Envio_email_aniversariante processo;


        static void Main(string[] args)
        {
            ServiceBase.Run(new Program());
        }

        public Program()
        {
            this.ServiceName = "Fcesp_envio_email_aniversarios";
            processo = new Envio_email_aniversariante();
            //Envio_email_aniversariante.enviar_email_notificacao(Properties.Settings.Default.Email_Status_Servico.ToString(), "Serviço de envio de email aniversariante - Iniciado", "Serviço foi iniciado");
            //Envio_email_aniversariante.enviar_email_aniversariante("duhlopesmachado@hotmail.com", "Serviço de envio de email aniversariante - Iniciado", "Serviço foi iniciado");

            //Envio_email_aniversariante.enviar_email_aniversariante("duhlopesmachado@hotmail.com", "Email de aniversário", "Email de aniversário");
            //Envio_email_aniversariante.enviar_email_notificacao_aniversario("duhlopesmachado@hotmail.com", "Email de aniversário - Teste smtp.office365", "Email de aniversário");
            //processo.Executar();
        }

        protected override void OnStart(string[] args)
        {

            base.OnStart(args);
            
            try
            {
                Envio_email_aniversariante.gravar_log("Serviço iniciado - Delay: " + Properties.Settings.Default.Tempo_de_espera_em_minutos.ToString());
                processo = new Envio_email_aniversariante();
                
                Envio_email_aniversariante.enviar_email_notificacao(Properties.Settings.Default.Email_Status_Servico.ToString(), "Serviço de envio de email aniversariante - Iniciado", "Serviço foi iniciado");

                processo.Executar();

                timer = new Timer();
                timer.Interval = 1000 * 60 * Convert.ToInt16(Properties.Settings.Default.Tempo_de_espera_em_minutos);//set interval of one day  
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                start_timer();
            }
            catch (Exception ee)
            {                
                Envio_email_aniversariante.gravar_log(ee.Message);
            }



        }

        protected override void OnStop()
        {
            base.OnStop();
            Envio_email_aniversariante.gravar_log("Serviço Finalizado");
            Envio_email_aniversariante.enviar_email_notificacao(Properties.Settings.Default.Email_Status_Servico.ToString(), "Serviço de envio de email aniversariante - Finalizado", "Serviço foi Finalizado");
            //TODO: clean up any variables and stop any threads
        }


        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Envio_email_aniversariante.gravar_log("-------------------------------------");
                processo.Executar();
            }
            catch (Exception ee)
            {
                Envio_email_aniversariante.gravar_log(ee.Message);
            }
        }


        private static void start_timer()
        {
            timer.Start();
        }
    }
}
