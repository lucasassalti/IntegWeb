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
    
    public partial class PRE_TBL_CARGA_PROTHEUS
    {
        public int COD_CARGA_PROTHEUS { get; set; }
        public Nullable<System.DateTime> DTH_PAGAMENTO { get; set; }
        public short COD_CARGA_STATUS { get; set; }
        public Nullable<System.DateTime> DTH_EXECUCAO { get; set; }
        public string IND_EXEC_IMEDIATA { get; set; }
        public string DCR_PARAMETROS { get; set; }
        public string LOG_INCLUSAO { get; set; }
        public System.DateTime DTH_INCLUSAO { get; set; }
        public Nullable<System.DateTime> DTH_INCIO_PROCESSO { get; set; }
        public Nullable<System.DateTime> DTH_FIM_PROCESSO { get; set; }
        public Nullable<System.DateTime> DTH_SUPLEMENTAR { get; set; }
        public Nullable<System.DateTime> DTH_COMPLEMENTAR { get; set; }
        public Nullable<int> COD_LOTE { get; set; }
        public Nullable<int> NUM_LOTE { get; set; }
        public Nullable<System.DateTime> DTH_DOCUMENTO_INICIAL { get; set; }
        public Nullable<System.DateTime> DTH_DOCUMENTO_FINAL { get; set; }
        public Nullable<int> COD_ASSOCIADO { get; set; }
        public int COD_CARGA_TIPO { get; set; }
        public int ID_PROTHEUS { get; set; }
    
        public virtual PRE_TBL_CARGA_PROTHEUS_STATUS PRE_TBL_CARGA_PROTHEUS_STATUS { get; set; }
        public virtual PRE_TBL_CARGA_PROTHEUS_TIPO PRE_TBL_CARGA_PROTHEUS_TIPO { get; set; }
    }
}
