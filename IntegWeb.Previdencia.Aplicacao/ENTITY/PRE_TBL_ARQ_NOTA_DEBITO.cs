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
    
    public partial class PRE_TBL_ARQ_NOTA_DEBITO
    {
        public int COD_NOTA_DEBITO { get; set; }
        public short COD_GRUPO_EMPRS { get; set; }
        public short ANO_REF { get; set; }
        public short MES_REF { get; set; }
        public string DCR_NOTA_DEBITO { get; set; }
        public Nullable<System.DateTime> DTH_VENCIMENTO { get; set; }
        public decimal VLR_NOTA_DEBITO { get; set; }
    }
}