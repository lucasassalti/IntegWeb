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
    
    public partial class PRE_TBL_RECADASTRAMENTO
    {
        public System.DateTime DAT_REF_RECAD { get; set; }
        public short COD_EMPRS { get; set; }
        public int NUM_RGTRO_EMPRG { get; set; }
        public int NUM_IDNTF_RPTANT { get; set; }
        public int NUM_MATR_PARTF { get; set; }
        public string NOME { get; set; }
        public Nullable<decimal> NUM_PRCINS_ASINSS { get; set; }
        public Nullable<System.DateTime> DAT_NASCIMENTO { get; set; }
        public Nullable<System.DateTime> DAT_FALECIMENTO { get; set; }
        public Nullable<System.DateTime> DIB { get; set; }
        public Nullable<System.DateTime> DAT_RECADASTRAMENTO { get; set; }
        public Nullable<System.DateTime> DAT_NOVO_PRAZO { get; set; }
        public string OBS { get; set; }
        public string LOG_INCLUSAO { get; set; }
        public System.DateTime DTH_INCLUSAO { get; set; }
        public string LOG_EXCLUSAO { get; set; }
        public Nullable<System.DateTime> DTH_EXCLUSAO { get; set; }
        public Nullable<short> TIP_RECADASTRAMENTO { get; set; }
        public short NUM_CONTRATO { get; set; }
    
        public virtual PRE_TBL_RECADASTRAMENTO_TIPO PRE_TBL_RECADASTRAMENTO_TIPO { get; set; }
    }
}