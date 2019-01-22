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
    public class ControleCRCBLL
    {
        public bool Inserir(PagamentoCRC obj)
        {
            return new ControleCRCDAL().InserAreaAssinatura(obj);
        }

        public DataTable ListaVidas(PagamentoCRC obj)
        {
            return new ControleCRCDAL().ListaVidas(obj);
        }

        public DataTable ListaUsuario(PagamentoCRC obj)
        {
            return new ControleCRCDAL().ListaUsuario(obj);
        }

    }
}
