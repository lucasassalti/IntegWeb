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
    
    public partial class AAT_TBL_BOLETO_ITEM
    {
        public long COD_BOLETO_ITEM { get; set; }
        public long COD_BOLETO { get; set; }
        public short NUM_SEQ_DETALHE { get; set; }
        public Nullable<int> COD_DETALHE { get; set; }
        public Nullable<System.DateTime> DTH_REFERENCIA { get; set; }
        public Nullable<System.DateTime> DTH_VENCIMENTO { get; set; }
        public string DSC_DETALHE { get; set; }
        public Nullable<decimal> VLR_VALOR { get; set; }
    
        public virtual AAT_TBL_BOLETO AAT_TBL_BOLETO { get; set; }
    }
}
