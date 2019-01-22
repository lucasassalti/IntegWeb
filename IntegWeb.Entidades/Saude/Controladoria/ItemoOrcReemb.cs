using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Controladoria
{
    public class ItemoOrcReemb
    {
        public string nom_abrvo_emprs { get; set; }
        public string nom_rzsoc_emprs { get; set; }
        public int cod_emprs { get; set; }
        public int cod_plano { get; set; }
        public int cod_emprs_ct { get; set; }
        public int cod_plano_ct { get; set; }
        public string cod_natureza_ct { get; set; }
        public string desc_item { get; set; }
        public string desc_plano { get; set; }
        public string desc_natureza { get; set; }
        public string situacao { get; set; }
        public string item_orcamentario { get; set; }
    }
}
