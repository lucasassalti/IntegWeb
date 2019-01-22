using IntegWeb.Entidades;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Periodico.Aplicacao.DAL
{

    internal class OrgaoDAL
    {

            public DataTable SelectAll(Orgao obj)
            {

                DataTable dt = new DataTable();
                ConexaoOracle objConexao = new ConexaoOracle();
                try
                {
                    objConexao.AdicionarParametro("p_sigla",obj.cod_orgao);
                    objConexao.AdicionarParametroCursor("DADOS");

                    OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_ORGAO.LISTAR");

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
