using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Processos
{
    public class QtdAtendimentos
    {
        public int cod_empresa {get;set;}
        public string num_matricula {get;set;}
        public string num_sub_matricula {get;set;}
        public string nome_participante { get; set; }
        public string procedimento { get; set; }
        public decimal qtd_recurso { get; set; }
        public string anoFatura { get; set; }
        public string numSeqFatura { get; set; }
    }
}
