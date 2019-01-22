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
    public class ArqPatrocinaCargaDAL
    {

        internal PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        internal EMPREGADO LoadEmpregado(short pCOD_EMPRS, int pNUM_RGTRO_EMPRG)
        {
            EMPREGADO emp = null;
            try
            {
                //var atualiza = m_DbContext.EMPREGADO.FirstOrDefault(p => p.COD_EMPRS == pCOD_EMPRS &&
                                                                         //p.NUM_RGTRO_EMPRG == pNUM_RGTRO_EMPRG);
                emp = m_DbContext.EMPREGADO.Find(pCOD_EMPRS, pNUM_RGTRO_EMPRG);
            }
            catch (Exception ex)
            {
                //res.Erro(Util.GetInnerException(ex));
            }

            return emp;
        }

        internal Resultado InsertData(EMPREGADO newEmpregado)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                m_DbContext.EMPREGADO.Add(newEmpregado);
                res.Sucesso("INSERT", 1);
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        internal AFASTAMENTO LoadAfastamento(short pCOD_EMPRS, int pNUM_RGTRO_EMPRG, DateTime pDAT_INAFT_AFAST)
        {
            AFASTAMENTO afas = null;
            try
            {
                afas = m_DbContext.AFASTAMENTO.Find(pCOD_EMPRS, pNUM_RGTRO_EMPRG, pDAT_INAFT_AFAST);
            }
            catch (Exception ex)
            {
                //res.Erro(Util.GetInnerException(ex));
            }

            return afas;
        }

        internal Resultado InsertData(AFASTAMENTO newAfastamento)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                m_DbContext.AFASTAMENTO.Add(newAfastamento);
                res.Sucesso("INSERT", 1);
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        //internal ORGAO LoadOrgao(short pCOD_EMPRS, int pNUM_ORGAO)
        internal ORGAO LoadOrgao(int pNUM_ORGAO)
        {
            ORGAO orgao = null;
            try
            {
                orgao = m_DbContext.ORGAO.Find(pNUM_ORGAO);
                //orgao = m_DbContext.ORGAO.FirstOrDefault(p => p.COD_EMPRS == pCOD_EMPRS &&
                //                                              p.NUM_ORGAO == pNUM_ORGAO);
            }
            catch (Exception ex)
            {
                //res.Erro(Util.GetInnerException(ex));
            }

            return orgao;
        }

        //internal List<ATT_CHARGER_DEPARA> Carrega_cache_DePara_OrgaoLotacao_table(short pCOD_EMPRS, string pNUM_ORGAO_DE = null)
        //{
        //    IQueryable<ATT_CHARGER_DEPARA> query;
        //    query = from c in m_DbContext.ATT_CHARGER_DEPARA
        //            where (c.CODAPLICACAO == 1)
        //               && (c.CODTABELA == 181) // ORGAO
        //               && (c.CODCOLUNA == 1)   // NUM_ORGAO
        //               && (c.CODEMPRESA == pCOD_EMPRS)
        //               && (c.CONTEUDODE == pNUM_ORGAO_DE || pNUM_ORGAO_DE == null)
        //            select c;
        //    return query.ToList();
        //}

        //internal ATT_CHARGER_DEPARA GetOrgaoLotacao_DE_PARA2(short pCOD_EMPRS, string pNUM_ORGAO_DE)
        //{

        //    IQueryable<ATT_CHARGER_DEPARA> query;
        //    query =  from c in m_DbContext.ATT_CHARGER_DEPARA
        //            where (c.CODAPLICACAO == 1)
        //               && (c.CODTABELA == 181 ) // ORGAO
        //               && (c.CODCOLUNA == 1 )   // NUM_ORGAO
        //               && (c.CODEMPRESA == pCOD_EMPRS)
        //               && (c.CONTEUDODE == pNUM_ORGAO_DE)
        //            select c;

        //    return query.FirstOrDefault();
        //}

        internal Resultado InsertData(ORGAO newOrgao)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                m_DbContext.ORGAO.Add(newOrgao);
                res.Sucesso("INSERT", 1);
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        internal Resultado InsertData(ATT_CHARGER_DEPARA newDePara)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                //var atualiza = m_DbContext.ATT_CHARGER_DEPARA.FirstOrDefault(p => p.CONTEUDODE == newDePara.CONTEUDODE &&
                //                                                                  p.CODAPLICACAO == newDePara.CODAPLICACAO &&
                //                                                                  p.CODTABELA == newDePara.CODTABELA &&
                //                                                                  p.CODCOLUNA == newDePara.CODCOLUNA &&
                //                                                                  p.CODEMPRESA == newDePara.CODEMPRESA &&
                //                                                                  p.CONTEUDODE == newDePara.CONTEUDODE);
                //if (atualiza == null)
                //{
                    m_DbContext.ATT_CHARGER_DEPARA.Add(newDePara);
                //}
                //else
                //{
                //    m_DbContext.Entry(atualiza).CurrentValues.SetValues(newDePara);
                //}
                //m_DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }

        internal Resultado InsertData(FICHA_FINANCEIRA newFicha)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                m_DbContext.FICHA_FINANCEIRA.Add(newFicha);
                res.Sucesso("INSERT", 1);
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        //internal Resultado SaveData(FICHA_FINANCEIRA newFicha)
        //{
        //    Resultado res = new Entidades.Resultado();
        //    try
        //    {
        //        var atualiza = m_DbContext.FICHA_FINANCEIRA.FirstOrDefault(p => p.COD_EMPRS == newFicha.COD_EMPRS &&
        //                                                                        p.NUM_RGTRO_EMPRG == newFicha.NUM_RGTRO_EMPRG &&
        //                                                                        p.ANO_COMPET_VERFIN == newFicha.ANO_COMPET_VERFIN &&
        //                                                                        p.MES_COMPET_VERFIN == newFicha.MES_COMPET_VERFIN &&
        //                                                                        p.COD_VERBA == newFicha.COD_VERBA);

        //        if (atualiza == null)
        //        {
        //            m_DbContext.FICHA_FINANCEIRA.Add(newFicha);
        //            res.Sucesso("INSERT", 1);
        //        }
        //        else
        //        {
        //            //m_DbContext.Entry(atualiza).CurrentValues.SetValues(newFicha);
        //            //atualiza.COD_EMPRS = newFicha.
        //            //atualiza.NUM_RGTRO_EMPRG = int.Parse(pNUM_RGTRO_EMPRG);
        //            if (newFicha._NAO_ATUALIZAR.IndexOf("COD_VERBA") == -1)
        //            {
        //                atualiza.COD_VERBA = newFicha.COD_VERBA;
        //            }
        //            atualiza.ANO_COMPET_VERFIN = newFicha.ANO_COMPET_VERFIN;
        //            atualiza.MES_COMPET_VERFIN = newFicha.MES_COMPET_VERFIN;
        //            atualiza.VLR_VERFIN = newFicha.VLR_VERFIN;
        //            atualiza.ANO_PAGTO_VERFIN = newFicha.ANO_PAGTO_VERFIN;
        //            atualiza.MES_PAGTO_VERFIN = newFicha.MES_PAGTO_VERFIN;
        //            atualiza.DAT_PAGTO_VERFIN = newFicha.DAT_PAGTO_VERFIN;
        //            res.Sucesso("UPDATE", 2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Erro(Util.GetInnerException(ex));
        //    }

        //    return res;
        //}

        internal Resultado SaveChanges()
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                // TESTE SCRIPT ORACLE
                // IEnumerable<String> IEnum = m_DbContext.Database.SqlQuery<String>("SELECT SYS_CONTEXT ('USERENV', 'MODULE') MODULO FROM DUAL", 0).ToList();

                int rows_updated = m_DbContext.SaveChanges();                
                //if (rows_updated > 0)
                //{
                res.Sucesso("Carga realizada com sucesso.");
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

        internal int GetMaxPk_CARGA()
        {
            int maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ARQ_PATROCINA_CARGA.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_ARQ_PAT_CARGA);
            return maxPK;
        }

        internal int GetMaxPk_ORGAO()
        {
            int maxPK = 0;
            maxPK = m_DbContext.ORGAO.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.NUM_ORGAO);
            return maxPK;
        }

        internal Resultado RegistraCarga(PRE_TBL_ARQ_PATROCINA_CARGA newCarga)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                int iMaxPk_CARGA = GetMaxPk_CARGA();
                long iMaxPk_CRITICA = new ArqPatrocinadoraDAL().GetMaxPk_CRITICA();
                iMaxPk_CARGA++;

                newCarga.PRE_TBL_ARQ_PATROCINA_CRITICA.ToList()
                    .ForEach(cr =>
                    {
                        iMaxPk_CRITICA++;
                        cr.COD_ARQ_PAT_CARGA = iMaxPk_CARGA;
                        cr.COD_ARQ_PAT_CRITICA = iMaxPk_CRITICA;
                        cr.DCR_CRITICA = Util.String2Limit(cr.DCR_CRITICA, 0, 200);
                    });

                newCarga.COD_ARQ_PAT_CARGA = iMaxPk_CARGA;
                m_DbContext.PRE_TBL_ARQ_PATROCINA_CARGA.Add(newCarga);
                int rows_updated = m_DbContext.SaveChanges();                
                res.Sucesso("Registro de carga inserido com sucesso.");
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        internal Resultado LinhaCarregada(PRE_TBL_ARQ_PATROCINA_LINHA uptLinha, DateTime DAT_IMPORTADO)
        {
            Resultado res = new Entidades.Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_ARQ_PATROCINA_LINHA.FirstOrDefault(p => p.COD_ARQ_PAT_LINHA == uptLinha.COD_ARQ_PAT_LINHA);

                if (atualiza != null)
                {
                    atualiza.DAT_IMPORTADO = DAT_IMPORTADO;              
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

    }
}
