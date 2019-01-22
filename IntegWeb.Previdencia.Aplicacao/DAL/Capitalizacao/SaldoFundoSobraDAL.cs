using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using System.Data;
using IntegWeb.Framework;
using System.Data.Entity.Validation;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Capitalizacao
{
    public class SaldoFundoSobraDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public List<PRE_TBL_ARQ_SALDO_FUNDO> GetData(int startRowIndex, int maximumRows, string sortParameter, int? pMes, int? pAno, int? codEmp)
        {
            return GetWhere(pMes, pAno, codEmp)
                   .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<PRE_TBL_ARQ_SALDO_FUNDO> GetWhere(int? pMes, int? pAno, int? codEmp)
        {
            IQueryable<PRE_TBL_ARQ_SALDO_FUNDO> query;

            query = from u in m_DbContext.PRE_TBL_ARQ_SALDO_FUNDO
                        where (u.ANO_REF == pAno || pAno == null)
                        && (u.MES_REF == pMes || pMes == null)
                        && (u.COD_GRUPO_EMPRS == codEmp || codEmp == 0)

                        select u;

            return query;
        }

        public int GetDataCount(int? pMes, int? pAno, int? codEmp)
        {
            return GetWhere(pMes, pAno, codEmp).SelectCount();
        }



        public List<PRE_TBL_GRUPO_EMPRS> GetPRE_TBL_GRUPO_EMPRS()
        {
            IQueryable<PRE_TBL_GRUPO_EMPRS> dropSaldo;

            dropSaldo = from u in m_DbContext.PRE_TBL_GRUPO_EMPRS
                        select  u;

            return dropSaldo.ToList();

           
        }


        public Resultado InserirSaldo(PRE_TBL_ARQ_SALDO_FUNDO obj)
        {
            Resultado res = new Resultado();

            try
            {

                var atualiza = m_DbContext.PRE_TBL_ARQ_SALDO_FUNDO.FirstOrDefault(p => p.COD_GRUPO_EMPRS == obj.COD_GRUPO_EMPRS );

                if (atualiza == null)
                {
                    m_DbContext.PRE_TBL_ARQ_SALDO_FUNDO.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }
                }
                else
                {
                    res.Alert("Esse registro já existe! ");
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }

        public Resultado AtualizaSaldo(PRE_TBL_ARQ_SALDO_FUNDO obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.PRE_TBL_ARQ_SALDO_FUNDO.FirstOrDefault(p => (p.COD_GRUPO_EMPRS == obj.COD_GRUPO_EMPRS) && (p.ANO_REF == obj.ANO_REF) && (p.MES_REF == obj.MES_REF) && (p.NUM_PLBNF == obj.NUM_PLBNF));

                if (atualiza != null)
                {
                    atualiza.VLR_SALDO_FUNDO = obj.VLR_SALDO_FUNDO;
                    atualiza.LOG_EXCLUSAO = obj.LOG_EXCLUSAO;
                    atualiza.DTH_EXCLUSAO = obj.DTH_EXCLUSAO;
                    
                    int rows_updated = m_DbContext.SaveChanges();

                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro Atualizado com Sucesso");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }


    }
}



