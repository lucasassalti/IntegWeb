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
    class RetornoMonitoramentoCabecalhoDAL
    {
        internal Resultado Inserir(DataTable dt)
        {
            Resultado retorno = new Resultado();

            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName = "OWN_FUNCESP.SAU_TBL_RMTCABECALHO";
                bulkCopy.ColumnMappings.Add("cabecalho_Id", "cabecalho_id");
                bulkCopy.ColumnMappings.Add("registroANS", "registroans");
                bulkCopy.ColumnMappings.Add("versaoPadrao", "versaopadrao");
                bulkCopy.ColumnMappings.Add("COD_RETMONITISS", "cod_retmonitiss");
                try
                {
                    bulkCopy.WriteToServer(dt);
                    retorno.Sucesso("Cabeçalho inserido com sucesso.");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir Cabeçalho: " + erro.Message);
                }
                finally
                {
                    bulkCopy.Close();
                }
                return retorno;
            }
        }
    }
}
