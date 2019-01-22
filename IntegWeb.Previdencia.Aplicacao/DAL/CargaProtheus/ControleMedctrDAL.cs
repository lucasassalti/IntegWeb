using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IntegWeb.Previdencia.Aplicacao.BLL.CargaProtheus;
using IntegWeb.Framework;
using System.Data.OracleClient;
using IntegWeb.Entidades;
using IntegWeb.Previdencia.Aplicacao.ENTITY;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class ControleMedctrDAL
    {
        ConexaoOracle objConexao = new ConexaoOracle();
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public DataTable GetGridLote(int num_lote)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("select distinct med.num_lote, med.dtvenc, med.tp_proc, med.status from own_intprotheus.medctr med where med.num_lote = " + num_lote);
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return dt;
        }

        public DataTable GetGridGeral()
        {
            DataTable dt = new DataTable();
            try
            {
                //OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("select distinct med.num_lote, med.tp_proc, p.dcr_parametros, Trunc(p.dth_pagamento) dt_pagto, med.status from own_intprotheus.medctr med, own_funcesp.pre_tbl_carga_protheus p where med.num_lote = p.num_lote and med.status in (1,8,3) order by med.num_lote desc");
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("select distinct med.num_lote, med.tp_proc, p.dcr_parametros, med.dtvenc dt_pagto, med.status from own_intprotheus.medctr med, own_funcesp.pre_tbl_carga_protheus p where med.tp_proc = p.cod_carga_tipo and med.status in (1,8,3,4) order by med.num_lote desc");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return dt;
        }

        public DataTable GetTipoMedDdl()
        {
            DataTable dt = new DataTable();
            try
            {
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("select dcr_carga_tipo, cod_carga_tipo from own_funcesp.pre_tbl_carga_protheus_tipo order by dcr_carga_tipo asc");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return dt;
        }

        public DataTable GetPesqLotes(int? num_lote, string dtvenc, int? tpproc)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try
            {

                obj.AdicionarParametro("v_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");
                obj.AdicionarParametro("v_dtvenc", dtvenc);
                obj.AdicionarParametro("v_tpproc", tpproc);


                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_pesquisa_lotes_controle");
                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas na pesquisa dos lotes, favor entrar em contato com o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                obj.Dispose();
            }

            return dt;
        }

        public bool AlteraLoteMedctr(string dtvenc, string status, int num_lote, string statusant, string dataant)
        {
            bool retorno = false;

            try
            {
                retorno = objConexao.ExecutarNonQuery("update own_intprotheus.medctr med set med.dtvenc = '" + dtvenc + "', med.status = '" + status + "' where med.num_lote = " + num_lote + " and med.status = '" + statusant + "' and med.dtvenc = '" + dataant + "'");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return retorno;
        }

        public bool AlteraLoteMedctrLiq(string dtvenc, int num_lote, string dataant)
        {
            bool retorno = false;

            try
            {
                retorno = objConexao.ExecutarNonQuery("update own_intprotheus.medctr_liq med set med.dtvenc = '" + dtvenc + "' where med.num_lote = " + num_lote + "and med.dtvenc = '" + dataant + "'");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return retorno;
        }

        public bool AlteraLoteHistorico(string dtvenc, int num_lote)
        {
            bool retorno = false;

            try
            {
                retorno = objConexao.ExecutarNonQuery("update own_funcesp.pre_tbl_carga_protheus p set p.dth_pagamento = to_date('" + dtvenc + "','dd/MM/yyyy') where p.num_lote = " + num_lote);

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return retorno;
        }

        public bool AlteraLoteTmp(string dtvenc, int num_lote, string dataant)
        {
            bool retorno = false;

            try
            {
                retorno = objConexao.ExecutarNonQuery("update own_intprotheus.medctr_tmp med set med.dtvenc = '" + dtvenc + "' where med.num_lote = " + num_lote + "and med.dtvenc = '" + dataant + "'");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return retorno;
        }

        public bool AlteraLoteDet(string dtvenc, int num_lote, string dataant)
        {
            bool retorno = false;

            try
            {
                retorno = objConexao.ExecutarNonQuery("update own_intprotheus.medctr_det med set med.dat_pagamento = '" + dtvenc + "' where med.num_lote = " + num_lote + "and med.dat_pagamento = '" + dataant + "'");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return retorno;
        }

        public bool ExcluiLote(int num_lote)
        {
            bool retorno = false;
            try
            {
                objConexao.AdicionarParametro("v_num_lote", num_lote);



                retorno = objConexao.ExecutarNonQuery("own_intprotheus.pkg_pagto_saude.STP_EXCLUI_LOTE");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return retorno;
        }

        public bool DuplicaLote(int num_lote, string dt_venc, int num_lote_novo)
        {
            bool retorno = false;
            try
            {
                objConexao.AdicionarParametro("v_lote", num_lote);
                objConexao.AdicionarParametro("v_numlote_novo", num_lote_novo);
                objConexao.AdicionarParametro("v_dtvenc", dt_venc);


                retorno = objConexao.ExecutarNonQuery("own_intprotheus.pkg_medctr_rel_auto.prc_duplica_lote");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return retorno;
        }

        public DataTable GetInfoLotePreTbl(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();
            try
            {
                obj.AdicionarParametro("v_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");


                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_gera_info_pretbl");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return dt;
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

        public int retornaMaxLote()
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try
            {
                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select own_intprotheus.lote_medctr.nextval from dual");
                adpt.Fill(dt);

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }

        public bool insereHistorico(int num_lote, string desc_alter, int status_a, int status_n, DateTime dt_a, DateTime dt_n, string log)
        {
            bool retorno = false;
            try
            {
                objConexao.AdicionarParametro("v_lote", num_lote);
                objConexao.AdicionarParametro("DESC_ALTER", desc_alter);
                objConexao.AdicionarParametro("STATUS_A", status_a);
                objConexao.AdicionarParametro("STATUS_N", status_n);
                objConexao.AdicionarParametro("DT_A", dt_a);
                objConexao.AdicionarParametro("DT_N", dt_n);
                objConexao.AdicionarParametro("LOG_INCLUSAO", log);



                retorno = objConexao.ExecutarNonQuery("own_intprotheus.pkg_medctr_rel_auto.prc_insere_historico");

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return retorno;
        }

    }
}
