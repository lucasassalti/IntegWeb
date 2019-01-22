using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Intranet.Aplicacao.DAL;
using System.Data;
namespace IntegWeb.Intranet.Aplicacao
{
    public class RelatorioFestaAposentadosBLL : RelatorioFestaAposentadosDAL
    {
        public DataTable geraRelatorio(DateTime dtInicio, DateTime dtFim, string forma)
        {
            DataTable dt = new DataTable();
            dt = geraRelatorioIntervalo(dtInicio, dtFim, forma);
            return dt;
        }
        public DataTable geraRelatorioGeral(string forma)
        {
            DataTable dt = new DataTable();
            dt = geraRelatorioTudo(forma);
            return dt;
        }
    }
}
