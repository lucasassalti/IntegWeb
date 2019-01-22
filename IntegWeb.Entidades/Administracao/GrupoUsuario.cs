using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Administracao
{
       [Serializable]
    public class GrupoUsuario
    {
        public string  nome { get; set; }
        public int? matricula { get; set; }
        public String listid { get; set; }
        public string descricao_status { get; set; }
        public int? id_status { get; set; }
    }
}
