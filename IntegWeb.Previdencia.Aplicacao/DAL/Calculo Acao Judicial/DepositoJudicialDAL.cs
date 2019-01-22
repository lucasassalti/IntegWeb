using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial
{
    public class DepositoJudicialDAL
    {

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<PRE_TBL_DEPOSITO_JUDIC> GetData(int startRowIndex, int maximumRows, int? pEmpresa, int? pMatricula, int? filType, string filValue, string sortParameter)
        {
            return GetWhere(pEmpresa, pMatricula, filType, filValue)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<PRE_TBL_DEPOSITO_JUDIC> GetWhere(int? pEmpresa, int? pMatricula, int? filType, string filValue)
        {
            long lCPF_EMPRG = 0;
            long.TryParse(filValue, out lCPF_EMPRG);

            IQueryable<PRE_TBL_DEPOSITO_JUDIC> query =
                from dj in m_DbContext.PRE_TBL_DEPOSITO_JUDIC
                where (dj.DTH_EXCLUSAO == null)
                   && (dj.COD_EMPRS == pEmpresa || pEmpresa == null)
                   && (dj.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                   && ((dj.NOM_EMPRG.ToLower().Contains(filValue.ToLower()) && filType == 1) || filType != 1 || filValue == null)
                   && ((dj.CPF_EMPRG == lCPF_EMPRG && filType == 2) || filType != 2 || filValue == null)
                   && ((dj.NRO_PROCESSO.ToLower().Contains(filValue.ToLower()) && filType == 3) || filType != 3 || filValue == null)
                select dj;

            return query;
        }

        public PRE_TBL_DEPOSITO_JUDIC GetData(int pCOD_DEPOSITO_JUDIC)
        {
            IQueryable<PRE_TBL_DEPOSITO_JUDIC> query =
                from dj in m_DbContext.PRE_TBL_DEPOSITO_JUDIC
                where (dj.DTH_EXCLUSAO == null)
                   && (dj.COD_DEPOSITO_JUDIC == pCOD_DEPOSITO_JUDIC)
                select dj;

            return query.FirstOrDefault();
        }

        public int GetDataCount(int? pEmpresa, int? pMatricula, int? filType, string filValue)
        {
            return GetWhere(pEmpresa, pMatricula, filType, filValue).SelectCount();
        }

        public Resultado SaveData(PRE_TBL_DEPOSITO_JUDIC newDebConta)
        {
            Resultado res = new Resultado();
            try
            {

                //var atualiza = m_DbContext.PRE_TBL_DEPOSITO_JUDIC.FirstOrDefault(p => p.COD_DEPOSITO_JUDIC == newDebConta.COD_DEPOSITO_JUDIC);
                var atualiza = m_DbContext.PRE_TBL_DEPOSITO_JUDIC.Find(newDebConta.COD_DEPOSITO_JUDIC);
                bool iguais = (atualiza != null) ? atualiza.Comparar(newDebConta) : false;

                if (iguais)
                {
                    //res.Sucesso("Não inserido. Já existe um registro 'igual'.");
                    res.Sucesso("Registro atualizado com sucesso. (*) ");
                }
                else
                {
                    newDebConta.PRE_TBL_DEPOSITO_JUDIC_PGTO.Clear();
                    if (atualiza == null)
                    {
                        newDebConta.DAT_CADASTRO = newDebConta.DTH_INCLUSAO;
                        newDebConta.COD_DEPOSITO_JUDIC = GetMaxPk() + 1;
                        m_DbContext.PRE_TBL_DEPOSITO_JUDIC.Add(newDebConta);
                    }
                    else
                    {
                        newDebConta.DAT_CADASTRO = atualiza.DAT_CADASTRO;
                        m_DbContext.Entry(atualiza).CurrentValues.SetValues(newDebConta);
                        //atualiza = newDebConta;
                    }
                     // Inseri um registro novo ativo (DTH_EXCLUSAO=null)
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(ex.Message);
            }
            return res;

        }

        public Resultado DeleteData(int pCOD_DEPOSITO_JUDIC, string user)
        {
            Resultado res = new Resultado();
            var deleta = m_DbContext.PRE_TBL_DEPOSITO_JUDIC.Find(pCOD_DEPOSITO_JUDIC);
            DateTime dthExclusao = DateTime.Now;
            
            if (deleta!=null)
            {

                // Exclui os filhos:
                //deleta.PRE_TBL_DEPOSITO_JUDIC_PGTO.ToList().ForEach(p => m_DbContext.PRE_TBL_DEPOSITO_JUDIC_PGTO.Remove(p));
                //m_DbContext.PRE_TBL_DEPOSITO_JUDIC.Remove(deleta);

                deleta.PRE_TBL_DEPOSITO_JUDIC_PGTO.ToList().ForEach(p => { p.DTH_EXCLUSAO = dthExclusao; p.LOG_EXCLUSAO = user; });
                deleta.DTH_EXCLUSAO = dthExclusao;
                deleta.LOG_EXCLUSAO = user;

                int rows_deleted = m_DbContext.SaveChanges();
                if (rows_deleted > 0)
                {
                    res.Sucesso("Registro excluído com sucesso.");
                }
            }
            return res;
        }

        public int GetMaxPk()
        {
            int maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_DEPOSITO_JUDIC.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_DEPOSITO_JUDIC);
            return maxPK;
        }

        public Resultado SaveData2(PRE_TBL_DEPOSITO_JUDIC newDebConta)
        {
            Resultado res = new Resultado();
            try
            {
                //var atualiza = m_DbContext.PRE_TBL_DEPOSITO_JUDIC.FirstOrDefault(p => p.COD_EMPRS == newDebConta.COD_EMPRS
                //                                                              && p.NUM_RGTRO_EMPRG == newDebConta.NUM_RGTRO_EMPRG
                //                                                              && p.NUM_MATR_PARTF == newDebConta.NUM_MATR_PARTF
                //                                                              && p.DTH_EXCLUSAO == null);

                var atualiza = m_DbContext.PRE_TBL_DEPOSITO_JUDIC.FirstOrDefault(p => p.COD_DEPOSITO_JUDIC == newDebConta.COD_DEPOSITO_JUDIC
                                                                                 && p.DTH_EXCLUSAO == null);

                //bool iguais = atualiza.Comparar(newDebConta);
                bool iguais = (atualiza != null) ? atualiza.Comparar(newDebConta) : false;

                if (iguais)
                {
                    res.Sucesso("Não inserido. Já existe um registro 'igual'.");
                }
                else
                {
                    if (atualiza != null)
                    {
                        newDebConta.COD_DEPOSITO_JUDIC = atualiza.COD_DEPOSITO_JUDIC;
                        newDebConta.DAT_CADASTRO = atualiza.DAT_CADASTRO;
                        newDebConta.DTH_INCLUSAO = DateTime.Now;
                        atualiza.DTH_EXCLUSAO = newDebConta.DTH_INCLUSAO; // Desativa o registro atual

                        foreach(PRE_TBL_DEPOSITO_JUDIC_PGTO iPgto in atualiza.PRE_TBL_DEPOSITO_JUDIC_PGTO)
                        {
                            iPgto.DTH_EXCLUSAO = atualiza.DTH_EXCLUSAO;
                        }

                        foreach (PRE_TBL_DEPOSITO_JUDIC_PGTO newPgto in newDebConta.PRE_TBL_DEPOSITO_JUDIC_PGTO)
                        {
                            newPgto.DTH_INCLUSAO = newDebConta.DTH_INCLUSAO;
                            newDebConta.PRE_TBL_DEPOSITO_JUDIC_PGTO.Add(newPgto);
                        }

                    }
                    else
                    {
                        int maxPK = 0;
                        if (m_DbContext.PRE_TBL_DEPOSITO_JUDIC.ToList().Count() > 0)
                        {
                            maxPK = m_DbContext.PRE_TBL_DEPOSITO_JUDIC.Max(m => m.COD_DEPOSITO_JUDIC);
                        }
                        newDebConta.COD_DEPOSITO_JUDIC = maxPK + 1;

                        PRE_TBL_DEPOSITO_JUDIC_PGTO newPgto = newDebConta.PRE_TBL_DEPOSITO_JUDIC_PGTO.FirstOrDefault();
                        newPgto.COD_DEPOSITO_JUDIC_PGTO = GetMaxPk() + 1;
                        newPgto.COD_DEPOSITO_JUDIC = newDebConta.COD_DEPOSITO_JUDIC;
                        //newPgto.DTH_INCLUSAO_FK = newDebConta.DTH_INCLUSAO;

                    }
                    m_DbContext.PRE_TBL_DEPOSITO_JUDIC.Add(newDebConta); // Inseri um registro novo ativo (DTH_EXCLUSAO=null)
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
                    }
                }

                //Salva o item:
                //if (newDebConta.PRE_TBL_DEPOSITO_JUDIC_PGTO.Count() > 0)
                //{
                //    PRE_TBL_DEPOSITO_JUDIC_PGTO newPgto = newDebConta.PRE_TBL_DEPOSITO_JUDIC_PGTO.FirstOrDefault<PRE_TBL_DEPOSITO_JUDIC_PGTO>();
                //    newPgto.COD_DEPOSITO_JUDIC = newDebConta.COD_DEPOSITO_JUDIC;
                //    newPgto.DTH_INCLUSAO_FK = (atualiza ?? newDebConta).DTH_INCLUSAO;
                //    res = SaveDataPgto(newDebConta.PRE_TBL_DEPOSITO_JUDIC_PGTO.FirstOrDefault<PRE_TBL_DEPOSITO_JUDIC_PGTO>());
                //}
                

            }
            catch (Exception ex)
            {
                res.Erro(ex.Message);
            }
            return res;

        }

        #region Pagamentos Deposito Judicial

        public List<PRE_TBL_DEPOSITO_JUDIC_PGTO> GetDataPgto(int startRowIndex, int maximumRows, int pCOD_DEPOSITO_JUDIC, string sortParameter)
        {
            return GetWherePgto(pCOD_DEPOSITO_JUDIC)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<PRE_TBL_DEPOSITO_JUDIC_PGTO> GetWherePgto(int pCOD_DEPOSITO_JUDIC)
        {
            IQueryable<PRE_TBL_DEPOSITO_JUDIC_PGTO> query =
                from dj in m_DbContext.PRE_TBL_DEPOSITO_JUDIC_PGTO
                where (dj.DTH_EXCLUSAO == null)
                   && (dj.COD_DEPOSITO_JUDIC == pCOD_DEPOSITO_JUDIC)
                select dj;
            return query;
        }

        public PRE_TBL_DEPOSITO_JUDIC_PGTO GetDataPgto(int pCOD_DEPOSITO_JUDIC_PGTO)
        {
            IQueryable<PRE_TBL_DEPOSITO_JUDIC_PGTO> query =
                from dj in m_DbContext.PRE_TBL_DEPOSITO_JUDIC_PGTO
                where (dj.DTH_EXCLUSAO == null)
                   && (dj.COD_DEPOSITO_JUDIC_PGTO == pCOD_DEPOSITO_JUDIC_PGTO)
                select dj;

            return query.FirstOrDefault();
        }

        public int GetDataCountPgto(int pCOD_DEPOSITO_JUDIC)
        {
            return GetWherePgto(pCOD_DEPOSITO_JUDIC).SelectCount();
        }

        public Resultado SaveDataPgto(PRE_TBL_DEPOSITO_JUDIC_PGTO newDebContaPgto)
        {
            Resultado res = new Resultado();
            try
            {

                var atualiza = m_DbContext.PRE_TBL_DEPOSITO_JUDIC_PGTO.Find(newDebContaPgto.COD_DEPOSITO_JUDIC_PGTO);
                bool iguais = (atualiza != null) ? atualiza.Comparar(newDebContaPgto) : false;

                if (iguais)
                {
                    res.Sucesso("Não inserido. Já existe um registro 'igual'.");
                }
                else
                {

                    if (atualiza == null)
                    {
                        newDebContaPgto.COD_DEPOSITO_JUDIC_PGTO = GetMaxPkPgto() + 1;
                        m_DbContext.PRE_TBL_DEPOSITO_JUDIC_PGTO.Add(newDebContaPgto);
                    }
                    else
                    {
                        m_DbContext.Entry(atualiza).CurrentValues.SetValues(newDebContaPgto);
                        //atualiza = newDebConta;
                    }

                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
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

        public Resultado DeleteDataPgto(int pCOD_DEPOSITO_JUDIC_PGTO, string user)
        {
            Resultado res = new Resultado();
            var deleta = m_DbContext.PRE_TBL_DEPOSITO_JUDIC_PGTO.Find(pCOD_DEPOSITO_JUDIC_PGTO);
            DateTime dthExclusao = DateTime.Now;

            if (deleta != null)
            {

                deleta.DTH_EXCLUSAO = dthExclusao;
                deleta.LOG_EXCLUSAO = user;

                int rows_deleted = m_DbContext.SaveChanges();
                if (rows_deleted > 0)
                {
                    res.Sucesso("Registro excluído com sucesso.");
                }
            }
            return res;
        }

        public int GetMaxPkPgto()
        {
            int maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_DEPOSITO_JUDIC_PGTO.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_DEPOSITO_JUDIC_PGTO);
            return maxPK;
        }

        #endregion
           
    }
}
