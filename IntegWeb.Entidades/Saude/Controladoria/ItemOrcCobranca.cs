using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Controladoria
{
    public class ItemOrcCobranca
    {
        public string Nom_Abrvo_Emprs { get; set; }
        public string Nom_Rzsoc_Emprs { get; set; }
        public int?    Cod_Emprs { get; set; }
        public int?    Cod_Plano { get; set; }
        public int?    Cod_Emprs_Ct { get; set; }
        public int?    Cod_Plano_Ct { get; set; }
        public int?    Cod_Tipo_Comp { get; set; }
        public string Desc_Tipo_Comp { get; set; }
        public int?    Cod_Grupo { get; set; }
        public string Desc_Grupo { get; set; }
        public string Item_Orcamentario { get; set; }
        public string Fcesp_Natureza { get; set; }
        public string Suplem_Natureza { get; set; }
        public string Patroc_Natureza { get; set; }
        public string Compl_Natureza { get; set; }

        public ItemOrcCobranca()
        {

            Nom_Abrvo_Emprs = null;
            Nom_Rzsoc_Emprs = null;
            Cod_Emprs = null;
            Cod_Plano = null;
            Cod_Emprs_Ct = null;
            Cod_Plano_Ct = null;
            Cod_Tipo_Comp = null;
            Desc_Tipo_Comp = null;
            Cod_Grupo = null;
            Desc_Grupo = null;
            Item_Orcamentario = null;
            Fcesp_Natureza = null;
            Suplem_Natureza = null;
            Patroc_Natureza = null;
            Compl_Natureza = null;


        }
    }
   
}
