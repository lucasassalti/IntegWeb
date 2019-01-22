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

    //public class PARTICIPANTE
    //{
    //    public short COD_EMPRS { get; set; }
    //    public int NUM_RGTRO_EMPRG { get; set; }
    //    public Nullable<int> NUM_IDNTF_RPTANT { get; set; }
    //    public Nullable<int> NUM_MATR_PARTF { get; set; }
    //    public string NOME { get; set; }
    //    public Nullable<System.DateTime> DAT_ADMISSAO { get; set; }
    //    public Nullable<System.DateTime> DAT_DESLIGAMENTO { get; set; }
    //    public Nullable<System.DateTime> DAT_NASCIMENTO { get; set; }
    //    public Nullable<System.DateTime> DAT_FALECIMENTO { get; set; }
    //    public Nullable<System.DateTime> DAT_INICIO_BFPART { get; set; } //DIB
    //    public Nullable<long> NUM_PRCINS_ASINSS { get; set; }
    //}

    public class PARTICIPANTE
    {
        public short COD_EMPRS { get; set; }
        public int NUM_RGTRO_EMPRG { get; set; }
        public Nullable<short> NUM_DIGVR_EMPRG { get; set; }
        public string NOM_PAI_EMPRG { get; set; }
        public string NOM_MAE_EMPRG { get; set; }
        public Nullable<int> NUM_IDNTF_RPTANT { get; set; }
        public Nullable<int> NUM_MATR_PARTF { get; set; }
        //public Nullable<short> COD_CONFL_EMPRG { get; set; }
        //public Nullable<short> COD_CTTRB_EMPRG { get; set; }
        public string COD_DDD_EMPRG { get; set; }
        //public Nullable<short> QTD_MESTRB_EMPRG { get; set; }
        public string COD_DDI_EMPRG { get; set; }
        public Nullable<short> COD_ESTCV_EMPRG { get; set; }
        public string COD_SEXO_EMPRG { get; set; }
        public Nullable<System.DateTime> DAT_ADMSS_EMPRG { get; set; }
        public Nullable<System.DateTime> DAT_DESLG_EMPRG { get; set; }
        public Nullable<System.DateTime> DAT_NASCM_EMPRG { get; set; }
        public string DCR_ENDER_EMPRG { get; set; }
        public string NOM_BAIRRO_EMPRG { get; set; }
        public string NOM_EMPRG { get; set; }
        public string NOM_PAIS_EMPRG { get; set; }
        public Nullable<long> NUM_CPF_EMPRG { get; set; }
        public string NUM_CI_EMPRG { get; set; }
        public string COD_OREXCI_EMPRG { get; set; }
        public string COD_UFCI_EMPRG { get; set; }
        public string NUM_CTCOR_EMPRG { get; set; }
        public Nullable<int> NUM_CXPTL_EMPRG { get; set; }
        //public Nullable<short> NUM_GRSAL_EMPRG { get; set; }
        public Nullable<short> NUM_RAMAL_EMPRG { get; set; }
        public Nullable<int> NUM_TELEF_EMPRG { get; set; }
        //public Nullable<decimal> VLR_SALAR_EMPRG { get; set; }
        //public Nullable<long> NUM_CTPRF_EMPRG { get; set; }
        //public string NUM_SRCTP_EMPRG { get; set; }
        //public Nullable<short> QTD_INSS_EMPRG { get; set; }
        public Nullable<int> COD_CEP_EMPRG { get; set; }
        //public Nullable<int> NUM_CARGO { get; set; }
        //public Nullable<short> NUM_FILIAL { get; set; }
        public Nullable<short> COD_BANCO { get; set; }
        public Nullable<int> COD_AGBCO { get; set; }
        //public Nullable<int> NUM_CR { get; set; }
        public string COD_MUNICI { get; set; }
        public string COD_ESTADO { get; set; }
        //public Nullable<int> NUM_ORGAO { get; set; }
        //public Nullable<short> COD_MTDSL { get; set; }
        public string NOM_CIDRS_EMPRG { get; set; }
        public string COD_UNDFD_EMPRG { get; set; }
        //public string DCR_OBSERVACAO { get; set; }
        //public string MRC_PLSAUD_EMPRG { get; set; }
        public string COD_EMAIL_EMPRG { get; set; }
        //public Nullable<long> NUM_PISPAS_EMPRG { get; set; }
        public Nullable<System.DateTime> DAT_FALEC_EMPRG { get; set; }
        public string DCR_NATURAL_EMPRG { get; set; }
        public Nullable<System.DateTime> DAT_EXPCI_EMPRG { get; set; }
        public string DCR_COMPL_EMPRG { get; set; }
        //public string DCR_OCPPROF_EMPRG { get; set; }
        public Nullable<int> NUM_CELUL_EMPRG { get; set; }
        public string DCR_NACNL_EMPRG { get; set; }
        public string TIP_CTCOR_EMPRG { get; set; }
        //public string COD_UFNAT_EMPRG { get; set; }
        //public Nullable<decimal> VLR_01_EMPRG { get; set; }
        //public string NUM_TELRES_EMPRG { get; set; }
        public string COD_DDDCEL_EMPRG { get; set; }
        //public Nullable<short> QTD_DIATRB_EMPRG { get; set; }
        //public Nullable<short> QTD_ANOTRB_EMPRG { get; set; }
        //public Nullable<short> QTD_DIAANT_EMPRG { get; set; }
        //public Nullable<short> QTD_MESANT_EMPRG { get; set; }
        //public Nullable<short> QTD_ANOANT_EMPRG { get; set; }
        //public Nullable<long> NUM_TITULO_EMPRG { get; set; }
        //public Nullable<short> NUM_ZONA_EMPRG { get; set; }
        //public Nullable<short> NUM_SECAO_EMPRG { get; set; }
        //public string NUM_RGORIG_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_ULT_RCD_EMPRG { get; set; }
        //public Nullable<short> AGCCODCODIGODAAGENCIA { get; set; }
        //public Nullable<int> STECOD { get; set; }
        //public Nullable<long> NUM_CPFCTC_EMPRG { get; set; }
        //public string NOM_SEGTIT_EMPRG { get; set; }
        public Nullable<int> NUM_ENDER_EMPRG { get; set; }
        //public Nullable<short> COD_EMPRSRPTANT_EMPRG { get; set; }
        //public Nullable<int> NUM_RGTRORPTANT_EMPRG { get; set; }
        //public string NUM_DDDFAX_EMPRG { get; set; }
        //public Nullable<int> NUM_FAX_EMPRG { get; set; }
        //public string NUM_DDIFAX_EMPRG { get; set; }
        //public Nullable<short> ATECODATRIBUTOEMPRG { get; set; }
        //public Nullable<decimal> VLR_PATRIMONIAL_EMPRG { get; set; }
        //public Nullable<decimal> VLR_RENDIMENTO_EMPRG { get; set; }
        //public string DCR_SITPPE_EMPRG { get; set; }
        //public string DCR_RENDIMENTO_EMPRG { get; set; }
        //public string NAT_DOCIDNT_EMPRG { get; set; }
        //public Nullable<int> PAICOD { get; set; }
        //public string COD_ESTRUT_EMPRG { get; set; }
        //public string DCR_IDIOMA_EMPRG { get; set; }
        //public string MRC_NEGSEF_EMPRG { get; set; }
        //public string NUM_IP_EMPRG { get; set; }
        //public string MRC_NAOPART_EMPRG { get; set; }
        //public string NUM_CELUL2_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_OBTSRS_EMPRG { get; set; }
        //public string DCR_MOTOBT_EMPRG { get; set; }
        //public string EMPRGIDCTIPOMARCACAO { get; set; }
        //public Nullable<System.DateTime> DAT_PREVAPOS_EMPRG { get; set; }
        //public string EMPRGIDCGRPSANGUINEO { get; set; }
        //public string EMPRGIDCFATORRH { get; set; }
        //public string EMPRGNUMNATUREZA { get; set; }
        //public string EMPRGORGEMINATUREZA { get; set; }
        //public string EMPRGUNFORGEMINATUREZA { get; set; }
        //public Nullable<System.DateTime> EMPRGDTHEMINATUREZA { get; set; }
        //public Nullable<short> TLGCODTIPOLOGRAD { get; set; }
        //public string DCR_LOGRAD { get; set; }
        //public string NOM_EMPGR_ANS { get; set; }
        //public string EMPRGCODDDI2 { get; set; }
        //public string EMPRGCODDDD2 { get; set; }
        //public Nullable<int> EMPRGNUMTELEF2 { get; set; }
        //public string EMPRGDESEMAIL2 { get; set; }
        //public string NUM_SIAPE_EMPRG { get; set; }
        //public string NUM_MATFNC_EMPRG { get; set; }
        //public string NUM_CNS_EMPRG { get; set; }
        //public Nullable<int> PESCODPESSOA { get; set; }
        //public string DCR_APOS1_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_INIAPOS1_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_FIMAPOS1_EMPREG { get; set; }
        //public string DCR_APOS2_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_INIAPOS2_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_FIMAPOS2_EMPREG { get; set; }
        //public string DCR_APOS3_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_INIAPOS3_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_FIMAPOS3_EMPREG { get; set; }
        //public string DCR_APOS4_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_INIAPOS4_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_FIMAPOS4_EMPREG { get; set; }
        //public Nullable<System.DateTime> DAT_PRIEMP_EMPRG { get; set; }
        //public string MRC_ASSOC_EMPRG { get; set; }
        //public string MRC_DEFFIS_EMPRG { get; set; }
        //public Nullable<short> GDECOD { get; set; }
        //public Nullable<int> PAICODNASCIMENTO { get; set; }
        //public Nullable<int> PAICODRESIDENCIA { get; set; }
        //public Nullable<int> PAICODCIDADANIA1 { get; set; }
        //public Nullable<int> PAICODCIDADANIA2 { get; set; }
        //public string NUM_GRNCRD_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_VLDGRNCRD_EMPRG { get; set; }
        //public string NUM_DDICEL_EMPRG { get; set; }
        //public Nullable<System.DateTime> DAT_RECAD_EMPRG { get; set; }
        //public string MRC_RES_EXT_EMPRG { get; set; }
        //public string NOM_PAIS_EXT_1 { get; set; }
        //public string NOM_PAIS_EXT_2 { get; set; }
        //public string NOM_PAIS_EXT_3 { get; set; }
        //public Nullable<int> PAICODPAISEXT1 { get; set; }
        //public Nullable<int> PAICODPAISEXT2 { get; set; }
        //public Nullable<int> PAICODPAISEXT3 { get; set; }
        //public string MRC_DOC_GRNCRD { get; set; }
        //public string NOM_CJGE_EMPRG { get; set; }
        public Nullable<System.DateTime> DAT_INICIO_BFPART { get; set; } //DIB
        public Nullable<long> NUM_PRCINS_ASINSS { get; set; }
        public string ORIGEM_TABELA { get; set; }
        public Nullable<DateTime> ORDER_BY { get; set; }

    }
                                        
    //CASE WHEN A.COD_UNDFD_EMPRG IS NULL
    //THEN (SELECT DISTINCT COD_ESTADO FROM ATT.TB_CEP WHERE NUM_CEP = A.COD_CEP_EMPRG)
    //ELSE A.COD_UNDFD_EMPRG
    //END COD_UNDFD_EMPRG,                                              
                                                
    //TIP_CTCOR_EMPRG = (r.TIP_CTCOR_REPRES!=null ? r.TIP_CTCOR_REPRES.ToString() : ""), 

    public class ParticipanteDAL
    {

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<PARTICIPANTE> GetParticipante(int startRowIndex, int maximumRows, short pEmpresa, int pMatricula, int pRepres, string sortParameter)
        {
            return GetWhere(pEmpresa, pMatricula, pRepres)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        //public IQueryable<PARTICIPANTE> GetWhereEmpregado(short pEmpresa, int pMatricula)
        //{

        //    IQueryable<PARTICIPANTE> query;
        //    query = from e in m_DbContext.EMPREGADO
        //            from p in m_DbContext.PARTICIPANTE_FSS
        //            from b in m_DbContext.BENEFICIO_PARTIC_FSS
        //            where (e.COD_EMPRS == p.COD_EMPRS)
        //               && (e.NUM_RGTRO_EMPRG == p.NUM_RGTRO_EMPRG)
        //               && (b.NUM_MATR_PARTF == p.NUM_MATR_PARTF)
        //                //&& (e.COD_EMPRS != 41)
        //               && (e.DAT_FALEC_EMPRG != null)
        //               && (e.COD_EMPRS == pEmpresa || pEmpresa == null)
        //               && (e.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
        //            select new PARTICIPANTE()
        //            {
        //                COD_EMPRS = e.COD_EMPRS,
        //                NUM_RGTRO_EMPRG = e.NUM_RGTRO_EMPRG,
        //                NUM_IDNTF_RPTANT = 0,
        //                NUM_MATR_PARTF = e.NUM_MATR_PARTF,
        //                //NOME = e.NOM_EMPRG,
        //                DAT_ADMISSAO = e.DAT_ADMSS_EMPRG,
        //                DAT_DESLIGAMENTO = e.DAT_DESLG_EMPRG,
        //                DAT_NASCIMENTO = e.DAT_NASCM_EMPRG,
        //                DAT_FALECIMENTO = e.DAT_FALEC_EMPRG,
        //                //DAT_INICIO_BFPART
        //                //NUM_PRCINS_ASINSS
        //            };

        //    return query;
        //}

        public IQueryable<PARTICIPANTE> GetWhere(short pEmpresa, int pMatricula, int pRepres, bool Falecidos = true)
        {
            IQueryable<PARTICIPANTE> query;

            query = from e in m_DbContext.EMPREGADO.AsNoTracking()
                    where (e.DAT_FALEC_EMPRG == null || Falecidos)
                       && (e.COD_EMPRS.Equals(pEmpresa))
                       && (e.NUM_RGTRO_EMPRG.Equals(pMatricula))
                       && (pRepres == 0)
                    //&& e.NUM_RGTRO_EMPRG == 210719
                    select new PARTICIPANTE
                    {
                        ORIGEM_TABELA = "E",
                        ORDER_BY = e.DAT_DESLG_EMPRG ?? DateTime.Now,
                        COD_EMPRS = e.COD_EMPRS,
                        NUM_RGTRO_EMPRG = e.NUM_RGTRO_EMPRG,
                        NUM_DIGVR_EMPRG = e.NUM_DIGVR_EMPRG,
                        NUM_MATR_PARTF = e.NUM_MATR_PARTF,
                        NOM_EMPRG = e.NOM_EMPRG,
                        NOM_PAI_EMPRG = e.NOM_PAI_EMPRG,
                        NOM_MAE_EMPRG = e.NOM_MAE_EMPRG,
                        //COD_CONFL_EMPRG = r.COD_CONFL_EMPRG,
                        //COD_CTTRB_EMPRG = r.COD_CTTRB_EMPRG,
                        //QTD_MESTRB_EMPRG = r.QTD_MESTRB_EMPRG,
                        COD_ESTCV_EMPRG = e.COD_ESTCV_EMPRG,
                        COD_SEXO_EMPRG = e.COD_SEXO_EMPRG,
                        DAT_ADMSS_EMPRG = e.DAT_ADMSS_EMPRG,
                        DAT_DESLG_EMPRG = e.DAT_DESLG_EMPRG,
                        DAT_NASCM_EMPRG = e.DAT_NASCM_EMPRG,
                        DCR_ENDER_EMPRG = e.DCR_ENDER_EMPRG,
                        NUM_ENDER_EMPRG = e.NUM_ENDER_EMPRG,
                        DCR_COMPL_EMPRG = e.DCR_COMPL_EMPRG,
                        NOM_BAIRRO_EMPRG = e.NOM_BAIRRO_EMPRG,
                        NOM_CIDRS_EMPRG = e.NOM_CIDRS_EMPRG,
                        COD_UNDFD_EMPRG = e.COD_UNDFD_EMPRG,
                        //CASE WHEN A.COD_UNDFD_EMPRG IS NULL
                        //THEN (SELECT DISTINCT COD_ESTADO FROM ATT.TB_CEP WHERE NUM_CEP = A.COD_CEP_EMPRG)
                        //ELSE A.COD_UNDFD_EMPRG
                        //END COD_UNDFD_EMPRG,
                        NOM_PAIS_EMPRG = e.NOM_PAIS_EMPRG,
                        DCR_NATURAL_EMPRG = e.DCR_NATURAL_EMPRG,
                        DCR_NACNL_EMPRG = e.DCR_NACNL_EMPRG,
                        NUM_CPF_EMPRG = e.NUM_CPF_EMPRG,
                        NUM_CI_EMPRG = e.NUM_CI_EMPRG,
                        DAT_EXPCI_EMPRG = e.DAT_EXPCI_EMPRG,
                        COD_OREXCI_EMPRG = e.COD_OREXCI_EMPRG,
                        COD_UFCI_EMPRG = e.COD_UFCI_EMPRG,
                        TIP_CTCOR_EMPRG = e.TIP_CTCOR_EMPRG,
                        NUM_CTCOR_EMPRG = e.NUM_CTCOR_EMPRG,
                        NUM_CXPTL_EMPRG = e.NUM_CXPTL_EMPRG,
                        //NUM_GRSAL_EMPRG = e.NUM_GRSAL_EMPRG,
                        COD_DDI_EMPRG = e.COD_DDI_EMPRG,
                        COD_DDD_EMPRG = e.COD_DDD_EMPRG,
                        NUM_TELEF_EMPRG = e.NUM_TELEF_EMPRG,
                        NUM_RAMAL_EMPRG = e.NUM_RAMAL_EMPRG,
                        COD_DDDCEL_EMPRG = e.COD_DDDCEL_EMPRG,
                        NUM_CELUL_EMPRG = e.NUM_CELUL_EMPRG,
                        //VLR_SALAR_EMPRG = e.VLR_SALAR_EMPRG,
                        //NUM_CTPRF_EMPRG = e.NUM_CTPRF_EMPRG,
                        //NUM_SRCTP_EMPRG = e.NUM_SRCTP_EMPRG,
                        //QTD_INSS_EMPRG = e.QTD_INSS_EMPRG,
                        COD_CEP_EMPRG = e.COD_CEP_EMPRG,
                        COD_EMAIL_EMPRG = e.COD_EMAIL_EMPRG,
                        //NUM_CARGO = e.NUM_CARGO,
                        //NUM_FILIAL = e.NUM_FILIAL,
                        COD_BANCO = e.COD_BANCO,
                        COD_AGBCO = e.COD_AGBCO,
                        //NVL(H.COD_BANCO, A.COD_BANCO) COD_BANCO,
                        //NVL(H.COD_AGBCO, A.COD_AGBCO) COD_AGBCO,
                        //NUM_CR = e.NUM_CR,
                        COD_MUNICI = e.COD_MUNICI,
                        COD_ESTADO = e.COD_ESTADO
                    };

            if (pRepres > 0)
            {
                query = query.Union(
                            from e in m_DbContext.EMPREGADO.AsNoTracking()
                            join d in m_DbContext.REPRES_DEPEND_FSS.AsNoTracking() on new { e.NUM_RGTRO_EMPRG, e.COD_EMPRS } equals new { d.NUM_RGTRO_EMPRG, d.COD_EMPRS }
                            join r in m_DbContext.REPRES_UNIAO_FSS.AsNoTracking() on d.NUM_IDNTF_RPTANT equals r.NUM_IDNTF_RPTANT
                            //join b in m_DbContext.BENEFICIO_PARTIC_FSS on b.NUM_MATR_PARTF equals e.NUM_MATR_PARTF
                            where (e.DAT_FALEC_EMPRG != null && r.DAT_FALEC_REPRES == null || Falecidos)
                               && (e.COD_EMPRS.Equals(pEmpresa))
                               && (e.NUM_RGTRO_EMPRG.Equals(pMatricula))
                               && (r.NUM_IDNTF_RPTANT == pRepres && pRepres > 0)
                               && (r.NUM_IDNTF_RPTANT > 0)
                            //&& (r.NUM_CPF_REPRES == pCpf || pCpf == null)
                            //&& (r.NOM_REPRES.ToLower().Contains(pNome.ToLower()) || pNome == null)
                            //&&  (e.NUM_RGTRO_EMPRG.Equals(d.NUM_RGTRO_EMPRG))
                            //&&  (e.COD_EMPRS.Equals(d.COD_EMPRS))
                            //&& e.NUM_RGTRO_EMPRG == 210719
                            orderby r.NUM_IDNTF_RPTANT descending //Sempre o mais atual
                            select new PARTICIPANTE
                            {
                                ORIGEM_TABELA = "R",
                                ORDER_BY = DateTime.MinValue,
                                COD_EMPRS = r.COD_EMPRS ?? 0,
                                NUM_RGTRO_EMPRG = r.NUM_RGTRO_EMPRG ?? 0,
                                NUM_DIGVR_EMPRG = 0,
                                NUM_MATR_PARTF = 0,
                                NOM_EMPRG = r.NOM_REPRES,
                                NOM_PAI_EMPRG = r.NOM_PAI_REPRES,
                                NOM_MAE_EMPRG = r.NOM_MAE_REPRES,
                                //COD_CONFL_EMPRG = r.COD_CONFL_REPRES,
                                //COD_CTTRB_EMPRG = r.COD_CTTRB_REPRES,
                                //QTD_MESTRB_EMPRG = r.QTD_MESTRB_REPRES,
                                COD_ESTCV_EMPRG = r.COD_ESTCV_REPRES,
                                COD_SEXO_EMPRG = r.COD_SEXO_REPRES,
                                DAT_ADMSS_EMPRG = null,
                                DAT_DESLG_EMPRG = null,
                                DAT_NASCM_EMPRG = r.DAT_NASCM_REPRES,
                                DCR_ENDER_EMPRG = r.DCR_ENDER_REPRES,
                                NUM_ENDER_EMPRG = r.NUM_ENDER_REPRES,
                                DCR_COMPL_EMPRG = r.DCR_COMPL_REPRES,
                                NOM_BAIRRO_EMPRG = r.NOM_BAIRRO_REPRES,
                                NOM_CIDRS_EMPRG = r.NOM_CIDRS_REPRES,
                                COD_UNDFD_EMPRG = r.COD_UNDFD_REPRES,
                                //CASE WHEN A.COD_UNDFD_EMPRG IS NULL
                                //THEN (SELECT DISTINCT COD_ESTADO FROM ATT.TB_CEP WHERE NUM_CEP = A.COD_CEP_EMPRG)
                                //ELSE A.COD_UNDFD_EMPRG
                                //END COD_UNDFD_EMPRG,
                                NOM_PAIS_EMPRG = r.NOM_PAIS_REPRES,
                                DCR_NATURAL_EMPRG = r.DCR_NATURAL_REPRES,
                                DCR_NACNL_EMPRG = r.DCR_NACNL_REPRES,
                                NUM_CPF_EMPRG = r.NUM_CPF_REPRES,
                                NUM_CI_EMPRG = r.NUM_CI_REPRES,
                                DAT_EXPCI_EMPRG = r.DAT_EXPCI_REPRES,
                                COD_OREXCI_EMPRG = r.COD_OREXCI_REPRES,
                                COD_UFCI_EMPRG = r.COD_UFCI_REPRES,
                                //TIP_CTCOR_EMPRG = (r.TIP_CTCOR_REPRES != null ? r.TIP_CTCOR_REPRES.ToString() : ""),
                                TIP_CTCOR_EMPRG = "", //Convert.ToString(r.TIP_CTCOR_REPRES ?? 0),
                                NUM_CTCOR_EMPRG = r.NUM_CTCOR_REPRES,
                                NUM_CXPTL_EMPRG = r.NUM_CXPTL_REPRES,
                                //NUM_GRSAL_EMPRG = r.NUM_GRSAL_REPRES,
                                COD_DDI_EMPRG = r.NUM_DDI_REPRES,
                                COD_DDD_EMPRG = r.NUM_DDD_REPRES,
                                NUM_TELEF_EMPRG = r.NUM_TELEF_REPRES,
                                NUM_RAMAL_EMPRG = r.NUM_RAMAL_REPRES,
                                COD_DDDCEL_EMPRG = r.COD_DDDCEL_REPRES,
                                NUM_CELUL_EMPRG = r.NUM_CELUL_REPRES,
                                //VLR_SALAR_EMPRG = r.VLR_SALAR_REPRES,
                                //NUM_CTPRF_EMPRG = r.NUM_CTPRF_REPRES,
                                //NUM_SRCTP_EMPRG = r.NUM_SRCTP_REPRES,
                                //QTD_INSS_EMPRG = r.QTD_INSS_REPRES,
                                COD_CEP_EMPRG = r.COD_CEP_REPRES,
                                COD_EMAIL_EMPRG = r.COD_EMAIL_REPRES,
                                //NUM_CARGO = r.NUM_CARGO,
                                //NUM_FILIAL = r.NUM_FILIAL,
                                COD_BANCO = r.COD_BANCO,
                                COD_AGBCO = r.COD_AGBCO,
                                //NVL(H.COD_BANCO, A.COD_BANCO) COD_BANCO,
                                //NVL(H.COD_AGBCO, A.COD_AGBCO) COD_AGBCO,
                                //NUM_CR = r.NUM_CR,
                                COD_MUNICI = r.COD_MUNICI_REPRES,
                                COD_ESTADO = r.COD_ESTADO_REPRES
                            }
                );
            }

            return query;
        }

        public IQueryable<PARTICIPANTE> GetWhere(long pCpf, string pNome, bool pBusca_Repres_Uniao = true, bool Falecidos = true)
        {
            IQueryable<PARTICIPANTE> query;

            query = from e in m_DbContext.EMPREGADO.AsNoTracking()
                    where (e.DAT_FALEC_EMPRG == null || Falecidos)
                       //&& (e.COD_EMPRS == (pEmpresa))
                       //&& (e.NUM_RGTRO_EMPRG.Equals(pMatricula) || pMatricula == null)
                       && (e.NUM_CPF_EMPRG == pCpf || pCpf == null)
                       && (e.NOM_EMPRG.ToLower().Contains(pNome.ToLower()) || pNome == null)
                       //&& e.NUM_RGTRO_EMPRG == 210719
                    select new PARTICIPANTE
                    {
                        ORIGEM_TABELA = "E",
                        ORDER_BY = e.DAT_DESLG_EMPRG ?? DateTime.Now,
                        COD_EMPRS = e.COD_EMPRS,
                        NUM_RGTRO_EMPRG = e.NUM_RGTRO_EMPRG,
                        NUM_DIGVR_EMPRG = e.NUM_DIGVR_EMPRG,
                        NUM_MATR_PARTF = e.NUM_MATR_PARTF,
                        NOM_EMPRG = e.NOM_EMPRG,
                        NOM_PAI_EMPRG = e.NOM_PAI_EMPRG,
                        NOM_MAE_EMPRG = e.NOM_MAE_EMPRG,
                        //COD_CONFL_EMPRG = r.COD_CONFL_EMPRG,
                        //COD_CTTRB_EMPRG = r.COD_CTTRB_EMPRG,
                        //QTD_MESTRB_EMPRG = r.QTD_MESTRB_EMPRG,
                        COD_ESTCV_EMPRG = e.COD_ESTCV_EMPRG,
                        COD_SEXO_EMPRG = e.COD_SEXO_EMPRG,
                        DAT_ADMSS_EMPRG = e.DAT_ADMSS_EMPRG,
                        DAT_DESLG_EMPRG = e.DAT_DESLG_EMPRG,
                        DAT_NASCM_EMPRG = e.DAT_NASCM_EMPRG,
                        DCR_ENDER_EMPRG = e.DCR_ENDER_EMPRG,
                        NUM_ENDER_EMPRG = e.NUM_ENDER_EMPRG,
                        DCR_COMPL_EMPRG = e.DCR_COMPL_EMPRG,
                        NOM_BAIRRO_EMPRG = e.NOM_BAIRRO_EMPRG,
                        NOM_CIDRS_EMPRG = e.NOM_CIDRS_EMPRG,
                        COD_UNDFD_EMPRG = e.COD_UNDFD_EMPRG,
                        //CASE WHEN A.COD_UNDFD_EMPRG IS NULL
                        //THEN (SELECT DISTINCT COD_ESTADO FROM ATT.TB_CEP WHERE NUM_CEP = A.COD_CEP_EMPRG)
                        //ELSE A.COD_UNDFD_EMPRG
                        //END COD_UNDFD_EMPRG,
                        NOM_PAIS_EMPRG = e.NOM_PAIS_EMPRG,
                        DCR_NATURAL_EMPRG = e.DCR_NATURAL_EMPRG,
                        DCR_NACNL_EMPRG = e.DCR_NACNL_EMPRG,
                        NUM_CPF_EMPRG = e.NUM_CPF_EMPRG,
                        NUM_CI_EMPRG = e.NUM_CI_EMPRG,
                        DAT_EXPCI_EMPRG = e.DAT_EXPCI_EMPRG,
                        COD_OREXCI_EMPRG = e.COD_OREXCI_EMPRG,
                        COD_UFCI_EMPRG = e.COD_UFCI_EMPRG,
                        TIP_CTCOR_EMPRG = e.TIP_CTCOR_EMPRG,
                        NUM_CTCOR_EMPRG = e.NUM_CTCOR_EMPRG,
                        NUM_CXPTL_EMPRG = e.NUM_CXPTL_EMPRG,
                        //NUM_GRSAL_EMPRG = e.NUM_GRSAL_EMPRG,
                        COD_DDI_EMPRG = e.COD_DDI_EMPRG,
                        COD_DDD_EMPRG = e.COD_DDD_EMPRG,
                        NUM_TELEF_EMPRG = e.NUM_TELEF_EMPRG,
                        NUM_RAMAL_EMPRG = e.NUM_RAMAL_EMPRG,
                        COD_DDDCEL_EMPRG = e.COD_DDDCEL_EMPRG,
                        NUM_CELUL_EMPRG = e.NUM_CELUL_EMPRG,
                        //VLR_SALAR_EMPRG = e.VLR_SALAR_EMPRG,
                        //NUM_CTPRF_EMPRG = e.NUM_CTPRF_EMPRG,
                        //NUM_SRCTP_EMPRG = e.NUM_SRCTP_EMPRG,
                        //QTD_INSS_EMPRG = e.QTD_INSS_EMPRG,
                        COD_CEP_EMPRG = e.COD_CEP_EMPRG,
                        COD_EMAIL_EMPRG = e.COD_EMAIL_EMPRG,
                        //NUM_CARGO = e.NUM_CARGO,
                        //NUM_FILIAL = e.NUM_FILIAL,
                        COD_BANCO = e.COD_BANCO,
                        COD_AGBCO = e.COD_AGBCO,
                        //NVL(H.COD_BANCO, A.COD_BANCO) COD_BANCO,
                        //NVL(H.COD_AGBCO, A.COD_AGBCO) COD_AGBCO,
                        //NUM_CR = e.NUM_CR,
                        COD_MUNICI = e.COD_MUNICI,
                        COD_ESTADO = e.COD_ESTADO
                    };

            if (pBusca_Repres_Uniao)
            {
                query = query.Union(
                            from e in m_DbContext.EMPREGADO.AsNoTracking()
                            join d in m_DbContext.REPRES_DEPEND_FSS.AsNoTracking() on new { e.NUM_RGTRO_EMPRG, e.COD_EMPRS } equals new { d.NUM_RGTRO_EMPRG, d.COD_EMPRS }
                            join r in m_DbContext.REPRES_UNIAO_FSS.AsNoTracking() on d.NUM_IDNTF_RPTANT equals r.NUM_IDNTF_RPTANT
                            //join b in m_DbContext.BENEFICIO_PARTIC_FSS on b.NUM_MATR_PARTF equals e.NUM_MATR_PARTF
                            where (e.DAT_FALEC_EMPRG != null && r.DAT_FALEC_REPRES == null || Falecidos)
                              && (r.NUM_CPF_REPRES == pCpf || pCpf == null)
                              && (r.NOM_REPRES.ToLower().Contains(pNome.ToLower()) || pNome == null)
                            //&&  (e.NUM_RGTRO_EMPRG.Equals(d.NUM_RGTRO_EMPRG))
                            //&&  (e.COD_EMPRS.Equals(d.COD_EMPRS))
                            //&& e.NUM_RGTRO_EMPRG == 210719
                            orderby r.NUM_IDNTF_RPTANT descending //Sempre o mais atual
                            select new PARTICIPANTE
                            {
                                ORIGEM_TABELA = "R",
                                ORDER_BY = DateTime.MinValue,
                                COD_EMPRS = r.COD_EMPRS ?? 0,
                                NUM_RGTRO_EMPRG = r.NUM_RGTRO_EMPRG ?? 0,
                                NUM_DIGVR_EMPRG = 0,
                                NUM_MATR_PARTF = 0,
                                NOM_EMPRG = r.NOM_REPRES,
                                NOM_PAI_EMPRG = r.NOM_PAI_REPRES,
                                NOM_MAE_EMPRG = r.NOM_MAE_REPRES,
                                //COD_CONFL_EMPRG = r.COD_CONFL_REPRES,
                                //COD_CTTRB_EMPRG = r.COD_CTTRB_REPRES,
                                //QTD_MESTRB_EMPRG = r.QTD_MESTRB_REPRES,
                                COD_ESTCV_EMPRG = r.COD_ESTCV_REPRES,
                                COD_SEXO_EMPRG = r.COD_SEXO_REPRES,
                                DAT_ADMSS_EMPRG = null,
                                DAT_DESLG_EMPRG = null,
                                DAT_NASCM_EMPRG = r.DAT_NASCM_REPRES,
                                DCR_ENDER_EMPRG = r.DCR_ENDER_REPRES,
                                NUM_ENDER_EMPRG = r.NUM_ENDER_REPRES,
                                DCR_COMPL_EMPRG = r.DCR_COMPL_REPRES,
                                NOM_BAIRRO_EMPRG = r.NOM_BAIRRO_REPRES,
                                NOM_CIDRS_EMPRG = r.NOM_CIDRS_REPRES,
                                COD_UNDFD_EMPRG = r.COD_UNDFD_REPRES,
                                //CASE WHEN A.COD_UNDFD_EMPRG IS NULL
                                //THEN (SELECT DISTINCT COD_ESTADO FROM ATT.TB_CEP WHERE NUM_CEP = A.COD_CEP_EMPRG)
                                //ELSE A.COD_UNDFD_EMPRG
                                //END COD_UNDFD_EMPRG,
                                NOM_PAIS_EMPRG = r.NOM_PAIS_REPRES,
                                DCR_NATURAL_EMPRG = r.DCR_NATURAL_REPRES,
                                DCR_NACNL_EMPRG = r.DCR_NACNL_REPRES,
                                NUM_CPF_EMPRG = r.NUM_CPF_REPRES,
                                NUM_CI_EMPRG = r.NUM_CI_REPRES,
                                DAT_EXPCI_EMPRG = r.DAT_EXPCI_REPRES,
                                COD_OREXCI_EMPRG = r.COD_OREXCI_REPRES,
                                COD_UFCI_EMPRG = r.COD_UFCI_REPRES,
                                //TIP_CTCOR_EMPRG = (r.TIP_CTCOR_REPRES != null ? r.TIP_CTCOR_REPRES.ToString() : ""),
                                TIP_CTCOR_EMPRG = "", //Convert.ToString(r.TIP_CTCOR_REPRES ?? 0),
                                NUM_CTCOR_EMPRG = r.NUM_CTCOR_REPRES,
                                NUM_CXPTL_EMPRG = r.NUM_CXPTL_REPRES,
                                //NUM_GRSAL_EMPRG = r.NUM_GRSAL_REPRES,
                                COD_DDI_EMPRG = r.NUM_DDI_REPRES,
                                COD_DDD_EMPRG = r.NUM_DDD_REPRES,
                                NUM_TELEF_EMPRG = r.NUM_TELEF_REPRES,
                                NUM_RAMAL_EMPRG = r.NUM_RAMAL_REPRES,
                                COD_DDDCEL_EMPRG = r.COD_DDDCEL_REPRES,
                                NUM_CELUL_EMPRG = r.NUM_CELUL_REPRES,
                                //VLR_SALAR_EMPRG = r.VLR_SALAR_REPRES,
                                //NUM_CTPRF_EMPRG = r.NUM_CTPRF_REPRES,
                                //NUM_SRCTP_EMPRG = r.NUM_SRCTP_REPRES,
                                //QTD_INSS_EMPRG = r.QTD_INSS_REPRES,
                                COD_CEP_EMPRG = r.COD_CEP_REPRES,
                                COD_EMAIL_EMPRG = r.COD_EMAIL_REPRES,
                                //NUM_CARGO = r.NUM_CARGO,
                                //NUM_FILIAL = r.NUM_FILIAL,
                                COD_BANCO = r.COD_BANCO,
                                COD_AGBCO = r.COD_AGBCO,
                                //NVL(H.COD_BANCO, A.COD_BANCO) COD_BANCO,
                                //NVL(H.COD_AGBCO, A.COD_AGBCO) COD_AGBCO,
                                //NUM_CR = r.NUM_CR,
                                COD_MUNICI = r.COD_MUNICI_REPRES,
                                COD_ESTADO = r.COD_ESTADO_REPRES
                            }
                );
            }

            return query;
        }

        //public IQueryable<PARTICIPANTE> GetWhere(short pEmpresa, int pMatricula, int? pRepres)
        //{

        //    pRepres = pRepres ?? 0;

        //    IQueryable<PARTICIPANTE> query;

        //    query = from e in m_DbContext.EMPREGADO
        //            from p in m_DbContext.PARTICIPANTE_FSS
        //            from b in m_DbContext.BENEFICIO_PARTIC_FSS
        //            where (e.COD_EMPRS == p.COD_EMPRS)
        //               && (e.NUM_RGTRO_EMPRG == p.NUM_RGTRO_EMPRG)
        //               && (b.NUM_MATR_PARTF == p.NUM_MATR_PARTF)
        //                //&& (e.COD_EMPRS != 41)
        //               //&& (e.DAT_FALEC_EMPRG == null)
        //               && (e.COD_EMPRS == pEmpresa || pEmpresa == null)
        //               && (e.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
        //               && (pRepres == 0)
        //            select new PARTICIPANTE()
        //            {
        //                COD_EMPRS = e.COD_EMPRS,
        //                NUM_RGTRO_EMPRG = e.NUM_RGTRO_EMPRG,
        //                NUM_IDNTF_RPTANT = 0,
        //                NUM_MATR_PARTF = e.NUM_MATR_PARTF,
        //                NOM_EMPRG = e.NOM_EMPRG,
        //                DAT_ADMSS_EMPRG = e.DAT_ADMSS_EMPRG,
        //                DAT_DESLG_EMPRG = e.DAT_DESLG_EMPRG,
        //                DAT_NASCM_EMPRG = e.DAT_NASCM_EMPRG,
        //                DAT_FALEC_EMPRG = e.DAT_FALEC_EMPRG,
        //                //DAT_INICIO_BFPART
        //                //NUM_PRCINS_ASINSS
        //            };

        //    query = query.Union(
        //            from e in m_DbContext.EMPREGADO
        //            from p in m_DbContext.PARTICIPANTE_FSS
        //            from r in m_DbContext.REPRES_UNIAO_FSS
        //            from b in m_DbContext.BENEFICIO_PARTIC_FSS
        //            where (e.COD_EMPRS == p.COD_EMPRS)
        //               && (e.NUM_RGTRO_EMPRG == p.NUM_RGTRO_EMPRG)
        //               && (b.NUM_MATR_PARTF == p.NUM_MATR_PARTF)
        //               && (p.COD_EMPRS == r.COD_EMPRS)
        //               && (p.NUM_RGTRO_EMPRG == r.NUM_RGTRO_EMPRG)
        //                //&& (e.COD_EMPRS != 41)
        //               //&& (e.DAT_FALEC_EMPRG != null)
        //               //&& (r.DAT_FALEC_REPRES == null)
        //               && (e.COD_EMPRS == pEmpresa || pEmpresa == null)
        //               && (e.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
        //               && (r.NUM_IDNTF_RPTANT == pRepres && pRepres != null)
        //               && (r.NUM_IDNTF_RPTANT > 0)
        //            select new PARTICIPANTE()
        //            {
        //                COD_EMPRS = e.COD_EMPRS,
        //                NUM_RGTRO_EMPRG = e.NUM_RGTRO_EMPRG,
        //                NUM_IDNTF_RPTANT = r.NUM_IDNTF_RPTANT,
        //                NUM_MATR_PARTF = e.NUM_MATR_PARTF,
        //                NOM_EMPRG = r.NOM_REPRES,
        //                DAT_ADMSS_EMPRG = e.DAT_ADMSS_EMPRG,
        //                DAT_DESLG_EMPRG = e.DAT_DESLG_EMPRG,
        //                DAT_NASCM_EMPRG = r.DAT_NASCM_REPRES,
        //                DAT_FALEC_EMPRG = r.DAT_FALEC_REPRES,
        //                //DAT_INICIO_BFPART
        //                //NUM_PRCINS_ASINSS
        //            });

        //    return query;
        //}

        public int GetDataCount(short pEmpresa, int pMatricula, int? pRepres)
        {
            return GetWhere(pEmpresa, pMatricula, 0).SelectCount();
        }

        //public PARTICIPANTE GetParticipanteBy(short pEmpresa, int pMatricula, int? pRepres)
        //{
        //    return GetWhere(pEmpresa, pMatricula, pRepres, null, null).FirstOrDefault();
        //}

        public PARTICIPANTE GetParticipanteBy(long pCpf, string pNome, bool pBusca_Repres_Uniao = true, bool Falecidos = true)
        {
            //var Emp = GetWhere(pEmpresa, pMatricula, pCpf, pNome).ToList()                    
            var Emp = GetWhere(pCpf, pNome, pBusca_Repres_Uniao, Falecidos).ToList()
                      //.Select(s => { s.DAT_DESLG_EMPRG = (s.DAT_DESLG_EMPRG ?? DateTime.Now); return s; })
                      .OrderByDescending(s => s.ORDER_BY); //Sempre o mais atual

            return Emp.FirstOrDefault();
        }

        public PARTICIPANTE GetParticipanteBy(short pEmpresa, int pMatricula, int pRepres, bool Falecidos = true)
        {
            var Emp = GetWhere(pEmpresa, pMatricula, pRepres, Falecidos).FirstOrDefault();
            return Emp;
        }

        public AGENCIA GetAgencia(short pCOD_BANCO, int pCOD_AGBCO)
        {
            IQueryable<AGENCIA> query;
            query = from e in m_DbContext.AGENCIA
                    where (e.COD_BANCO == pCOD_BANCO)
                       && (e.COD_AGBCO == pCOD_AGBCO)
                    select e;

            return query.FirstOrDefault();
        }

        public MOTIVO_DESLIG_FSS GetMotivoDesligamento(short pCOD_MTDSL)
        {
            IQueryable<MOTIVO_DESLIG_FSS> query;
            query = from m in m_DbContext.MOTIVO_DESLIG_FSS
                    where (m.COD_MTDSL == pCOD_MTDSL)
                    select m;

            return query.FirstOrDefault();
        }

        public List<ATT_CHARGER_DEPARA> GetEmpregado_DE_PARA2(short pCOD_EMPRS, string pNUM_ORGAO_DE = null)
        {

            IQueryable<ATT_CHARGER_DEPARA> query;
            query = from c in m_DbContext.ATT_CHARGER_DEPARA
                    where (c.CODAPLICACAO == 1)
                       && (c.CODTABELA == 84) // EMPREGADO
                       && (c.CODCOLUNA == 2)   // NUM_RGTRO_EMPRG
                       && (c.CODEMPRESA == pCOD_EMPRS)
                       && (c.CONTEUDODE == pNUM_ORGAO_DE || pNUM_ORGAO_DE == null)
                    select c;

            return query.ToList();
        }

        public bool AlterouDadosBancarios(short codEmprs, int numRgtroEmprg)
        {
            IQueryable<FC_CAD_TBL_MOV_DADOS_BANC> query = from bank in m_DbContext.FC_CAD_TBL_MOV_DADOS_BANC
                                                         where bank.COD_EMPRS.Equals(codEmprs)
                                                            && bank.NUM_RGTRO_EMPRG.Equals(numRgtroEmprg)
                                                        select bank;
            return query.Any();

        }

        public bool AlterouDadosCadastrais(short codEmprs, int numRgtroEmprg)
        {
            IQueryable<FC_CAD_TBL_MOV_DADOS_CAD> query = from cad in m_DbContext.FC_CAD_TBL_MOV_DADOS_CAD
                                                          where cad.COD_EMPRS.Equals(codEmprs)
                                                             && cad.NUM_RGTRO_EMPRG.Equals(numRgtroEmprg)
                                                          select cad;
            return query.Any();
        }

        public ADESAO_PLANO_PARTIC_FSS GetSituacaoPlanoPrevidencia(int pNUM_MATR_PARTF, short? pNUM_PLBNF)
        {
            IQueryable<ADESAO_PLANO_PARTIC_FSS> query;
            query = from a in m_DbContext.ADESAO_PLANO_PARTIC_FSS.AsNoTracking()
                    where (a.NUM_MATR_PARTF == pNUM_MATR_PARTF)
                       && (a.NUM_PLBNF == pNUM_PLBNF || pNUM_PLBNF == null)
                       && (a.DAT_FIM_ADPLPR == null)
                    select a;

            return query.FirstOrDefault();
        }
    }
}