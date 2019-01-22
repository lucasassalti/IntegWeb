using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System.Data;
using System.Data.OracleClient;


namespace IntegWeb.Financeira.Aplicacao.DAL.Tesouraria
{
    public class RelatorioBorderoDAL
    {
        protected DataTable geraRelSemRateio(string dtInicio, string dtFim)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            //  dt.Columns[2].DataType = System.Type.GetType("System.DateTime");
            try
            {
                obj.AdicionarParametro("dt_inicio", dtInicio);
                obj.AdicionarParametro("dt_final", dtFim);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("OWN_FUNCESP.PKG_FIN_REL_BORDERO.PRC_SEM_RATEIO");
                adpt.Fill(dt);


                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }
            return dt;

        }

        protected DataTable geraRelSemRateioNumero(string bordInicial, string bordFinal)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try
            {
                obj.AdicionarParametro("bord_inicial",bordInicial );
                obj.AdicionarParametro("bord_final", bordFinal);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("OWN_FUNCESP.PKG_FIN_REL_BORDERO.PRC_SEM_RATEIO_NUMERO");
                adpt.Fill(dt);


                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }
            return dt;
        }

        protected DataTable geraRelComRateio(string dtInicio, string dtFim)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try
            {
                obj.AdicionarParametro("dt_inicio", dtInicio);
                obj.AdicionarParametro("dt_final", dtFim);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("OWN_FUNCESP.PKG_FIN_REL_BORDERO.PRC_COM_RATEIO");
                adpt.Fill(dt);
                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }
            return dt;
        }

        protected DataTable geraRelComRateioNumero(string bordInicial, string bordFinal)
        {
            ConexaoOracle obj = new ConexaoOracle();
            DataTable dt = new DataTable();

            try
            {
                obj.AdicionarParametro("bord_inicial", bordInicial);
                obj.AdicionarParametro("bord_final", bordFinal);
                obj.AdicionarParametroCursor("DADOS");

                OracleDataAdapter adpt = obj.ExecutarAdapter("OWN_FUNCESP.PKG_FIN_REL_BORDERO.PRC_COM_RATEIO_NUMERO");
                adpt.Fill(dt);


                adpt.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas com a conexão com o banco de dados, favor entrar em contato com Administrador do sistema " + ex.Message);
            }
            return dt;
        }
    }
}
