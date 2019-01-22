using IntegWeb.Entidades;
using IntegWeb.Entidades.Saude;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    public class CidadesDAL
    {

        public Cidade Consultar(string cidade, string estado)
        {
            return Consultar(null,cidade,estado,null);
        }

        public Cidade ConsultarPorCodigo(string cod_cidade, string estado_sigla)
        {
            return Consultar(cod_cidade, null, null, estado_sigla);
        }

        public Cidade ConsultarPorIBGE(int COD_MUNICI_IBGE)
        {
            return Consultar(null, null, null, null, COD_MUNICI_IBGE);
        }

        private Cidade Consultar(string cod_cidade = null, string cidade = null, string estado = null, string estado_sigla = null, int? cod_ibge = null)
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
                objConexao.AdicionarParametro("P_COD_MUNICI_IBGE", cod_ibge);
                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("FUN_PKG_LOGRADOURO.CARREGAR_CIDADE");

                adpt.Fill(dt);
                adpt.Dispose();

                if (dt.Rows.Count > 1)
                {
                    if (dt.Select("DCR_MUNICI = '" + cidade + "'").Count() > 0)
                    {
                        dt = dt.Select("DCR_MUNICI = '" + cidade + "'").CopyToDataTable();
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    cRetorno.codigo = Convert.ToInt32(dt.Rows[0]["COD_MUNICI"]);
                    cRetorno.nome = dt.Rows[0]["DCR_MUNICI"].ToString();
                    cRetorno.nome_resumido = dt.Rows[0]["DCR_RSUMD_MUNICI"].ToString();
                    cRetorno.estado_sigla = dt.Rows[0]["COD_ESTADO"].ToString();
                    cRetorno.estado = dt.Rows[0]["DCR_ESTADO"].ToString();
                    cRetorno.cod_ibge = Util.String2Int32(dt.Rows[0]["COD_IBGE"].ToString()) ?? 0;     
                    //cRetorno.cod_ibge_digito = Convert.ToInt32(dt.Rows[0]["COD_IBGE_DIGITO"]);
                    cRetorno.cod_ibge_digito = Util.String2Int32(dt.Rows[0]["COD_IBGE_DIGITO"].ToString());
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