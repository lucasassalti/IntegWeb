using IntegWeb.Entidades.Saude.ExigenciasLegais.MonitoramentoTISS;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.ExigenciasLegais
{
    class RetornoMonitoramentoMensagemEnvioANSDAL
    {
        internal Resultado Inserir(mensagemEnvioANS msgenvio)
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.AdicionarParametroOut("P_COD_RETMONITISS");
                db.AdicionarParametro("P_DESC_XML", msgenvio.xml);
                db.AdicionarParametro("P_USERNAME", msgenvio.username);

                try
                {
                    // capturar o código de saída
                    int codigoGerado = db.ExecutarDMLOutput("SAU_PKG_MONITORAMENTOTISS.SAU_PRC_INSERIRRETORNOTISS", "P_COD_RETMONITISS");

                    retorno.Sucesso("Mensagem Envio ANS importada com sucesso!", codigoGerado);
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir Mensagem Envio ANS: " + erro.Message);
                }
            }

            return retorno;
        }
    }
}
