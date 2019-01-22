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
   public class ItemOrcInssDAL : ItemOrcInss
    {

        protected DataTable SelectData()
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {

                objConexao.AdicionarParametroCursor("DADOS");
                objConexao.AdicionarParametro("P_COD_EMPRS", cod_emprs);
                objConexao.AdicionarParametro("P_COD_PLANO", cod_plano);
                OracleDataAdapter adpt = objConexao.ExecutarAdapter("SAU_PKG_ORCINSS.LISTAR_DADOS");

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

        protected bool UpdateData(out string mensagem)
        {

            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("P_NOM_ABRVO_EMPRS", nom_abrvo_emprs);
                objConexao.AdicionarParametro("P_NOM_RZSOC_EMPRS", nom_rzsoc_emprs);
                objConexao.AdicionarParametro("P_COD_PLANO", cod_plano);
                objConexao.AdicionarParametro("P_TIPO_ITEM_ORC", tipo_item_orc);
                objConexao.AdicionarParametro("P_ITEM_ORCAMENTARIO", item_orcamentario);
                objConexao.AdicionarParametro("P_CONSOLIDA_PLANO", consolida_plano);
                objConexao.AdicionarParametro("P_COD_EMPRS_CT", cod_emprs_ct);
                objConexao.AdicionarParametro("P_COD_PLANO_CT", cod_plano_ct);
                objConexao.AdicionarParametro("P_DESC_PLANO", desc_plano);
                objConexao.AdicionarParametro("P_COD_NATUREZA_CT", cod_natureza_ct);
                objConexao.AdicionarParametro("P_DESC_NATUREZA", desc_natureza);
                objConexao.AdicionarParametro("P_COD_EMPRS", cod_emprs);


                mensagem = "Registro Atualizado com Sucesso";
                return objConexao.ExecutarNonQuery("SAU_PKG_ORCINSS.ATUALIZAR_DADOS");
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
