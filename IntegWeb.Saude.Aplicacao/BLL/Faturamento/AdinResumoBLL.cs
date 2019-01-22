using IntegWeb.Saude.Aplicacao.DAL.Faturamento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Faturamento
{
    public class AdinResumoBLL : AdinResumoDAL
    {
        public bool CriarRelatorio(out string mensagem)
        {
            return CreateReport(out mensagem);
        }

        public DataTable ListaRelatorio()
        {
            return SelectReport();
        }
    }
}
