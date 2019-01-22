using IntegWeb.Entidades;
using IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Previdencia.Aplicacao.ENTITY;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Capitalizacao
{
    public class ContribEsporadicaDAL
    {

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public class PRE_TBL_CONTRIB_ESPORADICA_view
        {
            public short? EMPRESA { get; set; }
            public int? REGISTRO { get; set; }
            public int? REPRESENTANTE { get; set; }
            public string NOME { get; set; }
            public System.DateTime DAT_VENCIMENTO { get; set; }
            public Nullable<decimal> VLR_CONTRIB { get; set; }
            public long? NUM_CPF { get; set; }
            public Nullable<long> COD_BOLETO { get; set; }
            public DateTime? DTH_INCLUSAO { get; set; }
        }

        public List<PRE_TBL_CONTRIB_ESPORADICA_view> GetData(int startRowIndex, int maximumRows, short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome, string sortParameter)
        {
            return GetWhere(pEmpresa, pMatricula, pRepresentante, pCpf, pNome)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<PRE_TBL_CONTRIB_ESPORADICA_view> GetWhere(short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome)
        {
            IQueryable<PRE_TBL_CONTRIB_ESPORADICA_view> query;
            query = from a in m_DbContext.PRE_TBL_CONTRIB_ESPORADICA
                    from e in m_DbContext.EMPREGADO
                    where (a.COD_EMPRS == e.COD_EMPRS)
                       && (a.NUM_RGTRO_EMPRG == e.NUM_RGTRO_EMPRG)
                       && (a.DTH_EXCLUSAO == null)
                       && (a.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (a.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       && (e.NUM_CPF_EMPRG == pCpf || pCpf == null)
                       && (e.NOM_EMPRG.ToLower().Contains(pNome.ToLower()) || pNome == null)
                    select new PRE_TBL_CONTRIB_ESPORADICA_view()
                    {
                        EMPRESA = a.COD_EMPRS,
                        REGISTRO = a.NUM_RGTRO_EMPRG,
                        NOME = e.NOM_EMPRG,
                        DAT_VENCIMENTO = a.DAT_VENCIMENTO,
                        VLR_CONTRIB = a.VLR_CONTRIB,
                        NUM_CPF = e.NUM_CPF_EMPRG,
                        COD_BOLETO = a.COD_BOLETO,
                        DTH_INCLUSAO = a.DTH_INCLUSAO
                    };

            return query;
        }

        public int GetDataCount(short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome)
        {
            return GetWhere(pEmpresa, pMatricula, pRepresentante, pCpf, pNome).SelectCount();
        }

        public Resultado SaveData(PRE_TBL_CONTRIB_ESPORADICA newDebConta)
        {
            Resultado res = new Resultado();
            try
            {
                //var atualiza = m_DbContext.AAT_TBL_DEB_CONTA.FirstOrDefault(p => p.COD_EMPRS == newDebConta.COD_EMPRS
                //                                                              && p.NUM_RGTRO_EMPRG == newDebConta.NUM_RGTRO_EMPRG
                //                                                              && p.NUM_IDNTF_RPTANT == newDebConta.NUM_IDNTF_RPTANT
                //                                                              && p.COD_PRODUTO == newDebConta.COD_PRODUTO
                //                                                              && p.DTH_EXCLUSAO == null);

                var atualiza = m_DbContext.PRE_TBL_CONTRIB_ESPORADICA.FirstOrDefault(p => p.COD_CONTRIB_ESPORADICA == newDebConta.COD_CONTRIB_ESPORADICA
                                                                                       && p.DTH_EXCLUSAO == null);

                bool iguais = (atualiza != null) ? atualiza.Comparar(newDebConta) : false;
                //bool iguais = atualiza.Comparar(newDebConta);

                if (iguais)
                {
                    res.Sucesso("Registro não inserido. Já existe um 'igual'.");
                }
                else
                {
                    if (atualiza != null) atualiza.DTH_EXCLUSAO = newDebConta.DTH_INCLUSAO; // Desativa o registro atual

                    m_DbContext.PRE_TBL_CONTRIB_ESPORADICA.Add(newDebConta); // Insere um registro novo ativo (DTH_EXCLUSAO=null)
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(ex.Message);
            }
            return res;

        }

        public Resultado DeleteData(int pPK, string user)
        {
            Resultado res = new Resultado();
            var deleta = m_DbContext.PRE_TBL_CONTRIB_ESPORADICA.Find(pPK);
            DateTime dthExclusao = DateTime.Now;

            if (deleta != null)
            {

                // Exclui os filhos:
                //deleta.PRE_TBL_DEPOSITO_JUDIC_PGTO.ToList().ForEach(p => m_DbContext.PRE_TBL_DEPOSITO_JUDIC_PGTO.Remove(p));
                //m_DbContext.PRE_TBL_DEPOSITO_JUDIC.Remove(deleta);

                int rows_deleted = m_DbContext.SaveChanges();
                if (rows_deleted > 0)
                {
                    res.Sucesso("Registro excluído com sucesso.");
                }
            }
            return res;
        }
           
    }
}
