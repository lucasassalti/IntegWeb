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
    
    public partial class FATTBLRSP
    {
        public string HDRDATHOR { get; set; }
        public string HDRCODUSU { get; set; }
        public string HDRCODETC { get; set; }
        public string HDRCODPGR { get; set; }
        public decimal COD_TIPO_COND_CONT { get; set; }
        public System.DateTime DAT_VALIDADE { get; set; }
        public System.DateTime RSPDATVALIDINI { get; set; }
        public decimal RSPVALPRIORIDADE { get; set; }
        public string RSPTIPPRECO { get; set; }
        public decimal RSPVALTAXA { get; set; }
        public decimal RSPVALADICAO { get; set; }
        public string RSPCODSIMPROINI { get; set; }
        public string RSPCODSIMPROFIM { get; set; }
        public Nullable<decimal> FSOSEQ { get; set; }
        public string RSPCODMERCADO { get; set; }
        public string RSPCNDCAIXASELECAO { get; set; }
    
        public virtual TB_COND_CONTRAT TB_COND_CONTRAT { get; set; }
    }
}
