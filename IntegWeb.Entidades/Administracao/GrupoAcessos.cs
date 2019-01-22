using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Administracao
{
    [Serializable]
    public class GrupoAcessos
    {
        public int? id_grupo_acessos { get; set; }
        public string area { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string descricao_status { get; set; }
        public int? id_status { get; set; }
        public List<GrupoUsuario> usuarios { get; set; }
        public List<GrupoMenu> menus { get; set; }


        public GrupoAcessos()
        {
            id_grupo_acessos = null;
            area = null;
            nome = null;
            descricao = null;
            descricao_status = null;
            id_status = null;
            menus = new List<GrupoMenu>();
            usuarios = new List<GrupoUsuario>();
        }
    }
}
