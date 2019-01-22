using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial
{
    public class RevisaoLei
    {
        public int? num_lei { get; set; }
        public string dsc_lei { get; set; }
        public DateTime? data_inic_vig { get; set; }
        public DateTime? data_fim_vig { get; set; }


        public RevisaoLei()
        {

            num_lei = null;
            dsc_lei = null;
            data_inic_vig = null;
            data_fim_vig = null;

        }
    }
}
