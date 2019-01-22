using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial
{
    public class CoeficientePensao
    {
        public int? num_matr_partf { get; set; }
        public int? num_idntf_rptant { get; set; }
        public int? ano_de { get; set; }
        public int? mes_de { get; set; }
        public int? ano_ate { get; set; }
        public int? mes_ate { get; set; }
        public decimal? coef_pens_mes { get; set; }

        public CoeficientePensao() { 

               num_matr_partf =null;
               num_idntf_rptant =null;
               ano_de =null;
               mes_de =null;
               ano_ate =null;
               mes_ate =null;
               coef_pens_mes = null;
        
        }
    }
}
