using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Processos
{
    public class ExtratoUtilizacao
    {
        public string empresa { get; set; }
        public string registro { get; set; }
        public string usuario { get; set; }
        public string num_sub_matric { get; set; }
        public string periodo_desconto { get; set; }
        public string data_emissao  { get; set; }
        public string total_servicos  { get; set; }
        public string total_pagar { get; set; }
    }
}
