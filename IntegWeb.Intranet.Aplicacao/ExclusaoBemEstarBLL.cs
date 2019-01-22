using IntegWeb.Entidades;
using IntegWeb.Intranet.Aplicacao.DAL;
using IntegWeb.Intranet.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Intranet.Aplicacao
{
    public class ExclusaoBemEstarBLL : ExclusaoBemEstarDAL
    {

        public List<FUN_TBL_EXCLUSAO_REVISTA> GetData(short codEmp, Int32 numRgtroEmp)
        {
            return base.GetData(codEmp, numRgtroEmp);
        }

        public Resultado DeleteData(short codEmp, Int32 numRgtroEmp)
        {
            return base.DeleteData(codEmp,numRgtroEmp);
        }

        public Resultado Inserir(short codEmp, Int32 numRgtroEmp, Int32? numIdntfRptant, string usuInc)
        {
            return base.Inserir(codEmp, numRgtroEmp, numIdntfRptant, usuInc);
        }


    }
}
