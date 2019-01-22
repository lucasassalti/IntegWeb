using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{

    public class CadProtheusDal
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();
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

        public string GetEmpresasRepasse2()
        {

            DataTable dt = new DataTable();

            ConexaoOracle obj = new ConexaoOracle();
            string result = ""; 
            try
            {

                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select COD_EMPRS,NOM_ABRVO_EMPRS  from att.empresa where cod_emprs in (1, 4, 40, 41, 42, 43, 44, 45, 50, 88, 2, 62, 66, 71, 79, 80, 84, 87, 89, 91, 92, 97, 98, 99, 100, 41, 4819)");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                result = ex.Message; 
                //throw new Exception("Erro select das empresas: //n" + ex.Message);
            }

            return result; 
        }
        public List<EMPRESA> GetEmpresasRepasse()
        {
            IQueryable<EMPRESA> query;

            int[] empresas = new int[] { 1, 4, 40, 41, 42, 43, 44, 45, 50, 88, 2, 62, 66, 71, 79, 80, 84, 87, 89, 91, 92, 97, 98, 99, 100, 41, 4819 };

            query = from i in m_DbContext.EMPRESA
                    where empresas.Contains(i.COD_EMPRS)
                    select i;
            query = query.OrderBy(x => x.NOM_ABRVO_EMPRS);

            //DataTable dt = new DataTable();

            //ConexaoOracle obj = new ConexaoOracle();

            //try
            //{

            //    OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select COD_EMPRS,NOM_ABRVO_EMPRS  from att.empresa where cod_emprs in (1, 4, 40, 41, 42, 43, 44, 45, 50, 88, 2, 62, 66, 71, 79, 80, 84, 87, 89, 91, 92, 97, 98, 99, 100, 41, 4819)");
            //    adpt.Fill(dt);
            //    adpt.Dispose();
            //}
            //catch(Exception ex)
            //{
            //    throw new Exception("Erro select das empresas: //n" + ex.Message);
            //}

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

        public DataTable GetGridProcesso(string login)
        {
            DataTable dt = new DataTable();

            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                OracleDataAdapter adpt = obj.ExecutarQueryAdapter("select distinct med.num_lote, p.dcr_parametros, p.dth_pagamento DATA_PAGAMENTO, p.log_inclusao, p.dth_inclusao,med.status  from own_intprotheus.medctr med,own_funcesp.pre_tbl_carga_protheus p where med.num_lote = p.num_lote and med.status in ('8','1','9') and p.log_inclusao ='" + login + "' order by p.dth_inclusao desc");
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

        public DataTable GetRejeitada(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_verifica_rejeitada");
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

            obj.Dispose();
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

            obj.Dispose();

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

        public DataTable GetProdutoCredenciada(int num_lote)
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

        public DataTable GetPatrocinadorCredenciada(int num_lote)
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

        public DataTable GetLiquidezCredenciada(int num_lote)
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

        public DataTable GetRepasse(int num_lote)
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();

            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_gera_repasse");

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

                bool retorno = objConexao.ExecutarNonQuery("own_intprotheus.PKG_PAGTO_SAUDE.STP_EXCLUI_LOTE");
                //adpt.Fill(dt);
                //adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            //return dt;
        }

        public void InsereEmpresasRepasse(string empresas)
        {
            string[] emp = empresas.Split(',');

            bool retorno = false;

            try
            {
                ConexaoOracle obj = new ConexaoOracle();

                for (int i = 0; i < emp.Length; i++)
                {
                    if (emp[i] != "")
                    {
                        retorno = obj.ExecutarNonQuery("insert into own_intprotheus.empresas_repasse (COD_EMPRS) values (" + Convert.ToInt32(emp[i]) + ")");
                    }                                     
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
        }

        public void DeletaEmpresasRepasse()
        {
            bool retorno = false;

            try
            {
                ConexaoOracle obj = new ConexaoOracle();

                retorno = obj.ExecutarNonQuery("delete from own_intprotheus.empresas_repasse");
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
        }


        public void GetProcessosGerados(int tipo_carga, DateTime data_pagto, DateTime data_inclusao, int mes_ref, int ano_ref,int cod_emprs)
        {
            DataTable dt = new DataTable();
            try
            {
                objConexao.AdicionarParametro("v_tp_proc", tipo_carga);
                objConexao.AdicionarParametro("v_dat_pagto", data_pagto);
                objConexao.AdicionarParametro("v_dat_incl", data_inclusao);
                objConexao.AdicionarParametro("v_mes_ref", mes_ref);
                objConexao.AdicionarParametro("v_ano_ref", ano_ref);
                objConexao.AdicionarParametro("v_cod_emprs",cod_emprs); 


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

        //Copia com assinatura diferente.
        public void GetProcessosGerados(int tipo_carga, DateTime data_pagto, DateTime data_inclusao, int mes_ref, int ano_ref)
        {
            DataTable dt = new DataTable();
            try
            {
                objConexao.AdicionarParametro("v_tp_proc", tipo_carga);
                objConexao.AdicionarParametro("v_dat_pagto", data_pagto);
                objConexao.AdicionarParametro("v_dat_incl", data_inclusao);
                objConexao.AdicionarParametro("v_mes_ref", mes_ref);
                objConexao.AdicionarParametro("v_ano_ref", ano_ref);
            //    objConexao.AdicionarParametro("v_cod_emprs", cod_emprs);


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

        public void AtualizaClifor()
        {
            ConexaoOracle obj = new ConexaoOracle();
            try
            {
                obj.ExecutarNonQuery("own_intprotheus.pkg_medctr_rel_auto.prc_atualiza_clifor");
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

        }



        public DataTable GetDadosClifor(int cod_empr, int num_rgtro)
        {

            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();
            try
            {
                obj.AdicionarParametro("v_cod_emprs", cod_empr);
                obj.AdicionarParametro("v_num_rgtro_emprg", num_rgtro);
                obj.AdicionarParametroCursor("DADOS");
                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_gera_clifor_info");



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

        public void ExecutaProcProtheus()
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                //DataTable dt;

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("own_intprotheus.pre_prc_carga_protheus");

                // adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

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

        public bool InsereProtheus(int num_lote, string xnumct, string xptliq, string dtref, string dtvenc, string produt, string ccusto, string nossonumero, string programa, string patrocinador,
                                        string submassa, string projeto, decimal valmed, string tipopar, int? cod_assoc, int? cod_convenente, int cod_emprs, int num_rgtro_emprg, int? num_matr_partf, int? num_idntf_rptant, int? num_idntf_dpdte,
                                        string banco, string agencia, string dvage, string numcon, string dvnumcon, string evento)
        {
            ConexaoOracle obj = new ConexaoOracle();
            bool retorno = false;

            if (ccusto == "")
            {
                ccusto = " ";
            }

            if (produt == "")
            {
                produt = " ";
            }

            if (nossonumero == "")
            {
                nossonumero = " ";
            }

            if (cod_assoc == null)
            {
                cod_assoc = 0;
            }

            if (cod_convenente == null)
            {
                cod_convenente = 0;
            }

            if (num_idntf_rptant == null)
            {
                num_idntf_rptant = 0;
            }

            if (num_idntf_dpdte == null)
            {
                num_idntf_dpdte = 0;
            }


            try
            {
                obj.AdicionarParametro("v_num_lote", num_lote);
                obj.AdicionarParametro("v_xnumct", xnumct);
                obj.AdicionarParametro("v_xptliq", xptliq);
                obj.AdicionarParametro("v_dtref", dtref);
                obj.AdicionarParametro("v_dtvenc", dtvenc);
                obj.AdicionarParametro("v_produt", produt);
                obj.AdicionarParametro("v_ccusto", ccusto);
                obj.AdicionarParametro("v_nossonumero", nossonumero);
                obj.AdicionarParametro("v_programa", programa);
                obj.AdicionarParametro("v_patrocinador", patrocinador);
                obj.AdicionarParametro("v_submassa", submassa);
                obj.AdicionarParametro("v_projeto", projeto);
                obj.AdicionarParametro("v_valmed", valmed);
                obj.AdicionarParametro("v_tipopar", tipopar);
                obj.AdicionarParametro("v_cod_assoc", cod_assoc);
                obj.AdicionarParametro("v_cod_convenente", cod_convenente);
                obj.AdicionarParametro("v_cod_emprs", cod_emprs);
                obj.AdicionarParametro("v_num_rgtro_emprg", num_rgtro_emprg);
                obj.AdicionarParametro("v_num_matr_partf", num_matr_partf);
                obj.AdicionarParametro("v_num_idntf_rptant", num_idntf_rptant);
                obj.AdicionarParametro("v_num_idntf_dpdte", num_idntf_dpdte);
                obj.AdicionarParametro("v_banco", banco);
                obj.AdicionarParametro("v_agencia", agencia);
                obj.AdicionarParametro("v_dvage", dvage);
                obj.AdicionarParametro("v_numcon", numcon);
                obj.AdicionarParametro("v_dvnumcon", dvnumcon);
                obj.AdicionarParametro("v_evento", evento);

                retorno = obj.ExecutarNonQuery("own_intprotheus.pkg_medctr_rel_auto.prc_insere_lote");
                //adpt.Fill(dt);
                //adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            finally
            {
                obj.Dispose();
            }

            return retorno;
        }

        public bool InsereReembFarm(DateTime dt_incl, string login, int cod_emprs, int num_rgtro, int dig, int num_matr, int dig_matr, string nome, double vlr_medcto, double vlr_reemb, int protocolo, DateTime dt_arq, DateTime dt_cred)
        {
            ConexaoOracle obj = new ConexaoOracle();
            bool retorno = false;

            try
            {
                obj.AdicionarParametro("v_datahora", dt_incl);
                obj.AdicionarParametro("v_login", login);
                obj.AdicionarParametro("v_cod_emprs", cod_emprs);
                obj.AdicionarParametro("v_num_rgtro_emprg", num_rgtro);
                obj.AdicionarParametro("v_digito", dig);
                obj.AdicionarParametro("v_num_matr_sub", num_matr);
                obj.AdicionarParametro("v_dig_num_matr", dig_matr);
                obj.AdicionarParametro("v_nome", nome);
                obj.AdicionarParametro("v_vlr_medcto", vlr_medcto);
                obj.AdicionarParametro("v_vlr_reemb", vlr_reemb);
                obj.AdicionarParametro("v_protocolo", protocolo);
                obj.AdicionarParametro("v_dt_arq", dt_arq);
                obj.AdicionarParametro("v_dt_cred", dt_cred);

                retorno = obj.ExecutarNonQuery("own_intprotheus.pkg_medctr_rel_auto.prc_reembolso_farm");


            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            finally
            {
                obj.Dispose();
            }
            return retorno;
        }

        public bool InsereMedLiq(int num_lote)
        {
            ConexaoOracle obj = new ConexaoOracle();
            bool retorno = false;

            try
            {
                obj.AdicionarParametro("v_lote", num_lote);

                retorno = obj.ExecutarNonQuery("own_intprotheus.pkg_medctr_rel_auto.prc_insere_medliq");
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }

            return retorno;
        }

        public string retornaMaxLote()
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

            return dt.Rows[0][0].ToString();
        }

        public DataTable GeraErroClifor()
        {
            DataTable dt = new DataTable();
            ConexaoOracle obj = new ConexaoOracle();
            try
            {
                obj.AdicionarParametroCursor("DADOS");
                OracleDataAdapter adpt = obj.ExecutarAdapter("own_intprotheus.pkg_medctr_rel_auto.prc_retorna_erro_clifor");

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
    }
}
