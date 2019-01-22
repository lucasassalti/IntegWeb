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
    public class RecadastramentoDAL
    {

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public class PRE_TBL_RECADASTRAMENTO_BASE
        {
            public DateTime DAT_REF_RECAD { get; set; }
            public int NUM_CONTRATO { get; set; }
            //public DateTime DTH_INCLUSAO { get; set; }
            public int TOTAL_REGISTROS { get; set; }
        }

        public class PRE_VW_RECADASTRAMENTO_ARQ
        {
            public String LINHA { get; set; }
            public DateTime DATREF { get; set; }
        }

        public PRE_TBL_RECADASTRAMENTO GetDataBy(DateTime pDAT_REF_RECAD, short pNUM_CONTRATO, short pCOD_EMPRS, int pNUM_RGTRO_EMPRG, int pNUM_IDNTF_RPTANT)
        {
            IQueryable<PRE_TBL_RECADASTRAMENTO> query =
                from recad in m_DbContext.PRE_TBL_RECADASTRAMENTO
                where (recad.DAT_REF_RECAD == pDAT_REF_RECAD)
                && (recad.NUM_CONTRATO == pNUM_CONTRATO)
                && (recad.COD_EMPRS == pCOD_EMPRS)
                && (recad.NUM_RGTRO_EMPRG == pNUM_RGTRO_EMPRG)
                && (recad.NUM_IDNTF_RPTANT == pNUM_IDNTF_RPTANT)
                && (recad.DTH_EXCLUSAO == null)
                select recad;

            return query.FirstOrDefault();
        }

        public List<PRE_TBL_RECADASTRAMENTO> GetData(int startRowIndex, int maximumRows, int? pEmpresa, int? pMatricula, int? pRepresentante, string pNome, int pSituacao, DateTime? pDtRecad_ini, DateTime? pDtRecad_final, string sortParameter)
        {
            return GetWhere(pEmpresa, pMatricula, pRepresentante, pNome, pSituacao, pDtRecad_ini, pDtRecad_final)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public List<PRE_TBL_RECADASTRAMENTO_BASE> GetRecad_base(int startRowIndex, int maximumRows, DateTime? pDtRef_ini, DateTime? pDtRef_fim, int? pNUM_CONTRATO, string sortParameter)
        {
            return GetWhereBase(pDtRef_ini, pDtRef_fim, pNUM_CONTRATO)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public List<PRE_TBL_RECADASTRAMENTO_BASE> GetRecad_base(int startRowIndex, int maximumRows, string sortParameter)
        {
            return GetWhereBase(null, null, null)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public List<object> GetLista_Recad_base()
        {
            return (from c in GetRecad_base(0, 999, "DAT_REF_RECAD")
                    orderby c.DAT_REF_RECAD descending
                    select (
                    new
                    {
                        Text = Util.Date2String(c.DAT_REF_RECAD) + "    (Contrato " + c.NUM_CONTRATO + ")",
                        Value = Util.Date2String(c.DAT_REF_RECAD) + "," + c.NUM_CONTRATO
                    })).ToList<object>();
        }

        public IQueryable<PRE_TBL_RECADASTRAMENTO> GetWhere(int? pEmpresa, int? pMatricula, int? pRepresentante, string pNome, int pSituacao, DateTime? pDtRecad_ini, DateTime? pDtRecad_final)
        {
            IQueryable<PRE_TBL_RECADASTRAMENTO> query;
            query = from r in m_DbContext.PRE_TBL_RECADASTRAMENTO
                    where (r.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (r.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       && (r.NUM_IDNTF_RPTANT == pRepresentante || pRepresentante == null)
                       && (r.NOME.ToLower().Contains(pNome.ToLower()) || pNome == null)
                       && (r.TIP_RECADASTRAMENTO == pSituacao || pSituacao <= 0)
                       && (r.DAT_RECADASTRAMENTO == null && pSituacao == -1 || pSituacao >= 0)
                       && (r.DAT_RECADASTRAMENTO >= pDtRecad_ini || pDtRecad_ini == null)
                       && (r.DAT_RECADASTRAMENTO <= pDtRecad_final || pDtRecad_final == null)
                       && (r.DTH_EXCLUSAO == null)
                    select r;
            return query;
        }

        public IQueryable<PRE_TBL_RECADASTRAMENTO_BASE> GetWhereBase(DateTime? pDtRef_ini, DateTime? pDtRef_fim, int? pNUM_CONTRATO)
        {
            IQueryable<PRE_TBL_RECADASTRAMENTO_BASE> query;
            query = from r in m_DbContext.PRE_TBL_RECADASTRAMENTO
                    where (r.DAT_REF_RECAD >= pDtRef_ini || pDtRef_ini == null)
                       && (r.DAT_REF_RECAD <= pDtRef_fim || pDtRef_fim == null)
                       && (r.NUM_CONTRATO == pNUM_CONTRATO || pNUM_CONTRATO == null)
                       && (r.DTH_EXCLUSAO == null)
                    group r by new { r.DAT_REF_RECAD, r.NUM_CONTRATO} into g
                    select new PRE_TBL_RECADASTRAMENTO_BASE()
                    {
                        DAT_REF_RECAD = g.Key.DAT_REF_RECAD,
                        NUM_CONTRATO = g.Key.NUM_CONTRATO,
                        TOTAL_REGISTROS = g.Count() 
                    };
            
            return query;
        }

        public int GetDataCount(int? pEmpresa, int? pMatricula, int? pRepresentante, string pNome, int pSituacao, DateTime? pDtRecad_ini, DateTime? pDtRecad_final)
        {
            return GetWhere(pEmpresa, pMatricula, pRepresentante, pNome, pSituacao, pDtRecad_ini, pDtRecad_final).SelectCount();
        }

        public int GetRecad_baseCount(DateTime? pDtRef_ini, DateTime? pDtRef_fim, int? pNUM_CONTRATO)
        {
            return GetWhereBase(pDtRef_ini, pDtRef_fim, pNUM_CONTRATO).SelectCount();
        }

        public int GetRecad_baseCount()
        {
            return GetWhereBase(null, null, null).SelectCount();
        }

        public void Gerar_Nova_Base(DateTime pDAT_REF_RECAD, int pNUM_CONTRATO, string pLOG_INCLUSAO, DateTime pDTH_INCLUSAO)
        {
            m_DbContext.PRE_PRC_GERA_BASE_RECAD(pDAT_REF_RECAD, pNUM_CONTRATO, pLOG_INCLUSAO, pDTH_INCLUSAO);
        }

        public DataTable Exportar(DateTime pDAT_REF_RECAD, int pNUM_CONTRATO, bool DataRecadastroNulo = false)
        {
            IEnumerable<PRE_VW_RECADASTRAMENTO_ARQ> IEnum = m_DbContext.Database.SqlQuery<PRE_VW_RECADASTRAMENTO_ARQ>("SELECT * FROM OWN_FUNCESP.PRE_VW_RECADASTRAMENTO_ARQ " + 
                                                                                                                      "WHERE NUM_CONTRATO = " + pNUM_CONTRATO.ToString() + " AND " +
                                                                                                                      "DATREF = to_date('" + pDAT_REF_RECAD.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') " +
                                                                                                                      (DataRecadastroNulo ? "AND DAT_RECADASTRAMENTO IS NULL" : ""), 0);
                                                                                                                      //"DATREF = '01-JAN-1900' OR " +
//                                                                                                                      "DATREF = '01-JAN-2999')", 0);
            DataTable ret = Util.ToDataTable<PRE_VW_RECADASTRAMENTO_ARQ>(IEnum.ToList());

            DataRow header = ret.NewRow();
            header[0] = "0" + pNUM_CONTRATO.ToString("00000") + DateTime.Now.ToString("ddMMyyyy") + String.Empty.PadRight(956, ' ');

            DataRow footer = ret.NewRow();
            footer[0] = "9" + pNUM_CONTRATO.ToString("00000") + (ret.Rows.Count).ToString("00000") + String.Empty.PadRight(957, ' ');

            ret.Rows.InsertAt(header, 0);
            ret.Rows.Add(footer);

            return ret;
        }

        public Entidades.Resultado UpdateDtRecadastramento(PRE_TBL_RECADASTRAMENTO newRecad)
        {
            Entidades.Resultado res = new Entidades.Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_RECADASTRAMENTO.FirstOrDefault(p => p.NUM_CONTRATO == newRecad.NUM_CONTRATO
                                                                                  && p.COD_EMPRS == newRecad.COD_EMPRS
                                                                                  && p.NUM_RGTRO_EMPRG == newRecad.NUM_RGTRO_EMPRG
                                                                                  && p.NUM_IDNTF_RPTANT == newRecad.NUM_IDNTF_RPTANT
                                                                                  && p.DAT_RECADASTRAMENTO == null
                                                                                  && p.DTH_EXCLUSAO == null);

                if (atualiza != null)
                {                    
                    DateTime? auxDAT_RECADASTRAMENTO = newRecad.DAT_RECADASTRAMENTO;
                    DateTime auxDTH_INCLUSAO = newRecad.DTH_INCLUSAO;
                    string auxLOG_INCLUSAO = newRecad.LOG_INCLUSAO;
                    //newRecad2 = atualiza.Clone();
                    //newRecad2 = Util.Clone<PRE_TBL_RECADASTRAMENTO>(atualiza);
                    //newRecad2 = Util.ShallowCopyEntity<PRE_TBL_RECADASTRAMENTO>();
                    newRecad.TIP_RECADASTRAMENTO = 1; //Arquivo
                    newRecad = atualiza.Clone();
                    newRecad.DAT_RECADASTRAMENTO = auxDAT_RECADASTRAMENTO;
                    newRecad.DTH_INCLUSAO = auxDTH_INCLUSAO;
                    newRecad.LOG_INCLUSAO = auxLOG_INCLUSAO;
                    newRecad.TIP_RECADASTRAMENTO = 1;
                    atualiza.DTH_EXCLUSAO = auxDTH_INCLUSAO; // Desativa o registro atual
                    atualiza.LOG_EXCLUSAO = auxLOG_INCLUSAO;
                    m_DbContext.PRE_TBL_RECADASTRAMENTO.Add(newRecad); // Insere um registro novo ativo (DTH_EXCLUSAO=null)
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
                    }
                }
                else
                {
                    res.Sucesso("Registro não localizado ou recadastro já efetuado.");
                }

                //}
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public Entidades.Resultado Update(PRE_TBL_RECADASTRAMENTO recad)
        {
            Entidades.Resultado res = new Entidades.Resultado();
            try
            {
                var atualiza = m_DbContext.PRE_TBL_RECADASTRAMENTO.FirstOrDefault(p => p.NUM_CONTRATO == recad.NUM_CONTRATO
                                                                                  && p.COD_EMPRS == recad.COD_EMPRS
                                                                                  //&& p.NUM_MATR_PARTF == recad.NUM_MATR_PARTF
                                                                                  && p.NUM_RGTRO_EMPRG == recad.NUM_RGTRO_EMPRG
                                                                                  && p.NUM_IDNTF_RPTANT == recad.NUM_IDNTF_RPTANT
                                                                                  && p.DTH_EXCLUSAO == null);

                //bool iguais = (atualiza != null) ? atualiza.Comparar(newDebConta) : false;
                //bool iguais = atualiza.Comparar(newDebConta);

                //if (iguais)
                //{
                //    res.Sucesso("Registro não inserido. Já existe um 'igual'.");
                //}
                //else
                //{

                if (atualiza != null)
                {
                    atualiza.DTH_EXCLUSAO = recad.DTH_INCLUSAO; // Desativa o registro atual
                    atualiza.LOG_EXCLUSAO = recad.LOG_INCLUSAO;
                }
                m_DbContext.PRE_TBL_RECADASTRAMENTO.Add(recad); // Insere um registro novo ativo (DTH_EXCLUSAO=null)
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
                    }
                //}
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado DeleteData(short NUM_CONTRATO, short COD_EMPRS, int NUM_RGTRO_EMPRG, int NUM_IDNTF_RPTANT, int NUM_MATR_PARTF, string user)
        {
            Resultado res = new Resultado();
          

            try
            {
                var delete = m_DbContext.PRE_TBL_RECADASTRAMENTO.FirstOrDefault(p => p.NUM_CONTRATO ==NUM_CONTRATO
                                                                                  && p.COD_EMPRS == COD_EMPRS
                                                                                  && p.NUM_MATR_PARTF == NUM_MATR_PARTF
                                                                                  && p.NUM_IDNTF_RPTANT == NUM_IDNTF_RPTANT
                                                                                  && p.DTH_EXCLUSAO == null);

                if (delete != null)
                {
                    delete.LOG_EXCLUSAO = user;
                    delete.DTH_EXCLUSAO = DateTime.Now;
                }
                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated > 0)
                {
                    res.Sucesso("Registro excluído com sucesso.");
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;

        }

        public List<PRE_TBL_RECADASTRAMENTO_TIPO> CarregaTipos()
        {
            IQueryable<PRE_TBL_RECADASTRAMENTO_TIPO> query;
            query = from t in m_DbContext.PRE_TBL_RECADASTRAMENTO_TIPO                    
                    select t;

            return query.ToList();
        }

    }
}
