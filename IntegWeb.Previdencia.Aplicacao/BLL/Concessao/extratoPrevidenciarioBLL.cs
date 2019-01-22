using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Previdencia.Concessao;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using System.Data;

namespace IntegWeb.Previdencia.Aplicacao.BLL

{
    public class extratoPrevidenciarioBLL
    {

        public ExtratoPrevidenciario Consultar(int CodEmpresa, int txtCodMatricula)
        {
            return new extratoPrevidenciarioDAL().Consultar(CodEmpresa, txtCodMatricula);
        }

        public DataTable ListaPeriodos(int CodEmpresa, int txtCodMatricula)
        {
            DataTable dt = new extratoPrevidenciarioDAL().ListaPeriodos(CodEmpresa, txtCodMatricula);
            dt.Columns.Remove("TITULO");
            return dt;
        }

        public DataTable ListaPeriodos(int CodEmpresa, int txtCodMatricula, DateTime DatIni, DateTime DatFim)
        {
            DataTable dtTemp = new extratoPrevidenciarioDAL().ListaPeriodos(CodEmpresa, txtCodMatricula);
            dtTemp.Columns.Remove("TITULO");
            string strFiltro = "DATA_BASE >= #" + DatIni.ToString("MM/dd/yyyy") + "# AND DATA_BASE <= #" + DatFim.ToString("MM/dd/yyyy") + "#";
            DataRow[] arDR =  dtTemp.Select(strFiltro);
            DataTable dt = (arDR.Length > 0) ? arDR.CopyToDataTable() : new DataTable();
            return dt;
        }

        public List<UsuarioPortal> ConsultarRepresentantes(int CodEmpresa, int CodMatricula, int? NumIdntfRptant)
        {
            return new UsuariosPortalBLL().ListarUsuariosPortal(CodEmpresa, CodMatricula, NumIdntfRptant);
        }

    }
}