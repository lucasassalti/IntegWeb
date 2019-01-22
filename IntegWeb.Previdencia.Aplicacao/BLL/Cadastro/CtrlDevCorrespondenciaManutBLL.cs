using IntegWeb.Previdencia.Aplicacao.DAL.Cadastro;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Entidades;
using IntegWeb.Framework;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Cadastro
{
    public class CtrlDevCorrespondenciaManutBLL : CtrlDevCorrespondenciaManutDAL
    {

        #region "TipoAcao"

        public DataTable buscarTipoAcao()
        {
            DataTable dt = new DataTable();
            List<CAD_TBL_CTRLDEV_TIPOACAO> list = new List<CAD_TBL_CTRLDEV_TIPOACAO>();
            list = GetTipoAcao().ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }

        #endregion

        #region "TipoMotivoDevolucao"
        public DataTable buscarTipoMotivoDevolucao()
        {
            DataTable dt = new DataTable();
            List<CAD_TBL_CTRLDEV_TIPOMOTDEV> list = new List<CAD_TBL_CTRLDEV_TIPOMOTDEV>();
            list = GetTipoMotivoDevolucao().ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }
        #endregion

        #region "TipoDocumento"
        public DataTable buscarTipoDocumento()
        {
            DataTable dt = new DataTable();
            List<CAD_TBL_CTRLDEV_TIPODOCUMENTO> list = new List<CAD_TBL_CTRLDEV_TIPODOCUMENTO>();
            list = GetTipoDocumento().ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }

        #endregion

        #region "FluxoAcao"
        public DataTable buscarFluxoAcao()
        {
            DataTable dt = new DataTable();
            List<CAD_VIEW_CTRLDEV_FLUXOACAO> list = new List<CAD_VIEW_CTRLDEV_FLUXOACAO>();
            list = GetFluxoAcao().ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }
      
        #endregion

    }
}
