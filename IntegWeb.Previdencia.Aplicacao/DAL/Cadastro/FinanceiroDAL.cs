using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity.ModelConfiguration;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class FinanceiroDAL
    {

        public partial class ADESAO_PLANO_PARTIC_FSS
        {
            public short NUM_PLBNF { get; set; }
        }

        //public partial class PRE_VIEW_ARQ_PAT_VERBA
        //{
        //    public short COD_EMPRS { get; set; }
        //    public int COD_VERBA { get; set; }
        //    public string COD_VERBA_PATROCINA { get; set; }
        //    public short COD_VERBA_PRODUTO { get; set; }
        //    public Nullable<short> NUM_PLBNF { get; set; }
        //    public Nullable<short> IND_DEMONSTRATIVO { get; set; }
        //    public Nullable<short> IND_OBRIGATORIA { get; set; }
        //}

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        //public PRE_TBL_ARQ_PAT_VERBA GetGrupoVerba2(short pCOD_EMPRS, int pCOD_VERBA, short? pCOD_VERBA_PRODUTO = null)
        public List<PRE_TBL_ARQ_PAT_VERBA> GetGrupoVerba2(short pCOD_EMPRS)
        {
            short?[] shortValue = new short?[] { pCOD_EMPRS };

            IQueryable<PRE_TBL_ARQ_PAT_VERBA> query;
            query = from e in m_DbContext.PRE_TBL_ARQ_PAT_VERBA.AsNoTracking()
                    //where (e.COD_EMPRS == pCOD_EMPRS)
                    where (shortValue.Contains(e.COD_EMPRS))
                    //&& (e.COD_VERBA == pCOD_VERBA)
                    //&& (e.COD_VERBA_PRODUTO == pCOD_VERBA_PRODUTO || pCOD_VERBA_PRODUTO == null)
                    select e;

            return query.ToList();
        }

        //public bool GetGrupoVerba2(short pCOD_EMPRS, int pCOD_VERBA, short pCOD_VERBA_PRODUTO)
        //{
        //    short?[] shortValue = new short?[] { pCOD_EMPRS };
        //    return m_DbContext
        //           .PRE_TBL_ARQ_PAT_VERBA
        //           .AsNoTracking()
        //           .Where(u => shortValue.Contains(u.COD_EMPRS) &&
        //                  u.COD_VERBA == pCOD_VERBA &&
        //                  u.COD_VERBA_PRODUTO == pCOD_VERBA_PRODUTO)
        //           .Any();
        //}

        // Função para saber se o particiante tem plano de PREVIDÊNCIA ativo:
        //public ADESAO_PLANO_PARTIC_FSS GetPlanoPartic_ATIVO(int pNUM_MATR_PARTF, short? pNUM_PLBNF)
        //{
        //    IQueryable<ADESAO_PLANO_PARTIC_FSS> query;
        //    query = from a in m_DbContext.ADESAO_PLANO_PARTIC_FSS.AsNoTracking()
        //            where (a.NUM_MATR_PARTF == pNUM_MATR_PARTF)
        //               && (a.NUM_PLBNF == pNUM_PLBNF || pNUM_PLBNF == null)
        //               //&& (a.COD_TPPCP < 5)
        //               //&& (a.COD_SITPAR != 11)
        //               //&& (a.COD_SITPAR != 21)
        //               //&& (a.COD_SITPAR != 23)
        //               && (a.DAT_FIM_ADPLPR==null)
        //            select a;

        //    return query.FirstOrDefault();
        //}

        // Função para saber se o particiante tem plano de PREVIDÊNCIA ativo:
        public short GetPlanoPartic_ATIVO(int pNUM_MATR_PARTF, short? pNUM_PLBNF)
        {
            IQueryable<short> query;
            query = from a in m_DbContext.ADESAO_PLANO_PARTIC_FSS.AsNoTracking()
                    where (a.NUM_MATR_PARTF == pNUM_MATR_PARTF)
                       && (a.NUM_PLBNF == pNUM_PLBNF || pNUM_PLBNF == null)
                       && (a.COD_TPPCP < 5)
                       && (a.COD_SITPAR != 11)
                       && (a.COD_SITPAR != 21)
                       && (a.COD_SITPAR != 23)
                       && (a.DAT_FIM_ADPLPR == null)
                    select a.NUM_PLBNF;

            return query.FirstOrDefault();
        }

        // Função para saber se o particiante tem plano de SAÚDE ativo:
        //public TB_PARTICIP_PLANO GetPlanoSaudePartic_ATIVO(short? pCOD_EMPRS, string pNUM_RGTRO_EMPRG)
        //{

        //    //Otimização da query no engine do Entity Framework:
        //    short?[] shortValue = new short?[] { pCOD_EMPRS };

        //    //Tabela com coluna tipo CHAR(18) preenchida com espaços:
        //    pNUM_RGTRO_EMPRG = pNUM_RGTRO_EMPRG.PadRight(18, ' ');

        //    IQueryable<TB_PARTICIP_PLANO> query;

        //    query = from a in m_DbContext.TB_PARTICIP_PLANO.AsNoTracking()
        //            where (a.SIT_PARTIC_PLANO == "A")
        //               && (a.NUM_SUB_MATRIC == "00")
        //                //&& (a.COD_EMPRS == pCOD_EMPRS || pCOD_EMPRS == null)
        //               && (shortValue.Contains(a.COD_EMPRS) || pCOD_EMPRS == null)
        //                //&& (a.NUM_MATRICULA.Trim() == pNUM_RGTRO_EMPRG)
        //               && (a.NUM_MATRICULA == pNUM_RGTRO_EMPRG)
        //            //orderby a.DAT_ADESAO descending
        //            select a;

        //    //var str = ((System.Data.Objects.ObjectQuery)query).ToTraceString();

        //    return query.FirstOrDefault();
        //    //return query.SingleOrDefault();

        //}

        //Função otimizada:
        public bool Tem_PLANO_SAUDE_Ativo(short? pCOD_EMPRS, string pNUM_RGTRO_EMPRG)
        {
            //Otimização da query no engine do Entity Framework:
            short?[] shortValue = new short?[] { pCOD_EMPRS };

            //Tabela com coluna tipo CHAR(18) preenchida com espaços:
            pNUM_RGTRO_EMPRG = pNUM_RGTRO_EMPRG.PadRight(18, ' ');

            return m_DbContext
                   .TB_PARTICIP_PLANO
                   .AsNoTracking()
                   .Where(u =>                        
                            u.SIT_PARTIC_PLANO == "A" &&
                            u.NUM_SUB_MATRIC == "00" &&
                            shortValue.Contains(u.COD_EMPRS) && 
                            u.NUM_MATRICULA == pNUM_RGTRO_EMPRG
                          )
                   //.Where(u => u.COD_EMPRS == pCOD_EMPRS && u.NUM_MATRICULA == pNUM_RGTRO_EMPRG)
                   .Any();
        }

        // Função para saber se o particiante possui EMPRESTIMO ativo:
        //public IRE_EMPREST_RECEBE GetEmpretimoPartic_ATIVO(short? pCOD_EMPRS, int pNUM_RGTRO_EMPRG, string pANO_MES_REF)
        //{

        //    short?[] shortValue = new short?[] { pCOD_EMPRS };

        //    IQueryable<IRE_EMPREST_RECEBE> query;
        //    query = from a in m_DbContext.IRE_EMPREST_RECEBE.AsNoTracking()
        //            //where (a.COD_EMPRS == pCOD_EMPRS)
        //            where (shortValue.Contains(a.COD_EMPRS))
        //               && (a.NUM_RGTRO_EMPRG == pNUM_RGTRO_EMPRG)
        //               && (a.ANO_MES_REF == pANO_MES_REF)
        //            select a;

        //    return query.FirstOrDefault();
        //}

        //Função otimizada:
        public bool Tem_EMPRESTIMO_Ativo(short? pCOD_EMPRS, int pNUM_RGTRO_EMPRG, string pANO_MES_REF)
        {
            short?[] shortValue = new short?[] { pCOD_EMPRS };
            return m_DbContext
                   .IRE_EMPREST_RECEBE
                   .AsNoTracking()
                   .Where(u => shortValue.Contains(u.COD_EMPRS) && 
                          u.NUM_RGTRO_EMPRG == pNUM_RGTRO_EMPRG &&
                          u.ANO_MES_REF == pANO_MES_REF)
                   .Any();
        }
     
    }
}
