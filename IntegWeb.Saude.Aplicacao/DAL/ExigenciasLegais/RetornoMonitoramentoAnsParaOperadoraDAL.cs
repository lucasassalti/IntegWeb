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
    class RetornoMonitoramentoAnsParaOperadoraDAL
    {
        public Resultado Inserir(DataTable dt)
        {
            Resultado retorno = new Resultado();

            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName = "OWN_FUNCESP.Sau_Tbl_Rmtansparaoperadora";
                bulkCopy.ColumnMappings.Add("ansParaOperadora_Id", "ANSPARAOPERADORA_ID");
                bulkCopy.ColumnMappings.Add("mensagem_Id", "MENSAGEM_ID");
                bulkCopy.ColumnMappings.Add("COD_RETMONITISS", "COD_RETMONITISS");
                try
                {
                    bulkCopy.WriteToServer(dt);
                    retorno.Sucesso("ANS Para Operadora inserida com sucesso.");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir ANS Para Operadora: " + erro.Message);
                }
                finally
                {
                    bulkCopy.Close();
                }
                return retorno;
            }
        }

        internal Resultado Inserir(Entidades.Saude.ExigenciasLegais.MonitoramentoTISS.mensagemEnvioANSMensagemAnsParaOperadora ansParaOperadora)
        {
            throw new NotImplementedException();
        }
    }
}
