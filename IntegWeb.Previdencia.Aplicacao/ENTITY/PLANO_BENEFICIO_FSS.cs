//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    using System;
    using System.Collections.Generic;
    
    public partial class PLANO_BENEFICIO_FSS
    {
        public PLANO_BENEFICIO_FSS()
        {
            this.ADESAO_PLANO_PARTIC_FSS = new HashSet<ADESAO_PLANO_PARTIC_FSS>();
            this.PRE_TBL_ACAO_JUDIC = new HashSet<PRE_TBL_ACAO_JUDIC>();
        }
    
        public short NUM_PLBNF { get; set; }
        public string DCR_PLBNF { get; set; }
        public Nullable<System.DateTime> DAT_INIVG_PLBNF { get; set; }
        public Nullable<System.DateTime> DAT_FIMVIG_PLBNF { get; set; }
        public string MRC_CTBDEF_PLBNF { get; set; }
        public Nullable<short> NUM_VBCF_PLBNF { get; set; }
        public Nullable<short> NUM_VBIF_PLBNF { get; set; }
        public Nullable<short> NUM_VBCF13_PLBNF { get; set; }
        public Nullable<short> NUM_VBIF13_PLBNF { get; set; }
        public Nullable<short> NUM_TCTBDP_PLBNF { get; set; }
        public Nullable<short> NUM_VBDCPT_PLBNF { get; set; }
        public Nullable<short> NUM_VRBCTB_PLBNF { get; set; }
        public string MRC_DPTACM_PLBNF { get; set; }
        public Nullable<short> NUM_CTDPT_PLBNF { get; set; }
        public Nullable<decimal> PCT_VLZRSV_PLBNF { get; set; }
        public Nullable<short> TIP_ORIGDP_PLBNF { get; set; }
        public string MRC_UTLZPRF_PLBNF { get; set; }
        public string MRC_DECTER_PLBNF { get; set; }
        public Nullable<short> FDCCODFUNDACAO { get; set; }
        public string MRC_CRRCCOTA_PLBNF { get; set; }
        public string TIP_CRRCCOTA_PLBNF { get; set; }
        public string MRC_FNAUTPATROC_PLBNF { get; set; }
        public string MRC_PERCOTA_PLBNF { get; set; }
        public Nullable<short> NUM_FDORESID_PLBNF { get; set; }
        public string MRC_RSVCONUS_PLBNF { get; set; }
        public Nullable<short> NUM_VBDSTONUS_PLBNF { get; set; }
        public string MRC_EXIMPORTRAT_PLBNF { get; set; }
        public string MRC_EXCCTZCTB_PLBNF { get; set; }
        public string MRC_UTZDTCTZ_PLBNF { get; set; }
        public Nullable<short> TIP_OPCPRFINV_PLBNF { get; set; }
        public Nullable<short> TIP_FRMAPLICPRF_PLBNF { get; set; }
        public Nullable<System.DateTime> DAT_CRIACAO_PLBNF { get; set; }
        public string NUM_INSCORGREG_PLBNF { get; set; }
        public string TIP_MODALIDADE_PLBNF { get; set; }
        public Nullable<short> COD_COTAPRZACUM_PLBNF { get; set; }
        public Nullable<short> COD_TPPCP_CUSTEIOPDR_PLBNF { get; set; }
        public Nullable<short> COD_SITPAR_CUSTEIOPDR_PLBNF { get; set; }
        public Nullable<short> TIP_PLANOAMD_PLBNF { get; set; }
        public Nullable<short> NUM_FNDBASEISENTA_PLBNF { get; set; }
        public Nullable<short> MES_REAJ_AUTOPATROC { get; set; }
        public Nullable<short> IND_REAJ_AUTOPATROC { get; set; }
        public Nullable<short> COD_TPPCP_MOVRESERV { get; set; }
        public Nullable<short> COD_SITPAR_MOVRESERV { get; set; }
        public Nullable<short> QTD_MAXPARC_RSGPARC { get; set; }
        public string MRC_UT_FUNDS_RESID { get; set; }
    
        public virtual ICollection<ADESAO_PLANO_PARTIC_FSS> ADESAO_PLANO_PARTIC_FSS { get; set; }
        public virtual UNIDADE_MONETARIA UNIDADE_MONETARIA { get; set; }
        public virtual ICollection<PRE_TBL_ACAO_JUDIC> PRE_TBL_ACAO_JUDIC { get; set; }
    }
}
