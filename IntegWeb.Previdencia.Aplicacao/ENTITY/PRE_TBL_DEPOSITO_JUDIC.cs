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
    
    public partial class PRE_TBL_DEPOSITO_JUDIC
    {
        public PRE_TBL_DEPOSITO_JUDIC()
        {
            this.PRE_TBL_DEPOSITO_JUDIC_PGTO = new HashSet<PRE_TBL_DEPOSITO_JUDIC_PGTO>();
        }
    
        public int COD_DEPOSITO_JUDIC { get; set; }
        public int NUM_MATR_PARTF { get; set; }
        public Nullable<short> COD_EMPRS { get; set; }
        public Nullable<int> NUM_RGTRO_EMPRG { get; set; }
        public Nullable<long> CPF_EMPRG { get; set; }
        public string NOM_EMPRG { get; set; }
        public Nullable<System.DateTime> DAT_ADMISSAO { get; set; }
        public Nullable<System.DateTime> DAT_DEMISSAO { get; set; }
        public Nullable<System.DateTime> DAT_NASCTO { get; set; }
        public Nullable<System.DateTime> DAT_ADESAO { get; set; }
        public Nullable<System.DateTime> DIB { get; set; }
        public Nullable<System.DateTime> DIP { get; set; }
        public Nullable<short> NUM_PLBNF { get; set; }
        public string PLANO { get; set; }
        public Nullable<short> COD_SITPAR { get; set; }
        public Nullable<short> COD_TPPCP { get; set; }
        public string PERFIL { get; set; }
        public string NRO_PROCESSO { get; set; }
        public string COD_VARA_PROC { get; set; }
        public string POLO_ACJUD { get; set; }
        public Nullable<short> COD_TIPLTO { get; set; }
        public string NRO_PASTA { get; set; }
        public Nullable<System.DateTime> DAT_CADASTRO { get; set; }
        public string LOG_INCLUSAO { get; set; }
        public System.DateTime DTH_INCLUSAO { get; set; }
        public string LOG_EXCLUSAO { get; set; }
        public Nullable<System.DateTime> DTH_EXCLUSAO { get; set; }
    
        public virtual PRE_TBL_ACAO_VR_TIPLTO PRE_TBL_ACAO_VR_TIPLTO { get; set; }
        public virtual ICollection<PRE_TBL_DEPOSITO_JUDIC_PGTO> PRE_TBL_DEPOSITO_JUDIC_PGTO { get; set; }
    }
}
