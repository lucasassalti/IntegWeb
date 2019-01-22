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
    internal class ArquivoSisObDAL
    {

        public bool Inserir(List<ArquivoSisOb> list)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            bool ret = false;
            try
            {
                foreach (var objM in list)
                {
                    objConexao.LimpaParametros();
                    objConexao.AdicionarParametro("P_LIVRO", objM.livro);
                    objConexao.AdicionarParametro("P_FOLHA", objM.folha);
                    objConexao.AdicionarParametro("P_TERMO", objM.termo);
                    objConexao.AdicionarParametro("P_DTCERTIDAO", objM.dtcertidao);
                    objConexao.AdicionarParametro("P_NBENEFICIO", objM.nbeneficio);

                    objConexao.AdicionarParametro("P_NOMEFALECIDO", objM.nomefalecido);
                    objConexao.AdicionarParametro("P_DTNASCIMENTO", objM.dtnascimento);
                    objConexao.AdicionarParametro("P_NOMEMAE", objM.nomemae);
                    objConexao.AdicionarParametro("P_DTOBITO", objM.dtobito);
                    objConexao.AdicionarParametro("P_NUMCPF", objM.numcpf);
                    objConexao.AdicionarParametro("P_NIT", objM.nit);

                    objConexao.AdicionarParametro("P_TIPOIDCARTORIO", objM.tipoidcartorio);
                    objConexao.AdicionarParametro("P_IDCARTORIO", objM.idcartorio);

                    objConexao.AdicionarParametro("P_FILLER", objM.filler);
                    objConexao.AdicionarParametro("P_MESANOREF", objM.mesanoref);

                    objConexao.AdicionarParametro("P_MATRICULA", objM.matricula);
                    objConexao.AdicionarParametro("P_DT_INCLUSAO", objM.dat_importacao);

                    ret = objConexao.ExecutarNonQuery("PRE_PKG_SYSOB.INSERIR");
                }
                return ret;
            }
            catch (Exception ex)
            {
                throw new Exception("Atenção!\\n\\nOcorreu um erro verifique o arquivo.\\n\\nErro de Banco de Dados:\\n\\n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public bool Deletar(string  mesano)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_MESANO", mesano);

                return objConexao.ExecutarNonQuery("PRE_PKG_SYSOB.DELETAR");

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

        public DataTable SelectAll(ArquivoSisOb obj)
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_MESANO", obj.mesanoref);
                System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_SYSOB.LISTAR");

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

        public DataTable SelectSysOb(ArquivoSisOb obj)
        {


            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_CPF", obj.numcpf);
                objConexao.AdicionarParametro("P_NOME_FALEC", obj.nomefalecido);
                objConexao.AdicionarParametro("P_NOME_MAE", obj.nomemae);
                objConexao.AdicionarParametro("P_DATA_NASC", obj.dtnascimento);
                System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("PRE_PKG_SYSOB.LISTAR_SYSOB");

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
    }
}
