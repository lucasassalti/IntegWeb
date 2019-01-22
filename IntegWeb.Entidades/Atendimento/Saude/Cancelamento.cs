using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Atendimento.Saude
{
    public class Cancelamento
    {
        public string empresa { get; set; }
        public string matricula { get; set; }
        public string protocolo { get; set; }
        public string nomePlano { get; set; }
        public string responsavel { get; set; }
        public string nomeBeneficiario { get; set; }
    }
}
