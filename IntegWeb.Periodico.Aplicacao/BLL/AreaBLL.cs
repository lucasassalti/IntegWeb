
using IntegWeb.Entidades;
using IntegWeb.Entidades.Periodico;
using IntegWeb.Saude.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao
{
    public class AreaBLL
    {
        private AreaDAL _obj;
        private Area _objM;
        private DataTable _dt;

         
        public DataTable ListaTodos(Area obj)
        {
            return new AreaDAL().SelectAll(obj);
        }

        public bool ValidaCampos(out string mensagem, Area objM, bool isUpdate)
        {

            bool ret = false;

     
            if (string.IsNullOrEmpty(objM.descricao))
            {
                mensagem = "Informe o a Descrição!";
                return false;
            }
         

            if (string.IsNullOrEmpty(objM.edificio))
            {
                mensagem = "Informe o Edifício!";
                return false;
            }

            if (string.IsNullOrEmpty(objM.andar))
            {
                mensagem = "Informe o Andar!";
                return false;
            }

            if (string.IsNullOrEmpty(objM.responsavel))
            {
                mensagem = "Informe o Responsável!";
                return false;
            }
            _obj = new AreaDAL();

            if (isUpdate)
                ret = _obj.Update(out mensagem, objM);
            else

                ret = _obj.Insert(out mensagem, objM);

            return ret;

        }

        public bool Deletar(Area objM, out string msg){

          bool ret = new AreaDAL().Delete(out msg, objM);

          if (!ret)
              msg = "ATENÇÃO! \\n\\nNão foi possível deletar o registro, verifique se a área não esta vinculado a assinatura.";
          
          return ret;
        }

     
    
    }
}
