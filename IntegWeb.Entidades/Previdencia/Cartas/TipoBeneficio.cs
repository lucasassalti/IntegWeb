using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Cartas
{
    public class TipoBeneficio
    {
        public int? id_tpbeneficio{ get; set; }
        public string descricao { get; set; }

        public TipoBeneficio() { id_tpbeneficio = null; descricao = null; }
    }
}
