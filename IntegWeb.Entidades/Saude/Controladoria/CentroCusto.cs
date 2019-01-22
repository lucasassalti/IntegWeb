using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Controladoria
{
    public class CentroCusto
    {
        public int empresa { get; set; }
        public string ds_ccusto { get; set; }
        public string sg_ccusto { get; set; }
        public string num_orgao { get; set; }
        public string cod_plano { get; set; }
        public string ccusto_deb_util { get; set; }
        public string ccusto_cre_util { get; set; }
        public string ccusto_deb_glosa { get; set; }
        public string ccusto_cre_glosa { get; set; }
        public string aux_deb_util { get; set; }
        public string aux_cre_util { get; set; }
        public string aux_deb_glosa { get; set; }
        public string aux_cre_glosa { get; set; }
        public string ir_aceita_lanc { get; set; }
        public string cod_tab { get; set; }
    }
}
