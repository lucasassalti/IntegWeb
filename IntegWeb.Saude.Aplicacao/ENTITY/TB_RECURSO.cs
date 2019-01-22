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
    
    public partial class TB_RECURSO
    {
        public TB_RECURSO()
        {
            this.CADTBLRCORECURSOCODIGO = new HashSet<CADTBLRCORECURSOCODIGO>();
            this.SAU_TBL_QTDE_MATMED = new HashSet<SAU_TBL_QTDE_MATMED>();
            this.TB_VAL_RECURSO = new HashSet<TB_VAL_RECURSO>();
        }
    
        public decimal COD_RECURSO { get; set; }
        public string COD_ESPECIALID { get; set; }
        public string COD_GRUPO { get; set; }
        public string COD_ITEM { get; set; }
        public string DES_RECURSO { get; set; }
        public string COD_TIP_RECURSO { get; set; }
        public string COD_CATEGORIA { get; set; }
        public string COD_UNIDADE { get; set; }
        public string COD_LABORATORIO { get; set; }
        public string COD_APRESENTACAO { get; set; }
        public string NUM_BRASINDICE { get; set; }
        public Nullable<decimal> QTD_MEDIC { get; set; }
        public string COD_SEXO { get; set; }
        public Nullable<decimal> VAL_IDADE_INICIAL { get; set; }
        public Nullable<decimal> VAL_IDADE_FINAL { get; set; }
        public string IDC_ORT_PROT { get; set; }
        public Nullable<decimal> QTD_REUTILIZA { get; set; }
        public Nullable<decimal> QTD_FREQUENCIA { get; set; }
        public string COD_TIPO_ACOMOD { get; set; }
        public Nullable<decimal> NUM_INTERVALO { get; set; }
        public string TIP_PER_INTERVALO { get; set; }
        public Nullable<decimal> QTD_DIAS_INTERNACAO { get; set; }
        public string IDC_AUTORIZACAO { get; set; }
        public string IDC_AUDITORIA { get; set; }
        public string DES_OBSERVACAO { get; set; }
        public string IDC_INSTRUMENTADOR { get; set; }
        public string IDC_TRIBUTAVEL { get; set; }
        public string IDC_FICHA_CLINICA { get; set; }
        public string IDC_DESATIVADO { get; set; }
        public Nullable<short> COD_DESPESA_NORMAL { get; set; }
        public Nullable<short> COD_DESPESA_INTERN { get; set; }
        public Nullable<decimal> NVACODNIVELAUTORIZ { get; set; }
        public Nullable<decimal> QTD_MAX_AUTORIZ { get; set; }
        public Nullable<decimal> QTD_LIM_UTILIZ_MES { get; set; }
        public Nullable<decimal> QTD_LIM_UTILIZ_ANO { get; set; }
        public Nullable<decimal> QTD_LIM_UTILIZ_VIDA { get; set; }
        public string TIP_REGIME_ATEND { get; set; }
        public string TIP_CARATER_ATEND { get; set; }
        public string IDC_REC_REALIZ_SOLIC { get; set; }
        public string IDC_IDENTIF_EVENTO { get; set; }
        public Nullable<decimal> COD_CAPITULO { get; set; }
        public string IDC_VER_PRECEDENCIA { get; set; }
        public string IDC_EXIGE_TRATAMENTO { get; set; }
        public string NOM_ARQ_IMAGEM { get; set; }
        public string IDC_PERICIA_FINAL { get; set; }
        public Nullable<short> COD_DESPESA_ANS { get; set; }
        public string IDC_ANALISE_JUSTIFIC { get; set; }
        public string IDC_EXAME_PERICIAL { get; set; }
        public string TIP_AVALIACAO_PREVIA { get; set; }
        public string DES_RECURSO_COMPL { get; set; }
        public string DES_PERIODO_VENC { get; set; }
        public Nullable<short> NUM_VENCIMENTO { get; set; }
        public string DES_PERIODO_APLIC { get; set; }
        public Nullable<short> NUM_APLIC_3 { get; set; }
        public Nullable<short> NUM_APLIC_2 { get; set; }
        public Nullable<short> NUM_DOSES { get; set; }
        public string IDC_PER_PLANO { get; set; }
        public Nullable<System.DateTime> DAT_INI_ANSROL { get; set; }
        public Nullable<System.DateTime> DAT_FIM_ANSROL { get; set; }
        public string IDC_EXTRACAO_DENTE { get; set; }
        public string DES_RECURSO_FONEMA { get; set; }
        public string COD_GRUPO_ANS { get; set; }
        public string COD_UNIDADE_FRAC { get; set; }
        public Nullable<int> UDMSEQ { get; set; }
        public Nullable<int> UDMSEQ_FRACIONADO { get; set; }
        public Nullable<long> UDMSEQ_FRACAOMINIMA { get; set; }
        public string IDC_FRACIONADO { get; set; }
        public Nullable<decimal> QTD_FRACIONAMENTO { get; set; }
        public Nullable<decimal> QTD_FRACAO_MINIMA { get; set; }
        public string IDC_CPT { get; set; }
    
        public virtual ICollection<CADTBLRCORECURSOCODIGO> CADTBLRCORECURSOCODIGO { get; set; }
        public virtual ICollection<SAU_TBL_QTDE_MATMED> SAU_TBL_QTDE_MATMED { get; set; }
        public virtual ICollection<TB_VAL_RECURSO> TB_VAL_RECURSO { get; set; }
    }
}
