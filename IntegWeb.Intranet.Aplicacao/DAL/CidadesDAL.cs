using IntegWeb.Entidades;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace Intranet.Aplicacao.DAL
{
    public class CidadesDAL
    {

        public Cidade Consultar(string cidade, string estado)
        {
            return Consultar(null,cidade,estado);
        }

        private Cidade Consultar(int? cod_cidade = null, string cidade = null, string estado = null, string estado_sigla = null)
        {
            Cidade cRetorno = new Cidade();
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_COD_MUNICI", cod_cidade);
                objConexao.AdicionarParametro("P_DCR_MUNICI", cidade);
                objConexao.AdicionarParametro("P_COD_ESTADO", estado_sigla);
                objConexao.AdicionarParametro("P_DCR_ESTADO", estado);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("FUN_PKG_LOGRADOURO.CARREGAR_CIDADE");

                adpt.Fill(dt);
                adpt.Dispose();

                if (dt.Rows.Count > 0)
                {
                    cRetorno.codigo = Convert.ToInt32(dt.Rows[0]["COD_MUNICI"]);
                    cRetorno.nome = dt.Rows[0]["DCR_MUNICI"].ToString();
                    cRetorno.nome_resumido = dt.Rows[0]["DCR_RSUMD_MUNICI"].ToString();
                    cRetorno.estado_sigla = dt.Rows[0]["COD_ESTADO"].ToString();
                    cRetorno.estado = dt.Rows[0]["DCR_ESTADO"].ToString();
                    cRetorno.cod_ibge = Convert.ToInt32(dt.Rows[0]["COD_IBGE"]);
                    cRetorno.cod_ibge_digito = Convert.ToInt32(dt.Rows[0]["COD_IBGE_DIGITO"]);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return cRetorno;
        }

     }
}