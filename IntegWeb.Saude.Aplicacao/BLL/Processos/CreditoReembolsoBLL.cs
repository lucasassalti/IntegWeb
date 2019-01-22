using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Saude.Aplicacao.DAL;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using System.Data;

namespace IntegWeb.Saude.Aplicacao.BLL

{
    public class CreditoReembolsoBLL
    {

        public CreditoReembolso Consultar(int CodEmpresa, int CodMatricula, int NumIdntfRptant, string NumSubMatric, DateTime DatIni, DateTime DatFim)
        {
            return new CreditoReembolsoDAL().Consultar(CodEmpresa, CodMatricula, NumIdntfRptant, NumSubMatric, DatIni, DatFim);
        }

        public DataTable Listar(int CodEmpresa, int CodMatricula, int NumIdntfRptant, string NumSubMatric, DateTime DatIni, DateTime DatFim)
        {
            return new CreditoReembolsoDAL().Listar(CodEmpresa, CodMatricula, NumIdntfRptant, NumSubMatric, DatIni, DatFim);
        }

        public DataTable ListarUsuarios(int CodEmpresa, int CodMatricula, int NumIdntfRptant)
        {
            DataTable dt = new CreditoReembolsoDAL().ListarUsuarios(CodEmpresa, CodMatricula, NumIdntfRptant,1); // Quadro 1
            dt.Columns.Remove("COD_EMPRS");
            dt.Columns.Remove("NUM_MATRICULA");
            return dt;
        }

        public DataTable ListarUsuariosCRM(int CodEmpresa, int CodMatricula, int NumIdntfRptant)
        {
            DataTable dt = new CreditoReembolsoDAL().ListarUsuarios(CodEmpresa, CodMatricula, NumIdntfRptant, 3); //Quadro 3
            dt.Columns.Remove("COD_EMPRS");
            dt.Columns.Remove("NUM_MATRICULA");
            return dt;
        }

        public List<UsuarioPortal> ConsultarRepresentantes(int CodEmpresa, int CodMatricula, int? NumIdntfRptant)
        {
            return new UsuariosPortalBLL().ListarUsuariosPortal(CodEmpresa, CodMatricula, NumIdntfRptant);
        }

        public int ConsultarQtdRegistrosCarga(DateTime DatIni, DateTime DatFim)
        {
            return new CreditoReembolsoDAL().ConsultarQtdRegistrosCarga(DatIni, DatFim);
        }
        
        public bool ExecutarCargaCredito(DateTime DatIni, DateTime DatFim)
        {
            return new CreditoReembolsoDAL().ExecutarCargaCredito(DatIni, DatFim);
        }

        public CargaCreditoReembolsoTotal getBuscaTotalizadorReembolso(String DataBusca, String DataFim)
        {
            return new CreditoReembolsoDAL().getTotalizadorReembolso(DataBusca, DataFim);
        }

    }
}