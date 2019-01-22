using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Auditoria
{
    public class PagamentoCRC
    {
        public string cod_emprs { get; set; }
        public string nom_abrvo_emprs { get; set; }
        public string cod_plano { get; set; }
        public string crc { get; set; }
        int? qtde { get; set; }
        public DateTime? dt_inclusao { get; set; }
        public string matricula { get; set; }
        public string mesano { get; set; }
        public decimal? valor { get; set; }
        public decimal? nf { get; set; }
        public string cod_emprs_ct { get; set; }
        public string cod_plano_ct { get; set; }

        public PagamentoCRC() {

            cod_emprs = null;
            nom_abrvo_emprs = null;
            cod_plano = null;
            crc = null;
            qtde = null;
            dt_inclusao = null;
            matricula = null;
            mesano = null;
            valor = null;
            nf = null;
            cod_emprs_ct = null;
            cod_plano_ct = null;

        
        }
    }
}
