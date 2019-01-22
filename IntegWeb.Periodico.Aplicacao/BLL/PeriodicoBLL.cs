
using IntegWeb.Entidades;
using IntegWeb.Saude.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Periodico.Aplicacao
{
    public  class PeriodicoBLL
    {
        private PeriodicoDAL _objd;

        public DataTable ListaTodos(PeriodicoObj obj) 
        {
            _objd = new PeriodicoDAL();
            return _objd.SelectAll(obj);
        }

        public bool ValidaCampos(out string mensagem, PeriodicoObj objM, bool isUpdate)
        {

            bool ret = false;

            if (objM.id_editora==null)
            {
                mensagem = "Informe a Editora!";
                return false;
            }
          
            if (string.IsNullOrEmpty(objM.nome_periodico))
            {
                mensagem = "Informe o nome do Periódico";
                return false;
            }

            _objd = new PeriodicoDAL();

            if (isUpdate)
                ret = _objd.Update(out mensagem, objM);
            else

                ret = _objd.Insert(out mensagem, objM);

            return ret;

        }

        public bool Deletar(out string mensagem, PeriodicoObj objM)
        {
            _objd = new PeriodicoDAL();
            return _objd.Delete(out mensagem, objM);
        }

    }
}
