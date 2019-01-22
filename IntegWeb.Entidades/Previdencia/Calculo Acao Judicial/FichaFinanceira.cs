using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial
{
    public class FichaFinanceira
    {
        public int cod_emprs { get; set; }
        public int num_rgtro_emprg { get; set; }
        public int cod_verba { get; set; }
        public int ano_compet_verfin { get; set; }
        public int mes_compet_verfin { get; set; }
        public decimal vlr_verfin { get; set; }
        public int ano_pagto_verfin { get; set; }
        public int mes_pagto_verfin { get; set; }
        public int num_matr_partf { get; set; }
        public int matricula { get; set; }
        public DateTime dataInclusao { get; set; }

        public FichaFinanceira() { }
        public FichaFinanceira(
                               int cod_emprs,
                                int num_rgtro_emprg,
                                int cod_verba,
                                decimal vlr_verfin,
                                int ano_compet_verfin,
                                int mes_compet_verfin,
                                int ano_pagto_verfin,
                                int mes_pagto_verfin,
                                int num_matr_partf,
                                DateTime dataInclusao

            )
        {

            this.cod_emprs = cod_emprs;
            this.num_rgtro_emprg = num_rgtro_emprg;
            this.cod_verba = cod_verba;
            this.ano_compet_verfin = ano_compet_verfin;
            this.mes_compet_verfin = mes_compet_verfin;
            this.vlr_verfin = vlr_verfin;
            this.ano_pagto_verfin = ano_pagto_verfin;
            this.mes_pagto_verfin = mes_pagto_verfin;
            this.num_matr_partf = num_matr_partf;
            this.dataInclusao = dataInclusao;


        }


    }
}
