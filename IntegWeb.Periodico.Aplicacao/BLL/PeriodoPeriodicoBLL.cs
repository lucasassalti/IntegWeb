
using  IntegWeb.Entidades;
using  IntegWeb.Periodico.Aplicacao.DAL;
using  IntegWeb.Saude.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Periodico.Aplicacao
{
    public class PeriodoPeriodicoBLL 
    {
        private PeriodoPeriodicoDAO _objd;
        public DataTable ListaTodos(PeriodoPeriodico obj)
        {
            _objd = new PeriodoPeriodicoDAO();
            return _objd.SelectAll(obj); ; 
        }

        public bool ValidaCampos(out string mensagem, PeriodoPeriodico objM, bool isUpdate)
        {

            bool ret = false;
            _objd = new PeriodoPeriodicoDAO();

            if (string.IsNullOrEmpty(objM.desc_periodo))
            {
                mensagem = "Informe a desecrição do Periodo!";
                return false;
            }

            if (isUpdate)
                ret = _objd.Update(out mensagem, objM);
            else

                ret = _objd.Insert(out mensagem, objM);

            return ret;

        }

        public bool Deletar(out string mensagem, PeriodoPeriodico obj)
        {
            _objd = new PeriodoPeriodicoDAO();
            return _objd.Delete(out mensagem, obj);
        }
    }
}
