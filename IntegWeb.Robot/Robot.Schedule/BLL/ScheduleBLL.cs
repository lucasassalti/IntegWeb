using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Schedule.ENTITY;
using Robot.Schedule.DAL;
using Robot.Entidades;
using Robot.Framework;

namespace Robot.Schedule.BLL
{
    class ScheduleBLL : ScheduleDAL
    {
        public Resultado Processar()
        {
            Resultado res = new Resultado();
            foreach(FUN_TBL_ROBOT_SCHEDULE job in GetActiveJobs())
            {
                /*Intervalo especifico
                Data especifica
                A cada hora
                Diariamente
                Semanalmente
                Mensalmente
                Anualmente*/

                DateTime date_time = DateTime.Now;

                bool _execute = DeveExecutar(job, date_time);

                if (_execute)
                {
                    Log_Insert(job, 0, "Em execução...", date_time, "Desenv");
                    res = Executar(job, date_time);
                    date_time = DateTime.Now.AddMilliseconds(1);
                    if (res.Ok)
                    {
                        Log_Insert(job, 1, res.Mensagem, date_time, "Desenv");
                    }
                    else
                    {
                        Log_Insert(job, 2, res.Mensagem, date_time, "Desenv");
                    }
                }

            }
            return res;
        }

        private Resultado Executar(FUN_TBL_ROBOT_SCHEDULE job, DateTime date_time)
        {
            Resultado res = new Resultado();
            switch (job.TIP_COMANDO)
            {
                case 1: //Execução de Proc
                    res = ExecutarProc(job.DCR_COMANDO);
                    break;
                case 2: //Prompt Comando
                    break;
                case 3: //Start servico
                    res = ExecutarServico(job.DCR_COMANDO);
                    Util.GetAllServices("IntegWeb");
                    break;
            }
            return res;
        }

        protected Resultado ExecutarServico(string servine_name)
        {
            Resultado res = new Resultado();

            try
            {

                res = Util.StartService(servine_name, 2);

                if (res.Ok)
                {
                    res.Sucesso("Serviço iniciado com sucesso! " + servine_name);
                }

            }
            catch (Exception ex)
            {
                res.Erro(servine_name + ": " + ex.Message);
            }
            finally
            {
                //objConexao.Dispose();
            }
            return res;
        }

        private bool DeveExecutar(FUN_TBL_ROBOT_SCHEDULE job, DateTime date_time)
        {
            bool ret = false;

            if (job.IND_ATIVO == 1)  // Somente Jobs ativos
            {
                FUN_TBL_ROBOT_SCHEDULE_LOG job_log = job
                                                     .FUN_TBL_ROBOT_SCHEDULE_LOG
                                                     .FirstOrDefault(l => l.DTH_INCLUSAO == job
                                                                           .FUN_TBL_ROBOT_SCHEDULE_LOG
                                                                           .Max(m => m.DTH_INCLUSAO));
                if (job_log == null) // Job com log vazio. (Primeira utlização)
                {
                    job_log = new FUN_TBL_ROBOT_SCHEDULE_LOG();
                    //job_log.COD_JOB
                    job_log.COD_RESULTADO = 999;
                    //job_log.DCR_RESULTADO
                    //job_log.DTH_INCLUSAO = new DateTime(0);
                    //job_log.LOG_EXCLUSAO
                    //job.FUN_TBL_ROBOT_SCHEDULE_LOG.Add();
                }

                if (job_log.COD_RESULTADO > 0) // Não esta em execução
                {
                    switch (job.TIP_PERIODIC)
                    {
                        case 1: //Intervalo especifico
                            if (date_time >= job_log.DTH_INCLUSAO.AddSeconds(Convert.ToInt64(job.NUM_INTERVALO)))
                            {
                                //res.Sucesso("Intervalo especifico: " + job_log.DTH_INCLUSAO.AddSeconds(Convert.ToInt64(job.NUM_INTERVALO)).ToString("dd/MM/yyyy hh:mm:ss"));
                                ret = true;
                            }
                            break;
                        case 2: //Data especifica
                            break;
                        case 3: //A cada hora
                            break;
                        case 4: //Diariamente
                            break;
                        case 5: //Semanalmente
                            break;
                        case 6: //Mensalmente
                            break;
                        case 7: //Anualmente
                            break;
                    }
                }
            }

            return ret;
        }

        private void Log_Insert(FUN_TBL_ROBOT_SCHEDULE job, int? COD_RESULTADO, string DCR_RESULTADO, DateTime DTH_INCLUSAO, string LOG_EXCLUSAO)
        {
            if (job.NUM_NIVEL_LOG > 0)
            {
                FUN_TBL_ROBOT_SCHEDULE_LOG new_log = new FUN_TBL_ROBOT_SCHEDULE_LOG();
                new_log.COD_JOB = job.COD_JOB;
                new_log.COD_RESULTADO = COD_RESULTADO;
                new_log.DCR_RESULTADO = DCR_RESULTADO;
                new_log.DTH_INCLUSAO = DTH_INCLUSAO;
                new_log.LOG_EXCLUSAO = LOG_EXCLUSAO;
                job.FUN_TBL_ROBOT_SCHEDULE_LOG.Add(new_log);
                base.SaveData(job);
            }
        }

    }
}
