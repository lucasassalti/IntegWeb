using IntegWeb.Saude.Aplicacao.DAL.Financeiro;
using IntegWeb.Entidades.Saude.Financeiro;
using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IntegWeb.Saude.Aplicacao.BLL.Financeiro
{
    class CartasBLL
    {
        public DataTable ListaDatas ()
        {
            return new CartasDAL().ListaDatas();
        }

    }

}
