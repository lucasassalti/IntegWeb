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
    
    public partial class PRE_TBL_CONTRIB_ESPORADICA
    {
        public long COD_CONTRIB_ESPORADICA { get; set; }
        public short COD_EMPRS { get; set; }
        public int NUM_RGTRO_EMPRG { get; set; }
        public System.DateTime DAT_VENCIMENTO { get; set; }
        public Nullable<decimal> VLR_CONTRIB { get; set; }
        public Nullable<long> COD_BOLETO { get; set; }
        public Nullable<System.DateTime> DAT_PGTO { get; set; }
        public string LOG_INCLUSAO { get; set; }
        public System.DateTime DTH_INCLUSAO { get; set; }
        public string LOG_EXCLUSAO { get; set; }
        public Nullable<System.DateTime> DTH_EXCLUSAO { get; set; }
    
        public virtual AAT_TBL_BOLETO AAT_TBL_BOLETO { get; set; }
    }
}
