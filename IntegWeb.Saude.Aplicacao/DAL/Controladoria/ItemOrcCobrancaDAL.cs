using IntegWeb.Entidades.Saude.Controladoria;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Controladoria
{
    internal class ItemOrcCobrancaDAL : ItemOrcCobranca
    {

        public DataTable SelectAll(ItemOrcCobranca obj)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_COD_PLANO", obj.Cod_Plano);
                objConexao.AdicionarParametro("P_COD_EMPRS", obj.Cod_Emprs);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_ITEMORC.LISTAR");


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

        public bool Atualizar(out string mensagem, ItemOrcCobranca obj)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_FCESP_NATUREZA", obj.Fcesp_Natureza);
                objConexao.AdicionarParametro("P_SUPLEM_NATUREZA", obj.Suplem_Natureza);
                objConexao.AdicionarParametro("P_PATROC_NATUREZA", obj.Patroc_Natureza);
                objConexao.AdicionarParametro("P_COMPL_NATUREZA", obj.Compl_Natureza);
                objConexao.AdicionarParametro("P_COD_EMPRS", obj.Cod_Emprs);
                objConexao.AdicionarParametro("P_COD_PLANO", obj.Cod_Plano);
                objConexao.AdicionarParametro("P_COD_TIPO_COMP", obj.Cod_Tipo_Comp);
                objConexao.AdicionarParametro("P_COD_GRUPO", obj.Cod_Grupo);
                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_ITEMORC.ALTERAR");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }
    }
}
