//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntegWeb.Financeira.Aplicacao.ENTITY
{
    using System;
    using System.Collections.Generic;
    
    public partial class AAT_TBL_RET_DEB_CONTA_CRITICAS
    {
        public string DCR_NOM_ARQ { get; set; }
        public int NUM_SEQ_LINHA { get; set; }
        public string COD_CRITICA { get; set; }
        public string DCR_CRITICA { get; set; }
    
        public virtual AAT_TBL_RET_DEB_CONTA AAT_TBL_RET_DEB_CONTA { get; set; }
    }
}
