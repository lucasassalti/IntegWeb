using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Framework
{

    [Serializable]
    public class UsuarioHistorico
    {
        public int? id_usuario_historico { get; set; }
        public string login { get; set; }
        public string login_aplicacao { get; set; }
        public int? id_status { get; set; }
        public string descricao_status { get; set; }
        public string ds_justitificativa { get; set; }
        public string nome { get; set; }
        public DateTime dt_inclusao { get; set; }

        public UsuarioHistorico()
        {
            nome = null;
            id_usuario_historico = null;
            login = null;
            login_aplicacao = null;
            id_status = null;
            ds_justitificativa = null;
            dt_inclusao = DateTime.MinValue;

        }
    }

}
