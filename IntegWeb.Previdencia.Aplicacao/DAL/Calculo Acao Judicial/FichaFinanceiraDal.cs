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
    public class FichaFinanceiraDal : FichaFinanceira
    {

        protected bool DeletarVerba(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);


                mensagem = "Deletado com sucesso";

                return objConexao.ExecutarNonQuery("PRE_PKG_CARGAVERBA.DELETAR_VERBA");



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

        protected bool TrocarVerba()
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_NUM_MATR_PARTF", num_matr_partf);
                objConexao.AdicionarParametroOut("V_RETURN");
                objConexao.ExecutarNonQuery("PRE_PKG_CARGAVERBA.TROCAR_VERBA");
                return int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) > 0;

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

        protected bool VerificaUsuarioVerba()
        {
           
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("vv_num_matr_partf", num_matr_partf);

                objConexao.AdicionarParametroOut("v_count");
                objConexao.ExecutarNonQuery("PRE_PKG_CARGAVERBA.LISTAR_VERBAS_USU");
                return int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) == 0;

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

        protected bool InserirVerba(List<FichaFinanceira> list)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            bool ret = false;
            try
            {
                foreach (var objM in list)
                {
                    objConexao.LimpaParametros();
                    objConexao.AdicionarParametro("v_cod_emprs", objM.cod_emprs);
                    objConexao.AdicionarParametro("v_num_rgtro_emprg", objM.num_rgtro_emprg);
                    objConexao.AdicionarParametro("v_cod_verba", objM.cod_verba);
                    objConexao.AdicionarParametro("v_ano_compet_verfin", objM.ano_compet_verfin);
                    objConexao.AdicionarParametro("v_mes_compet_verfin", objM.mes_compet_verfin);
                    objConexao.AdicionarParametro("v_vlr_verfin", objM.vlr_verfin);
                    objConexao.AdicionarParametro("v_ano_pagto_verfin", objM.ano_pagto_verfin);
                    objConexao.AdicionarParametro("v_mes_pagto_verfin", objM.mes_pagto_verfin);
                    objConexao.AdicionarParametro("v_data_inclusao", objM.dataInclusao);


                    ret = objConexao.ExecutarNonQuery("PRE_PKG_CARGAVERBA.INSERIR_VERBA");
                }
       
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de Banco de Dados:\\n\\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }


        }

        protected bool ConsistenciaVerba(DateTime dtinclusao, out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametro("V_DAT_INCLUSAO", dtinclusao);
                objConexao.AdicionarParametroOut("V_RETURN");
                objConexao.ExecutarNonQuery("PRE_PKG_CARGAVERBA.VALIDA_VERBA");
                mensagem = "Processado com sucesso";
                return int.Parse(objConexao.ReturnParemeterOut().Value.ToString()) > 0;
               
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de Banco de Dados:\\n\\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }


        }

        protected DataTable ListarVerbasIncorporacao()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("v_cod_emprs", cod_emprs);
                objConexao.AdicionarParametro("v_matricula", matricula);
                objConexao.AdicionarParametroCursor("dados");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_CARGAVERBA.LISTAR_VERBAS_INCORPORACAO");

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
    }
}
