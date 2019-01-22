using IntegWeb.Administracao.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Administracao.Aplicacao.DAL
{
    public class RelatorioDAL
    {
        public EntitiesConn m_DbContext = new EntitiesConn();

        public FUN_TBL_RELATORIO GetRelatorio(decimal pID_RELATORIO)
        {
            return GetWhere(pID_RELATORIO, null, null).ToList().FirstOrDefault();
        }

        public List<FUN_TBL_RELATORIO> GetData(int startRowIndex, int maximumRows, string pRELATORIO, string pTITULO, string sortParameter)
        {
            return GetWhere(null, pRELATORIO, pTITULO)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<FUN_TBL_RELATORIO> GetWhere(decimal? pID_RELATORIO, string pRELATORIO, string pTITULO)
        {
            IQueryable<FUN_TBL_RELATORIO> query;
            query = from c in m_DbContext.FUN_TBL_RELATORIO
                    where (c.RELATORIO.ToUpper().Contains(pRELATORIO.ToUpper().Trim()) || pRELATORIO == null)
                       && (c.TITULO.ToUpper().Contains(pTITULO.ToUpper().Trim()) || pTITULO == null)
                       && (c.ID_RELATORIO == pID_RELATORIO || pID_RELATORIO == null)
                    select c;
            return query;
        }

        public int GetDataCount(string pRELATORIO, string pTITULO)
        {
            return GetWhere(null, pRELATORIO, pTITULO).SelectCount();
        }

        public Resultado UpdateData(FUN_TBL_RELATORIO uptRelatorio)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.FUN_TBL_RELATORIO.FirstOrDefault(p => p.ID_RELATORIO == uptRelatorio.ID_RELATORIO);
                if (atualiza != null)
                {
                    atualiza.RELATORIO = uptRelatorio.RELATORIO;
                    atualiza.TITULO = uptRelatorio.TITULO;
                    atualiza.ARQUIVO = uptRelatorio.ARQUIVO;
                    atualiza.ID_TIPO_RELATORIO = uptRelatorio.ID_TIPO_RELATORIO;
                    atualiza.RELATORIO_EXTENSAO = uptRelatorio.RELATORIO_EXTENSAO;
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso(String.Format("{0} registro(s) atualizado(s).", rows_updated));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro("Problemas contate o administrador do sistema: \\n" + ex.Message);
            }
            return res;
        }

        public Resultado InsertData(FUN_TBL_RELATORIO uptRelatorio)
        {
            Resultado res = new Resultado();
            try
            {

                var ja_existe = m_DbContext.FUN_TBL_RELATORIO.FirstOrDefault(p => p.ID_RELATORIO == uptRelatorio.ID_RELATORIO);
                if (ja_existe == null)
                {
                    decimal max_key = m_DbContext.FUN_TBL_RELATORIO.Max(r => r.ID_RELATORIO);
                    uptRelatorio.ID_RELATORIO = max_key + 1;
                    m_DbContext.FUN_TBL_RELATORIO.Add(uptRelatorio);
                    int rows_inserted = m_DbContext.SaveChanges();
                    if (rows_inserted > 0)
                    {
                        res.Sucesso(String.Format("{0} registro(s) inserido(s).", rows_inserted));
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro("Problemas contate o administrador do sistema: \\n" + ex.Message);
            }

            return res;
        }

        public Resultado DeleteData(decimal key_ID_RELATORIO)
        {
            Resultado res = new Resultado();

            var deleta = m_DbContext.FUN_TBL_RELATORIO.FirstOrDefault(p => p.ID_RELATORIO == key_ID_RELATORIO);
            if (deleta != null)
            {

                int param_count = deleta.FUN_TBL_RELATORIO_PARAM.Count; 

                // Exclui os parametros relacionados:
                deleta.FUN_TBL_RELATORIO_PARAM.ToList().ForEach(p => m_DbContext.FUN_TBL_RELATORIO_PARAM.Remove(p));

                m_DbContext.FUN_TBL_RELATORIO.Remove(deleta);
                int rows_deleted = m_DbContext.SaveChanges();
                
                if (rows_deleted > 0)
                {
                    res.Sucesso(String.Format("{0} registro(s) excluído(s).", rows_deleted - param_count));
                }
            }

            return res;
        }

        public List<FUN_TBL_TIPO_RELATORIO> GetRelatorioTipos()
        {
            IQueryable<FUN_TBL_TIPO_RELATORIO> query;
            query = from c in m_DbContext.FUN_TBL_TIPO_RELATORIO
                    select c;
            return query.ToList();
        }

    }
}
