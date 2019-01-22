using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Entidades.Previdencia.Concessao
{
    public class DadosPrevidenciarios
    {
        public int cod_emprs { get; set; }
        public int num_rgtro_emprg { get; set; }
        public int num_digvr_emprg { get; set; }
        public string nom_emprg { get; set; }
        public string nom_patroc { get; set; }
        public DateTime dat_admss_emprg { get; set; }
        public string Dcr_Tppcp { get; set; }
        public string Dcr_Sitpar { get; set; }
        public DateTime adesao { get; set; } //Dat_Vncfdc_Partf
        public int tip_opctribir_adplpr { get; set; }
    }
}
