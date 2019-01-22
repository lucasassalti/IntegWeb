using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Administracao
{
    [Serializable]
    public class GrupoMenu
    {
        public int? id_menu { get; set; }
        public string menu_pai { get; set; }
        public string menu { get; set; }
        public string area { get; set; }
        public string sistema { get; set; }
        public String listids { get; set; }
        public string descricao_status { get; set; }
        public int? id_status { get; set; }
    }
}
