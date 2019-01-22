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
    class RetornoMonitoramentoResumoProcessamentoDAL
    {
        public Resultado Inserir(DataTable dt)
        {
            Resultado retorno = new Resultado();

            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName = "OWN_FUNCESP.sau_tbl_rmtresumprocess";
                bulkCopy.ColumnMappings.Add("nomeArquivo", "NOMEARQUIVO");
                bulkCopy.ColumnMappings.Add("resumoProcessamento_Id", "RESUMOPROCESSAMENTO_ID");
                bulkCopy.ColumnMappings.Add("arquivoProcessadoPelaAns", "ARQUIVOPROCESSADOPELAANS");
                bulkCopy.ColumnMappings.Add("AnsParaOperadora_Id", "ANSPARAOPERADORA_ID");
                bulkCopy.ColumnMappings.Add("COD_RETMONITISS", "COD_RETMONITISS");
                try
                {
                    bulkCopy.WriteToServer(dt);
                    retorno.Sucesso("Resumo do Processamento inserido com sucesso.");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir Resumo do Processamento: " + erro.Message);
                }
                finally
                {
                    bulkCopy.Close();
                }
                return retorno;
            }
        }

        internal Resultado Inserir(Entidades.Saude.ExigenciasLegais.MonitoramentoTISS.mensagemEnvioANSMensagemAnsParaOperadoraResumoProcessamento resumoProcessamento)
        {
            throw new NotImplementedException();
        }
    }
}
