using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Saude.Aplicacao.DAL;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using System.Data;

namespace IntegWeb.Saude.Aplicacao.BLL

{
    public class ExtratoUtilizacaoBLL
    {

        public ExtratoUtilizacao Consultar(int CodEmpresa, int CodMatricula, int NumIdntfRptant, DateTime DatIni, DateTime DatFim)
        {
            return new ExtratoUtilizacaoDAL().Consultar(CodEmpresa, CodMatricula, NumIdntfRptant, DatIni, DatFim);
        }

        public DataTable Listar(int CodEmpresa, int CodMatricula, int NumIdntfRptant, string NumSubMatric, DateTime DatIni, DateTime DatFim)
        {
            return new ExtratoUtilizacaoDAL().Listar(CodEmpresa, CodMatricula, NumIdntfRptant, NumSubMatric, DatIni, DatFim);
        }

        public DataTable ListarUsuarios(int CodEmpresa, int CodMatricula, int NumIdntfRptant)
        {
            DataTable dt = new ExtratoUtilizacaoDAL().ListarUsuarios(CodEmpresa, CodMatricula, NumIdntfRptant);
            return dt;
        }

        public List<UsuarioPortal> ConsultarRepresentantes(int CodEmpresa, int CodMatricula, int? NumIdntfRptant)
        {
            return new UsuariosPortalBLL().ListarUsuariosPortal(CodEmpresa, CodMatricula, NumIdntfRptant);
        }

        public int ConsultarQtdRegistrosCarga(DateTime DatMovimento)
        {
            return new ExtratoUtilizacaoDAL().ConsultarQtdRegistrosCarga(DatMovimento);
        }

        public bool ExecutarCargaExtratoUtilizacao(DateTime DatMovimento)
        {
            return new ExtratoUtilizacaoDAL().ExecutarCargaExtratoUtilizacao(DatMovimento);
        }

    }
}