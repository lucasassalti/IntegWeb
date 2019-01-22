using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Cadastro
{
    public class CadEmissaoCartasDAL
    {

        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public Resultado GerarCartas(DateTime datIni, DateTime datFim, string tipoRelatorio)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            Resultado res = new Resultado();
    
             
            try
            {
                objConexao.AdicionarParametro("VAR_DAT_INI", datIni);
                objConexao.AdicionarParametro("VAR_DAT_FIN", datFim);
                objConexao.AdicionarParametro("VAR_TP_CARTA", tipoRelatorio);
                //bool result = objConexao.ExecutarNonQuery("OWN_SAUDE.PKG_CARTAS_ATD.PRC_EMIS_CARTAS_ATD"); 
                bool result = true;

                if (result == true)
                {
                    res.Sucesso("Processamento Feito com Sucesso");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }
            finally
            {
                objConexao.Dispose();
            }

            return res;
        }

        public Resultado DeleteGeracao(string tipoRelatorio)
        {

            Resultado res = new Resultado();


            try
            {
                int rows_delete = m_DbContext.Database.ExecuteSqlCommand("DELETE OWN_SAUDE.TB_CARTAS_ATC A WHERE A.TIPO_CARTA = '" + tipoRelatorio + "' AND A.DAT_EMISSAO = TO_CHAR(SYSDATE, 'DD/MM/RRRR')");



                if (rows_delete >= 0)
                {
                    res.Sucesso(String.Format("{0} registros excluido.", rows_delete));
                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));

            }
            return res;
        }

    }
}
