using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;
using IntegWeb.Framework;


namespace IntegWeb.Saude.Aplicacao.DAL
{
    public class DirBoletoSauDAL
    {
        public void ConsultaURLDocs(string bCod)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter("select link from own_portal.vw_boleto_saude_email where cod_url = "+ bCod);

            }
            catch (Exception ex)
            {

            }
        }

    }
}
