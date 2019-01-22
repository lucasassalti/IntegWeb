using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Cartas
{
    public class BenefRecusado
    {
        public int? id_benefrecusado { get; set; }
        public int? id_pradprev { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string grau { get; set; }
        public string rg { get; set; }
        public string mae { get; set; }
        public string pai { get; set; }
        public DateTime? dtNascimento { get; set; }


        public BenefRecusado()
        {
            id_benefrecusado = null;
            id_pradprev = null;
            nome = null;
            cpf = null;
            grau = null;
            rg = null;
            mae = null;
            pai = null;
            dtNascimento = null;
        }
    }
}
