using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Cartas
{
    public class TempoRecusado
    {
        public int? id_temprecusado { get; set; }
        public string empresa { get; set; }
        public int? id_pradprev { get; set; }
        public DateTime? dtadmissao { get; set; }
        public DateTime? dtdemissao { get; set; }


        public TempoRecusado() { 
        id_temprecusado =null;
        empresa=null;
        id_pradprev =null;
        dtadmissao =null;
        dtdemissao = null;
        
        }
    }
}
