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
    
    public partial class TAB_CARTEIRINHA_CARENCIA
    {
        public short COD_USU_EMP { get; set; }
        public int COD_USU_MAT { get; set; }
        public short COD_USU_MATDC { get; set; }
        public short COD_USU_SUB { get; set; }
        public short COD_USU_SUBDC { get; set; }
        public Nullable<short> EMPRESA { get; set; }
        public Nullable<int> MATRICULA { get; set; }
        public string DESC_CART { get; set; }
        public string DESC_CART_EMP { get; set; }
        public string DESC_CART1 { get; set; }
        public string NOM_RED { get; set; }
        public string TEXTO_FIXO { get; set; }
        public Nullable<System.DateTime> DT_LIMIT_CATEG { get; set; }
        public string MSG_1 { get; set; }
        public string MSG_2 { get; set; }
        public string MSG_3 { get; set; }
        public string MSG_4 { get; set; }
        public string MSG_5 { get; set; }
        public string MSG_CARENCIA { get; set; }
        public string MSG_CARENCIA_UCETI { get; set; }
        public Nullable<System.DateTime> DT_CARENCIA_UCETI { get; set; }
        public string MSG_CARENCIA_IA { get; set; }
        public Nullable<System.DateTime> DT_CARENCIA_IA { get; set; }
        public string MSG_CARENCIA_IQ2L { get; set; }
        public Nullable<System.DateTime> DT_CARENCIA_IQ2L { get; set; }
        public string MSG_CARENCIA_PARTO { get; set; }
        public Nullable<System.DateTime> DT_CARENCIA_PARTO { get; set; }
        public string MSG_CARENCIA_PARTO_IA { get; set; }
        public Nullable<System.DateTime> DT_CARENCIA_PARTO_IA { get; set; }
        public string EMP { get; set; }
        public string PONTO { get; set; }
        public string MAT { get; set; }
        public string NOM_TIT_REP { get; set; }
        public string SIGLA_DEPART { get; set; }
        public string DESC_SIGLA_DEPART { get; set; }
        public string END_CORR { get; set; }
        public string BAIRRO_CORR { get; set; }
        public string CEP_CID_CORR { get; set; }
        public long SEQUENCIAL { get; set; }
        public System.DateTime DT_SOLIC_CI { get; set; }
        public Nullable<short> TIPO_CARTAO { get; set; }
        public Nullable<System.DateTime> DT_GRAFICA { get; set; }
        public string IDC_2VIA_CI { get; set; }
        public Nullable<short> LOCAL_ENTREGA_CI { get; set; }
        public long NUM_SEQ_PARTICIP { get; set; }
        public string NUM_REGISTRO_ANS { get; set; }
        public string NUM_NACIONAL_SAUDE { get; set; }
    }
}