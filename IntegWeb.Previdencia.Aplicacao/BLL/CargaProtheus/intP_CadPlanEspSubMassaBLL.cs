using IntegWeb.Previdencia.Aplicacao.DAL.Int_Protheus;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Entidades;
using IntegWeb.Framework;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Int_Protheus
{
    public class intP_CadPlanEspSubMassaBLL : intP_CadPlanEspSubMassaDAL
    {
        public DataTable buscarCadPlanEspSubMassaProtheus()
        {
            DataTable dt = new DataTable();
            List<PLN_SUBMASSA> list = new List<PLN_SUBMASSA>();
            list = GetCadPlanEspSubMassaProtheus().ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }
    }
}
