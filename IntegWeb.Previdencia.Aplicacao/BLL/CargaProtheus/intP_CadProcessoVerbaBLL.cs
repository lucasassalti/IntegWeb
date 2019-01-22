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
    public class intP_CadProcessoVerbaBLL : intP_CadProcessoVerbaDAL
    {
        public DataTable buscarCadProcessoVerbaProtheus()
        {
            DataTable dt = new DataTable();
            List<VRB_NEGOCIO> list = new List<VRB_NEGOCIO>();
            list = GetCadProcessoVerbaProtheus().ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }
    }
}
