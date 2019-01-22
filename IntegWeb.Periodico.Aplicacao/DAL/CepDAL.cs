
using  IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;

namespace IntegWeb.Periodico.Aplicacao.DAL
{
    internal class CepDAL
    {

        public DataTable SelectCep(string cep)
        { 
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_CEP", cep);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_CEP.LISTAR");
                

                adpt.Fill(dt);
                adpt.Dispose();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;

        }
    }
}
