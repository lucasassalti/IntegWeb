using IntegWeb.Entidades;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class FinanceiroBLL : FinanceiroDAL
    {

        internal List<PRE_TBL_ARQ_PAT_VERBA> cache_GrupoVerba_table = null;
        internal short cache_GrupoVerba_emprs = 0;

        internal PRE_TBL_ARQ_PAT_VERBA GetGrupoVerba(short pCOD_EMPRS, int pCOD_VERBA, short? pCOD_VERBA_PRODUTO = null)
        {
            Resultado res = new Resultado();
            if (cache_GrupoVerba_table == null || cache_GrupoVerba_emprs != pCOD_EMPRS)
            {
                cache_GrupoVerba_table = base.GetGrupoVerba2(pCOD_EMPRS);
                cache_GrupoVerba_emprs = pCOD_EMPRS;
            }

            return cache_GrupoVerba_table.FirstOrDefault(o => o.COD_VERBA == pCOD_VERBA && (o.COD_VERBA_PRODUTO == pCOD_VERBA_PRODUTO || pCOD_VERBA_PRODUTO == null));

        }
    }
}
