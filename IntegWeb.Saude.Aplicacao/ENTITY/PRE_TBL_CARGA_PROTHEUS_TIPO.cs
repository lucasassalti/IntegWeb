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
    
    public partial class PRE_TBL_CARGA_PROTHEUS_TIPO
    {
        public PRE_TBL_CARGA_PROTHEUS_TIPO()
        {
            this.PRE_TBL_CARGA_PROTHEUS = new HashSet<PRE_TBL_CARGA_PROTHEUS>();
        }
    
        public int COD_CARGA_TIPO { get; set; }
        public string DCR_CARGA_TIPO { get; set; }
        public Nullable<short> IND_PROC_PARCIAL { get; set; }
        public string AREA { get; set; }
        public string DCR_PACKAGE_PARAM { get; set; }
    
        public virtual ICollection<PRE_TBL_CARGA_PROTHEUS> PRE_TBL_CARGA_PROTHEUS { get; set; }
    }
}
