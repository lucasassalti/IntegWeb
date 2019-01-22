using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial
{
    public class IncideJuros
    {

        public int? num_matr_partf { get; set; }
        public int? num_idntf_rptant { get; set; }
        public decimal? tx_juros { get; set; }
        public DateTime? dt_inic_vig { get; set; }
        public DateTime? dt_fim_vig { get; set; }

        public IncideJuros()
        {

            num_matr_partf = null;
            num_idntf_rptant = null;
            tx_juros = null;
            dt_inic_vig = null;
            dt_fim_vig = null;

        }

    }
}
