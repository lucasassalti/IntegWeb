using IntegWeb.Framework;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.DAL
{
    public class DebitoContaRetornoDAL
    {

        public class AAT_TBL_RET_DEB_CONTA_view
        {
            public string DCR_NOM_ARQ { get; set; }
            public int    NUM_SEQ_LINHA { get; set; }
            public string ID_TP_REGISTRO { get; set; }
            public string COD_EMPRESA { get; set; }
            public string NUM_REGISTRO { get; set; }
            public string NUM_REPRESENTANTE { get; set; }
            public string NUM_NOSSO_NUMERO { get; set; }
            public string DTA_VENCIMENTO { get; set; }
            public string VLR_DEBITO { get; set; }            
            public string COD_CRITICA { get; set; }
            public string DCR_CRITICA { get; set; }
            public string COD_MOTIVO { get; set; }
            public string DESC_MOTIVO { get; set; }
        }

        public EntitiesConn m_DbContext = new EntitiesConn();

        public bool Persistir(List<AAT_TBL_RET_DEB_CONTA> lsDebConta)
        {
            bool ret = false;

            //ObjectParameter P_DCR_NOM_ARQ = new ObjectParameter("P_DCR_NOM_ARQ", lsDebConta[0].DCR_NOM_ARQ);
            
            foreach(AAT_TBL_RET_DEB_CONTA newRetDebConta in lsDebConta)
            {
                m_DbContext.AAT_TBL_RET_DEB_CONTA.Add(newRetDebConta);
            }
            m_DbContext.SaveChanges();
            
            return ret;
        }

        public List<AAT_TBL_RET_DEB_CONTA> GetData(int startRowIndex, int maximumRows, string pEmp, string pMatr, string pRepre, string pDCR_NOM_ARQ, string pID_TP_REGISTRO, bool? pComCritica, string sortParameter)
        {
            return GetWhere(pEmp, pMatr, pRepre,pDCR_NOM_ARQ, null, pID_TP_REGISTRO, pComCritica)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public AAT_TBL_RET_DEB_CONTA GetData(string pEmp, string pMatr, string  pRepre, string  pDCR_NOM_ARQ, string pID_TP_REGISTRO)
        {
            return GetWhere(pEmp, pMatr, pRepre,pDCR_NOM_ARQ, null, pID_TP_REGISTRO, null).ToList().FirstOrDefault();
        }

        public IQueryable<AAT_TBL_RET_DEB_CONTA> GetWhere(string  pEmp, string pMatr, string pRepre, string pDCR_NOM_ARQ, int? pNUM_SEQ_LINHA, string pID_TP_REGISTRO, bool? pComCritica)
        {
            IQueryable<AAT_TBL_RET_DEB_CONTA> query;
            query = from dc in m_DbContext.AAT_TBL_RET_DEB_CONTA
                    where (dc.DCR_NOM_ARQ == pDCR_NOM_ARQ && pDCR_NOM_ARQ != null || pDCR_NOM_ARQ == null)
                       && (dc.COD_EMPRESA == pEmp || pEmp == null )
                       && (dc.NUM_REGISTRO == pMatr || pMatr == null)
                       && (dc.NUM_REPRESENTANTE == pRepre || pRepre == null)
                       && (dc.NUM_SEQ_LINHA == pNUM_SEQ_LINHA || pNUM_SEQ_LINHA == null)
                       && (dc.ID_TP_REGISTRO == pID_TP_REGISTRO || pID_TP_REGISTRO == null)
                       && (pComCritica == null || pComCritica == false || pComCritica == true && m_DbContext.AAT_TBL_RET_DEB_CONTA_CRITICAS.Where(c => c.DCR_NOM_ARQ == dc.DCR_NOM_ARQ && c.NUM_SEQ_LINHA == dc.NUM_SEQ_LINHA).Count() > 0)
                    select dc;
            return query;
        }

        public IQueryable<AAT_TBL_RET_DEB_CONTA_view> GetDataReport(string pDCR_NOM_ARQ, int? pNUM_SEQ_LINHA, string pID_TP_REGISTRO, bool? pComCritica)
        {
            IQueryable<AAT_TBL_RET_DEB_CONTA_view> query;
            query = from dc in m_DbContext.AAT_TBL_RET_DEB_CONTA
                    join mot in m_DbContext.AAT_TBL_RET_DEB_CONTA_MOTIVO on dc.COD_MOTIVO_RET equals mot.COD_MOTIVO
                        into leftjoin
                    from mot in leftjoin.DefaultIfEmpty()
                    join crit in m_DbContext.AAT_TBL_RET_DEB_CONTA_CRITICAS on new { dc.DCR_NOM_ARQ, dc.NUM_SEQ_LINHA } equals new { crit.DCR_NOM_ARQ, crit.NUM_SEQ_LINHA }
                        into leftjoin2
                    from crit in leftjoin2.DefaultIfEmpty()
                    where (dc.DCR_NOM_ARQ == pDCR_NOM_ARQ && pDCR_NOM_ARQ != null || pDCR_NOM_ARQ == null)
                       && (dc.NUM_SEQ_LINHA == pNUM_SEQ_LINHA || pNUM_SEQ_LINHA == null)
                       && (dc.ID_TP_REGISTRO == pID_TP_REGISTRO || pID_TP_REGISTRO == null)
                       && (pComCritica == null || pComCritica == false || pComCritica == true && m_DbContext.AAT_TBL_RET_DEB_CONTA_CRITICAS.Where(c => c.DCR_NOM_ARQ == dc.DCR_NOM_ARQ && c.NUM_SEQ_LINHA == dc.NUM_SEQ_LINHA).Count() > 0)
                    orderby dc.DCR_NOM_ARQ, dc.NUM_SEQ_LINHA
                    select new AAT_TBL_RET_DEB_CONTA_view
                    {
                        DCR_NOM_ARQ = dc.DCR_NOM_ARQ,
                        NUM_SEQ_LINHA = dc.NUM_SEQ_LINHA,
                        ID_TP_REGISTRO = dc.ID_TP_REGISTRO,
                        COD_EMPRESA = dc.COD_EMPRESA,
                        NUM_REGISTRO = dc.NUM_REGISTRO,
                        NUM_REPRESENTANTE = dc.NUM_REPRESENTANTE,
                        NUM_NOSSO_NUMERO = dc.NUM_NOSSO_NUMERO,
                        DTA_VENCIMENTO = dc.DTA_VENCIMENTO,
                        VLR_DEBITO = dc.VLR_DEBITO,                        
                        COD_CRITICA = crit.COD_CRITICA,
                        DCR_CRITICA = crit.DCR_CRITICA,
                        COD_MOTIVO = mot.COD_MOTIVO,
                        DESC_MOTIVO = mot.DESC_MOTIVO
                    };

            return query;
        }

        public String GetDtArquivo(string pDCR_NOM_ARQ)
        {
            DateTime? dtArquivo = new DateTime();
            String DTA_VENCIMENTO = "";
            if (GetWhere(null,null,null,pDCR_NOM_ARQ, null, "A", null).FirstOrDefault() != null)
            {
                DTA_VENCIMENTO = GetWhere(null, null, null,pDCR_NOM_ARQ, null, "A", null).FirstOrDefault().DTA_VENCIMENTO;
                //DTA_VENCIMENTO.Substring
                dtArquivo = Util.ToDateTimeCustom(DTA_VENCIMENTO, "yyyyMMdd");
            }
            //DateTime.TryParse(GetWhere(pDCR_NOM_ARQ, null, "A").FirstOrDefault().DTA_VENCIMENTO, out dtArquivo);
            return (dtArquivo!=null) ? ((DateTime)DateTime.Parse(dtArquivo.ToString())).ToString("dd/MM/yyyy") : "--/--/----";
        }

        public int GetErros(string pDCR_NOM_ARQ)
        {
            //return GetWhere(pDCR_NOM_ARQ, null, "F").Where(r => !r.COD_MOTIVO_RET.Equals("00")).SelectCount();
            return GetWhere(null,null,null,pDCR_NOM_ARQ, null, null, null).Where(r => r.AAT_TBL_RET_DEB_CONTA_CRITICAS.Count() > 0).SelectCount();
        }

        public List<object> GetListaDCR_NOM_ARQ()
        {
            IQueryable<object> query;
            query = from c in m_DbContext.AAT_TBL_RET_DEB_CONTA
                    where c.ID_TP_REGISTRO == "A"
                    orderby c.DTA_VENCIMENTO descending
                    select (
                    new
                    {
                        Text = c.DTA_VENCIMENTO.Substring(6, 2) + "/" + c.DTA_VENCIMENTO.Substring(4, 2) + "/" + c.DTA_VENCIMENTO.Substring(0, 4) + " " + c.DCR_NOM_ARQ,
                        Value = c.DCR_NOM_ARQ
                    });
            return query.ToList();
        }

        public int GetDataCount(string pDCR_NOM_ARQ, string pID_TP_REGISTRO, bool? pComCritica)
        {
            return GetWhere(null,null,null,pDCR_NOM_ARQ, null, pID_TP_REGISTRO, pComCritica).SelectCount();
        }
        public int GetDataCount(string pEmp, string pMatr, string pRepre, string pDCR_NOM_ARQ, string pID_TP_REGISTRO, bool? pComCritica)
        {
            return GetWhere(null, null, null, pDCR_NOM_ARQ, null, pID_TP_REGISTRO, pComCritica).SelectCount();
        }

        public AAT_TBL_RET_DEB_CONTA_MOTIVO GetMotivo(string pCOD_MOTIVO)
        {
            IQueryable<AAT_TBL_RET_DEB_CONTA_MOTIVO> query;
            query = from m in m_DbContext.AAT_TBL_RET_DEB_CONTA_MOTIVO
                    where (m.COD_MOTIVO == pCOD_MOTIVO)   
                    select m;
            return query.ToList().FirstOrDefault();
        }

        public AAT_TBL_DEB_CONTA_PRODUTO GetProduto(short pCOD_PRODUTO)
        {
            IQueryable<AAT_TBL_DEB_CONTA_PRODUTO> query;
            query = from m in m_DbContext.AAT_TBL_DEB_CONTA_PRODUTO
                    where (m.COD_PRODUTO == pCOD_PRODUTO)
                    select m;
            return query.ToList().FirstOrDefault();
        }

        public int DeleteData(string pDCR_NOM_ARQ)
        {
            int ret = m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.AAT_TBL_RET_DEB_CONTA_CRITICAS WHERE DCR_NOM_ARQ = :param", pDCR_NOM_ARQ);

            ret = m_DbContext.Database.ExecuteSqlCommand("DELETE FROM OWN_FUNCESP.AAT_TBL_RET_DEB_CONTA WHERE DCR_NOM_ARQ = :param", pDCR_NOM_ARQ);

            m_DbContext.SaveChanges(); 
            return ret;
        }
    }
}
