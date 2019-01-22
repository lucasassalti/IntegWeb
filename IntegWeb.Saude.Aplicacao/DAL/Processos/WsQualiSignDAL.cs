using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Framework;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Saude;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace IntegWeb.Saude.Aplicacao.DAL
{
    public class WsQualiSignDAL
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public partial class SAU_TBL_QUALISIGN_DOC_view
        {
            public int COD_QUALISIGN_DOC { get; set; }
            public int COD_QUALISIGN { get; set; }
            public string NOM_ARQUIVO { get; set; }
            public string NOM_REF_ARQUIVO { get; set; }
            public string COD_STATUS { get; set; }
            public Nullable<short> COD_RETORNO { get; set; }
            public string DCR_RETORNO { get; set; }
            public string DCR_PASSCODE { get; set; }
            public string LOG_INCLUSAO { get; set; }
            public System.DateTime DTH_INCLUSAO { get; set; }
        }

        public List<SAU_TBL_QUALISIGN_DOC_view> GetData(int startRowIndex, int maximumRows, string pArqRef, string pNomeArquivo, DateTime pDtIni, DateTime pDtFim, string pStatus, string sortParameter)
        {
            return GetWhere(pArqRef, pNomeArquivo, pDtIni, pDtFim, pStatus)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<SAU_TBL_QUALISIGN_DOC_view> GetWhere(string pArqRef, string pNomeArquivo, DateTime pDtIni, DateTime pDtFim, string pStatus)
        {

            IQueryable<SAU_TBL_QUALISIGN_DOC_view> query;

            query = from qdoc in m_DbContext.SAU_TBL_QUALISIGN_DOC
                    join q in m_DbContext.SAU_TBL_QUALISIGN on qdoc.COD_QUALISIGN equals q.COD_QUALISIGN
                    where (qdoc.NOM_REF_ARQUIVO.ToLower().Contains(pArqRef.ToLower()) || pArqRef == null)
                       && (qdoc.NOM_ARQUIVO.ToLower().Contains(pNomeArquivo.ToLower()) || pNomeArquivo == null)
                       && (q.DTH_INCLUSAO >= pDtIni || pDtIni == DateTime.MinValue)
                       && (q.DTH_INCLUSAO <= pDtFim || pDtFim == DateTime.MinValue)
                       && (qdoc.COD_STATUS == pStatus || pStatus == null)
                    select new SAU_TBL_QUALISIGN_DOC_view
                    {
                        COD_QUALISIGN_DOC = qdoc.COD_QUALISIGN_DOC,
                        COD_QUALISIGN = qdoc.COD_QUALISIGN,
                        NOM_ARQUIVO = qdoc.NOM_ARQUIVO,
                        NOM_REF_ARQUIVO = qdoc.NOM_REF_ARQUIVO,
                        COD_STATUS = qdoc.COD_STATUS,
                        COD_RETORNO = qdoc.COD_RETORNO,
                        DCR_RETORNO = qdoc.DCR_RETORNO,
                        DCR_PASSCODE = qdoc.DCR_PASSCODE,
                        LOG_INCLUSAO = q.LOG_INCLUSAO,
                        DTH_INCLUSAO = q.DTH_INCLUSAO,
                    };

            return query;

        }

        public int GetDataCount(string pArqRef, string pNomeArquivo, DateTime pDtIni, DateTime pDtFim, string pStatus)
        {
            return GetWhere(pArqRef, pNomeArquivo, pDtIni, pDtFim, pStatus).SelectCount();
        }

        public Resultado GravaLoteDocumentos(SAU_TBL_QUALISIGN newQSDocto)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                //var atualiza = m_DbContext.SAU_TBL_QUALISIGN.Find(newQSDocto.COD_QUALISIGN);
                var atualiza = m_DbContext.SAU_TBL_QUALISIGN.FirstOrDefault(qs => qs.COD_QUALISIGN == newQSDocto.COD_QUALISIGN);

                //m_DbContext.Entry(atualiza).State = EntityState.Modified;

                int item_pk = GetMaxPk_DOC()+1;

                if (atualiza != null)
                {
                    atualiza.SAU_TBL_QUALISIGN_DOC
                        .Where<SAU_TBL_QUALISIGN_DOC>(it => it.COD_QUALISIGN_DOC == 0)
                        .ToList()
                        .ForEach(it => it.COD_QUALISIGN_DOC = item_pk++);
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newQSDocto);
                }
                else
                {
                    newQSDocto.COD_QUALISIGN = GetMaxPk() + 1;
                    newQSDocto.SAU_TBL_QUALISIGN_DOC.ToList().ForEach(it => it.COD_QUALISIGN_DOC = item_pk++);
                    m_DbContext.SAU_TBL_QUALISIGN.Add(newQSDocto);
                }
                int rows_updated = m_DbContext.SaveChanges();
                //if (rows_updated > 0)
                //{
                res.Sucesso("Registro inserido com sucesso.");
                //}
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

        public Resultado AtualizaStatus(string pNOM_REF_ARQUIVO, string Novo_Status)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                var atualiza = m_DbContext.SAU_TBL_QUALISIGN_DOC.FirstOrDefault(qs => qs.NOM_REF_ARQUIVO == pNOM_REF_ARQUIVO);

                if (atualiza != null)
                {
                    atualiza.COD_STATUS = Novo_Status;
                    //m_DbContext.SaveChanges();
                    int rows_updated = m_DbContext.SaveChanges();
                }
                //int rows_updated = m_DbContext.SaveChanges();
                res.Sucesso("Registro atualizado com sucesso.");
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

        public int GetMaxPk()
        {
            int maxPK = 0;
            maxPK = m_DbContext.SAU_TBL_QUALISIGN.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_QUALISIGN);
            return maxPK;
        }

        public int GetMaxPk_DOC()
        {
            int maxPK = 0;
            maxPK = m_DbContext.SAU_TBL_QUALISIGN_DOC.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_QUALISIGN_DOC);
            return maxPK;
        }

    }
}
