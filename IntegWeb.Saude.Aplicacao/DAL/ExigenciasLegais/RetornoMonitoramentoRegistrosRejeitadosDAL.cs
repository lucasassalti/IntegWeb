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
    class RetornoMonitoramentoRegistrosRejeitadosDAL
    {
        public Resultado Inserir(DataTable dt)
        {
            Resultado retorno = new Resultado();

            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName = "OWN_FUNCESP.sau_tbl_rmtregistrosrejeitados";
                bulkCopy.ColumnMappings.Add("registrosRejeitados_Id", "REGISTROSREJEITADOS_ID");
                bulkCopy.ColumnMappings.Add("numeroGuiaPrestador", "NUMEROGUIAPRESTADOR");
                bulkCopy.ColumnMappings.Add("numeroGuiaOperadora", "NUMEROGUIAOPERADORA");
                bulkCopy.ColumnMappings.Add("identificadorReembolso", "IDENTIFICADORREEMBOLSO");
                bulkCopy.ColumnMappings.Add("dataProcessamento", "DATAPROCESSAMENTO");
                bulkCopy.ColumnMappings.Add("resumoProcessamento_Id", "RESUMOPROCESSAMENTO_ID");
                bulkCopy.ColumnMappings.Add("COD_RETMONITISS", "COD_RETMONITISS");
                try
                {
                    bulkCopy.WriteToServer(dt);
                    retorno.Sucesso("Registros Rejeitados inseridos com sucesso.");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir Registros Rejeitados: " + erro.Message);
                }
                finally
                {
                    bulkCopy.Close();
                }
                return retorno;
            }
        }

        internal Resultado Inserir(Entidades.Saude.ExigenciasLegais.MonitoramentoTISS.mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitados registrosRejeitados)
        {
            throw new NotImplementedException();
        }
    }
}
