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
    
    public partial class FUN_TBL_MEMORIAL_UNIMED_HIST
    {
        public decimal ID_REG_HIST { get; set; }
        public System.DateTime DAT_GERACAO { get; set; }
        public short COD_EMPRS { get; set; }
        public long NUM_MATRICULA { get; set; }
        public string SUB_MATRICULA { get; set; }
        public string COD_IDENTIFICACAO { get; set; }
        public string NOM_PARTICIP { get; set; }
        public string MOVIMENTACAO { get; set; }
        public string COD_UNIMED { get; set; }
        public string COD_PLANO_CESP { get; set; }
        public Nullable<System.DateTime> DAT_PAGAMENTO { get; set; }
        public Nullable<decimal> VALOR { get; set; }
    }
}
