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
    
    public partial class PRE_TBL_ACAO_JUDIC
    {
        public decimal ID_REG { get; set; }
        public Nullable<decimal> NUM_SEQ_PROC { get; set; }
        public Nullable<short> COD_EMPRS { get; set; }
        public Nullable<long> NUM_RGTRO_EMPRG { get; set; }
        public Nullable<long> CPF_EMPRG { get; set; }
        public string NOM_PARTICIP { get; set; }
        public string NOM_RECLAMANTE { get; set; }
        public Nullable<System.DateTime> DAT_DIB { get; set; }
        public Nullable<short> NUM_PLBNF { get; set; }
        public Nullable<System.DateTime> DAT_SOLIC { get; set; }
        public string TIP_SOLIC { get; set; }
        public string NRO_PROCESSO { get; set; }
        public Nullable<System.DateTime> DAT_PRAZO { get; set; }
        public string COD_VARA_PROC { get; set; }
        public Nullable<short> COD_TIPLTO { get; set; }
        public string TIP_PLTO { get; set; }
        public string POLO_ACJUD { get; set; }
        public string NRO_PASTA { get; set; }
        public string NOM_ADVOG { get; set; }
        public string CALC_APRESENT { get; set; }
        public string NRO_MEDICAO { get; set; }
        public string DESC_PROC { get; set; }
        public string OBS_PROC { get; set; }
        public string USU_RESPON { get; set; }
        public Nullable<System.DateTime> DAT_SOLIC_SCR { get; set; }
        public Nullable<System.DateTime> DAT_RESP { get; set; }
        public string TP_DOC { get; set; }
        public Nullable<decimal> LOCAL_ARQ { get; set; }
        public string IDC_RECEB_SOLIC_SCR { get; set; }
        public string IDC_CANC_REV { get; set; }
        public string LOG_INCLUSAO { get; set; }
        public Nullable<System.DateTime> DTH_INCLUSAO { get; set; }
        public string LOG_EXCLUSAO { get; set; }
        public Nullable<System.DateTime> DTH_EXCLUSAO { get; set; }
    
        public virtual PLANO_BENEFICIO_FSS PLANO_BENEFICIO_FSS { get; set; }
        public virtual PRE_TBL_ACAO_VR_TIPLTO PRE_TBL_ACAO_VR_TIPLTO { get; set; }
        public virtual int MAX_PAG { get; set; }
        public virtual int MIN_PAG { get; set; }

    }
}