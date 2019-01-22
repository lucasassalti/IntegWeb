using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Cartas
{
    public class TipoServico
    {
        public int? id_tpservico { get; set; }
        public string descricao { get; set; }

        public TipoServico() { id_tpservico = null; descricao = null; }
    }
}
