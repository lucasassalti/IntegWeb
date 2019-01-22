using IntegWeb.Entidades;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Framework;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using System;
using System.Data;
using System.Data.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Data.OracleClient;

namespace IntegWeb.Financeira.Aplicacao.DAL
{
    public class EmprestimoDescontoDAL
    {

        public EntitiesConn m_DbContext = new EntitiesConn();

        public partial class AAT_TBL_EMPRESTIMO_DESCONTO_view : AAT_TBL_EMPRESTIMO_DESCONTO
        {
            public Nullable<short> NUM_DIGVR_EMPRG { get; set; }
            public string DCR_STATUS
            {
                get
                {
                    switch (COD_STATUS)
                    {
                        default:
                        case 1:
                            return "Novo";
                        case 2:
                            return "Calculado";
                        case 3:
                            return "Rejeitado";
                        case 4:
                            return "Gerado TXT";
                    }
                }
            }
            public string DCR_TIPO {
                get
                {
                    switch (COD_TIPO.ToUpper())
                    {
                        default:
                        case "A":
                            return "ASSISTIDO";
                        case "P":
                            return "PENSIONISTA";
                        case "D":
                            return "DESLIGADO";
                    }
                }
            }
            public string NOM_EMPRG { get; set; }
        }

        public List<AAT_TBL_EMPRESTIMO_DESCONTO_view> GetData(int startRowIndex, int maximumRows, short? pAno_ref, short? pMes_ref, short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome, short? pCod_Status, string sortParameter)
        {
            return GetWhere(pAno_ref, pMes_ref, pEmpresa, pMatricula, pRepresentante, pCpf, pNome, pCod_Status)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<AAT_TBL_EMPRESTIMO_DESCONTO_view> GetWhere(short? pAno_ref, short? pMes_ref, short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome, short? pCod_Status)
        {
            IQueryable<AAT_TBL_EMPRESTIMO_DESCONTO_view> query;
            query = from ed in m_DbContext.AAT_TBL_EMPRESTIMO_DESCONTO
                    join emp in m_DbContext.EMPREGADO on new {ed.COD_EMPRS, ed.NUM_RGTRO_EMPRG} equals new { emp.COD_EMPRS, emp.NUM_RGTRO_EMPRG}
                    //from emp in m_DbContext.EMPREGADO
                    //where (ed.COD_EMPRS == emp.COD_EMPRS)
                    //   && (ed.NUM_RGTRO_EMPRG == emp.NUM_RGTRO_EMPRG)
                    where (ed.DTH_EXCLUSAO == null)
                       && (ed.ANO_REF == pAno_ref || pAno_ref == null)
                       && (ed.MES_REF == pMes_ref || pMes_ref == null)
                       && (ed.COD_STATUS == pCod_Status || pCod_Status == null)
                       && (ed.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (ed.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       //&& (ed.NUM_MATR_PARTF == pMatricula || pMatricula == null)
                       && (ed.NUM_IDNTF_RPTANT == 0 && pRepresentante == null)
                       && (ed.NUM_CPF == pCpf || pCpf == null)
                       && (emp.NOM_EMPRG.ToLower().Contains(pNome.ToLower()) || pNome == null)
                       && (ed.NUM_RGTRO_EMPRG < 1000000000)
                    select new AAT_TBL_EMPRESTIMO_DESCONTO_view()
                    {
                        COD_EMPRESTIMO_DESCONTO = ed.COD_EMPRESTIMO_DESCONTO,
                        COD_EMPRS = ed.COD_EMPRS,
                        NUM_RGTRO_EMPRG = ed.NUM_RGTRO_EMPRG,
                        NUM_MATR_PARTF = ed.NUM_MATR_PARTF,
                        NUM_IDNTF_RPTANT = ed.NUM_IDNTF_RPTANT,
                        ANO_REF = ed.ANO_REF,
                        MES_REF = ed.MES_REF,
                        COD_STATUS = ed.COD_STATUS,
                        COD_TIPO = ed.COD_TIPO,
                        NUM_CPF = ed.NUM_CPF,
                        VLR_DIVIDA = ed.VLR_DIVIDA,
                        VLR_PROV = ed.VLR_PROV,
                        VLR_DESC = ed.VLR_DESC,
                        VLR_LIQ = ed.VLR_LIQ,
                        LIMITE = ed.LIMITE,
                        VLR_DO_MES = ed.VLR_DO_MES,
                        VLR_DIVIDA_POSS = ed.VLR_DIVIDA_POSS,
                        VLR_ABN_PROV = ed.VLR_ABN_PROV,
                        VLR_ABN_DESC = ed.VLR_ABN_DESC,
                        VLR_ABN_LIQ = ed.VLR_ABN_LIQ,
                        VLR_CARGA = ed.VLR_CARGA,
                        DTH_GERACAO = ed.DTH_GERACAO,
                        LOG_INCLUSAO = ed.LOG_INCLUSAO,
                        DTH_INCLUSAO = ed.DTH_INCLUSAO,
                        DTH_EXCLUSAO = ed.DTH_EXCLUSAO,
                        NOM_EMPRG = emp.NOM_EMPRG,
                        NUM_DIGVR_EMPRG = emp.NUM_DIGVR_EMPRG
                    };

            query = query.Union(
                    from ed in m_DbContext.AAT_TBL_EMPRESTIMO_DESCONTO
                    //join rep in m_DbContext.REPRES_UNIAO_FSS on new { ed.COD_EMPRS, ed.NUM_RGTRO_EMPRG, ed.NUM_IDNTF_RPTANT } equals new { rep.COD_EMPRS, rep.NUM_RGTRO_EMPRG, rep.NUM_IDNTF_RPTANT }
                    from rep in m_DbContext.REPRES_UNIAO_FSS
                    where (ed.COD_EMPRS == rep.COD_EMPRS || rep.COD_EMPRS == null)
                       && (ed.NUM_RGTRO_EMPRG == rep.NUM_RGTRO_EMPRG || rep.NUM_RGTRO_EMPRG == null)
                       && (ed.NUM_IDNTF_RPTANT == rep.NUM_IDNTF_RPTANT)
                       && (ed.DTH_EXCLUSAO == null)
                       && (ed.ANO_REF == pAno_ref || pAno_ref == null)
                       && (ed.MES_REF == pMes_ref || pMes_ref == null)
                       && (ed.COD_STATUS == pCod_Status || pCod_Status == null)
                       && (ed.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (ed.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       //&& (ed.NUM_MATR_PARTF == pMatricula || pMatricula == null)
                       && (ed.NUM_IDNTF_RPTANT == pRepresentante || pRepresentante == null)
                       && (ed.NUM_IDNTF_RPTANT > 0)
                       && (ed.NUM_CPF == pCpf || pCpf == null)
                       && (rep.NOM_REPRES.ToLower().Contains(pNome.ToLower()) || pNome == null)
                       && (ed.NUM_RGTRO_EMPRG < 1000000000)
                    select new AAT_TBL_EMPRESTIMO_DESCONTO_view()
                    {
                        COD_EMPRESTIMO_DESCONTO = ed.COD_EMPRESTIMO_DESCONTO,
                        COD_EMPRS = ed.COD_EMPRS,
                        NUM_RGTRO_EMPRG = ed.NUM_RGTRO_EMPRG,
                        NUM_MATR_PARTF = ed.NUM_MATR_PARTF,
                        NUM_IDNTF_RPTANT = ed.NUM_IDNTF_RPTANT,
                        ANO_REF = ed.ANO_REF,
                        MES_REF = ed.MES_REF,
                        COD_STATUS = ed.COD_STATUS,
                        COD_TIPO = ed.COD_TIPO,
                        NUM_CPF = ed.NUM_CPF,
                        VLR_DIVIDA = ed.VLR_DIVIDA,
                        VLR_PROV = ed.VLR_PROV,
                        VLR_DESC = ed.VLR_DESC,
                        VLR_LIQ = ed.VLR_LIQ,
                        LIMITE = ed.LIMITE,
                        VLR_DO_MES = ed.VLR_DO_MES,
                        VLR_DIVIDA_POSS = ed.VLR_DIVIDA_POSS,
                        VLR_ABN_PROV = ed.VLR_ABN_PROV,
                        VLR_ABN_DESC = ed.VLR_ABN_DESC,
                        VLR_ABN_LIQ = ed.VLR_ABN_LIQ,
                        VLR_CARGA = ed.VLR_CARGA,
                        DTH_GERACAO = ed.DTH_GERACAO,
                        LOG_INCLUSAO = ed.LOG_INCLUSAO,
                        DTH_INCLUSAO = ed.DTH_INCLUSAO,
                        DTH_EXCLUSAO = ed.DTH_EXCLUSAO,
                        NOM_EMPRG = rep.NOM_REPRES,
                        NUM_DIGVR_EMPRG = 0
                    }
            );
            return query;
        }

        public int GetDataCount(short? pAno_ref, short? pMes_ref, short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome, short? pCod_Status)
        {
            return GetWhere(pAno_ref, pMes_ref, pEmpresa, pMatricula, pRepresentante, pCpf, pNome, pCod_Status).SelectCount();
        }

        public long GetMaxPk()
        {
            long maxPK = 0;
            maxPK = m_DbContext.AAT_TBL_EMPRESTIMO_DESCONTO.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_EMPRESTIMO_DESCONTO) + 1;
            return maxPK;
        }

        public AAT_TBL_EMPRESTIMO_DESCONTO GetLancamentoDesconto(int iCOD_EMPRESTIMO_DESCONTO)
        {
            IQueryable<AAT_TBL_EMPRESTIMO_DESCONTO> query;
            query = from ed in m_DbContext.AAT_TBL_EMPRESTIMO_DESCONTO
                    where (ed.COD_EMPRESTIMO_DESCONTO == iCOD_EMPRESTIMO_DESCONTO)
                    select ed;
            return query.FirstOrDefault();
        }

        public Resultado Persistir(List<AAT_TBL_EMPRESTIMO_DESCONTO> lsDescontos)
        {
            Resultado res = new Resultado();
            try{
                //ObjectParameter P_DCR_NOM_ARQ = new ObjectParameter("P_DCR_NOM_ARQ", lsDebConta[0].DCR_NOM_ARQ);
                foreach (AAT_TBL_EMPRESTIMO_DESCONTO newDesconto in lsDescontos)
                {
                    m_DbContext.AAT_TBL_EMPRESTIMO_DESCONTO.Add(newDesconto);
                }
                m_DbContext.SaveChanges();
                res.Sucesso(lsDescontos.Count + " registro(s) importado(s) com sucesso.");
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

        public Resultado SaveData(AAT_TBL_EMPRESTIMO_DESCONTO updLancamentoDesc)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.AAT_TBL_EMPRESTIMO_DESCONTO.Find(updLancamentoDesc.COD_EMPRESTIMO_DESCONTO);

                if (atualiza != null)
                {
                    m_DbContext.Entry(atualiza).CurrentValues.SetValues(updLancamentoDesc);
                    m_DbContext.SaveChanges();
                    res.Sucesso("Lançamento salvo com sucesso!");
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

        public Resultado DeleteData(int iCOD_EMPRESTIMO_DESCONTO)
        {
            Resultado res = new Resultado();
            try
            {
                var delete = m_DbContext.AAT_TBL_EMPRESTIMO_DESCONTO.Find(iCOD_EMPRESTIMO_DESCONTO);
                m_DbContext.AAT_TBL_EMPRESTIMO_DESCONTO.Remove(delete);
                m_DbContext.SaveChanges();
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

        public Resultado DeleteData(short pAno_ref, short pMes_ref)
        {
            Resultado res = new Resultado();
            try
            {
                m_DbContext.AAT_TBL_EMPRESTIMO_DESCONTO
                    .Where(e => e.ANO_REF == pAno_ref && e.MES_REF == pMes_ref)
                    .ToList()
                    .ForEach( e=> m_DbContext.AAT_TBL_EMPRESTIMO_DESCONTO.Remove(e));
                m_DbContext.SaveChanges();
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

        public int Processar(String Mes_Ref, String Ano_Ref, DateTime DtComplementados, DateTime DtSuplementados)
        {
            int vReturn = 0;
            ConexaoOracle objConexao = new ConexaoOracle();
            try
            {
                objConexao.AdicionarParametro("p_Ano_Ref", Ano_Ref);
                objConexao.AdicionarParametro("p_Mes_Ref", Mes_Ref);
                objConexao.AdicionarParametro("p_DtComplementados", DtComplementados);
                objConexao.AdicionarParametro("p_DtSuplementados", DtSuplementados);
                objConexao.AdicionarParametroOut("p_retorno");
                //System.Data.OracleClient.OracleDataAdapter adpt = objConexao.ExecutarAdapter("AAT_PKG_SERASA_PEFIN.GERA_REMESSA");
                objConexao.ExecutarNonQuery("AAT_PRC_PROCESSA_EMPREST_DESC");
                if (objConexao.ReturnParemeterOut().Value != null)
                {
                    vReturn = int.Parse(objConexao.ReturnParemeterOut().Value.ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return vReturn;


        }

        //internal decimal getValorRecMes(int pMatricula, int pRepresentante, string ano_mes_ref)
        //{

        //    //select nvl(vlr_do_mes,0)
        //    //  into v_vlr_do_mes
        //    //  from int_real.ire_emprest_recebe@fcespp.world
        //    // where num_matr_partf          = reg.num_matr_partf
        //    //   and nvl(num_idntf_rptant,0) = reg.num_idntf_rptant 
        //    //   and ano_mes_Ref             = 201707; 

        //    decimal ret = 0;
        //    IQueryable<IRE_EMPREST_RECEBE> query;
        //    query = from e in m_DbContext.IRE_EMPREST_RECEBE
        //            where (e.NUM_MATR_PARTF == pMatricula)
        //               && (e.NUM_IDNTF_RPTANT == pRepresentante)
        //               && (e.ANO_MES_REF == ano_mes_ref)
        //            select e;

        //    IRE_EMPREST_RECEBE ier = query.FirstOrDefault();
        //    if (ier != null)
        //    {
        //        ret = ier.VLR_DO_MES ?? 0;
        //    }
        //    return ret;

        //}

        //internal decimal getValorProventos(int pMatricula, int pRepresentante, DateTime DtComplementados, DateTime DtSuplementados)
        //{

        //     //select nvl(sum(f.vlr_calcul_fcfnpt),0) 
        //     //  into v_vlr_prov  
        //     //  from att.fch_finan_partic_fss@fcespp.world f
        //     // where f.num_matr_partf          = reg.num_matr_partf
        //     //   and nvl(f.num_idntf_rptant,0) = nvl(reg.num_idntf_rptant,0)
        //     //   and f.num_vrbfss  in(51000,52000,52004,52012,53000,
        //     //                        53012,54000,54004,54005,54020,
        //     //                        54024,54025,54040,54051,54060,
        //     //                        54065,54080,54093,54099,54120,
        //     //                        54125,54132,54137,54140,54144,
        //     //                        54145,54160,54164,54165,54171,
        //     //                        54172,54178,54180,54200,54204,
        //     //                        54260,54272,54360,54365,55000,
        //     //                        57000,58000)
        //     //   and f.dat_pagto_fcfnpt   between to_date('28-jul-2017')--- incluir a data do pagamento de complementados do mês anterior
        //     //                                and to_date('31-jul-2017')--- incluir a data do pagamento do suplementados mês anterior
        //     //   and to_char(f.dat_refer_fcfnpt,'YYYYMM') = to_char(f.dat_pagto_fcfnpt,'YYYYMM');

        //    decimal ret = 0;

        //    int[] aVerbas = new int[] { 51000,52000,52004,52012,53000,
        //                                53012,54000,54004,54005,54020,
        //                                54024,54025,54040,54051,54060,
        //                                54065,54080,54093,54099,54120,
        //                                54125,54132,54137,54140,54144,
        //                                54145,54160,54164,54165,54171,
        //                                54172,54178,54180,54200,54204,
        //                                54260,54272,54360,54365,55000,
        //                                57000,58000 };

        //    IQueryable<FCH_FINAN_PARTIC_FSS> query;
        //    query = from f in m_DbContext.FCH_FINAN_PARTIC_FSS
        //            where (f.NUM_MATR_PARTF == pMatricula)
        //               && (f.NUM_IDNTF_RPTANT == pRepresentante)
        //               && (aVerbas.Contains(f.NUM_VRBFSS))
        //               && (EntityFunctions.TruncateTime(f.DAT_PAGTO_FCFNPT) >= DtComplementados)
        //               && (EntityFunctions.TruncateTime(f.DAT_PAGTO_FCFNPT) <= DtSuplementados)
        //               //&& (f.DAT_REFER_FCFNPT.ToString("YYYYMM") == "TESTE")
        //               && (EntityFunctions.DiffMonths(f.DAT_REFER_FCFNPT, f.DAT_PAGTO_FCFNPT) == 0)
        //               && (EntityFunctions.DiffYears(f.DAT_REFER_FCFNPT, f.DAT_PAGTO_FCFNPT) == 0)
        //               //&& (f.DAT_REFER_FCFNPT.Month == f.DAT_PAGTO_FCFNPT.Month)
        //            select f;

        //    FCH_FINAN_PARTIC_FSS ier = query.FirstOrDefault();
        //    if (ier != null)
        //    {
        //        ret = ier.VLR_CALCUL_FCFNPT ?? 0;
        //    }
        //    return ret;

        //}
    }
}
