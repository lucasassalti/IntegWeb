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
    
    public partial class SAU_VW_CONSULTA_USUARIO_SUS
    {
        public string COD_IDENTIFICACAO { get; set; }
        public decimal NUM_SEQ_PARTICIP { get; set; }
        public short COD_EMPRS { get; set; }
        public string NUM_RGTRO_EMPRG { get; set; }
        public string NUM_SUB_MATRIC { get; set; }
        public string NOM_PARTICIP { get; set; }
        public string SIT_PARTICIP { get; set; }
        public System.DateTime DAT_NASCIMENTO { get; set; }
        public System.DateTime DAT_ADESAO { get; set; }
        public Nullable<System.DateTime> DAT_CANCELAMENTO { get; set; }
        public decimal COD_PLANO_PERIODO { get; set; }
        public string DES_PLANO_PERIODO { get; set; }
        public string ENDERECO_TITULAR { get; set; }
        public Nullable<int> NUM_ENDERECO_TITULAR { get; set; }
        public string COMPLEMENTO_TITULAR { get; set; }
        public string CEP_TITULAR { get; set; }
        public string COD_DDD_TEL_TITULAR { get; set; }
        public string TELEFONE_TITULAR { get; set; }
        public string COD_DDD_CEL_TITULAR { get; set; }
        public Nullable<int> CELULAR_TITULAR { get; set; }
    }
}