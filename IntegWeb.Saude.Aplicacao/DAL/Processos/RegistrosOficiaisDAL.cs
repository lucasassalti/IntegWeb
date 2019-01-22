using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Processos
{
    public class RegistrosOficiaisDAL
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();


        public class FC_SAU_TBL_RO_GERACAO_VIEW
        {
            public decimal COD_SAU_TBL_RO_GERACAO { get; set; }
            public decimal COD_MES_RO { get; set; }
            public decimal COD_ANO_RO { get; set; }
            public System.DateTime DTH_ACAO { get; set; }
            public string DATA_DESC
            {
                get
                {
                    return String.Format("{0} / {1}", COD_MES_RO.ToString(), COD_ANO_RO.ToString());
                }

            }
        }

        public List<FC_SAU_TBL_RO_GERACAO_VIEW> GetDados()
        {
            IQueryable<FC_SAU_TBL_RO_GERACAO_VIEW> query;

            query = from ger in m_DbContext.FC_SAU_TBL_RO_GERACAO


                    select new FC_SAU_TBL_RO_GERACAO_VIEW
                    {
                        COD_ANO_RO = ger.COD_ANO_RO,
                        COD_MES_RO = ger.COD_MES_RO


                    };

            return query.Distinct().OrderBy(x => x.COD_MES_RO).ToList();


        }

        public List<FC_SAU_TBL_RO_GERACAO_VIEW> GetRel(decimal mes, decimal ano)
        {
            IQueryable<FC_SAU_TBL_RO_GERACAO_VIEW> query;

            query = from ger in m_DbContext.FC_SAU_TBL_RO_GERACAO
                    where
                        ger.COD_MES_RO == mes &&
                        ger.COD_ANO_RO == ano
                    orderby ger.COD_SAU_TBL_RO_GERACAO

                    select new FC_SAU_TBL_RO_GERACAO_VIEW
                    {
                        COD_ANO_RO = ger.COD_ANO_RO,
                        COD_MES_RO = ger.COD_MES_RO,
                        COD_SAU_TBL_RO_GERACAO = ger.COD_SAU_TBL_RO_GERACAO,
                        DTH_ACAO = ger.DTH_ACAO
                    };

            return query.ToList();

        }

        protected void ProcessarRO(int mes, int ano)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                objConexao.AdicionarParametro("v_MES", mes);
                objConexao.AdicionarParametro("v_ANO", ano);

                objConexao.ExecutarNonQuery("ATT.FC_SAU_PKG_RO.FC_SAU_PRC_RO");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }

        }

        protected void ProcessarRel(int idRel)
        {
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {

                objConexao.AdicionarParametro("COD_REL_RO", idRel);


                objConexao.ExecutarNonQuery("ATT.FC_SAU_PKG_RO.FC_SAU_PRC_RO_PESL_SOMENTEREL");


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objConexao.Dispose();

            }
        }

        public string GetStatus()
        {

            IQueryable<FC_SAU_TBL_RO_GERACAO_STATUS> query;

            query = from st in m_DbContext.FC_SAU_TBL_RO_GERACAO_STATUS
                    where st.COD_SAU_TBL_RO_GERACAO == (m_DbContext.FC_SAU_TBL_RO_GERACAO_STATUS.Max(x => x.COD_SAU_TBL_RO_GERACAO))
                    orderby st.DTH_ACAO descending
                    select st;

            return query.FirstOrDefault().STATUS.ToString();
        }


    }
}
