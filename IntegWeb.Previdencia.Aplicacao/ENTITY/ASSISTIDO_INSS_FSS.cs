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
    
    public partial class ASSISTIDO_INSS_FSS
    {
        public int NUM_SQNCL_ASINSS { get; set; }
        public Nullable<int> NUM_MATR_PARTF { get; set; }
        public Nullable<int> NUM_IDNTF_DPDTE { get; set; }
        public Nullable<short> COD_ESPBNF { get; set; }
        public Nullable<System.DateTime> DAT_APINSS_ASINSS { get; set; }
        public Nullable<decimal> VLR_APINSS_ASINSS { get; set; }
        public Nullable<System.DateTime> DAT_ATUINS_ASINSS { get; set; }
        public Nullable<decimal> VLR_ATUINS_ASINSS { get; set; }
        public Nullable<short> QTD_ANOCTB_ASINSS { get; set; }
        public Nullable<long> NUM_PRCINS_ASINSS { get; set; }
        public Nullable<short> COD_SITBNF_ASINSS { get; set; }
        public Nullable<short> COD_BNFINS { get; set; }
        public Nullable<short> QTD_MESCTB_ASINSS { get; set; }
        public Nullable<short> QTD_DIACTB_ASINSS { get; set; }
        public Nullable<short> COD_CNCBNF_ASINSS { get; set; }
        public Nullable<short> COD_MANBNF_ASINSS { get; set; }
        public Nullable<short> COD_PAGBNF_ASINSS { get; set; }
        public Nullable<short> TIP_LEIPRV_ASINS { get; set; }
        public Nullable<System.DateTime> DAT_INIPGT_ASINSS { get; set; }
        public Nullable<System.DateTime> DAT_ENTREQ_ASINSS { get; set; }
        public Nullable<short> SBNCODSITUACAOBENEF { get; set; }
        public Nullable<short> NUM_DIGVR_ASSINSS { get; set; }
        public Nullable<System.DateTime> DAT_FIMCCB_ASINSS { get; set; }
        public Nullable<System.DateTime> DAT_ANTINSS_ASINSS { get; set; }
    
        public virtual PARTICIPANTE_FSS PARTICIPANTE_FSS { get; set; }
        public virtual DEPENDENTE DEPENDENTE { get; set; }
    }
}
