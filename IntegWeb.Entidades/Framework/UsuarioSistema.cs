using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    [Serializable]
    public class UsuarioSistema
    {
        public int? id_usuario { get; set; }
        public int? id_status { get; set; }
        public string descricao_status { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string departamento { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime dt_inclusao { get; set; }
        public string justificativa { get; set; }
        public string login_aplicacao { get; set; }

        public UsuarioSistema() {
           
         descricao_status = null;
         justificativa = null;
         login_aplicacao = null;
         id_status = null;
         id_usuario =null;
         login =null;
         senha =null;
         departamento =null;
          nome =null;
          email =null;
          DateTime dt_inclusao = DateTime.MinValue;
        
        
        }
    }

}
