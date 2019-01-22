using IntegWeb.Entidades.Previdencia;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class PensaoBLL
    {
        PensaoDAL _obj;

        public DataTable ListaReprsentante(PercentualPensao obj)
        {
            return new PensaoDAL().ListaRepresentante(obj);
        }

        public DataTable ListaPensao(PercentualPensao obj)
        {
            return new PensaoDAL().ListaPercentual(obj);
        }

        public bool ValidaCampos(out string mensagem, PercentualPensao objM, bool isUpdate)
        {

            bool ret = false;

            if (objM.dat_validade == DateTime.MinValue)
            {
                mensagem = "Informe a data de Validade!";
                return false;
            }
            if (objM.pct_pensao_dividida < 1)
            {
                mensagem = "Informe o percentual dividido!";
                return false;
            }
            if (objM.pct_pensao_dividida < 1)
            {
                mensagem = "Informe o percentual total!";
                return false;
            }
            if (objM.num_idntf_rptant < 1)
            {
                mensagem = "Informe o representante!";
                return false;
            }


            _obj = new PensaoDAL();

            if (isUpdate)
                ret = _obj.Atualizar(out mensagem, objM);
            else
                ret = _obj.Inserir(out mensagem, objM);

            return ret;

        }

        public bool Deletar(out string mensagem, PercentualPensao objM)
        {
            _obj = new PensaoDAL();
            return _obj.Deletar(out mensagem, objM);
        }
    }
}
