using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Previdencia
{
    public class PercentualPensao
    {
        public Int64? num_idntf_rptant { get; set; }
        public DateTime? dat_validade { get; set; }
        public decimal? pct_pensao_total { get; set; }
        public decimal? pct_pensao_dividida { get; set; }
        public string  matricula { get; set; }

        public PercentualPensao() { 
          num_idntf_rptant =null;
          dat_validade=null;
          pct_pensao_total =null;
          pct_pensao_dividida=null;
          matricula = null;
        }
    }
}
