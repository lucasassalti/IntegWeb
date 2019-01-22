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
    class RetornoMonitoramentoContratadoExecutanteDAL
    {
        internal Resultado Inserir(DataTable dt)
        {
            Resultado retorno = new Resultado();

            using (OracleBulkCopy bulkCopy = new OracleBulkCopy(ConfigAplication.GetConnectString().Replace(";Unicode=True", "")))
            {
                bulkCopy.DestinationTableName = "OWN_FUNCESP.SAU_TBL_RMTCONTRATADOEXEC";
                bulkCopy.ColumnMappings.Add("CNES", "CNES");
                bulkCopy.ColumnMappings.Add("identificadorExecutante", "IDENTIFICADOREXECUTANTE");
                bulkCopy.ColumnMappings.Add("codigoCnpj_Cpf", "CODIGOCNPJ_CPF");
                bulkCopy.ColumnMappings.Add("registrosRejeitados_ID", "REGISTROSREJEITADOS_ID");
                bulkCopy.ColumnMappings.Add("COD_RETMONITISS", "COD_RETMONITISS");
                try
                {
                    bulkCopy.WriteToServer(dt);
                    retorno.Sucesso("Contratado Executante inserido com sucesso.");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir Contratado Executante: " + erro.Message);
                }
                finally
                {
                    bulkCopy.Close();
                }
                return retorno;
            }
        }

        internal Resultado InserirViaProcedure(DataTable dt)
        {
            Resultado retorno = new Resultado();

            using (ConexaoOracle db = new ConexaoOracle())
            {
                db.LimpaParametros();
                db.AdicionarParametro("P_TIPOTRANSACAO", dt.Rows[0]["tipoTransacao"].ToString());
                db.AdicionarParametro("P_NUMEROLOTE", dt.Rows[0]["numeroLote"].ToString());
                db.AdicionarParametro("P_COMPETENCIALOTE", dt.Rows[0]["competenciaLote"].ToString());
                db.AdicionarParametro("P_DATAREGISTROTRANSACAO", DateTime.ParseExact(dt.Rows[0]["dataRegistroTransacao"].ToString(), "yyyy-MM-dd", null));
                db.AdicionarParametro("P_HORAREGISTROTRANSACAO", DateTime.ParseExact(dt.Rows[0]["horaRegistroTransacao"].ToString(), "HH:mm:ss", null));
                db.AdicionarParametro("P_CABECALHO_ID", dt.Rows[0]["cabecalho_Id"].ToString());
                db.AdicionarParametro("COD_RETMONITISS", dt.Rows[0]["COD_RETMONITISS"].ToString());

                try
                {
                    db.ExecutarNonQuery("SAU_PKG_MONITORAMENTOTISS.SAU_PRC_INSERIRCONTRATADOEXEC");

                    retorno.Sucesso("Contratado Executante inserido com sucesso.");
                }
                catch (Exception erro)
                {
                    retorno.Erro("Erro ao inserir Contratado Executante: " + erro.Message);
                }
            }

            return retorno;
        }
    }
}
