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

namespace IntegWeb.Previdencia.Aplicacao.DAL.Int_Protheus
{
    public class IntegraProtheusDAL
    {

        public class VIEW_DadosProtheus_Cad1
        {
            public int CODIGO { get; set; }
            public string DESCRICAO { get; set; }
            public string DESCRICAO2 { get; set; }
            public string ENTITY_OBJ { get; set; }
            public bool NOVO { get; set; }
        }

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<VIEW_DadosProtheus_Cad1> GetData(int startRowIndex, int maximumRows, string pTabela, int? pCodigo, string pDescricao, string pDescricao2, string sortParameter)
        {
            return GetWhere(pTabela, pCodigo, pDescricao, pDescricao2)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<VIEW_DadosProtheus_Cad1> GetWhere(string pTabela, int? pCodigo, string pDescricao, string pDescricao2)
        {
            IQueryable<VIEW_DadosProtheus_Cad1> query = null;

            //int iCodigo = 0;
            //int.TryParse(pCodigo, out iCodigo);

            switch (pTabela)
            {
                case "CTA_TBL_PAIS":
                    //query = from tb in m_DbContext.CTA_TBL_PAIS
                    //        where (tb.COD == pCodigo.ToString() || pCodigo == null)
                    //           && (tb.NOME.ToUpper().Trim().Contains(pDescricao.ToUpper().Trim()) || pDescricao == null)
                    //        select new VIEW_DadosProtheus_Cad1
                    //        {
                    //            CODIGO = int.Parse(tb.COD),
                    //            DESCRICAO = tb.NOME
                    //        };
                    query = m_DbContext.Database.SqlQuery<VIEW_DadosProtheus_Cad1>("select to_number(COD) CODIGO, NOME DESCRICAO " +
                                                                                   "  from OWN_INTPROTHEUS.CTA_TBL_PAIS where " +
                                                                                     ((pCodigo != null) ? "COD = LPAD('" + pCodigo.ToString() + "',5,'0')" : "null is null") + " and " +
                                                                                     ((pDescricao != null) ? "PRG LIKE '%" + pDescricao + "%'" : "null is null"))
                                                .AsQueryable<VIEW_DadosProtheus_Cad1>();
                    break;
                case "SUBMASSA":
                    query = m_DbContext.Database.SqlQuery<VIEW_DadosProtheus_Cad1>("select to_number(COD_SUBMASSA) CODIGO, DCR_SUBMASSA DESCRICAO " +
                                                                                   "  from OWN_INTPROTHEUS.SUBMASSA where " +
                                                                                     ((pCodigo != null) ? "COD_SUBMASSA = " + pCodigo.ToString() : "null is null") + " and " +
                                                                                     ((pDescricao != null) ? "DCR_SUBMASSA LIKE '%" + pDescricao + "%'" : "null is null"))
                                                .AsQueryable<VIEW_DadosProtheus_Cad1>();
                    break;
                case "TP_MOVTO":
                    query = from tb in m_DbContext.TP_MOVTO
                            where (tb.COD_MOVTO == pCodigo || pCodigo == null)
                               && (tb.DSC_MOVTO.ToUpper().Trim().Contains(pDescricao.ToUpper().Trim()) || pDescricao == null)
                            select new VIEW_DadosProtheus_Cad1
                            {
                                CODIGO = tb.COD_MOVTO,
                                DESCRICAO = tb.DSC_MOVTO
                            };
                    break;
                case "PLN_PRG_SAU":
                    query = from tb in m_DbContext.PLN_PRG_SAU
                            where (tb.COD_PLN == pCodigo || pCodigo == null)
                               && (tb.PRG.ToUpper().Trim().Contains(pDescricao.ToUpper().Trim()) || pDescricao == null)
                            select new VIEW_DadosProtheus_Cad1
                            {
                                CODIGO = tb.COD_PLN,
                                DESCRICAO = tb.PRG
                            };
                    break;
                case "PLN_PRG_PRV":
                    query = from tb in m_DbContext.PLN_PRG_PRV
                            where (tb.NUM_PLBNF == pCodigo || pCodigo == null)
                               && (tb.PRG.ToUpper().Trim().Contains(pDescricao.ToUpper().Trim()) || pDescricao == null)
                            select new VIEW_DadosProtheus_Cad1
                            {
                                CODIGO = tb.NUM_PLBNF,
                                DESCRICAO = tb.PRG
                            };
                    break;
                case "ASSOCIACAO_VERBA":

                    //int iDescricao = 0;
                    //int.TryParse(pDescricao, out iDescricao);

                    //query = from tb in m_DbContext.ASSOCIACAO_VERBA
                    //        where (tb.COD_ASSOC == pCodigo || pCodigo == null)
                    //           && (tb.NUM_VRBFSS == iDescricao || pDescricao == null)
                    //        select new VIEW_DadosProtheus_Cad1
                    //        {
                    //            CODIGO = tb.COD_ASSOC,
                    //            DESCRICAO = tb.NUM_VRBFSS
                    //        };

                    query = m_DbContext.Database.SqlQuery<VIEW_DadosProtheus_Cad1>("select COD_ASSOCIACAO_VERBA CODIGO, to_char(COD_ASSOC) DESCRICAO, to_char(NUM_VRBFSS) DESCRICAO2 " +
                                                                                   "  from OWN_INTPROTHEUS.ASSOCIACAO_VERBA where " +
                                                                                     ((pCodigo != null) ? "COD_ASSOCIACAO_VERBA = '" + pCodigo.ToString() + "'" : "null is null") + " and " +
                                                                                     ((pDescricao != null) ? "COD_ASSOC = " + pDescricao.ToString() : "null is null") + " and " +
                                                                                     ((pDescricao2 != null) ? "NUM_VRBFSS = " + pDescricao2.ToString() : "null is null"))
                                                .AsQueryable<VIEW_DadosProtheus_Cad1>();

                    break;
            }
            return query;
        }

        public int GetDataCount(string pTabela, int? pCodigo, string pDescricao, string pDescricao2)
        {
            return GetWhere(pTabela, pCodigo, pDescricao, pDescricao2).SelectCount();
        }

        public VIEW_DadosProtheus_Cad1 GetDataCad1(string pTabela, int? pCodigo)
        {
            VIEW_DadosProtheus_Cad1 Cad1 = GetWhere(pTabela, pCodigo, null, null).FirstOrDefault();
            if (Cad1 != null) { Cad1.ENTITY_OBJ = pTabela; };
            return Cad1;
        }

        //public void AddEx(VIEW_DadosProtheus_Cad1 newCad1)
        //{
        //    switch (newCad1.ENTITY_OBJ)
        //    {
        //        case "CTA_TBL_PAIS":
        //            CTA_TBL_PAIS target = new CTA_TBL_PAIS();
        //            target.COD = newCad1.CODIGO;
        //            target.NOME = newCad1.DESCRICAO;
        //            m_DbContext.CTA_TBL_PAIS.Add(target);
        //            //m_DbContext.Entry(target).State = System.Data.EntityState.Added;
        //            break;
        //    }
        //}

        //public void RemoveEx(VIEW_DadosProtheus_Cad1 newCad1)
        //{
        //    switch (newCad1.ENTITY_OBJ)
        //    {
        //        case "CTA_TBL_PAIS":
        //            CTA_TBL_PAIS delete = m_DbContext.CTA_TBL_PAIS.Find(newCad1.CODIGO);
        //            m_DbContext.CTA_TBL_PAIS.Remove(delete);
        //            break;
        //    }
        //}

        //public bool Delete<E>(E entity) where E : class
        //{
        //    m_DbContext.Entry(entity).State = System.Data.EntityState.Deleted;
        //    m_DbContext.SaveChanges();
        //    return true;
        //}

        public object FindEx(VIEW_DadosProtheus_Cad1 newCad1)
        {
            object atualiza = null;

            //int iCodigo = 0;
            //int.TryParse(newCad1.CODIGO, out iCodigo);

            switch (newCad1.ENTITY_OBJ)
            {
                case "CTA_TBL_PAIS":
                    atualiza = m_DbContext.CTA_TBL_PAIS.Find(newCad1.CODIGO.ToString());
                    return atualiza;
                case "SUBMASSA":
                    atualiza = m_DbContext.SUBMASSA.Find(newCad1.CODIGO.ToString());
                    return atualiza;
                case "TP_MOVTO":
                    atualiza = m_DbContext.TP_MOVTO.Find(newCad1.CODIGO);
                    return atualiza;
                case "PLN_PRG_SAU":
                    atualiza = m_DbContext.PLN_PRG_SAU.Find(newCad1.CODIGO);
                    return atualiza;
                case "PLN_PRG_PRV":
                    atualiza = m_DbContext.PLN_PRG_PRV.Find(newCad1.CODIGO);
                    return atualiza;
                case "ASSOCIACAO_VERBA":
                    atualiza = m_DbContext.ASSOCIACAO_VERBA.Find(newCad1.CODIGO);
                    return atualiza;
            }
            return null;
        }

        public object MoveEx(VIEW_DadosProtheus_Cad1 newCad1, bool Add = false)
        {
            switch (newCad1.ENTITY_OBJ)
            {
                case "CTA_TBL_PAIS":
                    CTA_TBL_PAIS tarCTA_TBL_PAIS = new CTA_TBL_PAIS() {COD = newCad1.CODIGO.ToString(), 
                                                                       NOME = newCad1.DESCRICAO };
                    if (Add) { m_DbContext.CTA_TBL_PAIS.Add(tarCTA_TBL_PAIS); }
                    return tarCTA_TBL_PAIS;
                case "SUBMASSA":
                    SUBMASSA tarSUBMASSA = new SUBMASSA() {COD_SUBMASSA = newCad1.CODIGO.ToString(), 
                                                           DCR_SUBMASSA = newCad1.DESCRICAO };
                    if (Add) { m_DbContext.SUBMASSA.Add(tarSUBMASSA); }
                    return tarSUBMASSA;
                case "TP_MOVTO":
                    TP_MOVTO tarTP_MOVTO = new TP_MOVTO() { COD_MOVTO = short.Parse(newCad1.CODIGO.ToString()), 
                                                            DSC_MOVTO = newCad1.DESCRICAO };
                    if (Add) { m_DbContext.TP_MOVTO.Add(tarTP_MOVTO); }
                    return tarTP_MOVTO;
                case "PLN_PRG_SAU":
                    PLN_PRG_SAU tarPLN_PRG_SAU = new PLN_PRG_SAU() { COD_PLN = short.Parse(newCad1.CODIGO.ToString()), 
                                                                     PRG = newCad1.DESCRICAO };
                    if (Add) { m_DbContext.PLN_PRG_SAU.Add(tarPLN_PRG_SAU); }
                    return tarPLN_PRG_SAU;
                case "PLN_PRG_PRV":
                    PLN_PRG_PRV tarPLN_PRG_PRV = new PLN_PRG_PRV() { NUM_PLBNF = short.Parse(newCad1.CODIGO.ToString()), 
                                                                     PRG = newCad1.DESCRICAO };
                    if (Add) { m_DbContext.PLN_PRG_PRV.Add(tarPLN_PRG_PRV); }
                    return tarPLN_PRG_PRV;
                case "ASSOCIACAO_VERBA":
                    ASSOCIACAO_VERBA tarASSOCIACAO_VERBA = new ASSOCIACAO_VERBA()
                    {
                        COD_ASSOCIACAO_VERBA = int.Parse(newCad1.CODIGO.ToString()), 
                        COD_ASSOC = short.Parse(newCad1.DESCRICAO),
                        NUM_VRBFSS = int.Parse(newCad1.DESCRICAO2)
                    };
                    if (Add) { m_DbContext.ASSOCIACAO_VERBA.Add(tarASSOCIACAO_VERBA); }
                    return tarASSOCIACAO_VERBA;
            }
            return null;
        }

        public Resultado SaveData(VIEW_DadosProtheus_Cad1 newCad1)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = FindEx(newCad1); // m_DbContext.CCUSTO_SAU.Find(newCad1.CODIGO);

                if (atualiza != null)
                {
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(MoveEx(newCad1));
                } else {
                    MoveEx(newCad1, true);
                }

                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated > 0)
                {
                    if (atualiza != null)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
                    }
                    else
                    {
                        res.Sucesso("Registro inserido com sucesso.");
                    }
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

        public Resultado DeleteData(VIEW_DadosProtheus_Cad1 newCad1)
        {
            Resultado res = new Resultado();
          
            try
            {
                var delete = FindEx(newCad1);

                if (delete != null)
                {
                    //m_DbContext.Entry(delete).State = System.Data.Entity.EntityState.Deleted; //EF 6
                    m_DbContext.Entry(delete).State = System.Data.EntityState.Deleted;
                    int rows_deleted = m_DbContext.SaveChanges();
                    if (rows_deleted > 0)
                    {
                        res.Sucesso("Registro excluído com sucesso.");
                    }
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
