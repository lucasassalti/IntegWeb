using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Objects; 
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Intranet.Aplicacao.ENTITY;
using IntegWeb.Framework;

namespace Intranet.Aplicacao.DAL
{
    public class PriorizaChamadoDAL 
    {

        public INTRA_Entity_Conn m_DbContext = new INTRA_Entity_Conn();

        //public List<TB_PRIORIZACHAMADO> GetData(int startRowIndex, int maximumRows, string sortParameter)
        //{
        //    return m_DbContext.TB_PRIORIZACHAMADO.GetData(startRowIndex, maximumRows, sortParameter).ToList();
        //}

        public List<VW_PRIORIZACHAMADO> GetData(int startRowIndex, int maximumRows, string paramNumChamado, string paramSiglaArea, string paramStatus, int paramIdadeStatus, string paramLoginAnalista, string sortParameter)
        {
            //return m_DbContext.TB_PRIORIZACHAMADO.GetData(startRowIndex, maximumRows, searchParameter, sortParameter).ToList();

            if (String.IsNullOrEmpty(sortParameter))
            {
                sortParameter = "DT_TERMINO";
            }

            return GetWhere(paramNumChamado, paramSiglaArea, paramStatus, paramIdadeStatus, paramLoginAnalista)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();            

        }

        public IQueryable<VW_PRIORIZACHAMADO> GetWhere(string paramNumChamado, string paramSiglaArea, string paramStatus, int paramIdadeStatus, string paramLoginAnalista)
        {

            IQueryable<VW_PRIORIZACHAMADO> query;

            decimal dCHAMADO = 0;
            decimal.TryParse(paramNumChamado, out dCHAMADO);

            decimal dUSUARIO = 0;
            decimal.TryParse(paramLoginAnalista, out dUSUARIO);

                query = from c in m_DbContext.VW_PRIORIZACHAMADO
                        //join d in m_DbContext.FUN_TBL_USUARIO on c.ID_USUARIO equals d.ID_USUARIO
                        where (c.CHAMADO == dCHAMADO || dCHAMADO == 0)
                           && (c.STATUS == paramStatus || paramStatus == "0")
                           && (c.AREA == paramSiglaArea || paramSiglaArea == "0")
                           && (c.ID_USUARIO == dUSUARIO || dUSUARIO == 0)
                           //&& (DateTime.Now.Subtract(c.DT_STATUS.Value).Days > paramIdadeStatus || paramIdadeStatus == 0)
                           && (EntityFunctions.DiffDays(c.DT_STATUS.Value, DateTime.Now) > paramIdadeStatus || paramIdadeStatus == 0)
                        select c;
            
            //else
            //{
            //    query = from c in m_DbContext.TB_PRIORIZACHAMADO
            //            select c;
            //}

            return query; 

        }

        public int GetDataCount(string paramNumChamado, string paramSiglaArea, string paramStatus, int paramIdadeStatus, string paramLoginAnalista)
        {
            return GetWhere(paramNumChamado, paramSiglaArea, paramStatus, paramIdadeStatus, paramLoginAnalista).SelectCount();
        }

        public Resultado SaveData(string upt_TITULO, string upt_AREA, int? upt_ID_USUARIO, string upt_STATUS, DateTime? upt_DT_INCLUSAO, DateTime? upt_DT_TERMINO, string upt_OBS, decimal key_CHAMADO)
        {
            Resultado res = new Resultado();

            var atualiza = m_DbContext.TB_PRIORIZACHAMADO.FirstOrDefault(p => p.CHAMADO == key_CHAMADO);
                if (atualiza != null)
                {
                    atualiza.TITULO = upt_TITULO;
                    atualiza.AREA = upt_AREA;
                    atualiza.ID_USUARIO = upt_ID_USUARIO;
                    if (!atualiza.STATUS.Equals(upt_STATUS))
                    {
                        atualiza.DT_STATUS = DateTime.Now.Date;
                    }
                    atualiza.STATUS = upt_STATUS;
                    atualiza.DT_INCLUSAO = upt_DT_INCLUSAO;
                    atualiza.DT_TERMINO = upt_DT_TERMINO;
                    atualiza.OBS = upt_OBS;
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso(String.Format("{0} registros atualizados.", rows_updated));
                    }
                }

            return res;
        }

        public Resultado InsertData(string new_TITULO, string new_AREA, int? new_ID_USUARIO, string new_STATUS, DateTime? new_DT_INCLUSAO, DateTime? new_DT_TERMINO, string new_OBS, decimal new_CHAMADO)
        {
            Resultado res = new Resultado();

            try
            {
                var inseri = m_DbContext.TB_PRIORIZACHAMADO.FirstOrDefault(p => p.CHAMADO == new_CHAMADO);
                if (inseri == null)
                {

                    var PC = new TB_PRIORIZACHAMADO
                    {
                        CHAMADO = new_CHAMADO,
                        TITULO = new_TITULO,
                        AREA = new_AREA,
                        ID_USUARIO = new_ID_USUARIO,
                        STATUS = new_STATUS,
                        DT_INCLUSAO = new_DT_INCLUSAO,
                        DT_TERMINO = new_DT_TERMINO,
                        DT_STATUS = new_DT_INCLUSAO,
                        OBS = new_OBS
                    };

                    m_DbContext.TB_PRIORIZACHAMADO.Add(PC);

                    int rows_inserted = m_DbContext.SaveChanges();
                    if (rows_inserted > 0)
                    {
                        res.Sucesso(String.Format("Registro inserido.", rows_inserted));
                    }
                }
                else
                {
                    res.Erro("Chamado já esta priorizado para outro analista.");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }

            return res;
        }

        public Resultado DeleteData(decimal key_CHAMADO)
        {
            Resultado res = new Resultado();

            var exclui = m_DbContext.TB_PRIORIZACHAMADO.FirstOrDefault(p => p.CHAMADO == key_CHAMADO);
            if (exclui != null)
            {

                m_DbContext.TB_PRIORIZACHAMADO.Remove(exclui);

                int rows_inserted = m_DbContext.SaveChanges();
                if (rows_inserted > 0)
                {
                    res.Sucesso(String.Format("Registro excluído.", rows_inserted));
                }
            }

            return res;
        }

        //public int GetMaxKey()
        //{
        //    var atualiza = m_DbContext.TB_PRIORIZACHAMADO.Max(p => p.CHAMADO).ToString();
        //    return int.Parse(atualiza)+1;
        //}

    }
}