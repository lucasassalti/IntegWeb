using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Framework;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Capitalizacao
{
    class CoberturaSAPDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();


        //public List<RSC_VIEW_COBERTURA_SAP> GetData(int startRowIndex, int maximumRows, string sortParameter, string paramEmpresa, string paramMatricula, string paramChamado, string paramDTAbertura)
        //{

        //    return GetWhere(paramEmpresa, paramMatricula, paramChamado, paramDTAbertura)
        //          .GetData(startRowIndex, maximumRows, sortParameter).ToList();

        //}

        //public IQueryable<RSC_VIEW_COBERTURA_SAP> GetWhere(string paramEmpresa, string paramMatricula, string paramChamado, string paramDTAbertura)
        //{

        //    IQueryable<RSC_VIEW_COBERTURA_SAP> query;

        //    decimal dEmpresa = 0;
        //    decimal.TryParse(paramEmpresa, out dEmpresa);

        //    decimal dMatricula = 0;
        //    decimal.TryParse(paramMatricula, out dMatricula);

        //    decimal dNumChamado = 0;
        //    decimal.TryParse(paramChamado, out dNumChamado);

        //    DateTime dtDtAbertura = DateTime.Now;
        //    DateTime.TryParse(paramDTAbertura, out dtDtAbertura);

        //    query = from c in m_DbContext.RSC_VIEW_COBERTURA_SAP
        //            where (c.COD_EMPRS == dEmpresa && paramEmpresa != null || paramEmpresa == null)
        //                && (c.NUM_RGTRO_EMPRG == dMatricula && paramMatricula != null || paramMatricula == null)
        //                && (c.ID_CHAM_CD_CHAMADO == dNumChamado && paramChamado != null || paramChamado == null)
        //                && (c.DATA_SOLIC >= dtDtAbertura && c.DATA_SOLIC <= DateTime.Now && paramDTAbertura != null || paramDTAbertura == null)
        //            select c;

        //    return query;

        //}

        //public int GetDataCount(string paramEmpresa, string paramMatricula, string paramChamado, string paramDTAbertura)
        //{
        //    return GetWhere(paramEmpresa, paramMatricula, paramChamado, paramDTAbertura).SelectCount();
        //}
    }
}
