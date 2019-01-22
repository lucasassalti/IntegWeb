using IntegWeb.Entidades.Relatorio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Framework.Aplicacao
{
    public class RelatorioBLL
    {
        public Relatorio Listar(string relatorio)
        {
            return new RelatorioDAL().Consultar(relatorio);
        }
        public DataTable ListarDrop(string query)
        {
            return new RelatorioDAL().ConsultarDrop(query);
        }
        public DataTable ListarDinamico(Relatorio query)
        {
            return new RelatorioDAL().ConsultarPkg(query);
        }



    }
}
