//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntegWeb.Saude.Aplicacao.ENTITY
{
    using System;
    using System.Collections.Generic;
    
    public partial class SAU_TBL_CONV_PORTE_VIG
    {
        public decimal NUM_SEQ { get; set; }
        public decimal COD_CONVENENTE { get; set; }
        public Nullable<decimal> COD_TAB_RECURSO { get; set; }
        public System.DateTime DT_VIG_PORTE { get; set; }
        public System.DateTime DT_INI_VIG { get; set; }
        public Nullable<System.DateTime> DT_FIM_VIG { get; set; }
        public string USUARIO { get; set; }
        public Nullable<System.DateTime> DT_INCLUSAO { get; set; }
        public Nullable<System.DateTime> DT_ATU { get; set; }
    
        public virtual TB_TAB_RECURSO TB_TAB_RECURSO { get; set; }
        public virtual TB_CONVENENTE TB_CONVENENTE { get; set; }
    }
}
