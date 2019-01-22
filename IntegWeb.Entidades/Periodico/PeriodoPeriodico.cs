using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    public class PeriodoPeriodico
    {
        public int? cod_periodico { get; set; }
        public string desc_periodo { get; set; }

        public PeriodoPeriodico()
        {

            cod_periodico = null;
            desc_periodo = null;

        }
    }

}
