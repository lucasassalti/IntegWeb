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
    
    public partial class EMPRESA
    {
        public EMPRESA()
        {
            this.EMPREGADO = new HashSet<EMPREGADO>();
        }
    
        public short COD_EMPRS { get; set; }
        public string NOM_ABRVO_EMPRS { get; set; }
        public string NOM_RZSOC_EMPRS { get; set; }
        public string TIPO_EMPRS { get; set; }
        public Nullable<long> NUM_CGC_EMPRS { get; set; }
        public string DCR_ENDER_EMPRS { get; set; }
        public string DCR_COMPL_EMPRS { get; set; }
        public string NOM_BAIRRO_EMPRS { get; set; }
        public Nullable<int> COD_CEP_EMPRS { get; set; }
        public string COD_ESTADO { get; set; }
        public string COD_MUNICI { get; set; }
        public string COD_UNDFD_EMPRS { get; set; }
        public string NOM_CIDADE_EMPRS { get; set; }
        public string COD_DDI_EMPRS { get; set; }
        public string COD_DDD_EMPRS { get; set; }
        public Nullable<int> NUM_TELEF_EMPRS { get; set; }
        public string TIP_EMPRS { get; set; }
        public Nullable<short> NUM_FUNDAC_EMPRS { get; set; }
        public Nullable<short> NUM_GRUPO_EMPRS { get; set; }
        public Nullable<short> NUM_EMPRESA_EMPRS { get; set; }
        public string COD_ORIGEM_EMPRS { get; set; }
        public Nullable<int> NUM_ENDER_EMPRS { get; set; }
        public Nullable<short> TEPCODTIPOEMPRESA { get; set; }
        public Nullable<int> PAICOD { get; set; }
        public Nullable<int> PAICODREGISTRO { get; set; }
        public Nullable<int> PAICODOPERACAO { get; set; }
        public string COD_ORGAO_PUBLICO { get; set; }
    
        public virtual ICollection<EMPREGADO> EMPREGADO { get; set; }
    }
}
