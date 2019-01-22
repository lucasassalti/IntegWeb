using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Processos
{
    public class CreditoReembolso
    {
        public string empresa { get; set; }
        public string registro { get; set; }
        public string usuario { get; set; }
        public string num_sub_matric { get; set; }
        public string emissao { get; set; }
        public string previsao_credito { get; set; }
        public string reembolsado { get; set; }
    }
}
