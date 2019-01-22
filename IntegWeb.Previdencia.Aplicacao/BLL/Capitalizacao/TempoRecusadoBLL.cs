using IntegWeb.Previdencia.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class TempoRecusadoBLL : TempoRecusadoDAL
    {

        public DataTable ListarTemp()
        {
            return SelectTemp();
        }

        public bool ValidaCampos(out string msg)
        {

            StringBuilder mensagem = new StringBuilder();
            msg = "";
            bool ret = false;



            if (string.IsNullOrEmpty(empresa))
            {
                mensagem.Append("Informe a Empresa!\\n");

            }

        

            if (mensagem.Length > 0)
            {
                msg = mensagem.ToString();
                ret = true;
            }

            return ret;

        }


        public bool Inserir(out string msg, out int id)
        {
            id = 0;
            if (ValidaCampos(out msg))
            {
                return false;
            }
            return InsertTemp(out msg, out id);

        }


        public bool Alterar(out string msg)
        {

            if (ValidaCampos(out  msg))
            {
                return false;
            }
            return UpdateTemp(out msg);

        }

        public bool Deletar(out string msg)
        {

            return DeleteTemp(out msg);
        }
    }
}
