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
    
    public partial class PRE_TBL_CARGA_PROTHEUS_STATUS
    {
        public PRE_TBL_CARGA_PROTHEUS_STATUS()
        {
            this.PRE_TBL_CARGA_PROTHEUS = new HashSet<PRE_TBL_CARGA_PROTHEUS>();
        }
    
        public short COD_STATUS { get; set; }
        public string DCR_STATUS { get; set; }
    
        public virtual ICollection<PRE_TBL_CARGA_PROTHEUS> PRE_TBL_CARGA_PROTHEUS { get; set; }
    }
}
