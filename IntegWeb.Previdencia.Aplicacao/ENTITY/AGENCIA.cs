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
    
    public partial class AGENCIA
    {
        public AGENCIA()
        {
            this.EMPREGADO = new HashSet<EMPREGADO>();
            this.REPRES_UNIAO_FSS = new HashSet<REPRES_UNIAO_FSS>();
            this.TB_CONVENENTE = new HashSet<TB_CONVENENTE>();
            this.EMPRG_DPDTE = new HashSet<EMPRG_DPDTE>();
        }
    
        public short COD_BANCO { get; set; }
        public int COD_AGBCO { get; set; }
        public string NOM_AGBCO { get; set; }
        public string COD_ESTADO { get; set; }
        public string COD_MUNICI { get; set; }
        public string NOM_PRACA_AGBCO { get; set; }
        public string COD_UNDFD_AGBCO { get; set; }
        public string NOM_CIDAD_AGBCO { get; set; }
        public string COD_DIGVER_AGBCO { get; set; }
        public Nullable<int> PAICOD { get; set; }
    
        public virtual ICollection<EMPREGADO> EMPREGADO { get; set; }
        public virtual ICollection<REPRES_UNIAO_FSS> REPRES_UNIAO_FSS { get; set; }
        public virtual ICollection<TB_CONVENENTE> TB_CONVENENTE { get; set; }
        public virtual ICollection<EMPRG_DPDTE> EMPRG_DPDTE { get; set; }
        public virtual MUNICIPIO MUNICIPIO { get; set; }
    }
}
