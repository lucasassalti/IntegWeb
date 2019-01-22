using IntegWeb.Entidades;
using IntegWeb.Intranet.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Intranet.Aplicacao.DAL
{
    public class ExtratoSaudeCorreioDAL
    {
        public INTRA_Entity_Conn m_DbContext = new INTRA_Entity_Conn();

        public List<FCESP_EXT_AMH_EXCECAO> GetData(short codEmp, Int64 numRgtroEmp)
        {
            IQueryable<FCESP_EXT_AMH_EXCECAO> query;

            query = from r in m_DbContext.FCESP_EXT_AMH_EXCECAO
                    where (r.COD_EMPRS == codEmp)
                    && (r.NUM_RGTRO_EMPRG == numRgtroEmp)
                    select r;

            return query.ToList();
        }

        public void SaveLog(FCESP_EXT_AMH_EXCECAO_LOG parametroLog)
        {
            FCESP_EXT_AMH_EXCECAO_LOG obj = new FCESP_EXT_AMH_EXCECAO_LOG();

            obj.ID_LOG = GetMaxPk();
            obj.COD_EMPRS = parametroLog.COD_EMPRS;
            obj.NUM_RGTRO_EMPRG = parametroLog.NUM_RGTRO_EMPRG;
            obj.NUM_CPF_EMPRG = parametroLog.NUM_CPF_EMPRG;
            obj.NUM_IDNTF_RPTANT = parametroLog.NUM_IDNTF_RPTANT;
            obj.DATA_INC = DateTime.Now;
            obj.TP_ACAO = parametroLog.TP_ACAO;
            obj.USER_INC = parametroLog.USER_INC;
            m_DbContext.FCESP_EXT_AMH_EXCECAO_LOG.Add(obj);
            m_DbContext.SaveChanges();
        }

        public Resultado DeleteData(short codEmp, Int64 numRgtroEmp)
        {
            Resultado res = new Resultado();

            var exclui = m_DbContext.FCESP_EXT_AMH_EXCECAO.FirstOrDefault(p => p.COD_EMPRS == codEmp
                                                                            && p.NUM_RGTRO_EMPRG == numRgtroEmp);
            if (exclui != null)
            {

                m_DbContext.FCESP_EXT_AMH_EXCECAO.Remove(exclui);

                int rows_deleted = m_DbContext.SaveChanges();
                if (rows_deleted > 0)
                {
                    res.Sucesso(String.Format("Registro excluído.", rows_deleted));
                }
            }

            return res;
        }

        public Resultado Inserir(short codEmp, Int64 numRgtroEmp, Int64 numCpfEmprg ,Int32? numIdntfRptant, string usuInc, decimal idChamado)
        {
            Resultado res = new Resultado();
            FCESP_EXT_AMH_EXCECAO obj = new FCESP_EXT_AMH_EXCECAO();

            var inserir = m_DbContext.FCESP_EXT_AMH_EXCECAO.FirstOrDefault(p => p.COD_EMPRS == codEmp
                                                                            && p.NUM_RGTRO_EMPRG == numRgtroEmp);
            if (inserir == null)
            {
                obj.COD_EMPRS = codEmp;
                obj.NUM_RGTRO_EMPRG = numRgtroEmp;
                obj.NUM_CPF_EMPRG = numCpfEmprg;
                obj.NUM_IDNTF_RPTANT = numIdntfRptant;
                obj.DT_INC = DateTime.Now;
                obj.USER_INC = usuInc;
                obj.ID_CHAMADO = idChamado;
                m_DbContext.FCESP_EXT_AMH_EXCECAO.Add(obj);
                int rows_insert = m_DbContext.SaveChanges();

                if (rows_insert > 0)
                {
                    res.Sucesso("Registro incluído com Sucesso", rows_insert);
                }

            }
            return res;
        }

        public decimal GetMaxPk()
        {
            decimal maxPK = 0;
            maxPK = m_DbContext.FCESP_EXT_AMH_EXCECAO_LOG.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.ID_LOG) + 1;
            return maxPK;
        }
    }
}
