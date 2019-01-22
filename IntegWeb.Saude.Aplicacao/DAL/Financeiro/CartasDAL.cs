using IntegWeb.Entidades.Saude.Financeiro;
using IntegWeb.Entidades.Administracao;
using IntegWeb.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OracleClient;

namespace IntegWeb.Saude.Aplicacao.DAL.Financeiro
{
    class CartasDAL
    {
        public DataTable ListaDatas()
        {

            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = objConexao.ExecutarAdapter("OPPORTUNITY.FIN_PKG_BOLETOS_SAUDE.LISTA_DAT_VENCIMENTO");

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
