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
    
    public partial class EMPRG_DPDTE
    {
        public EMPRG_DPDTE()
        {
            this.REPRES_DEPEND_FSS = new HashSet<REPRES_DEPEND_FSS>();
            this.REPRES_UNIAO_FSS = new HashSet<REPRES_UNIAO_FSS>();
        }
    
        public short COD_EMPRS { get; set; }
        public int NUM_RGTRO_EMPRG { get; set; }
        public int NUM_IDNTF_DPDTE { get; set; }
        public Nullable<decimal> VLR_DSPCN_EMPDEP { get; set; }
        public Nullable<decimal> PCT_DSCPN_EMPDEP { get; set; }
        public Nullable<short> COD_NTPER_EMPDEP { get; set; }
        public string NUM_CTCOR_EMPDEP { get; set; }
        public Nullable<short> COD_BANCO { get; set; }
        public Nullable<int> COD_AGBCO { get; set; }
        public Nullable<short> COD_GRADPC { get; set; }
        public string TIP_CTCOR_EMPDEP { get; set; }
        public Nullable<long> NUM_CPFCTC_EMPDEP { get; set; }
        public string NOM_SEGTIT_EMPDEP { get; set; }
    
        public virtual AGENCIA AGENCIA { get; set; }
        public virtual DEPENDENTE DEPENDENTE { get; set; }
        public virtual EMPREGADO EMPREGADO { get; set; }
        public virtual ICollection<REPRES_DEPEND_FSS> REPRES_DEPEND_FSS { get; set; }
        public virtual ICollection<REPRES_UNIAO_FSS> REPRES_UNIAO_FSS { get; set; }
    }
}
