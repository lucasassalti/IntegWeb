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
    public class ExtratoComponenteBLL : ExtratoComponenteDAL
    {

        public ExtratoComponente Consultar(int CodEmpresa, int CodMatricula, int NumIdntfRptant, Int16 Semestre, int NumAno)
        {
            return base.Consultar(CodEmpresa, CodMatricula, NumIdntfRptant);
        }

        public new DataTable Listar(int CodEmpresa, int CodMatricula, int NumIdntfRptant, string NumSubMatric, Int16 Semestre, int NumAno)
        {
            DataTable lst = base.Listar(CodEmpresa, CodMatricula, NumIdntfRptant, NumSubMatric, Semestre, NumAno);
            foreach (DataRow dr in lst.Rows)
            {
                dr["valorTotal"] = String.Format("{0:c}",                
                                        base.ListarTotal(Convert.ToInt32(dr["COD_EMPRS"]), 
                                                                        Convert.ToInt32(dr["NUM_MATRICULA"]),
                                                                        NumIdntfRptant,
                                                                        dr["NUM_SUB_MATRIC"].ToString(),
                                                                        Semestre,
                                                                        NumAno));
            }
            return lst;

        }

        public new DataTable ListarUsuarios(int CodEmpresa, int CodMatricula, int NumIdntfRptant)
        {
            DataTable dt = base.ListarUsuarios(CodEmpresa, CodMatricula, NumIdntfRptant);
            return dt;
        }

        public List<UsuarioPortal> ConsultarRepresentantes(int CodEmpresa, int CodMatricula, int? NumIdntfRptant)
        {
            return new UsuariosPortalBLL().ListarUsuariosPortal(CodEmpresa, CodMatricula, NumIdntfRptant);
        }

    }
}