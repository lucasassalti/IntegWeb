using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntegWeb.Entidades;
using IntegWeb.Saude.Aplicacao.ENTITY;

namespace IntegWeb.Saude.Aplicacao.BLL
{
    public class CentroCustoBLL : CentroCustoDAL
    {

        public List<TB_CENTRO_CUSTO> GetCCustos(int startRowIndex, int maximumRows, string paramNumOrgao, string paramCodPlano, string sortParameter)
        {
            return base.GetData(startRowIndex, maximumRows, paramNumOrgao, paramCodPlano, sortParameter);
        }

        public int SelectCount(string paramNumOrgao, string paramCodPlano)
        {
            return base.GetDataCount(paramNumOrgao, paramCodPlano);
        }

        public Resultado UpdateData(string CCUSTO_DEB_UTIL, string CCUSTO_CRE_UTIL, string CCUSTO_DEB_GLOSA, string CCUSTO_CRE_GLOSA, string AUX_DEB_UTIL, string AUX_CRE_UTIL, string AUX_DEB_GLOSA, string AUX_CRE_GLOSA, decimal NUM_ORGAO, string COD_PLANO)
        {
            return base.SaveData(CCUSTO_DEB_UTIL, CCUSTO_CRE_UTIL, CCUSTO_DEB_GLOSA, CCUSTO_CRE_GLOSA, AUX_DEB_UTIL, AUX_CRE_UTIL, AUX_DEB_GLOSA, AUX_CRE_GLOSA, NUM_ORGAO, COD_PLANO);
        }
    }
}