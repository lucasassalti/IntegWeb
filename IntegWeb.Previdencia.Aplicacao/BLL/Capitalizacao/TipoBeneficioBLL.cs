using IntegWeb.Previdencia.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class TipoBeneficioBLL : TipoBeneficioDAL
    {
        public DataTable ListaDados()
        {

            return Select();
        }

        public bool ValidaCampos(out string msg, bool isupdate)
        {
            msg = "";
            StringBuilder str = new StringBuilder();

            if (string.IsNullOrEmpty(descricao))
            {
                str.Append("Informe a Descrição.\\n");
            }

            if (!str.Equals(""))
            {
                if (isupdate)
                {
                    return Update(out msg);
                }
                else
                    return Insert(out msg);
            }
            else
                return false;

        }

        public bool DeletarDados(out string msg)
        {

            return Delete(out msg);

        }
    }
}
