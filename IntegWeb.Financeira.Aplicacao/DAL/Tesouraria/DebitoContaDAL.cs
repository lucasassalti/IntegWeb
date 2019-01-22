using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.DAL
{
    public class DebitoContaDAL
    {

        public EntitiesConn m_DbContext = new EntitiesConn();

        public class AAT_TBL_DEB_CONTA_view
        {
            public short? EMPRESA { get; set; }
            public int? REGISTRO { get; set; }
            public int? REPRESENTANTE { get; set; }
            public string NOME { get; set; }
            public int COD_PRODUTO { get; set; }
            public string DESC_PRODUTO { get; set; }
            public long? NUM_CPF { get; set; }
            public string ID_DEB_BANC { get; set; }
            public short? IND_ATIVO { get; set; }
            public DateTime? DTH_INCLUSAO { get; set; }
            public string COD_BANCO { get; set; }
            public string COD_AGENCIA { get; set; }
            public string NUM_CONTA { get; set; }
            public string TIP_CONTA { get; set; }
        }

        public List<AAT_TBL_DEB_CONTA_view> GetData(int startRowIndex, int maximumRows, short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome, string sortParameter)
        {
            return GetWhere(pEmpresa, pMatricula, pRepresentante, pCpf, pNome)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<AAT_TBL_DEB_CONTA_view> GetWhere(short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome)
        {
            IQueryable<AAT_TBL_DEB_CONTA_view> query;
            query = from a in m_DbContext.AAT_TBL_DEB_CONTA
                    from e in m_DbContext.EMPREGADO
                    where (a.COD_EMPRS == e.COD_EMPRS)                       
                       && (a.NUM_RGTRO_EMPRG == e.NUM_RGTRO_EMPRG)  
                       && (a.DTH_EXCLUSAO == null)
                       && (a.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (a.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       && (a.NUM_IDNTF_RPTANT == 0)
                       && (a.NUM_CPF == pCpf || pCpf == null)
                       && (e.NOM_EMPRG.ToLower().Contains(pNome.ToLower()) || pNome == null)
                    select new AAT_TBL_DEB_CONTA_view()
                    {
                        EMPRESA = a.COD_EMPRS,
                        REGISTRO = a.NUM_RGTRO_EMPRG,
                        REPRESENTANTE = a.NUM_IDNTF_RPTANT,
                        NOME = e.NOM_EMPRG,
                        COD_PRODUTO = a.COD_PRODUTO,
                        DESC_PRODUTO = a.AAT_TBL_DEB_CONTA_PRODUTO.DESC_PRODUTO,
                        NUM_CPF = a.NUM_CPF,
                        ID_DEB_BANC = a.ID_DEB_BANC,
                        IND_ATIVO = a.IND_ATIVO,
                        DTH_INCLUSAO = a.DTH_INCLUSAO,
                        COD_BANCO = a.COD_BANCO,
                        COD_AGENCIA = a.COD_AGENCIA,
                        NUM_CONTA = a.NUM_CONTA,
                        TIP_CONTA = a.TIP_CONTA
                    };

            query = query.Union(
                //IQueryable<AAT_TBL_DEB_CONTA> query;
                    from a in m_DbContext.AAT_TBL_DEB_CONTA
                    from r in m_DbContext.REPRES_UNIAO_FSS
                    where (a.COD_EMPRS == r.COD_EMPRS || r.COD_EMPRS == null)
                       && (a.NUM_RGTRO_EMPRG == r.NUM_RGTRO_EMPRG || r.NUM_RGTRO_EMPRG == null)
                       && (a.NUM_IDNTF_RPTANT == r.NUM_IDNTF_RPTANT)
                       && (a.DTH_EXCLUSAO == null)
                       && (a.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (a.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       && (a.NUM_IDNTF_RPTANT == pRepresentante || pRepresentante == null)
                       && (a.NUM_IDNTF_RPTANT > 0)
                       && (a.NUM_CPF == pCpf || pCpf == null)
                       && (r.NOM_REPRES.ToLower().Contains(pNome.ToLower()) || pNome == null)
                    select new AAT_TBL_DEB_CONTA_view()
                    {
                        EMPRESA = a.COD_EMPRS,
                        REGISTRO = a.NUM_RGTRO_EMPRG,
                        REPRESENTANTE = a.NUM_IDNTF_RPTANT,
                        NOME = r.NOM_REPRES,
                        COD_PRODUTO = a.COD_PRODUTO,
                        DESC_PRODUTO = a.AAT_TBL_DEB_CONTA_PRODUTO.DESC_PRODUTO,
                        NUM_CPF = a.NUM_CPF,
                        ID_DEB_BANC = a.ID_DEB_BANC,
                        IND_ATIVO = a.IND_ATIVO,
                        DTH_INCLUSAO = a.DTH_INCLUSAO,
                        COD_BANCO = a.COD_BANCO,
                        COD_AGENCIA = a.COD_AGENCIA,
                        NUM_CONTA = a.NUM_CONTA,
                        TIP_CONTA = a.TIP_CONTA
                    }
            );
            return query;
        }

        public int GetDataCount(short? pEmpresa, int? pMatricula, int? pRepresentante, long? pCpf, string pNome)
        {
            return GetWhere(pEmpresa, pMatricula, pRepresentante, pCpf, pNome).SelectCount();
        }

        public Resultado SaveData(AAT_TBL_DEB_CONTA newDebConta)
        {
            Resultado res = new Resultado();
            try
            {
                //var atualiza = m_DbContext.AAT_TBL_DEB_CONTA.FirstOrDefault(p => p.COD_EMPRS == newDebConta.COD_EMPRS
                //                                                              && p.NUM_RGTRO_EMPRG == newDebConta.NUM_RGTRO_EMPRG
                //                                                              && p.NUM_IDNTF_RPTANT == newDebConta.NUM_IDNTF_RPTANT
                //                                                              && p.COD_PRODUTO == newDebConta.COD_PRODUTO
                //                                                              && p.DTH_EXCLUSAO == null);

                var atualiza = m_DbContext.AAT_TBL_DEB_CONTA.FirstOrDefault(p => p.NUM_CPF == newDebConta.NUM_CPF
                                                                              && p.COD_PRODUTO == newDebConta.COD_PRODUTO
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

                    m_DbContext.AAT_TBL_DEB_CONTA.Add(newDebConta); // Insere um registro novo ativo (DTH_EXCLUSAO=null)
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
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
