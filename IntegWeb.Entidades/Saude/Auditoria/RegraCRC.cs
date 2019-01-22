using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Auditoria
{
    public class RegraCRC
    {
        public int? id_regra { get; set; }
        public string des_regra { get; set; }
        public decimal? valor { get; set; }

        public RegraCRC()
        {
            id_regra = null;
            des_regra = null;
            valor = null;
        }
    }


}
