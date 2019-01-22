
using IntegWeb.Entidades;
using IntegWeb.Periodico.Aplicacao.DAL;
using IntegWeb.Saude.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao
{
    public class OrgaoBLL
    {
        OrgaoDAL _objd;

        public DataTable ListaTodos(Orgao obj)
        {
            _objd = new OrgaoDAL();
            return _objd.SelectAll(obj);
        }
    }
}
