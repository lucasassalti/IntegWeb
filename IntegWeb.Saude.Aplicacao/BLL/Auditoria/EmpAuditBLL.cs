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
    public class EmpAuditBLL
    {
        private EmpAuditDAL _objd;
        public DataTable ListaTodos(EmpAudit obj)
        {
            return new EmpAuditDAL().SelectAll(obj); ;
        }

        public bool ValidaCampos(out string mensagem, EmpAudit objM, bool isUpdate)
        {

            bool ret = false;
            _objd = new EmpAuditDAL();

            if (string.IsNullOrEmpty(objM.descricao))
            {
                mensagem = "Informe a desecrição!";
                return false;
            }

            if (isUpdate)
                ret = _objd.Update(out mensagem, objM);
            else

                ret = _objd.Insert(out mensagem, objM);

            return ret;

        }

        public bool Deletar(out string mensagem, EmpAudit obj)
        {
            return new EmpAuditDAL().Delete(out mensagem, obj);
        }
    }
}
