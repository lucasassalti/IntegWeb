using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    public class Cidade
    {
        public int codigo { get; set; }
        public string nome { get; set; }
        public string nome_resumido { get; set; }
        public string estado_sigla { get; set; }
        public string estado { get; set; }
        public int cod_ibge  { get; set; }
        public int? cod_ibge_digito { get; set; }
    }
}
