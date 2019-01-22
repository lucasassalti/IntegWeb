using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Periodico
{
    public class Area
    {
        public int? id_area { get; set; }
        public string sigla { get; set; }
        public string codigo { get; set; }
        public string descricao { get; set; }
        public string responsavel { get; set; }
        public string edificio { get; set; }
        public string andar { get; set; }

        public Area() { 
        
             id_area =null;
             sigla =null;
             codigo ="";
             descricao =null;
             responsavel =null;
             edificio=null;
             andar = null;
        }

    }
}
