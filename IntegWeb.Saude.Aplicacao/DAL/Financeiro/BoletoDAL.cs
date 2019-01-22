using IntegWeb.Entidades.Saude.Financeiro;
using IntegWeb.Entidades.Administracao;
using IntegWeb.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Financeiro
{
    internal class BoletoDAL
    {
        public int ProcessaBoleto( Boleto vencimento)
        {

           
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_DATA_VENCIMENTO", vencimento.DataVencimento);
                objConexao.AdicionarParametroOut("P_RETORNO");
                objConexao.ExecutarNonQuery("OPPORTUNITY.FIN_PRC_GERABOLETOS");

                return int.Parse(objConexao.ReturnParemeterOut().Value.ToString());

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }


        }
    }
}
