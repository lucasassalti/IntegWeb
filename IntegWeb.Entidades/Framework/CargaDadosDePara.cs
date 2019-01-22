using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Carga
{
    public class CargaDadosDePara
    {
        public int? id_dp { get; set; }
        public string tabela_destino { get; set; }
        public string origem_campo { get; set; }
        public int? origem_tipo { get; set; }
        public string destino_campo { get; set; }
        public int? destino_tipo { get; set; }
        public string destino_valor_padrao { get; set; }
        public int? ordem { get; set; }

        // tipos de campos:
        // 1 = TEXT
        // 2 = NUMBER
        // 3 = DECIMAL
        // 4 = DATETIME
    }
}
