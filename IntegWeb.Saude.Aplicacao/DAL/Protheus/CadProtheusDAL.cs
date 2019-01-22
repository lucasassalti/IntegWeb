using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OracleClient;

namespace IntegWeb.Saude.Aplicacao.DAL
{

    public class CadProtheusDal
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();
        ConexaoOracle objConexao = new ConexaoOracle();


        public List<PRE_TBL_CARGA_PROTHEUS_TIPO> GetCargaProtheusddl(string area)
        {
            IQueryable<PRE_TBL_CARGA_PROTHEUS_TIPO> query;

            //int[] iExcesoes = new int[] { 20, 26, 27, 28, 29, 31 };

            query = from u in m_DbContext.PRE_TBL_CARGA_PROTHEUS_TIPO
                    where u.AREA == area
                    orderby u.DCR_CARGA_TIPO
                    select u;

            return query.ToList();
        }

        public PRE_TBL_CARGA_PROTHEUS_TIPO GetCargaProtheusTabelaTipo(int CodTipo)
        {
            IQueryable<PRE_TBL_CARGA_PROTHEUS_TIPO> query;

            query = from u in m_DbContext.PRE_TBL_CARGA_PROTHEUS_TIPO
                    where u.COD_CARGA_TIPO == CodTipo
                    select u;

            return query.FirstOrDefault();
        }

        public PRE_TBL_CARGA_PROTHEUS GetCargaProtheusTabelaCarga(int CodTipo)
        {
            IQueryable<PRE_TBL_CARGA_PROTHEUS> query;

            query = from u in m_DbContext.PRE_TBL_CARGA_PROTHEUS
                    where u.COD_CARGA_TIPO == CodTipo
                    select u;

            return query.FirstOrDefault();

        }
        public DataTable GetGridProcesso(string area)
        {
            DataTable dt = new DataTable();

            try
            {
                //OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("select distinct p.dcr_parametros, p.dth_pagamento DATA_PAGAMENTO, p.log_inclusao, p.dth_inclusao,med.status  from own_intprotheus.medctr med,own_funcesp.pre_tbl_carga_protheus p where med.num_lote = p.num_lote and med.status in ('8','1','9') and p.log_inclusao ='" + login + "' order by p.dth_inclusao desc");

                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("select distinct p.dcr_parametros, p.dth_pagamento DATA_PAGAMENTO, p.log_inclusao, p.dth_inclusao, med.status from own_intprotheus.medctr med,own_funcesp.pre_tbl_carga_protheus p,own_funcesp.pre_tbl_carga_protheus_tipo tp where med.num_lote = p.num_lote and med.status in ('8', '1', '9') and tp.cod_carga_tipo = p.cod_carga_tipo and tp.area = '" + area + "' order by p.dth_inclusao desc");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }


        public void ValidaLote(int num_lote)
        {
            ConexaoOracle obj = new ConexaoOracle();
            Resultado res = new Resultado();
            try
            {
                obj.ExecutarNonQuery("update own_intprotheus.medctr med set med.status = 1 where med.num_lote = " + num_lote + "");
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);

            }
        }

        public DataTable GetResumoLiquidez(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_rel_resumo_liquidez");

                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }

        public DataTable GetResumoPatrocinador(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_rel_resumo_patrocinador");

                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }

        public DataTable GetResumoPrograma(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_rel_resumo_programa");

                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }

        public DataTable GetMedctrAberta(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_rel_medctr_aberta");

                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }

        public DataTable GetLiquidezPorTipo(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_rel_resumo_liquidez_tipo");

                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }

        public DataTable GetResumo(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_rel_resumo");

                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }

        public DataTable GetResumoRedeCred(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_rel_resu_rede_credenciada");

                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }

        public DataTable GetLiquidezRedeCred(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_rel_liqui_rede_credenciada");

                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }

        public DataTable GetPatrocinadorRedeCred(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_rel_patr_rede_credenciada");

                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }

        public DataTable GetProdutoRedeCred(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_rel_prod_rede_credenciada");

                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            return dt;
        }

        public DataTable GetNumeroLoteTipo(string login, DateTime dt_inclusao)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();
            try
            {
                obj.AdicionarParametro("login", login);
                obj.AdicionarParametro("dt_inclusao", dt_inclusao);
                obj.AdicionarParametroCursor("DADOS");
                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_retorna_num_lote");


                // objConexao.ExecutarNonQuery("own_intprotheus.pkg_medctr_rel_auto.prc_retorna_num_lote");
                adpt.Fill(dt);
                adpt.Dispose();



            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            obj.Dispose();
            return dt;
        }
        public bool ValidaMedctr(int num_lote)
        {

            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            bool resposta;
            try
            {
                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select * from own_intprotheus.medctr med where med.num_lote = " + num_lote + "");
                adpt.Fill(dt);
                if (dt.Rows.Count < 1)
                {
                    resposta = false;
                }
                else
                {
                    resposta = true;
                }
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return resposta;
        }

        public void ExcluiLote(int num_lote)
        {
            try
            {
                objConexao.AdicionarParametro("v_num_lote", num_lote);

                //  objConexao.AdicionarParametroOut("v_num_lote");

                bool retorno = objConexao.ExecutarNonQuery("own_intprotheus.PKG_CARGA_PROTHEUS.STP_EXCLUI_LOTE");
                //adpt.Fill(dt);
                //adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            //return dt;
        }

        public void GetProcessosGerados(int tipo_carga, DateTime data_pagto, DateTime data_inclusao)
        {
            DataTable dt = new DataTable();
            try
            {
                objConexao.AdicionarParametro("v_tp_proc", tipo_carga);
                objConexao.AdicionarParametro("v_dat_pagto", data_pagto);
                objConexao.AdicionarParametro("v_dat_incl", data_inclusao);
                //  objConexao.AdicionarParametroOut("v_num_lote");

                bool retorno = objConexao.ExecutarNonQuery("OWN_INTPROTHEUS.PRE_PRC_CARGA_PROTHEUS_AUTO");
                //adpt.Fill(dt);
                //adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            //return dt;
        }

        public Resultado SaveFila(PRE_TBL_CARGA_PROTHEUS newCargaProtheus)
        {
            Resultado res = new Resultado();
            try
            {
                m_DbContext.PRE_TBL_CARGA_PROTHEUS.Add(newCargaProtheus);

                // Inseri um registro novo ativo (DTH_EXCLUSAO=null)
                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated > 0)
                {
                    res.Sucesso("Registro atualizado com sucesso.");
                }

            }
            catch (Exception ex)
            {
                res.Erro(ex.Message);
            }
            return res;

        }

        public Resultado SaveTxt(REEMB_FRMCIA newCargaProtheus)
        {
            Resultado res = new Resultado();
            try
            {
                m_DbContext.REEMB_FRMCIA.Add(newCargaProtheus);

                // Inseri um registro novo ativo (DTH_EXCLUSAO=null)
                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated > 0)
                {
                    res.Sucesso("Registro atualizado com sucesso.");
                }

            }
            catch (Exception ex)
            {
                res.Erro(ex.Message);
            }
            return res;

        }

        public decimal GetMaxPk()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.REEMB_FRMCIA.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }



    }
}
