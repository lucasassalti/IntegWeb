using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using Intranet.Aplicacao.DAL;
using IntegWeb.Intranet.Aplicacao.ENTITY;

namespace Intranet.Aplicacao.BLL
{
    public class PriorizaChamadoBLL : PriorizaChamadoDAL
    {

        public List<VW_PRIORIZACHAMADO> GetPriorizaChamados(int startRowIndex, int maximumRows, string paramNumChamado, string paramSiglaArea, string paramStatus, int paramIdadeStatus, string paramLoginAnalista, string sortParameter)
        {
            return base.GetData(startRowIndex, maximumRows, paramNumChamado, paramSiglaArea, paramStatus, paramIdadeStatus, paramLoginAnalista, sortParameter);
        }

        public int SelectCount(string paramNumChamado, string paramSiglaArea, string paramStatus, int paramIdadeStatus, string paramLoginAnalista)
        {
            return base.GetDataCount(paramNumChamado, paramSiglaArea, paramStatus, paramIdadeStatus, paramLoginAnalista);
        }

        public void UpdateData(string TITULO, string AREA, int? ID_USUARIO, string STATUS, string DT_INCLUSAO, string DT_TERMINO, string OBS, decimal CHAMADO)
        {
            if (ID_USUARIO != null)
            {
                base.SaveData(TITULO, AREA, ID_USUARIO, STATUS, Util.String2Date(DT_INCLUSAO), Util.String2Date(DT_TERMINO), OBS, CHAMADO);
            }
        }

        public Resultado InsertData(string TITULO, string AREA, int? ID_USUARIO, string STATUS, string DT_INCLUSAO, string DT_TERMINO, string OBS, decimal CHAMADO)
        {
            return base.InsertData(TITULO, AREA, ID_USUARIO, STATUS, Util.String2Date(DT_INCLUSAO), Util.String2Date(DT_TERMINO), OBS, CHAMADO);
        }

        public new void DeleteData(decimal CHAMADO)
        {
            base.DeleteData(CHAMADO);
        }

    }
}