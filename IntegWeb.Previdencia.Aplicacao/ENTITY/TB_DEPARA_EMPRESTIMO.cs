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
    
    public partial class TB_DEPARA_EMPRESTIMO
    {
        public int SEQ { get; set; }
        public short EMP_ORIGEM { get; set; }
        public long MAT_ORIGEM { get; set; }
        public Nullable<int> NUM_MATR_PARTF { get; set; }
        public short EMP_DESTINO { get; set; }
        public long MAT_DESTINO { get; set; }
        public System.DateTime DAT_INCLUSAO { get; set; }
        public string DAT_EXCLUSAO { get; set; }
    }
}
