using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial
{
    public class IndiceRevisao
    {
        public int? num_matr_partf { get; set; }
        public int? num_lei { get; set; }
        public int? ano_lei { get; set; }
        public int? mes_lei { get; set; }
        public decimal? ind_lei { get; set; }

        public IndiceRevisao()
        {

            num_matr_partf = null;
            num_lei = null;
            ano_lei = null;
            mes_lei = null;
            ind_lei = null;


        }
    }
}
