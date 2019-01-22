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
namespace IntegWeb.Previdencia.Aplicacao.DAL.Cadastro
{
    public class CtrlDevCorrespondenciaDAL
    {
        internal PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public IQueryable<CAD_TBL_CTRLDEV_CORRESP> GetControleCorrespondencia()
        {
            IQueryable<CAD_TBL_CTRLDEV_CORRESP> query;
            query = from u in m_DbContext.CAD_TBL_CTRLDEV_CORRESP
                    select u;

            return query;
        }

        /// <summary>
        /// Retorna o Titular 
        /// </summary>
        /// <returns></returns>
        public DataTable GetTitular(int codEmpresa, int codMatricula)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select * FROM att.empregado ");
                querysql.Append(" where COD_EMPRS = " + codEmpresa);
                querysql.Append(" AND NUM_RGTRO_EMPRG = " + codMatricula);

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

        public Resultado AtualizaControleCorrespondencia(CAD_TBL_CTRLDEV_CORRESP obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.CAD_TBL_CTRLDEV_CORRESP.FirstOrDefault(p => p.ID_REG == obj.ID_REG);

                if (atualiza != null)
                {

                    atualiza.ID_TIPOMOTIVODEVOLUCAO = obj.ID_TIPOMOTIVODEVOLUCAO;
                    atualiza.ID_TIPODOCUMENTO = obj.ID_TIPODOCUMENTO;
                    atualiza.DATAPOSTAGEM = obj.DATAPOSTAGEM;
                    atualiza.DATADEVOLUCAO = obj.DATADEVOLUCAO;
                    atualiza.REENVIO = obj.REENVIO;
                    atualiza.TEMPO_PRAZO = obj.TEMPO_PRAZO;                  

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


        /// <summary>
        /// Retorna o endereço do Representantes
        /// </summary>
        /// <returns></returns>
        public DataTable GetRepressEnd(int rptant)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" select distinct * from repres_uniao_fss s ");
                querysql.Append(" where s.num_idntf_rptant=  " + rptant);

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

        public DataTable GetCredenEnd(int CodCreden)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" select ");
                querysql.Append(" distinct ");
                querysql.Append("  ec.num_logradouro, ");
                querysql.Append("  ec.num_cep, ");
                querysql.Append("  ec.nom_logradouro, ");
                querysql.Append("  ec.des_complemento, ");
                querysql.Append("  ec.nom_bairro, ");
                querysql.Append("  mp.dcr_munici, ");
                querysql.Append("  ec.cod_estado ");
                querysql.Append("   from att.tb_end_conven ec ");
                querysql.Append("   left join municipio mp on ec.cod_munici = mp.cod_munici ");
                querysql.Append("  where ec.cod_convenente = " + CodCreden);
                querysql.Append("  and ");
                querysql.Append("  ec.idc_desativado = 'N' ");
                querysql.Append("    and ec.num_seq_end = ");
                querysql.Append("        (Select max(ec.num_seq_end) ");
                querysql.Append("           from att.tb_end_conven d ");
                querysql.Append("          where d.cod_convenente = ec.cod_convenente )");


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

        /// <summary>
        /// Retorna os Representantes do Titular Ativos
        /// </summary>
        /// <returns></returns>
        public DataTable GetRepress(int codEmpresa, int codMatricula)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" select distinct f.cod_emprs, f.num_rgtro_emprg, s.nom_repres, s.num_idntf_rptant, ");
                querysql.Append(" s.num_idntf_rptant ||'-'|| s.nom_repres as  CodigoENomeRepress ");
                querysql.Append("   from repres_uniao_fss s, scam.repres_depend_fss f ");
                querysql.Append("  where s.num_idntf_rptant = f.num_idntf_rptant ");
                querysql.Append("    AND (f.DAT_FIMVIG_REPDEP IS NULL OR f.DAT_FIMVIG_REPDEP >= SYSDATE) ");
                querysql.Append(" AND f.cod_emprs =  " + codEmpresa);
                querysql.Append(" AND f.num_rgtro_emprg =  " + codMatricula);
                querysql.Append(" union ");
                querysql.Append(" select distinct f.cod_emprs, f.num_rgtro_emprg,s.nom_repres, s.num_idntf_rptant, ");
                querysql.Append(" s.num_idntf_rptant ||'-'|| s.nom_repres as CodigoENomeRepress ");
                querysql.Append("   from repres_uniao_fss s, att.repres_depend_fss f ");
                querysql.Append("  where s.num_idntf_rptant = f.num_idntf_rptant ");
                querysql.Append("    AND (f.DAT_FIMVIG_REPDEP IS NULL OR f.DAT_FIMVIG_REPDEP >= SYSDATE) ");
                querysql.Append(" AND f.cod_emprs =  " + codEmpresa);
                querysql.Append(" AND f.num_rgtro_emprg =  " + codMatricula);

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

        /// <summary>
        /// Retorna o convenente 
        /// </summary>
        /// <returns></returns>
        public DataTable GetCredenciado(int codContrato)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select * FROM att.tb_convenente ");
                querysql.Append(" where COD_CONVENENTE = " + codContrato);

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

        public Resultado InseriCorrespondencia(CAD_TBL_CTRLDEV_CORRESP obj)
        {
            Resultado res = new Resultado();

            try
            {
                //Valida o tipo de Motivo Devolucao
                var atualiza = m_DbContext.CAD_TBL_CTRLDEV_CORRESP.FirstOrDefault(p => p.ID_REG == obj.ID_REG);

                if (atualiza == null)
                {
                    obj.ID_REG = GetMaxPk() + 1;
                    m_DbContext.CAD_TBL_CTRLDEV_CORRESP.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }
                }
                else
                {
                    res.Alert("Esse motivo devolução já existe! ");
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
            maxPK = Convert.ToInt16(m_DbContext.CAD_TBL_CTRLDEV_CORRESP.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG));
            return maxPK;
        }

    }
}
