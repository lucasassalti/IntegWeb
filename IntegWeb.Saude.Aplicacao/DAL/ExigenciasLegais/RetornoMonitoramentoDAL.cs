using IntegWeb.Entidades.Saude.ExigenciasLegais.MonitoramentoTISS;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IntegWeb.Saude.Aplicacao.DAL.ExigenciasLegais
{
    internal class RetornoMonitoramentoDAL
    {
        public Resultado InserirXML(XElement xml, string login)
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.LimpaParametros();
                db.AdicionarParametroOut("P_COD_RETMONITISS");
                db.AdicionarParametro("P_DESC_XML", xml.Value);
                db.AdicionarParametro("P_USERNAME", login);
                //db.AdicionarParametro("P_IP_STATION", objMenu.Sistema.Codigo);
                //db.AdicionarParametro("P_WK_STATION", objMenu.Nivel);

                try
                {
                    //se FOR NECESSÁRIO capturar o código de saída
                    int codigoGerado = db.ExecutarDMLOutput("SAU_PKG_MONITORAMENTOTISS.SAU_PRC_INSERIRRETORNOTISS", "P_COD_RETMONITISS");

                    retorno.Sucesso("Retorno do Monitoramento importado com sucesso!", codigoGerado);
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir XML puro: " + erro.Message);
                }
            }

            return retorno;
        }
    }
}
