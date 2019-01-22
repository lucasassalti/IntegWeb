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
    class RetornoMonitoramentoRelacaoErrosDAL
    {
        public Resultado Inserir(DataTable dt)
        {
            Resultado retorno = new Resultado();

            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName = "OWN_FUNCESP.SAU_TBL_RMTRELACAOERROS";
                bulkCopy.ColumnMappings.Add("identificadorCampo", "IDENTIFICADORCAMPO");
                bulkCopy.ColumnMappings.Add("codigoErro", "CODIGOERRO");
                bulkCopy.ColumnMappings.Add("errosItensGuia_Id", "ERROSITENSGUIA_ID");
                bulkCopy.ColumnMappings.Add("COD_RETMONITISS", "COD_RETMONITISS");
                try
                {
                    bulkCopy.WriteToServer(dt);
                    retorno.Sucesso("Relação Erros inseridos com sucesso.");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir Relação Erros: " + erro.Message);
                }
                finally
                {
                    bulkCopy.Close();
                }
                return retorno;
            }
        }

        internal Resultado Inserir(Entidades.Saude.ExigenciasLegais.MonitoramentoTISS.mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamentoRegistrosRejeitadosErrosItensGuiaRelacaoErros relacaoErros)
        {
            throw new NotImplementedException();
        }
    }
}
