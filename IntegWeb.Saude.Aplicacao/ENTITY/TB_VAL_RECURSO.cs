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
    
    public partial class TB_VAL_RECURSO
    {
        public decimal COD_TAB_RECURSO { get; set; }
        public decimal COD_RECURSO { get; set; }
        public System.DateTime DAT_VAL_RECURSO { get; set; }
        public string COD_PORTE_ANEST { get; set; }
        public Nullable<decimal> VAL_RECURSO { get; set; }
        public Nullable<decimal> QTD_FILME { get; set; }
        public Nullable<long> QTD_AUXILIARES { get; set; }
        public Nullable<long> NUM_INCIDENCIAS { get; set; }
        public Nullable<decimal> QTD_HONORARIO { get; set; }
        public string RCOCODPROCEDIMENTO { get; set; }
        public Nullable<decimal> QTD_UCO { get; set; }
        public string PRECODPORTEREC { get; set; }
        public Nullable<decimal> VAL_PESO { get; set; }
        public Nullable<decimal> VAL_UCO { get; set; }
        public Nullable<decimal> PCT_BANDA_APLIC { get; set; }
        public Nullable<int> RCOSEQ { get; set; }
    
        public virtual CADTBLRCORECURSOCODIGO CADTBLRCORECURSOCODIGO { get; set; }
        public virtual TB_RECURSO TB_RECURSO { get; set; }
        public virtual TB_TAB_RECURSO TB_TAB_RECURSO { get; set; }
    }
}
