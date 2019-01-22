using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Concessao
{
  

    public class CadAcaoJudicialDAL
    {
        public class DADOS_CADASTRAIS_view
        {
            public string NOME { get; set; }
            public int? EMPRESA { get; set; }
            public int? MATRICULA { get; set; }
            public long? CPF { get; set; }
            public int? PLANO { get; set; }
            public DateTime? DIB { get; set; }

        }

        public class DADOS_ESTATISTICOS_view
        {
            public string MES { get; set; }
            public string ANO { get; set; }
            public int QTD { get; set; }
        }


        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();
        ConexaoOracle objConexao = new ConexaoOracle();
        List<PRE_TBL_ACAO_JUDIC> result = new List<PRE_TBL_ACAO_JUDIC>();

        public List<PRE_TBL_ACAO_JUDIC> GetData(int startRowIndex, int maximumRows, int? pEmpresa, int? pMatricula, int? filType, string filValue, string sortParameter)
        {
            return GetWhere(pEmpresa, pMatricula, filType, filValue)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<PRE_TBL_ACAO_JUDIC> GetWhere(int? pEmpresa, int? pMatricula, int? filType, string filValue)
        {

            long lCPF_EMPRG = 0;
            long.TryParse(filValue, out lCPF_EMPRG);

            IQueryable<PRE_TBL_ACAO_JUDIC> query;

            query = from a in m_DbContext.PRE_TBL_ACAO_JUDIC
                    where (a.COD_EMPRS == pEmpresa || pEmpresa == null)
                    && (a.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                    && ((a.NOM_PARTICIP.ToUpper().Contains(filValue.ToUpper()) && filType == 1) || filType != 1 || filValue == null)
                    && ((a.NRO_PROCESSO.ToLower().Contains(filValue.ToLower()) && filType == 3) || filType != 3 || filValue == null)
                    && ((a.CPF_EMPRG == lCPF_EMPRG && filType == 2) || filType != 2 || filValue == null)
                    && (a.DTH_EXCLUSAO == null)
                    select a;

            return query;
        }

        public int GetDataCount(int? pEmpresa, int? pMatricula, int? filType, string filValue)
        {
            return GetWhere(pEmpresa, pMatricula, filType, filValue).Count();
        }

        public PRE_TBL_ACAO_JUDIC GetAcaoJudic(int idReg)
        {
            IQueryable<PRE_TBL_ACAO_JUDIC> query =
               from ac in m_DbContext.PRE_TBL_ACAO_JUDIC
               where (ac.DTH_EXCLUSAO == null)
                  && (ac.ID_REG == idReg)
               select ac;

            return query.FirstOrDefault();

        }

        public List<PRE_TBL_ACAO_JUDIC> GetGroup(int startRowIndex, int maximumRows, string pEmpresa, string pMatricula, int? filType, string filValue, string sortParameter)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            PRE_TBL_ACAO_JUDIC reg = new PRE_TBL_ACAO_JUDIC();
            DataTable dt = new DataTable();
            string searchfield = "";

            try
            {
                StringBuilder querysql = new StringBuilder();
                StringBuilder searchsql = new StringBuilder();

                querysql.Append(" select distinct (select max(x.ID_REG) from own_funcesp.PRE_TBL_ACAO_JUDIC x where x.NRO_PROCESSO = a.NRO_PROCESSO and x.NUM_RGTRO_EMPRG = a.NUM_RGTRO_EMPRG and x.DTH_EXCLUSAO is null) as ID_REG, ");
                querysql.Append(" a.COD_EMPRS, a.NUM_RGTRO_EMPRG, a.NOM_PARTICIP, a.NRO_PROCESSO, f.DESC_TIPLTO, ");
                querysql.Append(" (select min(b.NUM_SEQ_PROC) from PRE_TBL_ACAO_JUDIC b where b.NUM_RGTRO_EMPRG = a.NUM_RGTRO_EMPRG and b.DTH_EXCLUSAO is null) as MIN_PAG, ");
                querysql.Append(" (select max(c.NUM_SEQ_PROC) from PRE_TBL_ACAO_JUDIC c where c.NUM_RGTRO_EMPRG = a.NUM_RGTRO_EMPRG and c.DTH_EXCLUSAO is null) as MAX_PAG ");
                querysql.Append(" from PRE_TBL_ACAO_JUDIC a inner join PRE_TBL_ACAO_VR_TIPLTO f on a.COD_TIPLTO = f.COD_TIPLTO ");
                querysql.Append(" where a.DTH_EXCLUSAO is null ");

                if (!(string.IsNullOrEmpty(pEmpresa))) { searchsql.Append((searchsql.Length <= 0 ? " and (" : "") + " a.COD_EMPRS like '%" + pEmpresa + "%'"); }
                if (!(string.IsNullOrEmpty(pMatricula))) { searchsql.Append((searchsql.Length <= 0 ? " and (" : " or ") + " a.NUM_RGTRO_EMPRG = '" + pMatricula + "'"); }
                if (!(string.IsNullOrEmpty(filValue)))
                {
                    searchfield = filType == 1 ? " UPPER(a.NOM_PARTICIP) like UPPER('%" : (filType == 2 ? " a.CPF_EMPRG like '%" : " a.NRO_PROCESSO like '%");
                    searchsql.Append((searchsql.Length <= 0 ? "and (" : "") + searchfield + filValue + (filType == 1 ? "%')" : "%'"));
                }
                searchsql.Append((searchsql.Length <= 0 ? "" : ")"));

                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(querysql.ToString() + searchsql.ToString());
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    reg = new PRE_TBL_ACAO_JUDIC();
                    reg.COD_EMPRS = Convert.ToInt16(dt.Rows[i]["COD_EMPRS"].ToString());
                    reg.NUM_RGTRO_EMPRG = Convert.ToInt64(dt.Rows[i]["NUM_RGTRO_EMPRG"].ToString());
                    reg.NOM_PARTICIP = dt.Rows[i]["NOM_PARTICIP"].ToString();
                    reg.NRO_PROCESSO = dt.Rows[i]["NRO_PROCESSO"].ToString();
                    reg.MIN_PAG = Convert.ToInt32(dt.Rows[i]["MIN_PAG"].ToString());
                    reg.MAX_PAG = Convert.ToInt32(dt.Rows[i]["MAX_PAG"].ToString());
                    reg.NUM_SEQ_PROC = reg.MAX_PAG;
                    //reg.ID_REG = GetID((int)reg.COD_EMPRS, (int)reg.NUM_RGTRO_EMPRG, reg.MAX_PAG, reg.NRO_PROCESSO);
                    reg.ID_REG = Convert.ToInt32(dt.Rows[i]["ID_REG"].ToString());
                    reg.TIP_PLTO = dt.Rows[i]["DESC_TIPLTO"].ToString();
                    result.Add(reg);
                }
            }
            return result;
        }
        public int GetGroupCount(int? pEmpresa, int? pMatricula, int? filType, string filValue) { return result.Count; }
        public int GetLastPage(int pNumRgtroEmprg)
        {
            IQueryable<PRE_TBL_ACAO_JUDIC> query =
               from ac in m_DbContext.PRE_TBL_ACAO_JUDIC
               where (ac.DTH_EXCLUSAO == null)
                  && (ac.NUM_RGTRO_EMPRG == pNumRgtroEmprg)
               select ac;

            return Convert.ToInt32(query.Max(reg => reg.NUM_SEQ_PROC));

        }

        public List<PRE_TBL_ACAO_VR_TIPLTO> GetFato()
        {
            IQueryable<PRE_TBL_ACAO_VR_TIPLTO> query;

            query = from un in m_DbContext.PRE_TBL_ACAO_VR_TIPLTO
                    select un;

            return query.ToList();
        }

        public List<PLANO_BENEFICIO_FSS> GetPlano()
        {
            IQueryable<PLANO_BENEFICIO_FSS> query;

            query = from un in m_DbContext.PLANO_BENEFICIO_FSS
                    select un;

            return query.ToList();
        }

        public decimal GetMaxPk()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.PRE_TBL_ACAO_JUDIC.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_REG) + 1;
            return maxPK;
        }

        public Resultado Inserir(PRE_TBL_ACAO_JUDIC obj)
        {
            Resultado res = new Resultado();

            try
            {

                var atualiza = m_DbContext.PRE_TBL_ACAO_JUDIC.FirstOrDefault(a => a.ID_REG == obj.ID_REG);


                if (atualiza == null)
                {

                    obj.ID_REG = GetMaxPk();
                    m_DbContext.PRE_TBL_ACAO_JUDIC.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }

                }
                else
                {
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(obj);

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

        public Resultado DeleteAcaoJudic(PRE_TBL_ACAO_JUDIC obj)
        {
            Resultado res = new Resultado();

            try
            {
                var delete = m_DbContext.PRE_TBL_ACAO_JUDIC.Find(obj.ID_REG);
                delete.DTH_EXCLUSAO = DateTime.Today;
                delete.LOG_EXCLUSAO = obj.LOG_EXCLUSAO;

                int rows_delete = m_DbContext.SaveChanges();

                if (rows_delete > 0)
                {
                    ReviewAcaoJudic((int)delete.NUM_RGTRO_EMPRG, delete.NRO_PROCESSO);

                    res.Sucesso("Serviço Deletado com Sucesso");
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

        public void ReviewAcaoJudic(int pNumRgtroEmprg, string pNroProcesso)
        {
            IQueryable<PRE_TBL_ACAO_JUDIC> query =
               from ac in m_DbContext.PRE_TBL_ACAO_JUDIC
               where (ac.DTH_EXCLUSAO == null)
                  && (ac.NUM_RGTRO_EMPRG == pNumRgtroEmprg)
                  && (ac.NRO_PROCESSO == pNroProcesso)
               select ac;

            int lCount = 1;
            foreach (PRE_TBL_ACAO_JUDIC reg in query.ToList().OrderBy(r => r.NUM_SEQ_PROC))
            {
                reg.NUM_SEQ_PROC = lCount;
                lCount++;
            }
            m_DbContext.SaveChanges();

        }

        public int GetID(int pEmpresa, int pMatricula, int numSeq, string numProcesso)
        {
            IQueryable<decimal> query =
              from ac in m_DbContext.PRE_TBL_ACAO_JUDIC
              where (ac.DTH_EXCLUSAO == null)
                 && (ac.COD_EMPRS == pEmpresa)
                 && (ac.NUM_RGTRO_EMPRG == pMatricula)
                 && (ac.NUM_SEQ_PROC == numSeq)
                 && (ac.NRO_PROCESSO.Contains(numProcesso))
              select ac.ID_REG;

            return Convert.ToInt16(query.FirstOrDefault());
        }

        public DADOS_CADASTRAIS_view GetNome(int? pEmpresa, int? pMatricula, long? pCpf)
        {

               IQueryable<DADOS_CADASTRAIS_view> query = from emp in m_DbContext.EMPREGADO
                                                         from bp in m_DbContext.BENEFICIO_PARTIC_FSS.Where(bbp => bbp.NUM_MATR_PARTF == emp.NUM_MATR_PARTF).DefaultIfEmpty()
                        where (emp.COD_EMPRS == pEmpresa || pEmpresa == null)
                        && (emp.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                        && (emp.NUM_CPF_EMPRG == pCpf || pCpf == null)
                        && (bp.NUM_SQDEST_BFPART == null)
                        select new DADOS_CADASTRAIS_view
                             {
                                 NOME = emp.NOM_EMPRG,
                                 MATRICULA = emp.NUM_RGTRO_EMPRG,
                                 CPF = emp.NUM_CPF_EMPRG,
                                 EMPRESA = emp.COD_EMPRS,
                                 PLANO = bp.NUM_PLBNF,
                                 DIB = bp.DAT_INICIO_BFPART
                             };

            return query.FirstOrDefault();
        
        }

        public List<PRE_TBL_ACAO_JUDIC> GetDadosRespostaPendentes()
        {
            IQueryable<PRE_TBL_ACAO_JUDIC> query;

            query = from aj in m_DbContext.PRE_TBL_ACAO_JUDIC
                    where aj.DAT_RESP == null
                    select aj;

            return query.ToList();
        }

        public List<DADOS_ESTATISTICOS_view> GetDadosEstatistico(string dataIni, string dataFim)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            IEnumerable<DADOS_ESTATISTICOS_view> dadosEstatisticos = new List<DADOS_ESTATISTICOS_view>();

            try
            {
                IEnumerable<DADOS_ESTATISTICOS_view> tbAcaoJudic = m_DbContext.Database.SqlQuery<DADOS_ESTATISTICOS_view>
                    (@"select 
                        to_char(extract(month from dat_solic)) as MES,
                        to_char(extract(year from dat_solic)) as ANO, 
                        count(extract(month from dat_solic)) as QTD 
                      from OWN_FUNCESP.PRE_TBL_ACAO_JUDIC where to_date(dat_solic, 'DD-MM-RRRR') between to_date('" + dataIni + "', 'DD-MM-RRRR') and to_date('" + dataFim + "', 'DD-MM-RRRR') group by to_char(to_date(dat_solic, 'DD-MM-RRRR'), 'Month'), extract(year from dat_solic), extract(month from dat_solic) order by extract(month from dat_solic)");
                
                return tbAcaoJudic.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas ao buscar dados do Relatório Estatístico: \n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        public List<PRE_TBL_ACAO_JUDIC> GetDadosGeral(string plano, string fatorGerador, string tipoAndamento, string responsavel, string localArquivo, DateTime? dataResposta, DateTime? dataPrazo)
        {
            IQueryable<PRE_TBL_ACAO_JUDIC> query;

            short? vPlano = Util.String2Short(plano);
            short? vFator = Util.String2Short(fatorGerador);
            decimal? vLocalArq = Util.String2Decimal(localArquivo);

            query = from aj in m_DbContext.PRE_TBL_ACAO_JUDIC
                    from pb in m_DbContext.PLANO_BENEFICIO_FSS.Where(pb => pb.NUM_PLBNF == aj.NUM_PLBNF).DefaultIfEmpty()
                    from at in m_DbContext.PRE_TBL_ACAO_VR_TIPLTO.Where(at => at.COD_TIPLTO == aj.COD_TIPLTO).DefaultIfEmpty()
                    where (pb.NUM_PLBNF == vPlano || vPlano == null)
                    && (at.COD_TIPLTO == vFator || vFator == null)
                    && (aj.USU_RESPON.ToUpper().Contains(responsavel.ToUpper()) || responsavel == null)
                    && (aj.TIP_PLTO.ToUpper().Contains(tipoAndamento.ToUpper()) || tipoAndamento == null)                 
                    && (aj.DAT_PRAZO == dataPrazo || dataPrazo == null)
                    && (aj.DAT_RESP == dataResposta || dataResposta == null)
                    && (aj.LOCAL_ARQ == vLocalArq || localArquivo == null)
                    select aj;

            return query.ToList();
        }
    }
}
