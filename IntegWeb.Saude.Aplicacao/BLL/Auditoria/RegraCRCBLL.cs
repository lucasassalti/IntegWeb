using IntegWeb.Entidades.Saude.Auditoria;
using IntegWeb.Saude.Aplicacao.DAL.Auditoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Auditoria
{
    public class RegraCRCBLL
    {
        private RegraCRCDAL _objd;

        public DataTable ListaTodos(RegraCRC objM)
        {
            return new RegraCRCDAL().SelectAll(objM); ;
        }

        public bool ValidaCampos(out string mensagem, RegraCRC objM, bool isUpdate)
        {

            bool ret = false;
            _objd = new RegraCRCDAL();

            if (string.IsNullOrEmpty(objM.des_regra))
            {
                mensagem = "Informe a desecrição!";
                return false;
            }

            if (objM.valor < 1 || objM.valor==null)
            {
                mensagem = "Informe o valor da regra!";
                return false;
            }

            if (isUpdate)
                ret = _objd.Update(out mensagem, objM);
            else

                ret = _objd.Insert(out mensagem, objM);

            return ret;

        }

        public bool Deletar(out string mensagem, RegraCRC obj)
        {
            return new RegraCRCDAL().Delete(out mensagem, obj);
        }
    }
}
