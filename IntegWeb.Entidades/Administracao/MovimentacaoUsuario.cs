using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Administracao
{
      [Serializable]
    public class MovimentacaoUsuario
    {
        public int? id_movimentacao_usuario {get;set;}
        public int? id_usuario { get; set; }
        public int? id_usuario_aplicacao { get; set; }
        public int? status { get; set; }
        public int? id_grupos_acesso { get; set; }
        public DateTime dt_movimentacao { get; set; }
        public string descricao_movimentacao { get; set; }
        public string descricao_grupo { get; set; }
        public string descricao_usuario { get; set; }
        public string area { get; set; }
    }
}
