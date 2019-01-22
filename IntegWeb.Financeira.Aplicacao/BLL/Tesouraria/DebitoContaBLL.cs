using IntegWeb.Entidades;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Financeira.Aplicacao.DAL;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.BLL
{
    public class DebitoContaBLL : DebitoContaDAL
    {
        public DataTable ListarDadosParaExcel(short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome)
        {
            DataTable dt = new DataTable();
            List<AAT_TBL_DEB_CONTA_view> list = new List<AAT_TBL_DEB_CONTA_view>();
            list = GetWhere(pEmpresa, pMatricula, pRepresentante, pCpf, pNome).ToList();
            if (list!=null){                
                dt = list.ToDataTable();
            }
            return dt;
        }
    }
}
