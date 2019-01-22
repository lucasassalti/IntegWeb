using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Framework;
using IntegWeb.Entidades;
using System.Data;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Capitalizacao
{
    public class CadParamSimuladorDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public class PRE_TBL_POR_UREF_view
        {
            public short EMPRESA { get; set; }
            public short PLANO { get; set; }
            public string DESC_PLANO { get; set; }
            public short SEXO { get; set; }
            public short TB_ATUARIAL { get; set; }
            public short? CODIGO_UM { get; set; }
            public string DESCRICAO_UM { get; set; }
            public DateTime? DT_REFERENCIA { get; set; }
        }

        #region .: ABA 1 :.

        public List<PRE_TBL_POR_UREF> GetAllData()
        {

            IQueryable<PRE_TBL_POR_UREF> query;

            query = from tb in m_DbContext.PRE_TBL_POR_UREF
                    where (tb.DTH_EXCLUSAO == null)
                    select tb;
            return query.ToList();
        }

        public PRE_TBL_POR_UREF GetUrefData(short? emp, short? plano, short? sexo)
        {
            IQueryable<PRE_TBL_POR_UREF> query;

            query = from tb in m_DbContext.PRE_TBL_POR_UREF
                    where (tb.EMPRESA == emp)
                    && (tb.PLANO == plano)
                    && (tb.SEXO == sexo)
                    && (tb.DTH_EXCLUSAO == null)
                    select tb;
            return query.FirstOrDefault();
        }

        public List<PRE_TBL_POR_UREF_view> GetData(int startRowIndex, int maximumRows, short? emp, short? plano, short? codUm, DateTime? datIni, DateTime? datFim, string sortParameter)
        {
            return GetWhere(emp, plano, codUm, datIni, datFim)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<PRE_TBL_POR_UREF_view> GetWhere(short? emp, short? plano, short? codUm, DateTime? datIni, DateTime? datFim)
        {

            IQueryable<PRE_TBL_POR_UREF_view> query;

            query = from tb in m_DbContext.PRE_TBL_POR_UREF
                    from pbf in m_DbContext.PLANO_BENEFICIO_FSS
                    where tb.PLANO == pbf.NUM_PLBNF
                    && (tb.EMPRESA == emp || emp == null)
                    && (tb.PLANO == plano || plano == null)
                    && (tb.CODIGO_UM == codUm || codUm == null)
                    && (tb.DT_REFERENCIA >= datIni || datIni == null)
                    && (tb.DT_REFERENCIA <= datFim || datFim == null)
                    && (tb.DTH_EXCLUSAO == null)
                    && (tb.LOG_EXCLUSAO == null)
                    select new PRE_TBL_POR_UREF_view()
                    {
                        EMPRESA = tb.EMPRESA,
                        PLANO = tb.PLANO,
                        DESC_PLANO = pbf.DCR_PLBNF,
                        TB_ATUARIAL = tb.TB_ATUARIAL,
                        SEXO = tb.SEXO,
                        CODIGO_UM = tb.CODIGO_UM,
                        DESCRICAO_UM = tb.DESCRICAO_UM,
                        DT_REFERENCIA = tb.DT_REFERENCIA
                    };

            return query;
        }

        public int GetDataCount(short? emp, short? plano, short? codUm, DateTime? datIni, DateTime? datFim)
        {
            return GetWhere(emp, plano, codUm, datIni, datFim).SelectCount();
        }

        public List<PLANO_BENEFICIO_FSS> GetPlano()
        {
            IQueryable<PLANO_BENEFICIO_FSS> query;

            query = from pb in m_DbContext.PLANO_BENEFICIO_FSS
                    select pb;

            return query.ToList();

        }

        public List<UNIDADE_MONETARIA> GetUnidMonetaria()
        {
            IQueryable<UNIDADE_MONETARIA> query;

            query = from u in m_DbContext.UNIDADE_MONETARIA
                    select u;

            return query.ToList();
        }

        public UNIDADE_MONETARIA GetNomeUnidadeMonetaria(short cod_um)
        {
            UNIDADE_MONETARIA res = m_DbContext.UNIDADE_MONETARIA.FirstOrDefault(m => m.COD_UM == cod_um);

            return res;
        }

        public Resultado InserirUref(PRE_TBL_POR_UREF obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.PRE_TBL_POR_UREF.FirstOrDefault(ure => ure.EMPRESA == obj.EMPRESA
                                                                    && ure.PLANO == obj.PLANO
                                                                    && ure.SEXO == obj.SEXO
                                                                    && ure.DTH_EXCLUSAO == null);

                if (atualiza == null)
                {
                    // inclui novo registro na tabela
                    atualiza = obj;
                    m_DbContext.PRE_TBL_POR_UREF.Add(atualiza);

                    int rows_update = m_DbContext.SaveChanges();

                    if (rows_update > 0)
                    {
                        res.Sucesso(String.Format("{0} registro inserido.", rows_update));
                    }

                }
                else if (atualiza != null)
                {
                    //Desativa linha atual
                    atualiza.DTH_EXCLUSAO = obj.DTH_INCLUSAO;
                    atualiza.LOG_EXCLUSAO = obj.LOG_INCLUSAO;

                    //Insere nova linha
                    m_DbContext.PRE_TBL_POR_UREF.Add(obj);

                    int rows_update = m_DbContext.SaveChanges();

                    if (rows_update > 0)
                    {
                        res.Sucesso(String.Format("{0} registros atualizados.", rows_update));
                    }
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado ExcluirUref(PRE_TBL_POR_UREF obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.PRE_TBL_POR_UREF.FirstOrDefault(uref => uref.EMPRESA == obj.EMPRESA
                                                                    && uref.PLANO == obj.PLANO
                                                                    && uref.SEXO == obj.SEXO
                                                                    && uref.DTH_EXCLUSAO == null);

                if (atualiza != null)
                {
                    atualiza.DTH_EXCLUSAO = obj.DTH_INCLUSAO;
                    atualiza.LOG_EXCLUSAO = obj.LOG_INCLUSAO;

                    int rows_update = m_DbContext.SaveChanges();

                    if (rows_update > 0)
                    {
                        res.Sucesso(String.Format("{0} registros excluido.", rows_update));
                    }
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));

            }

            return res;
        }

        public Resultado AlterarDataReferencia(PRE_TBL_POR_UREF objUref)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.PRE_TBL_POR_UREF.FirstOrDefault(uref => uref.DTH_EXCLUSAO == null);

                if (atualiza != null)
                {
                    atualiza.DT_REFERENCIA = objUref.DT_REFERENCIA;
                    atualiza.DTH_EXCLUSAO = objUref.DTH_INCLUSAO;
                    atualiza.LOG_EXCLUSAO = objUref.LOG_INCLUSAO;
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));

            }
            return res;
        }

        public decimal? CarregarValor(short codUm, DateTime? DataReferencia)
        {
            PRE_TBL_POR_UREF objUref = new PRE_TBL_POR_UREF();
            COTACAO_MES_UM obj = m_DbContext.COTACAO_MES_UM.FirstOrDefault(m => m.COD_UM == codUm && m.DAT_CMESUM == DataReferencia);
            if (obj != null)
            {
                objUref.VALOR = obj.VLR_CMESUM;      
                return objUref.VALOR;
            }
            return objUref.VALOR = null;
        }

        //public decimal? CalculaValorMedio(short? codUm, DateTime DataReferencia) 
        //{
        //    PRE_TBL_POR_UREF objUref = new PRE_TBL_POR_UREF();

        //    DateTime DataReferencia_36 = DataReferencia.AddMonths(-36);
        //    DateTime DataReferencia_1 = DataReferencia.AddMonths(-1);

        //    IQueryable<COTACAO_MES_UM> query_indice_cor;
        //    query_indice_cor = from tb in m_DbContext.COTACAO_MES_UM
        //            where (tb.COD_UM == codUm)
        //            && (tb.DAT_CMESUM >= DataReferencia_36)
        //            && (tb.DAT_CMESUM <= DataReferencia_1)
        //            orderby tb.DAT_CMESUM descending
        //            select tb;

        //    decimal valorMedioQuery = query_indice_cor.Select(n => n.VLR_CMESUM ?? 0).ToList().Average();
        //    return Math.Round(valorMedioQuery,2);
        //}

        public List<COTACAO_MES_UM> GetHistUnidadeMonetaria(short? codUm, DateTime DataReferencia)
        {

            DateTime DataReferencia_36 = DataReferencia.AddMonths(-36);
            DateTime DataReferencia_1 = DataReferencia.AddMonths(-1);

            IQueryable<COTACAO_MES_UM> query_indice_cor;
            query_indice_cor = from tb in m_DbContext.COTACAO_MES_UM
                               where (tb.COD_UM == codUm)
                               && (tb.DAT_CMESUM >= DataReferencia_36)
                               && (tb.DAT_CMESUM <= DataReferencia_1)
                               orderby tb.DAT_CMESUM descending
                               select tb;

            return query_indice_cor.ToList();
        }

        #endregion

        #region .: ABA 2 :.

        public List<PRE_TBL_POR_CONT> GetDataTxContrib(int startRowIndex, int maximumRows, short? plano, string sortParameter)
        {
            return GetWhereTxContrib(plano).
                   GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<PRE_TBL_POR_CONT> GetWhereTxContrib(short? plano)
        {
            IQueryable<PRE_TBL_POR_CONT> query;

            query = from c in m_DbContext.PRE_TBL_POR_CONT
                    where (c.PLANO == plano || plano == null)
                    && c.DTH_EXCLUSAO == null
                    select c;

            return query;
        }

        public int GetDataCountTxContrib(short? plano)
        {
            return GetWhereTxContrib(plano).SelectCount();
        }

        public Resultado AtualizarCont(PRE_TBL_POR_CONT obj)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_POR_CONT.FirstOrDefault(PCT => PCT.PLANO == obj.PLANO
                    && PCT.DTH_EXCLUSAO == null);

                if (atualiza == null)
                {
                    atualiza = obj;
                    m_DbContext.PRE_TBL_POR_CONT.Add(obj);

                    int rows_update = m_DbContext.SaveChanges();

                    if (rows_update > 0)
                    {
                        res.Sucesso(String.Format("{0} registro inserido.", rows_update));
                    }
                }
                else if (atualiza != null)
                {
                    atualiza.DTH_EXCLUSAO = obj.DTH_INCLUSAO;
                    atualiza.LOG_EXCLUSAO = obj.LOG_INCLUSAO;

                    m_DbContext.PRE_TBL_POR_CONT.Add(obj);

                    int rows_update = m_DbContext.SaveChanges();

                    if (rows_update > 0)
                    {
                        res.Sucesso(String.Format("{0} registros atualizados.", rows_update));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado ExcluirCont(PRE_TBL_POR_CONT obj)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_POR_CONT.FirstOrDefault(tbc => tbc.PLANO == obj.PLANO
                    && tbc.DTH_EXCLUSAO == null);

                if (atualiza != null)
                {
                    atualiza.LOG_EXCLUSAO = obj.LOG_INCLUSAO;
                    atualiza.DTH_EXCLUSAO = obj.DTH_INCLUSAO;

                    int rows_update = m_DbContext.SaveChanges();

                    if (rows_update > 0)
                    {
                        res.Sucesso(String.Format("{0} registros excluido.", rows_update));
                    }
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));

            }

            return res;
        }

        #endregion

        #region .: ABA 3 :.

        public Resultado InserirParametro(PRE_TBL_POR_DREF obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualizaAb3 = m_DbContext.PRE_TBL_POR_DREF.FirstOrDefault(dref => dref.DTH_EXCLUSAO == null);

                if (atualizaAb3 == null)
                {
                    atualizaAb3 = obj;
                    m_DbContext.PRE_TBL_POR_DREF.Add(atualizaAb3);

                    int row = m_DbContext.SaveChanges();

                    if (row > 0)
                    {
                        res.Sucesso("Registro inserido com sucesso.");
                    }
                }

                if (atualizaAb3 != null)
                {
                    atualizaAb3.LOG_EXCLUSAO = obj.LOG_INCLUSAO;
                    atualizaAb3.DTH_EXCLUSAO = obj.DTH_INCLUSAO; //Exclusão lógica  
                    m_DbContext.PRE_TBL_POR_DREF.Add(obj);

                    int row = m_DbContext.SaveChanges();

                    if (row > 0)
                    {
                        res.Sucesso("Registro inserido com sucesso.");
                    }
                }

            }
            catch (Exception ex)
            {

                res.Erro(ex.Message);
            }

            return res;
        }

        #endregion
    }
}
