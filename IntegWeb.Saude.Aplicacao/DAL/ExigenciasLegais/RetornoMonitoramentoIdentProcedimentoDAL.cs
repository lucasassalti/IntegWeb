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
    class RetornoMonitoramentoIdentProcedimentoDAL
    {
        public Resultado Inserir(DataTable dt)
        {
            Resultado retorno = new Resultado();

            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName = "OWN_FUNCESP.SAU_TBL_RMTIDENTPROCEDIMENTO";
                bulkCopy.ColumnMappings.Add("codigoTabela", "CODIGOTABELA");
                bulkCopy.ColumnMappings.Add("identProcedimento_Id", "IDENTPROCEDIMENTO_ID");
                bulkCopy.ColumnMappings.Add("errosItensGuia_Id", "ERROSITENSGUIA_ID");
                bulkCopy.ColumnMappings.Add("COD_RETMONITISS", "COD_RETMONITISS");
                try
                {
                    bulkCopy.WriteToServer(dt);
                    retorno.Sucesso("Identificação do Procedimento inserido com sucesso.");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir Identificação do Procedimento: " + erro.Message);
                }
                finally
                {
                    bulkCopy.Close();
                }
                return retorno;
            }
        }

        internal Resultado Inserir(Entidades.Saude.ExigenciasLegais.MonitoramentoTISS.ct_monitoramentoGuiaProcedimentosIdentProcedimento identProcedimento)
        {
            throw new NotImplementedException();
        }
    }
}
