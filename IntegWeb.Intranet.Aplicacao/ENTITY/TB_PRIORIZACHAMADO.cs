//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IntegWeb.Intranet.Aplicacao.ENTITY
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_PRIORIZACHAMADO
    {
        public decimal CHAMADO { get; set; }
        public string TITULO { get; set; }
        public string AREA { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> DT_INCLUSAO { get; set; }
        public Nullable<System.DateTime> DT_TERMINO { get; set; }
        public Nullable<decimal> ID_USUARIO { get; set; }
        public string OBS { get; set; }
        public Nullable<System.DateTime> DT_STATUS { get; set; }
    }
}