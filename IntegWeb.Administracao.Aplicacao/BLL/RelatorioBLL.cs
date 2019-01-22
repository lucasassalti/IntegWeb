using IntegWeb.Administracao.Aplicacao.ENTITY;
using IntegWeb.Administracao.Aplicacao.DAL;
using IntegWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Administracao.Aplicacao.BLL
{
    public class RelatorioBLL : RelatorioDAL
    {

        public FUN_TBL_RELATORIO ConsultarRelatorio(int ID_RELATORIO)
        {
            return base.GetRelatorio(ID_RELATORIO);
        }

        public List<FUN_TBL_RELATORIO> GetRelatorios(int startRowIndex, int maximumRows, string pRELATORIO, string pTITULO, string sortParameter)
        {
            return base.GetData(startRowIndex, maximumRows, pRELATORIO, pTITULO, sortParameter);
        }

        public int SelectCount(string pRELATORIO, string pTITULO)
        {
            return base.GetDataCount(pRELATORIO, pTITULO);        
        }

        public Resultado DeleteData(decimal ID_RELATORIO)
        {
            return base.DeleteData(ID_RELATORIO);
        }

        public new Resultado UpdateData(FUN_TBL_RELATORIO uptRelatorio)
        {
            return base.UpdateData(uptRelatorio);
        }

        public new Resultado InsertData(FUN_TBL_RELATORIO uptRelatorio)
        {
            return base.InsertData(uptRelatorio);
        }

        public new List<FUN_TBL_TIPO_RELATORIO> GetRelatorioTipos()
        {
            return base.GetRelatorioTipos();
        }

        public FUN_TBL_RELATORIO_PARAM ConsultarParametro(int ID_RELATORIO_PARAM)
        {
            return new RelatorioParamDAL().GetParametro(ID_RELATORIO_PARAM);
        }

        public List<FUN_TBL_RELATORIO_PARAM> GetParametros(int startRowIndex, int maximumRows, int pID_RELATORIO, string sortParameter)
        {
            return new RelatorioParamDAL().GetData(startRowIndex, maximumRows, pID_RELATORIO, sortParameter);
        }

        public int SelectCountParam(int pID_RELATORIO)
        {
            return new RelatorioParamDAL().GetDataCount(pID_RELATORIO);
        }

        public Resultado DeleteParam(decimal ID_RELATORIO_PARAMETRO)
        {
            return new RelatorioParamDAL().DeleteParam(ID_RELATORIO_PARAMETRO);
        }

        public Resultado UpdateParam(FUN_TBL_RELATORIO_PARAM uptRelatorio)
        {
            return new RelatorioParamDAL().UpdateData(uptRelatorio);
        }

        public Resultado InsertParam(FUN_TBL_RELATORIO_PARAM uptRelatorio)
        {
            return new RelatorioParamDAL().InsertData(uptRelatorio);
        }

        //public List<object> GetParametrosTipos()
        //{
        //    return new RelatorioParamDAL().GetParametrosTipos();
        //}

    }
}
