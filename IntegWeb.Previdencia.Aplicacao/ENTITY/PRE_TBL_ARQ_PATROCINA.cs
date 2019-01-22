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
    
    public partial class PRE_TBL_ARQ_PATROCINA
    {
        public PRE_TBL_ARQ_PATROCINA()
        {
            this.PRE_TBL_ARQ_PATROCINA_CRITICA = new HashSet<PRE_TBL_ARQ_PATROCINA_CRITICA>();
            this.PRE_TBL_ARQ_PATROCINA_LINHA = new HashSet<PRE_TBL_ARQ_PATROCINA_LINHA>();
            this.PRE_TBL_ARQ_PATROCINA_CARGA = new HashSet<PRE_TBL_ARQ_PATROCINA_CARGA>();
            this.PRE_TBL_ARQ_PATROCINA_LOG = new HashSet<PRE_TBL_ARQ_PATROCINA_LOG>();
        }
    
        public int COD_ARQ_PAT { get; set; }
        public string NOM_ARQUIVO { get; set; }
        public Nullable<short> TIP_ARQUIVO { get; set; }
        public Nullable<long> NUM_HASH { get; set; }
        public string LOG_INCLUSAO { get; set; }
        public System.DateTime DTH_INCLUSAO { get; set; }
        public Nullable<short> ANO_REF { get; set; }
        public Nullable<short> MES_REF { get; set; }
        public short COD_STATUS { get; set; }
        public Nullable<int> NUM_QTD_VALIDOS { get; set; }
        public Nullable<int> NUM_QTD_ERROS { get; set; }
        public Nullable<int> NUM_QTD_ERROS_LINHAS { get; set; }
        public Nullable<int> NUM_QTD_ALERTAS { get; set; }
        public Nullable<int> NUM_QTD_ALERTAS_LINHAS { get; set; }
        public Nullable<int> NUM_QTD_IMPORTADOS { get; set; }
        public Nullable<int> NUM_QTD_PROCESSADOS { get; set; }
        public string GRUPO_PORTAL { get; set; }
        public Nullable<System.DateTime> DTH_EXCLUSAO { get; set; }
    
        public virtual ICollection<PRE_TBL_ARQ_PATROCINA_CRITICA> PRE_TBL_ARQ_PATROCINA_CRITICA { get; set; }
        public virtual ICollection<PRE_TBL_ARQ_PATROCINA_LINHA> PRE_TBL_ARQ_PATROCINA_LINHA { get; set; }
        public virtual ICollection<PRE_TBL_ARQ_PATROCINA_CARGA> PRE_TBL_ARQ_PATROCINA_CARGA { get; set; }
        public virtual ICollection<PRE_TBL_ARQ_PATROCINA_LOG> PRE_TBL_ARQ_PATROCINA_LOG { get; set; }
        public virtual PRE_TBL_ARQ_PATROCINA_STATUS PRE_TBL_ARQ_PATROCINA_STATUS { get; set; }
        public virtual PRE_TBL_ARQ_PATROCINA_TIPO PRE_TBL_ARQ_PATROCINA_TIPO { get; set; }
    }
}