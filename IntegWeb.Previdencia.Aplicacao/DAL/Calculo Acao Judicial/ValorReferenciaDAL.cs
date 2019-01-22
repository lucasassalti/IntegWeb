using IntegWeb.Entidades;
using IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial
{
    public class ValorReferenciaDAL : ValorReferencia
    {
        protected Resultado ImportaVr(bool ATLZ_IGPDI = false, bool ATLZ_TRAB = false, bool ATLZ_CIV = false)
        {
            Resultado retorno = new Resultado();
            using (ConexaoOracle objConexao = new ConexaoOracle())
            {
                try
                {
                    objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                    objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_sqncl_prc);
                    objConexao.AdicionarParametro("V_ATLZ_IGPDI", (ATLZ_IGPDI) ? "S" : "N");
                    objConexao.AdicionarParametro("V_ATLZ_TRAB", (ATLZ_TRAB) ? "S" : "N");
                    objConexao.AdicionarParametro("V_ATLZ_CIV", (ATLZ_CIV) ? "S" : "N");
                    objConexao.AdicionarParametroOut("V_RETURN");
                    objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_IMPORTAR_VR");
                    int id = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());

                    retorno.Sucesso("Registro importado com sucesso!");

                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao importado o registro: " + erro.Message);
                }
            }

            return retorno;

        }

        protected bool VerificarDadosVr()
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_sqncl_prc);

                objConexao.AdicionarParametroOut("V_RETURN");
                objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_VERIFICAD_DADOS_VR");
               int id = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());

                return id == 0;



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }



        }

        protected Resultado DesfazerImportacaoVr()
        {
            Resultado retorno = new Resultado();
            using (ConexaoOracle objConexao = new ConexaoOracle())
            {
                try
                {

                    objConexao.AdicionarParametro("P_NUM_MATR_PARTF", num_matr_partf);
                    objConexao.AdicionarParametro("P_NUM_SQNCL_PRC", num_sqncl_prc);
                    objConexao.AdicionarParametro("P_NUM_PROC", num_proc);
                    objConexao.AdicionarParametro("P_COD_TIP_ATLZ", cod_tip_atlz);
                    objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_DELETAR_VR");

                    retorno.Sucesso("Registro excluído com sucesso!");

                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao excluir o registro: " + erro.Message);
                }
            }

            return retorno;
            
        } 

        protected DataTable ListarProcesso()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("v_num_regtro", num_matr_partf);
                objConexao.AdicionarParametro("v_cod_emprs", cod_emprs);
                objConexao.AdicionarParametroCursor("dados");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_PROCESSO");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;



        }

        protected void Tratar_filtro(int filType, string filValue)
        {
            if (filValue != null)
            {
                switch (filType)
                {
                    case 1:
                        cod_emprs = int.Parse(filValue);
                        break;
                    case 2:
                        num_matr_partf = int.Parse(filValue);
                        break;
                    case 3:
                        nome_emprg = filValue.Trim().ToUpper();
                        break;
                    case 4:
                        cpf_emprg = int.Parse(filValue);
                        break;
                    case 5:
                        num_proc = filValue.Trim();
                        break;
                }
            }
        }

        protected DataTable ListarProcessosVr(int filType, string filValue, int? codEmpresa, int? codMatricula)
        {
            cod_emprs = codEmpresa;
            num_rgtro_emprg = codMatricula;
            if (filValue!=null) Tratar_filtro(filType, filValue);
            return ListarProcessosVr();
        }

        protected DataTable ListarProcessosVr()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_sqncl_prc);
                objConexao.AdicionarParametro("V_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_NUM_RGTRO_EMPRG", num_rgtro_emprg);
                objConexao.AdicionarParametro("V_NOME_EMPRG", nome_emprg);
                objConexao.AdicionarParametro("V_CPF_EMPRG", cpf_emprg);
                objConexao.AdicionarParametro("V_NUM_PROC", num_proc);
                objConexao.AdicionarParametro("V_COD_TIP_ATLZ", cod_tip_atlz);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_PROCESSO_VR");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected DataTable CarregaProcessoVr()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_sqncl_prc);
                //objConexao.AdicionarParametro("V_NUM_RGTRO_EMPRG", num_rgtro_emprg);
                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_NUM_PROC", num_proc);
                objConexao.AdicionarParametro("V_COD_TIP_ATLZ", cod_tip_atlz);

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_CARREGA_PROCESSO_VR");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected Resultado AtualizarProcessoVr()
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle objConexao = new ConexaoOracle())
            {
                try
                {
                    objConexao.AdicionarParametro("P_NUM_MATR_PARTF", num_matr_partf);
                    objConexao.AdicionarParametro("P_NUM_SQNCL_PRC", num_sqncl_prc);
                    objConexao.AdicionarParametro("P_NUM_PROC", num_proc);
                    //objConexao.AdicionarParametro("P_ID_ACAO_PROCESSO", id_acao_processo);
                    objConexao.AdicionarParametro("P_COD_TIP_ATLZ", cod_tip_atlz);
                    objConexao.AdicionarListaParametros(AdicionarParametrosVr());                    
                    objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_ATUALIZAR_VR");
                    retorno.Sucesso("Registro alterado com sucesso!");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao atualizar o registro: " + erro.Message);
                }
            }
            return retorno;
        }

        protected Resultado InserirProcessoVr()
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle objConexao = new ConexaoOracle())
            {
                try
                {
                    objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                    objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_sqncl_prc);
                    objConexao.AdicionarParametro("V_NUM_PROC", num_proc);
                    //objConexao.AdicionarParametro("V_ID_ACAO_PROCESSO", id_acao_processo);
                    objConexao.AdicionarParametro("V_COD_TIP_ATLZ", cod_tip_atlz);
                    objConexao.AdicionarListaParametros(AdicionarParametrosVr());
                    objConexao.AdicionarParametro("V_RESPONSAVEL", responsavel);
                    objConexao.ExecutarNonQuery("PRE_PKG_ACAO_JUDIC_VR.PRE_PRC_INSERIR_VR");
                    retorno.Sucesso("Registro inserido com sucesso!");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir o registro: " + erro.Message);
                }
            }
            return retorno;
        }

        private Dictionary<string, object> AdicionarParametrosVr()
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            ret.Add("V_NUM_RGTRO_EMPRG", num_rgtro_emprg);
            ret.Add("V_NUM_PASTA", num_pasta);
            ret.Add("V_DAT_PRESCR", dat_prescr);
            ret.Add("V_POLO_ACJUD", polo_acjud);
            ret.Add("V_COD_HSTPLTO", cod_hstplto);
            ret.Add("V_COD_VARA_PROC", cod_vara_proc);
            ret.Add("V_COD_TIPLTO", cod_tiplto);
            ret.Add("V_COD_EMPRS", cod_emprs);
            ret.Add("V_NOME_EMPRG", nome_emprg);
            ret.Add("V_CPF_EMPRG", cpf_emprg);
            ret.Add("V_DATA_ADMISSAO", data_admissao);
            ret.Add("V_DATA_DEMISSAO", data_demissao);
            ret.Add("V_DATA_NASCTO", data_nascto);
            ret.Add("V_DATA_ADESAO", data_adesao);
            ret.Add("V_DIB", dib);
            ret.Add("V_PLANO", plano);
            ret.Add("V_PERFIL", perfil);
            //ret.Add("V_DESC_PROCESSO", desc_processo);
            ret.Add("V_DTA_STATUS", dta_status);
            ret.Add("V_BSPS_DIB_SPLTO", bsps_dib_splto);
            ret.Add("V_BD_DIB_SPLTO", bd_dib_splto);
            ret.Add("V_CV_DIB_SPLTO", cv_dib_splto);
            ret.Add("V_BSPS_ATU_SPLTO", bsps_atu_splto);
            ret.Add("V_BD_ATU_SPLTO", bd_atu_splto);
            ret.Add("V_CV_ATU_SPLTO", cv_atu_splto);
            ret.Add("V_BSPS_DIB_CPLTO", bsps_dib_cplto);
            ret.Add("V_BD_DIB_CPLTO", bd_dib_cplto);
            ret.Add("V_CV_DIB_CPLTO", cv_dib_cplto);
            ret.Add("V_BSPS_ATU_CPLTO", bsps_atu_cplto);
            ret.Add("V_BD_ATU_CPLTO", bd_atu_cplto);
            ret.Add("V_CV_ATU_CPLTO", cv_atu_cplto);
            ret.Add("V_CNTR_PART_AT_BSPS", cntr_part_at_bsps);
            ret.Add("V_BNF_PART_RET_BSPS", bnf_part_ret_bsps);
            ret.Add("V_CNTR_PART_RET_BSPS", cntr_part_ret_bsps);
            ret.Add("V_RESMAT_PART_BSPS", resmat_part_bsps);
            ret.Add("V_RESMAT_ANT_PART_BSPS", resmat_ant_part_bsps);
            ret.Add("V_CNTR_PART_AT_BD", cntr_part_at_bd);
            ret.Add("V_BNF_PART_RET_BD", bnf_part_ret_bd);
            ret.Add("V_CNTR_PART_RET_BD", cntr_part_ret_bd);
            ret.Add("V_RESMAT_PART_BD", resmat_part_bd);
            ret.Add("V_CNTR_PART_AT_CV", cntr_part_at_cv);
            ret.Add("V_BNF_PART_RET_CV", bnf_part_ret_cv);
            ret.Add("V_PRC_PART_RESMAT_BSPS", prc_part_resmat_bsps);
            ret.Add("V_PRC_PART_RESMAT_BD", prc_part_resmat_bd);
            ret.Add("V_CNTR_PATR_AT_BSPS", cntr_patr_at_bsps);
            ret.Add("V_BNF_PATR_RET_BSPS", bnf_patr_ret_bsps);
            ret.Add("V_RESMAT_PATR_BSPS", resmat_patr_bsps);
            ret.Add("V_RESMAT_ANT_PATR_BSPS", resmat_ant_patr_bsps);
            ret.Add("V_CNTR_PATR_AT_BD", cntr_patr_at_bd);
            ret.Add("V_BNF_PATR_RET_BD", bnf_patr_ret_bd);
            ret.Add("V_RESMAT_PATR_BD", resmat_patr_bd);
            ret.Add("V_CNTR_PATR_AT_CV", cntr_patr_at_cv);
            ret.Add("V_BNF_PATR_RET_CV", bnf_patr_ret_cv);
            ret.Add("V_PRC_PATR_RESMAT_BSPS", prc_patr_resmat_bsps);
            ret.Add("V_PRC_PATR_RESMAT_BD", prc_patr_resmat_bd);
            ret.Add("V_NOTA", nota);
            ret.Add("V_OBS", obs);
            ret.Add("V_PASTA", pasta);
            ret.Add("V_DTA_RETR_ATLZ", dta_retr_atlz);
            ret.Add("V_COD_SITUACAO", cod_situacao);
            return ret;
        }

        protected DataTable ListarTipoAtualizacao()
        {
            return Listar("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_VR_TIP_ATLZ", "p_cod_tip_atlz", null);
        }

        protected DataTable ListarAssunto()
        {
            return Listar("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_VR_TIPLTO", "p_cod_tiplto", null);
        }

        protected DataTable ListarHistorico()
        {
            return Listar("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_VR_HSTPLTO", "p_cod_hstplto", null);
        }

        protected DataTable Listar(string pkg_name, string _param_name, object _param_value)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro(_param_name, _param_value);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter(pkg_name);

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;

        }

        protected DataTable ListarProcessosImportVr(int filType, string filValue, int? codEmpresa, int? codMatricula)
        {
            cod_emprs = codEmpresa;
            num_rgtro_emprg = codMatricula;
            if (filValue != null) Tratar_filtro(filType, filValue);
            return ListarProcessosImportVr();
        }

        protected DataTable ListarProcessosImportVr()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("V_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("V_NUM_RGTRO_EMPRG", num_rgtro_emprg);
                objConexao.AdicionarParametro("V_NOME_EMPRG", nome_emprg);
                objConexao.AdicionarParametro("V_CPF_EMPRG", cpf_emprg);
                objConexao.AdicionarParametro("V_NUM_PROC", num_proc);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_PROCESSOS_VR");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;



        }

        protected DataTable CarregarDadosParticipante()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG", num_rgtro_emprg);
                //objConexao.AdicionarParametro("P_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_ACAO_JUDIC_VR.PRC_CARREGAR_DADOS_PARTICIP");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected DataTable ListarCustoJudicial()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                //objConexao.AdicionarParametro("P_COD_EMPRS", cod_emprs);
                //objConexao.AdicionarParametro("P_NUM_RGTRO_EMPRG", num_rgtro_emprg);
                //objConexao.AdicionarParametro("P_NUM_MATR_PARTF", null);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_CUSTO_JUDICIAL");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        protected DataTable CarregaUnidadeMonetaria()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_UNID_MONETARIA");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        //protected DataTable CarregaCotacao(short p_cod_um)
        //{
        //    DataTable dt = new DataTable();
        //    ConexaoOracle objConexao = new ConexaoOracle();
        //    try
        //    {
        //        objConexao.AdicionarParametro("P_COD_UM", p_cod_um);
        //        objConexao.AdicionarParametroCursor("DADOS");

        //        OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_COTACAO");

        //        adpt.Fill(dt);
        //        adpt.Dispose();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        objConexao.Dispose();
        //    }
        //    return dt;
        //}

        protected Resultado GerarCustoJudicial(DateTime p_DAT_INI,
                                               DateTime p_DAT_FIN,
                                               Double   p_LIM_DSP,
                                               string   p_LIM_FLG,
                                               short    p_COD_UM,
                                               DateTime p_DAT_ATLZ)
        {
            Resultado retorno = new Resultado();
            using (ConexaoOracle objConexao = new ConexaoOracle())
            {
                try
                {
                    objConexao.AdicionarParametro("V_DAT_INI", p_DAT_INI);
                    objConexao.AdicionarParametro("V_DAT_FIN", p_DAT_FIN);
                    objConexao.AdicionarParametro("V_LIM_DSP", p_LIM_DSP);
                    objConexao.AdicionarParametro("V_LIM_FLG", p_LIM_FLG);
                    objConexao.AdicionarParametro("V_COD_UM", p_COD_UM);
                    objConexao.AdicionarParametro("V_DAT_ATLZ", p_DAT_ATLZ);
                    objConexao.ExecutarNonQuery("PRE_PKG_ACAO_JUDIC_VR.PRC_CORRIGE_VLRS");
                    retorno.Sucesso("Calculo de Custo Judicial gerado com com sucesso!");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir o registro: " + erro.Message);
                }
            }
            return retorno;
        }

        internal Resultado DeletarCustoJudicial(DateTime p_HDRDATHOR)
        {
            Resultado retorno = new Resultado();
            using (ConexaoOracle objConexao = new ConexaoOracle())
            {
                try
                {
                    objConexao.AdicionarParametro("P_HDRDATHOR", p_HDRDATHOR);
                    objConexao.ExecutarNonQuery("PRE_PKG_ACAO_JUDIC_SITE.PRE_PRC_DELETAR_CUSTO_JUDICIAL");
                    retorno.Sucesso("Calculo de Custo Judicial excluído com com sucesso!");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir o registro: " + erro.Message);
                }
            }
            return retorno;
        }
    }
}
