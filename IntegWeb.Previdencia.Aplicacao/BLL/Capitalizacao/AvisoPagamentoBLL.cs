using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Entidades.Cartas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class AvisoPagamentoBLL : AvisoPagamentoDAL
    {

        public DataTable ConsultarAvisoPgto()
        {
            return SelecionarAvisoPgto();
        }

    }


}
