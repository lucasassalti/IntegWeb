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
    
    public partial class PRE_TBL_POR_UREF
    {
        public short EMPRESA { get; set; }
        public short PLANO { get; set; }
        public short SEXO { get; set; }
        public short TB_ATUARIAL { get; set; }
        public Nullable<short> CODIGO_UM { get; set; }
        public string DESCRICAO_UM { get; set; }
        public Nullable<decimal> VALOR { get; set; }
        public Nullable<decimal> VALOR_MEDIO { get; set; }
        public Nullable<decimal> TETO_INSS { get; set; }
        public Nullable<decimal> PERC_MINIMO { get; set; }
        public Nullable<decimal> PERC_INV { get; set; }
        public Nullable<decimal> LIM_PERC { get; set; }
        public Nullable<System.DateTime> DT_REFERENCIA { get; set; }
        public Nullable<decimal> UQP { get; set; }
        public Nullable<decimal> JUROSA { get; set; }
        public Nullable<decimal> JUROSPADRAP { get; set; }
        public Nullable<decimal> JUROSMAX { get; set; }
        public string LOG_INCLUSAO { get; set; }
        public System.DateTime DTH_INCLUSAO { get; set; }
        public string LOG_EXCLUSAO { get; set; }
        public Nullable<System.DateTime> DTH_EXCLUSAO { get; set; }
    }
}