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
    
    public partial class PRE_TBL_ARQ_ENV_REPASSE_LINHA
    {
        public int COD_ARQ_ENV_REP_LINHA { get; set; }
        public int COD_ARQ_ENV_REPASSE { get; set; }
        public Nullable<short> COD_EMPRS { get; set; }
        public Nullable<int> NUM_RGTRO_EMPRG { get; set; }
        public Nullable<int> COD_VERBA { get; set; }
        public string COD_VERBA_PATROCINA { get; set; }
        public Nullable<decimal> VLR_PERCENTUAL { get; set; }
        public Nullable<decimal> VLR_DESCONTO { get; set; }
        public System.DateTime DTH_INCLUSAO { get; set; }
        public string LOG_INCLUSAO { get; set; }
        public Nullable<System.DateTime> DTH_EXCLUSAO { get; set; }
        public string LOG_EXCLUSAO { get; set; }
        public Nullable<int> NUM_RGTRO_EMPRG_ORIG { get; set; }
        public Nullable<System.DateTime> DTH_REFERENCIA { get; set; }
    
        public virtual PRE_TBL_ARQ_ENV_REPASSE PRE_TBL_ARQ_ENV_REPASSE { get; set; }
    }
}
