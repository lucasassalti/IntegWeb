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
    
    public partial class PRE_TBL_POR_DREF
    {
        public short COD_DREF { get; set; }
        public Nullable<System.DateTime> DT_REF_PROC { get; set; }
        public Nullable<System.DateTime> DT_REF_INSS { get; set; }
        public Nullable<decimal> PERC_INSS { get; set; }
        public string LOG_INCLUSAO { get; set; }
        public System.DateTime DTH_INCLUSAO { get; set; }
        public string LOG_EXCLUSAO { get; set; }
        public Nullable<System.DateTime> DTH_EXCLUSAO { get; set; }
    }
}
