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
    public class intP_CadEmpresaProtheusBLL : intP_CadEmpresaProtheusDAL
    {
        public DataTable buscarCadEmpresaProtheus()
        {
            DataTable dt = new DataTable();
            List<PATR_PRV> list = new List<PATR_PRV>();
            list = GetCadEmpresaProtheus().ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }
      
    }
}
