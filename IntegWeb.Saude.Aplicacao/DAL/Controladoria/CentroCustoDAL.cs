using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System.Data.Objects;

namespace IntegWeb.Saude.Aplicacao.BLL
{
    public class CentroCustoDAL
    {

        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        //public List<TB_CENTRO_CUSTO> GetData(int startRowIndex, int maximumRows, string sortParameter)
        //{
        //    return m_DbContext.TB_CENTRO_CUSTO.GetData(startRowIndex, maximumRows, sortParameter).ToList();
        //}

        public List<TB_CENTRO_CUSTO> GetData(int startRowIndex, int maximumRows, string paramNumOrgao, string paramCodPlano, string sortParameter)
        {
            //return m_DbContext.TB_CENTRO_CUSTO.GetData(startRowIndex, maximumRows, searchParameter, sortParameter).ToList();

            return GetWhere(paramNumOrgao, paramCodPlano)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();

        }

        public IQueryable<TB_CENTRO_CUSTO> GetWhere(string paramNumOrgao, string paramCodPlano)
        {

            IQueryable<TB_CENTRO_CUSTO> query;

            decimal dNUM_ORGAO = 0;
            decimal.TryParse(paramNumOrgao, out dNUM_ORGAO);

            query = from c in m_DbContext.TB_CENTRO_CUSTO
                    where (c.NUM_ORGAO == dNUM_ORGAO && paramNumOrgao != null || paramNumOrgao == null)
                        && (c.COD_PLANO == paramCodPlano || paramCodPlano == null)
                    select c;

            return query;

        }

        public int GetDataCount(string paramNumOrgao, string paramCodPlano)
        {
            return GetWhere(paramNumOrgao, paramCodPlano).SelectCount();
        }

        public Resultado SaveData(string upt_CCUSTO_DEB_UTIL, string upt_CCUSTO_CRE_UTIL, string upt_CCUSTO_DEB_GLOSA, string upt_CCUSTO_CRE_GLOSA, string upt_AUX_DEB_UTIL, string upt_AUX_CRE_UTIL, string upt_AUX_DEB_GLOSA, string upt_AUX_CRE_GLOSA, decimal key_NUM_ORGAO, string key_COD_PLANO)
        {
            Resultado res = new Resultado();

            var atualiza = m_DbContext.TB_CENTRO_CUSTO.FirstOrDefault(p => p.NUM_ORGAO == key_NUM_ORGAO && p.COD_PLANO == key_COD_PLANO);
            if (atualiza != null)
            {
                atualiza.CCUSTO_DEB_UTIL = upt_CCUSTO_DEB_UTIL;
                atualiza.CCUSTO_CRE_UTIL = upt_CCUSTO_CRE_UTIL;
                atualiza.CCUSTO_DEB_GLOSA = upt_CCUSTO_DEB_GLOSA;
                atualiza.CCUSTO_CRE_GLOSA = upt_CCUSTO_CRE_GLOSA;
                atualiza.AUX_DEB_UTIL = upt_AUX_DEB_UTIL;
                atualiza.AUX_CRE_UTIL = upt_AUX_CRE_UTIL;
                atualiza.AUX_DEB_GLOSA = upt_AUX_DEB_GLOSA;
                atualiza.AUX_CRE_GLOSA = upt_AUX_CRE_GLOSA;
                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated > 0)
                {
                    res.Sucesso(String.Format("{0} registros atualizados.", rows_updated));
                }
            }

            return res;
        }
    }
}