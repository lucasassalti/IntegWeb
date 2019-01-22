using IntegWeb.Entidades.Previdencia;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    internal class PensaoDAL 
    {

        public DataTable ListaRepresentante(PercentualPensao obj)
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_REPRES", obj.num_idntf_rptant);
                objConexao.AdicionarParametroCursor("DADOS");
                System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PENSAO.LISTAR_REPRESENTANTE");

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Atenção!\\n\\nProblemas contate o administrador do sistema:\\n\\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;


        }

        public DataTable ListaPercentual(PercentualPensao obj)
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_REPRES", obj.num_idntf_rptant);
                objConexao.AdicionarParametroCursor("DADOS");
                System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_PENSAO.LISTAR_PENSAO");

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

        public bool Inserir(out string mensagem, PercentualPensao objM)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_NUM_IDNTF_RPTANT", objM.num_idntf_rptant);
                objConexao.AdicionarParametro("P_PCT_PENSAO_DIVIDIDA", objM.pct_pensao_dividida);
                objConexao.AdicionarParametro("P_DAT_VALIDADE", objM.dat_validade);
                objConexao.AdicionarParametro("P_PCT_PENSAO_TOTAL", objM.pct_pensao_total);
                objConexao.AdicionarParametro("P_MATRICULA", objM.matricula);

                mensagem = "Registro Inserido com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_PENSAO.INSERIR");

            }
            catch (Exception ex)
            {
                throw new Exception("Verifique se o representante possui a data de validade informada.\\n\\nErro de Banco de Dados:\\n\\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public bool Atualizar(out string mensagem, PercentualPensao objM)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_NUM_IDNTF_RPTANT", objM.num_idntf_rptant);
                objConexao.AdicionarParametro("P_PCT_PENSAO_DIVIDIDA", objM.pct_pensao_dividida);
                objConexao.AdicionarParametro("P_DAT_VALIDADE", objM.dat_validade);
                objConexao.AdicionarParametro("P_PCT_PENSAO_TOTAL", objM.pct_pensao_total);
                objConexao.AdicionarParametro("P_MATRICULA", objM.matricula);

                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_PENSAO.ATUALIZAR");

            }
            catch (Exception ex)
            {
                throw new Exception("Verifique se o representante possui a data de validade informada.\\n\\nErro de Banco de Dados:\\n\\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public bool Deletar(out string mensagem, PercentualPensao objM)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_NUM_IDNTF_RPTANT", objM.num_idntf_rptant);
                objConexao.AdicionarParametro("P_DAT_VALIDADE", objM.dat_validade);
                objConexao.AdicionarParametro("P_MATRICULA", objM.matricula);

                mensagem = "Registro Deletado com Sucesso";
                return objConexao.ExecutarNonQuery("PRE_PKG_PENSAO.DELETAR");

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
    }


}
