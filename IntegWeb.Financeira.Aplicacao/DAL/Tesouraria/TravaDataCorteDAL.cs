using IntegWeb.Entidades;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.DAL.Tesouraria
{
    public class TravaDataCorteDAL
    {
        public EntitiesConn m_DbContext = new EntitiesConn();

        public FC_TBL_PARAMETRO GetParameter()
        {
            IQueryable<FC_TBL_PARAMETRO> query;

            query = from p in m_DbContext.FC_TBL_PARAMETRO
                    where p.ID_SISTEMA == 6
                    && p.NOM_PARAMETRO == "FLG_DTA_CORTE"
                    select p;

            return query.FirstOrDefault();
        }

        public Resultado Update(string valParametro)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.FC_TBL_PARAMETRO.FirstOrDefault(p => p.NOM_PARAMETRO == "FLG_DTA_CORTE");

                if (atualiza != null)
                {
                    atualiza.VALOR_PARAMETRO = valParametro;
                }
                int rows_updated = m_DbContext.SaveChanges();
                if (rows_updated == 1)
                {
                    res.Sucesso("Registro atualizado com sucesso.");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }

        public void SaveLog(FUN_TBL_PARAMETRO_LOG parametroLog)
        {
            FUN_TBL_PARAMETRO_LOG obj = new FUN_TBL_PARAMETRO_LOG();

            obj.ID_LOG = GetMaxPk();
            obj.ID_PARAMETRO = parametroLog.ID_PARAMETRO;
            obj.LOGIN = parametroLog.LOGIN;
            obj.DAT_ALTERACAO = DateTime.Now;
            obj.COLUNA_ALTERACAO = parametroLog.COLUNA_ALTERACAO;
            obj.VALOR_ALTERACAO = parametroLog.VALOR_ALTERACAO;
            m_DbContext.FUN_TBL_PARAMETRO_LOG.Add(obj);
            m_DbContext.SaveChanges();
        }

        public decimal GetMaxPk()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.FUN_TBL_PARAMETRO_LOG.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_LOG) + 1;
            return maxPK;
        }
    }
}
