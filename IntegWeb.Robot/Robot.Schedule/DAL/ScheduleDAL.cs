using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Schedule.ENTITY;
using Robot.Framework;
using Robot.Entidades;

namespace Robot.Schedule.DAL
{
    class ScheduleDAL
    {
        public SCHEDULE_Entity_Conn m_DbContext = new SCHEDULE_Entity_Conn();

        public List<FUN_TBL_ROBOT_SCHEDULE> GetData(int startRowIndex, int maximumRows, string pNome, string pMatricula, string sortParameter)
        {
            return GetWhere()
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public int GetDataCount(string pNome, string pMatricula)
        {
            return GetWhere().SelectCount();
        }

        public IQueryable<FUN_TBL_ROBOT_SCHEDULE> GetWhere()
        {
            IQueryable<FUN_TBL_ROBOT_SCHEDULE> query =
                    from sch in m_DbContext.FUN_TBL_ROBOT_SCHEDULE
                    select sch;

            return query;
        }

        public List<FUN_TBL_ROBOT_SCHEDULE> GetActiveJobs()
        {
            IQueryable<FUN_TBL_ROBOT_SCHEDULE> query =
                    from j in m_DbContext.FUN_TBL_ROBOT_SCHEDULE
                    where (j.IND_ATIVO == 1)
                    select j;

            return query.ToList();
        }

        public Resultado SaveData(FUN_TBL_ROBOT_SCHEDULE rsSchedule)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                var atualiza = m_DbContext.FUN_TBL_ROBOT_SCHEDULE.Find(rsSchedule.COD_JOB);

                if (atualiza != null)
                {                 
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(rsSchedule);
                }
                else
                {                  
                    m_DbContext.FUN_TBL_ROBOT_SCHEDULE.Add(rsSchedule);
                }
                m_DbContext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        protected Resultado ExecutarProc(string proc_command)
        {
            Resultado res = new Resultado();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                string[] proc_params = proc_command.Split(',');
                string _name = proc_params[0];                

                for (int p = 1; p < proc_params.Length; p++)
                {
                    string[] _params = proc_params[p].Split();

                    if (_params[1].ToUpper() == "OUT")
                    {
                        objConexao.AdicionarParametroOut(_params[0]);
                    }
                    else if (_params.Length > 3)
                    {
                        objConexao.AdicionarParametro(_params[0], _params[3]);
                    }
                }

                if (objConexao.ExecutarNonQuery(_name))
                {
                    res.Sucesso("Procedure executada com sucesso! " + proc_command);
                }

            }
            catch (Exception ex)
            {
                res.Erro(proc_command + ": " + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return res;
        }
    }
}
