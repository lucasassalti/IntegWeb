using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Relatorio
{
    public class Parametro
    {
        public string parametro { get; set; }
        public string descricao { get; set; }
        public string tipo { get; set; }
        public string componente_web { get; set; }
        public string dropdowlist_consulta { get; set; }
        public string valor_inicial { get; set; }
        public string habilitado { get; set; }
        public string visivel { get; set; }
        public string permite_null { get; set; }
        public string ordem { get; set; }
        public string valor { get; set; }
    }
}
