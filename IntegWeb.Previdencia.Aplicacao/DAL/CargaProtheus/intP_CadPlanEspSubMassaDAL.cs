using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Int_Protheus
{
    public class intP_CadPlanEspSubMassaDAL
    {

        internal PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public IQueryable<SUBMASSA> GetSubMassa()
        {
            IQueryable<SUBMASSA> query;

            query = from u in m_DbContext.SUBMASSA
                    select u;

            return query;
        }

        public DataTable GetEspecie()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select * FROM att.especie_benef_fss ");

                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(querysql.ToString());
                adpt.Fill(dt);
                adpt.Dispose();
            }

            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        public IQueryable<PLANO_BENEFICIO_FSS> GetPlano()
        {
            IQueryable<PLANO_BENEFICIO_FSS> query;

            query = from u in m_DbContext.PLANO_BENEFICIO_FSS
                    select u;

            return query;
        }

        public IQueryable<PLN_SUBMASSA> GetCadPlanEspSubMassaProtheus()
        {
            IQueryable<PLN_SUBMASSA> query;

            query = from u in m_DbContext.PLN_SUBMASSA
                    select u;

            return query;
        }

        public Resultado AtualizaCadPlanEspSubMassaProtheus(PLN_SUBMASSA obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.PLN_SUBMASSA.FirstOrDefault(p => p.COD_PLN_SUBMASSA == obj.COD_PLN_SUBMASSA);

                if (atualiza != null)
                {
                    atualiza.COD_EMPRS = obj.COD_EMPRS;
                    atualiza.COD_ESPBNF = obj.COD_ESPBNF;
                    atualiza.NUM_PLBNF = obj.NUM_PLBNF;
                    atualiza.COD_SUBMASSA = obj.COD_SUBMASSA;

                    int rows_updated = m_DbContext.SaveChanges();

                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro Atualizado com Sucesso");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado InseriCadPlanEspSubMassaProtheus(PLN_SUBMASSA obj)
        {
            Resultado res = new Resultado();

            try
            {
                //Valida a combinação de Fluxo e ação;
                var atualiza = m_DbContext.PLN_SUBMASSA.FirstOrDefault(p =>
                    (p.COD_EMPRS == obj.COD_EMPRS || obj.COD_EMPRS == null)
                     && (p.COD_ESPBNF == obj.COD_ESPBNF || obj.COD_ESPBNF == null)
                     && (p.COD_SUBMASSA == obj.COD_SUBMASSA || obj.COD_SUBMASSA == null)
                     && (p.NUM_PLBNF == obj.NUM_PLBNF || obj.NUM_PLBNF == null)
                     );

                if (atualiza == null)
                {
                    obj.COD_PLN_SUBMASSA = GetMaxPk() + 1;
                    m_DbContext.PLN_SUBMASSA.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }
                }
                else
                {
                    res.Alert("Essa Combinação de Fluxo e ação já existe! ");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public int GetMaxPk()
        {
            int maxPK = 0;
            maxPK = Convert.ToInt16(m_DbContext.PLN_SUBMASSA.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_PLN_SUBMASSA));
            return maxPK;
        }

    }
}
