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
    
    public partial class PRE_TBL_POR_CONT
    {
        public short PLANO { get; set; }
        public Nullable<decimal> PERC_F1 { get; set; }
        public Nullable<decimal> PERC_F2 { get; set; }
        public Nullable<decimal> PERC_F3 { get; set; }
        public Nullable<decimal> PERC_EM { get; set; }
        public string LOG_INCLUSAO { get; set; }
        public System.DateTime DTH_INCLUSAO { get; set; }
        public string LOG_EXCLUSAO { get; set; }
        public Nullable<System.DateTime> DTH_EXCLUSAO { get; set; }
    }
}
