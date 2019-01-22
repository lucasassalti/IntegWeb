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
    public  class AcaoJudicialDAL: AcaoJudicial
    {

        protected bool ProcessaSrc(out string mensagem, out int id)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_FLAG_ABN", flag_abn);
                objConexao.AdicionarParametro("V_NRO_PASTA", num_pasta);
                objConexao.AdicionarParametro("V_NRO_PROCESSO", num_processo);

                objConexao.AdicionarParametro("V_COD_VARA_PROC", cod_vara);
                objConexao.AdicionarParametro("V_POLO_ACJUD", flag_acao_Jud);
                objConexao.AdicionarParametro("V_COD_TIPLTO", cod_tiplto);
                objConexao.AdicionarParametro("V_RESPONSAVEL", responsavel);

                objConexao.AdicionarParametro("V_OBS", obs_src);

                objConexao.AdicionarParametroOut("V_NUM_SQNCL_PRC");
                mensagem = "Processado com sucesso";
                objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_PROCESSA_SRC");
                id = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
                return id > 0;



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

        protected bool ProcessaCtr(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_RESPONSAVEL", responsavel);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);

                objConexao.AdicionarParametro("V_DATA_ATUALIZ", data_atualiz);

                mensagem = "Processado com sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_PROCESSA_CTR");



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

        protected bool ProcessaDadosSPleito(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_RESPONSAVEL", responsavel);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);

                mensagem = "Processado com sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_SALVAR_S_PLEITO");



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

        protected bool ProcessaDadosCPleito(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_RESPONSAVEL", responsavel);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);

                mensagem = "Processado com sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_SALVAR_C_PLEITO");



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

        protected bool ProcessaSalarioReal(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_RESPONSAVEL", responsavel);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);

                objConexao.AdicionarParametro("V_LITERAL_MENSAGEM", desc_mensagem);

                objConexao.AdicionarParametro("V_ID_TIPO_PROCESSO", id_acao_processo);

                mensagem = "Processado com sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_SAlARIO_REAL");



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

        protected bool ProcessaParametro(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                //objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                //objConexao.AdicionarParametro("V_RESPONSAVEL", responsavel);
                //objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);
                //objConexao.AdicionarParametro("V_VLR_INI", vl_inicial);
                //objConexao.AdicionarParametro("V_RSV_SPLTO", vl_sempleito);
                //objConexao.AdicionarParametro("V_RSV_CPLTO", vl_compleito);
                //objConexao.AdicionarParametro("V_BENEFICIARIO", nome_benficiario);
                //objConexao.AdicionarParametro("V_DATA_INI_PGTO", dt_ini_pgto);
                //objConexao.AdicionarParametro("V_DATA_FIM_PGTO", dt_fin_pgto);
                //objConexao.AdicionarParametro("V_DATA_AJZTO", dt_ajuizamento);
                //objConexao.AdicionarParametro("V_ID_TIPO_PROCESSO", id_acao_processo);
                //objConexao.AdicionarParametro("V_MRC_ANT_RSV", mrc_ant_rsv);
                //objConexao.AdicionarParametro("V_MRC_CAD_BNF", mrc_cad_bnf);
                //objConexao.AdicionarParametro("V_MRC_CAD_RSV_SPLTO", mrc_cad_rsv_splto);
                //objConexao.AdicionarParametro("V_MRC_CAD_RSV_CPLTO", mrc_cad_rsv_cplto);
                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);     
                objConexao.AdicionarParametro("V_VLR_INI", vl_inicial);           
                objConexao.AdicionarParametro("V_MRC_CAD_BNF", mrc_cad_bnf);       
                objConexao.AdicionarParametro("V_MRC_ANT_RSV", mrc_ant_rsv);
                objConexao.AdicionarParametro("V_RSV_SPLTO", vl_sempleito);         
                objConexao.AdicionarParametro("V_MRC_CAD_RSV_SPLTO", mrc_cad_rsv_splto);
                objConexao.AdicionarParametro("V_RSV_CPLTO", vl_compleito);         
                objConexao.AdicionarParametro("V_MRC_CAD_RSV_CPLTO", mrc_cad_rsv_cplto);
                objConexao.AdicionarParametro("V_BENEFICIARIO", nome_benficiario);
                objConexao.AdicionarParametro("V_DATA_INI_PGTO", dt_ini_pgto);
                objConexao.AdicionarParametro("V_DATA_FIM_PGTO", dt_fin_pgto);
                objConexao.AdicionarParametro("V_DATA_AJZTO", dt_ajuizamento);
                objConexao.AdicionarParametro("V_ID_TIPO_PROCESSO", id_acao_processo);                  
                objConexao.AdicionarParametro("V_RESPONSAVEL", responsavel);
                mensagem = "Processado com sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_PAR_CR");



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

        protected bool ProcessaCalcReal(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_RESPONSAVEL", responsavel);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);

                objConexao.AdicionarParametro("V_ID_TIPO_PROCESSO", id_acao_processo);

                mensagem = "Processado com sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_CALC_RETRO");



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

        protected bool ProcessaProvisionamentoIR(int? anoRef)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);
                objConexao.AdicionarParametro("V_ANO_REFER", anoRef);

                return objConexao.ExecutarNonQuery("pre_pkg_acao_judic_assistidos.PRE_PRC_PROVISIONA_IR");
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

        protected bool ImportaVr(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);

                objConexao.AdicionarParametro("V_ATLZ_IGPDI", mrc_atlz_igpdi);
                objConexao.AdicionarParametro("V_ATLZ_TRAB", mrc_atlz_trab);
                objConexao.AdicionarParametro("V_ATLZ_CIV", mrc_catlz_civ);

                mensagem = "Processado com sucesso";

                objConexao.AdicionarParametroOut("V_RETURN");
                objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_IMPORTAR_VR");
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

        protected bool ValidaVr()
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);

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

        protected bool DesfazerImportacaoVr(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);

                mensagem = "Processado com sucesso";

                return objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_DELETAR_VR");



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

        protected bool DeletaSrc()
        {
            int retorno = 0;
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_RESPONSAVEL", responsavel);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);


                objConexao.AdicionarParametroOut("V_RETURN");
                objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_DELETA_SRC");
                retorno = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
                return retorno == 0;

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

        protected DataTable ListarParticipante()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("v_num_regtro", matricula);
                objConexao.AdicionarParametro("v_cod_emprs", cod_emprs);
                objConexao.AdicionarParametroCursor("dados");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_PARTICIPANTE");

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

        protected DataTable ListarProcesso()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("v_num_regtro", matricula);
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

        protected DataTable ListarProcessosVr(int filType, string filValue)
        {
            if (filValue != null)
            {
                switch (filType)
                {
                    case 1:
                        cod_emprs = int.Parse(filValue);
                        break;
                    case 2:
                        matricula = int.Parse(filValue);
                        break;
                    case 3:
                        nome_benficiario = filValue;
                        break;
                    case 4:
                        cpf_benficiario = filValue;
                        break;
                    case 5:
                        num_processo = filValue;
                        break;
                }
            }
            return ListarProcessosVr();
        }

        protected DataTable ListarProcessosVr()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);
                objConexao.AdicionarParametro("V_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", matricula);
                objConexao.AdicionarParametro("V_NOME_EMPRG", nome_benficiario);
                objConexao.AdicionarParametro("V_CPF_EMPRG", cpf_benficiario);
                objConexao.AdicionarParametro("V_NUM_PROC", num_processo);
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
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);
                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", matricula);
                objConexao.AdicionarParametro("V_NUM_PROC", num_processo);
                objConexao.AdicionarParametro("V_COD_TIP_ATLZ", cod_tipatlz);

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

        protected DataTable ListarTipoPleito()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("dados");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_TIPLTO");

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

        protected DataSet ListarRelatorio()
        {
            DataSet dt = new DataSet();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_ID_ACAO_PROCESSO", id_acao_processo);
                objConexao.AdicionarParametroCursor("dados");
                objConexao.AdicionarParametroCursor("dados1");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_RELATORIO");

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

        protected DataTable ListarParametro()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);
                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametroCursor("dados");


                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_PARAMETRO");

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

        protected DataSet ListarRelatoriosDisp()
        {
            DataSet dt = new DataSet();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);
                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametroCursor("dados");
                objConexao.AdicionarParametroCursor("dados1");
               

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_REL_DISP");

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

        protected DataSet ListarRelatorioParametro( string ids)
        {
            DataSet dt = new DataSet();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_ID_RELATORIOS", ids);
                objConexao.AdicionarParametroCursor("dados");
                objConexao.AdicionarParametroCursor("dados1");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_REL_PARAM");

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

        protected DataTable ListarTipoAtualizacao()
        {
            return Listar("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_VR_TIP_ATLZ", "p_cod_tip_atlz", 0);
        }

        protected DataTable ListarAssunto()
        {
            return Listar("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_VR_TIPLTO", "p_cod_tiplto", 0);
        }

        protected DataTable ListarHistorico()
        {
            return Listar("pre_pkg_acao_judic_site.PRE_PRC_LISTAR_VR_HSTPLTO", "p_cod_hstplto", 0);
        }

        protected DataTable Listar(string pkg_name, string _param_name, object _param_value)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro(_param_name, num_seq_prc);
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

        protected bool DeletaParametro()
        {
            int retorno = 0;
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametro("V_NUM_SQNCL_PRC", num_seq_prc);
                objConexao.AdicionarParametro("V_TIP_BNF", tip_bnf);
                objConexao.AdicionarParametroOut("V_RETURN");
                objConexao.ExecutarNonQuery("pre_pkg_acao_judic_site.PRE_PRC_DELETA_PARAMETRO");
                retorno = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

            return retorno == 0;

        }

        protected decimal RetornaValorSrb(int matr, int num_seq)
        {
            DataTable dt = new DataTable();
            decimal srb = 0;

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                //Bloco anônimo para testar consulta
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("select bp.media_bsps_cplto_atualiz from own_funcesp.Pre_Tbl_Acao_Rdp_Srb_Bsps bp where bp.num_matr_partf = " + matr + " and bp.num_sqncl_prc = " + num_seq);
                adpt.Fill(dt);
                adpt.Dispose();
                if (dt.Rows.Count != 0)
                {
                    srb = Convert.ToDecimal(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

            return srb;
            //return dt;

        }

        protected DateTime ValidaDIB(int matr, int num_seq)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            DateTime dib = new DateTime();

            try
            {
                //Bloco anônimo para testar consulta
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("select ini.dib_fundacao from own_funcesp.pre_tbl_acao_bnf_inics ini where ini.num_matr_partf = "+ matr + " and ini.num_sqncl_prc = " + num_seq);
                adpt.Fill(dt);
                adpt.Dispose();

                if (dt.Rows.Count > 0)
                {
                    dib = Convert.ToDateTime(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sitema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

            return dib;
        }

    
    }
}
