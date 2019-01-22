using IntegWeb.Entidades;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Framework; 
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class CriticasDAL
    {

        internal PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        internal long GetMaxPk_CRITICA()
        {
            long maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_ARQ_PAT_CRITICA);
            return maxPK;
        }

        internal Resultado AddData(List<PRE_TBL_ARQ_PATROCINA_CRITICA> lsCRITICAS, long? COD_ARQ_PAT_LINHA, int? COD_ARQ_PAT)
        {
            Resultado res = new Entidades.Resultado();
            long iMaxPk_CRITICA = GetMaxPk_CRITICA();
            try
            {
                lsCRITICAS.ForEach(c =>
                {
                    iMaxPk_CRITICA++;
                    c.COD_ARQ_PAT_CRITICA = iMaxPk_CRITICA;
                    c.COD_ARQ_PAT = COD_ARQ_PAT;
                    c.COD_ARQ_PAT_LINHA = COD_ARQ_PAT_LINHA;
                    c.DCR_CRITICA = Util.String2Limit(c.DCR_CRITICA, 0, 200);
                    m_DbContext.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(c);
                });
            }
            catch (Exception ex)
            {
                res.Erro(ex.Message);
            }
          
            return res;
        }

        internal Resultado SaveData()
        {
            Resultado res = new Entidades.Resultado();
            try
            {               
                m_DbContext.SaveChanges();
                res.Sucesso("Críticas gravados com sucesso.");
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

        internal Resultado DeleteCriticaLinha(long COD_ARQ_PAT_LINHA)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CRITICA " +
                                                       " WHERE COD_ARQ_PAT_LINHA = " + COD_ARQ_PAT_LINHA.ToString(), 0);

                //m_DbContext.SaveChanges();
                //res.Sucesso("Registro não localizado ou recadastro já efetuado.");
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

        internal Resultado DeleteCritica(long COD_ARQ_PAT)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                //Exclui criticas do arquivo:
                m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CRITICA " +
                                                       " WHERE COD_ARQ_PAT = " + COD_ARQ_PAT.ToString(), 0);

                //Exclui todas criticas das linhas:
                m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CRITICA   " +
                                                       " WHERE COD_ARQ_PAT_LINHA IN (SELECT L.COD_ARQ_PAT_LINHA " +
                                                       "                               FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_LINHA L " +
                                                       "                              INNER JOIN OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CRITICA C ON L.COD_ARQ_PAT_LINHA = C.COD_ARQ_PAT_LINHA " +
                                                       "                              WHERE L.COD_ARQ_PAT = " + COD_ARQ_PAT.ToString() + ")", 0);

                //Exclui criticas da carga :
                //m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CRITICA   " +
                //                                       " WHERE COD_ARQ_PAT_CARGA IN (SELECT C.COD_ARQ_PAT_CARGA " +
                //                                       "                               FROM OWN_FUNCESP.PRE_TBL_ARQ_PATROCINA_CARGA C " +
                //                                       "                              WHERE C.COD_ARQ_PAT = " + COD_ARQ_PAT.ToString() + ")", 0);

                //m_DbContext.SaveChanges();
                //res.Sucesso("Registro não localizado ou recadastro já efetuado.");
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

        internal List<ATT_CHARGER_DEPARA> Carrega_cache_DePara_OrgaoLotacao_table(short pCOD_EMPRS, string pNUM_ORGAO_DE = null)
        {
            IQueryable<ATT_CHARGER_DEPARA> query;
            query = from c in m_DbContext.ATT_CHARGER_DEPARA
                    where (c.CODAPLICACAO == 1)
                       && (c.CODTABELA == 181) // ORGAO
                       && (c.CODCOLUNA == 1)   // NUM_ORGAO
                       && (c.CODEMPRESA == pCOD_EMPRS)
                       && (c.CONTEUDODE == pNUM_ORGAO_DE || pNUM_ORGAO_DE == null)
                    select c;
            return query.ToList();
        }
    }
}
