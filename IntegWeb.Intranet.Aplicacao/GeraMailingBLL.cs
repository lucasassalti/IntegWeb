using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Aplicacao.DAL;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Intranet.Aplicacao.DAL;
using System.Data;


namespace Intranet.Aplicacao.BLL
{
   public class GeraMailingBLL
    {
        public DataTable RetornaDtSaude()
        {
            var retorno = new GeraMailingDAL().ExtRelSau();

            return retorno;
        }
        public DataTable RetornaDtPrevidencia()
        {
            var retorno = new GeraMailingDAL().ExtRelPrev();






            return retorno;
        }
    }
}
