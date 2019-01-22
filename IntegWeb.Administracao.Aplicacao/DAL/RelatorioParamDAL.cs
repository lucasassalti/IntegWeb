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
    public class RelatorioParamDAL
    {
        public EntitiesConn m_DbContext = new EntitiesConn();

        public FUN_TBL_RELATORIO_PARAM GetParametro(int pID_RELATORIO_PARAM)
        {
            return GetWhere(pID_RELATORIO_PARAM, null).ToList().FirstOrDefault();
        }

        public List<FUN_TBL_RELATORIO_PARAM> GetData(int startRowIndex, int maximumRows, int pID_RELATORIO, string sortParameter)
        {
            return GetWhere(null, pID_RELATORIO)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<FUN_TBL_RELATORIO_PARAM> GetWhere(int? pID_RELATORIO_PARAM, int? pID_RELATORIO)
        {
            IQueryable<FUN_TBL_RELATORIO_PARAM> query;
            query = from p in m_DbContext.FUN_TBL_RELATORIO_PARAM
                    where (p.ID_RELATORIO_PARAMETRO == pID_RELATORIO_PARAM || pID_RELATORIO_PARAM == null)
                       && (p.ID_RELATORIO == pID_RELATORIO || pID_RELATORIO == null)
                    select p;
            return query;
        }

        public int GetDataCount(int pID_RELATORIO)
        {
            return GetWhere(null, pID_RELATORIO).SelectCount();
        }

        public Resultado DeleteParam(decimal ID_RELATORIO_PARAMETRO)
        {
            Resultado res = new Resultado();

            var deleta = m_DbContext.FUN_TBL_RELATORIO_PARAM.FirstOrDefault(p => p.ID_RELATORIO_PARAMETRO == ID_RELATORIO_PARAMETRO);
            if (deleta != null)
            {
                m_DbContext.FUN_TBL_RELATORIO_PARAM.Remove(deleta);
                int rows_deleted = m_DbContext.SaveChanges();

                if (rows_deleted > 0)
                {
                    res.Sucesso(String.Format("{0} registro(s) excluído(s).", rows_deleted));
                }
            }

            return res;
        }

        public Resultado UpdateData(FUN_TBL_RELATORIO_PARAM uptParametro)
        {
            Resultado res = new Resultado();
            try
            {
                var atualiza = m_DbContext.FUN_TBL_RELATORIO_PARAM.FirstOrDefault(p => p.ID_RELATORIO_PARAMETRO == uptParametro.ID_RELATORIO_PARAMETRO);
                if (atualiza != null)
                {                    
                    atualiza.ID_RELATORIO = int.Parse(uptParametro.ID_RELATORIO.ToString());
                    atualiza.PARAMETRO = uptParametro.PARAMETRO;
                    atualiza.DESCRICAO = uptParametro.DESCRICAO;
                    atualiza.TIPO = uptParametro.TIPO;
                    atualiza.COMPONENTE_WEB = uptParametro.COMPONENTE_WEB;
                    atualiza.DROPDOWLIST_CONSULTA = uptParametro.DROPDOWLIST_CONSULTA;
                    atualiza.VALOR_INICIAL = uptParametro.VALOR_INICIAL;
                    atualiza.HABILITADO = uptParametro.HABILITADO;
                    atualiza.VISIVEL = uptParametro.VISIVEL;
                    atualiza.PERMITE_NULL = uptParametro.PERMITE_NULL;
                    atualiza.ORDEM = uptParametro.ORDEM;

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

        public Resultado InsertData(FUN_TBL_RELATORIO_PARAM uptParametro)
        {
            Resultado res = new Resultado();
            try
            {

                var ja_existe = m_DbContext.FUN_TBL_RELATORIO_PARAM.FirstOrDefault(p => p.ID_RELATORIO_PARAMETRO == uptParametro.ID_RELATORIO_PARAMETRO);
                if (ja_existe == null)
                {
                    decimal max_key = m_DbContext.FUN_TBL_RELATORIO_PARAM.Max(r => r.ID_RELATORIO_PARAMETRO);
                    uptParametro.ID_RELATORIO_PARAMETRO = max_key + 1;
                    m_DbContext.FUN_TBL_RELATORIO_PARAM.Add(uptParametro);
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

        //public List<object> GetParametrosTipos()
        //{
        //    IQueryable<object> query;
        //    query = from c in m_DbContext.FUN_TBL_RELATORIO_PARAM 
        //            select(
        //            new  
        //            { 
        //                Text = c.TIPO, 
        //                Value = c.TIPO
        //            });
        //    return query.Distinct().ToList(); 
        //}

    }
}
