using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IntegWeb.Intranet.Aplicacao.DAL;
using System.Threading.Tasks;

namespace IntegWeb.Intranet.Aplicacao
{
    public class BeneficiariosComIncentivoBLL : BeneficiariosComIncentivoDAL
    {
        public DataTable geraBeneficiariosIncentivo(int emp, int matricula)
        {
            DataTable dt = DependentesIncentivo(emp, matricula);
            return dt;
        }

        public DataTable geraParticipantesIncentivo(int emp, int matricula)
        {
            DataTable dt = ParticipantesIncentivo(emp,matricula);
            return dt;
        }
    }
}
