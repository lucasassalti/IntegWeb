//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntegWeb.Administracao.Aplicacao.ENTITY
{
    using System;
    using System.Collections.Generic;
    
    public partial class FUN_TBL_RELATORIO
    {
        public FUN_TBL_RELATORIO()
        {
            this.FUN_TBL_RELATORIO_PARAM = new HashSet<FUN_TBL_RELATORIO_PARAM>();
        }
    
        public decimal ID_RELATORIO { get; set; }
        public string RELATORIO { get; set; }
        public string TITULO { get; set; }
        public string ARQUIVO { get; set; }
        public Nullable<short> ID_TIPO_RELATORIO { get; set; }
        public string RELATORIO_EXTENSAO { get; set; }
    
        public virtual ICollection<FUN_TBL_RELATORIO_PARAM> FUN_TBL_RELATORIO_PARAM { get; set; }
        public virtual FUN_TBL_TIPO_RELATORIO FUN_TBL_TIPO_RELATORIO { get; set; }
    }
}
