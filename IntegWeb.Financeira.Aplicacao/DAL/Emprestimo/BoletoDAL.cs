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
using System.Globalization;

namespace IntegWeb.Financeira.Aplicacao.DAL
{
    public class BoletoDAL
    {

        public EntitiesConn m_DbContext = new EntitiesConn();

        public List<AAT_TBL_BOLETO> GetData(int startRowIndex, int maximumRows, short? pEmpresa, int? pMatricula, int? pTipoBoleto, int? pSubTipoBoleto, long? pCpf, int? pLote, string pNome, DateTime? pDtIni, DateTime? pDtFim, string sortParameter)
        {
            return GetWhere(pEmpresa, pMatricula, pTipoBoleto, pSubTipoBoleto, pCpf, pLote, pNome, pDtIni, pDtFim)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<AAT_TBL_BOLETO> GetWhere(short? pEmpresa, int? pMatricula, int? pTipoBoleto, int? pSubTipoBoleto, long? pCpf, int? pLote, string pNome, DateTime? pDtIni, DateTime? pDtFim)
        {
            if (pDtFim != null)
            {
                pDtFim = pDtFim.Value.AddDays(1).Date;
            }

            IQueryable<AAT_TBL_BOLETO> query;
            query = from a in m_DbContext.AAT_TBL_BOLETO
                    where (a.DTH_EXCLUSAO == null)
                       && (a.COD_EMPRS == pEmpresa || pEmpresa == null)
                       && (a.NUM_RGTRO_EMPRG == pMatricula || pMatricula == null)
                       && (a.COD_BOLETO_TIPO == pTipoBoleto || pTipoBoleto == 0)
                       && (a.COD_BOLETO_SUBTIPO == pSubTipoBoleto || pSubTipoBoleto == 0)
                       && (a.NUM_CPF == pCpf || pCpf == null)
                       && (a.NUM_LOTE == pLote || pLote == null)
                       && (a.NOM_EMPR.ToLower().Contains(pNome.ToLower()) || pNome == null)
                       && (a.DT_PROCESSAMENTO >= pDtIni && pDtFim == null ||
                           a.DT_PROCESSAMENTO <= pDtFim && pDtIni == null ||
                           a.DT_PROCESSAMENTO >= pDtIni && a.DT_PROCESSAMENTO <= pDtFim ||
                           pDtIni == null && pDtFim == null)
                    select a;
                    //select new AAT_TBL_BOLETO_view()
                    //{
                    //    COD_BOLETO = a.COD_BOLETO,
                    //    COD_EMPRS = a.COD_EMPRS,
                    //    NUM_RGTRO_EMPRG = a.NUM_RGTRO_EMPRG,
                    //    NUM_DIVR_EMPRG = a.NUM_DIVR_EMPRG,
                    //    COD_BOLETO_TIPO = a.COD_BOLETO_TIPO,
                    //    NUM_DOCTO = a.NUM_DOCTO,
                    //    NUM_DCMCOB_BLPGT = a.NUM_DCMCOB_BLPGT,
                    //    NUM_CPF = a.NUM_CPF,
                    //    NOM_EMPR = a.NOM_EMPR,
                    //    DAT_VENCT_LCEMP = a.DAT_VENCT_LCEMP,
                    //    VLR_DOCTO = a.VLR_DOCTO,
                    //    CALC_DIGITO = a.CALC_DIGITO,
                    //    TXT_FIX1 = a.TXT_FIX1,
                    //    TXT_FIX2 = a.TXT_FIX2,
                    //    TXT_FIX3 = a.TXT_FIX3,
                    //    TXT_FIX4 = a.TXT_FIX4,
                    //    LOCAL_PAGTO = a.LOCAL_PAGTO,
                    //    COD_BANCO = a.COD_BANCO,
                    //    COD_DIGITO_BANCO = a.COD_DIGITO_BANCO,
                    //    CEDENTE = a.CEDENTE,
                    //    AGENCIA = a.AGENCIA,
                    //    COD_CEDENTE= a.COD_CEDENTE,
                    //    DT_PROCESSAMENTO = a.DT_PROCESSAMENTO,
                    //    DCR_ESPECIEDOC = a.DCR_ESPECIEDOC,
                    //    DCR_ACEITE = a.DCR_ACEITE,
                    //    DCR_USOBANCO = a.DCR_USOBANCO,
                    //    DCR_CARTEIRA = a.DCR_CARTEIRA,
                    //    DCR_ESPECIE = a.DCR_ESPECIE,
                    //    DCR_QUANTIDADE = a.DCR_QUANTIDADE,
                    //    LIN_DIGITAVEL = a.LIN_DIGITAVEL,
                    //    INSTRUCOES1 = a.INSTRUCOES1,
                    //    INSTRUCOES2 = a.INSTRUCOES2,
                    //    INSTRUCOES3 = a.INSTRUCOES3,
                    //    INSTRUCOES4 = a.INSTRUCOES4,
                    //    INSTRUCOES5 = a.INSTRUCOES5,
                    //    DCR_OBSERVACAO = a.DCR_OBSERVACAO,
                    //    DCR_ENDER_EMPRG = a.DCR_ENDER_EMPRG,
                    //    BAIRRO_EMPRG = a.BAIRRO_EMPRG,
                    //    COD_CEP_EMPRG = a.COD_CEP_EMPRG,
                    //    NOM_CIDRS_EMPRG = a.NOM_CIDRS_EMPRG,
                    //    COD_UNDFD_EMPRG = a.COD_UNDFD_EMPRG,
                    //    COD_BARRAS = a.COD_BARRAS,
                    //    TXT_FIXO_ECT1 = a.TXT_FIXO_ECT1,
                    //    TXT_FIXO_ECT2 = a.TXT_FIXO_ECT2,
                    //    TXT_FIXO_ECT3 = a.TXT_FIXO_ECT3,
                    //    TXT_FIXO_ECT4 = a.TXT_FIXO_ECT4,
                    //    TXT_FIXO_ECT5 = a.TXT_FIXO_ECT5,
                    //    TXT_FIXO_ECT6 = a.TXT_FIXO_ECT6,
                    //    NUM_LOTE = a.NUM_LOTE,
                    //    CODBARRAS_ECT = a.CODBARRAS_ECT,
                    //    SEQ_POSTAGEM = a.SEQ_POSTAGEM,
                    //    LOG_INCLUSAO = a.LOG_INCLUSAO,
                    //    DTH_INCLUSAO = a.DTH_INCLUSAO,
                    //    LOG_EXCLUSAO = a.LOG_EXCLUSAO,
                    //    DTH_EXCLUSAO = a.DTH_EXCLUSAO,
                    //    AAT_TBL_BOLETO_TIPO = a.AAT_TBL_BOLETO_TIPO
                    //};

            return query;
        }

        public int GetDataCount(short? pEmpresa, int? pMatricula, int? pTipoBoleto, int? pSubTipoBoleto, long? pCpf, int? pLote, string pNome, DateTime? pDtIni, DateTime? pDtFim)
        {
            return GetWhere(pEmpresa, pMatricula, pTipoBoleto, pSubTipoBoleto, pCpf, pLote, pNome, pDtIni, pDtFim).SelectCount();
        }

        public long GetMaxPk()
        {
            long maxPK = 0;
            maxPK = m_DbContext.AAT_TBL_BOLETO.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_BOLETO) + 1;
            return maxPK;
        }

        public long GetMaxPkItem()
        {
            long maxPK = 0;
            maxPK = m_DbContext.AAT_TBL_BOLETO_ITEM.DefaultIfEmpty().Max(m => (m == null) ? 0 : m.COD_BOLETO_ITEM) + 1;
            return maxPK;
        }

        public Resultado SaveData(AAT_TBL_BOLETO newBoleto)
        {
            Resultado res = new Resultado();
            try
            {
                //var atualiza = m_DbContext.AAT_TBL_BOLETO.FirstOrDefault(p => p.COD_EMPRS == newBoleto.COD_EMPRS
                //                                                              && p.NUM_RGTRO_EMPRG == newBoleto.NUM_RGTRO_EMPRG
                //                                                              && p.NUM_IDNTF_RPTANT == newBoleto.NUM_IDNTF_RPTANT
                //                                                              && p.COD_PRODUTO == newBoleto.COD_PRODUTO
                //                                                              && p.DTH_EXCLUSAO == null);

                var atualiza = m_DbContext.AAT_TBL_BOLETO.Find(newBoleto.COD_BOLETO);
                
                bool iguais = (atualiza != null) ? atualiza.Comparar(newBoleto) : false;
                //bool iguais = atualiza.Comparar(newBoleto);

                if (iguais)
                {
                    res.Sucesso("Registro não inserido. Já existe um 'igual'.");
                } 
                else 
                {
                    if (atualiza != null) atualiza.DTH_EXCLUSAO = newBoleto.DTH_INCLUSAO; // Desativa o registro atual

                    m_DbContext.AAT_TBL_BOLETO.Add(newBoleto); // Insere um registro novo ativo (DTH_EXCLUSAO=null)
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro atualizado com sucesso.", newBoleto.COD_BOLETO);
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;

        }

        public Resultado SaveData(FIN_TBL_BOL_CRM newBoleto)
        {
            Resultado res = new Resultado();
            try
            {
                //var atualiza = m_DbContext.AAT_TBL_BOLETO.FirstOrDefault(p => p.COD_EMPRS == newBoleto.COD_EMPRS
                //                                                              && p.NUM_RGTRO_EMPRG == newBoleto.NUM_RGTRO_EMPRG
                //                                                              && p.NUM_IDNTF_RPTANT == newBoleto.NUM_IDNTF_RPTANT
                //                                                              && p.COD_PRODUTO == newBoleto.COD_PRODUTO
                //                                                              && p.DTH_EXCLUSAO == null);
                var atualiza = m_DbContext.FIN_TBL_BOL_CRM.Find(newBoleto.COD_EMPRS, newBoleto.NUM_RGTRO_EMPRG, newBoleto.NUM_IDNTF_RPTANT, newBoleto.NUM_NOSSO_NUMERO, newBoleto.NUM_SEQ_GER);
                //bool iguais = (atualiza != null) ? atualiza.Comparar(newBoleto) : false;
                //bool iguais = atualiza.Comparar(newBoleto);
                //if (iguais)
                //{
                //    res.Sucesso("Registro não inserido. Já existe um 'igual'.");
                //}
                //else
                //{
                if (atualiza == null)
                {
                    m_DbContext.FIN_TBL_BOL_CRM.Add(newBoleto); // Insere um registro novo ativo (DTH_EXCLUSAO=null)
                    int rows_updated = m_DbContext.SaveChanges();
                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro atualizado com sucesso.");
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;

        }

        public AAT_TBL_BOLETO_TIPO GetBoletoTipo(short pCOD_BOLETO_TIPO)
        {
            IQueryable<AAT_TBL_BOLETO_TIPO> query;
            query = from t in m_DbContext.AAT_TBL_BOLETO_TIPO
                    where (t.COD_BOLETO_TIPO == pCOD_BOLETO_TIPO)
                    select t;
            return query.FirstOrDefault();
        }

        public List<AAT_TBL_BOLETO_TIPO> GetBoletoTipos()
        {
            IQueryable<AAT_TBL_BOLETO_TIPO> query;
            query = from t in m_DbContext.AAT_TBL_BOLETO_TIPO
                    select t;
            return query.ToList();
        }

        public List<AAT_TBL_BOLETO_SUBTIPO> GetBoletoSubTipos(short? pCOD_BOLETO_TIPO = null)
        {
            IQueryable<AAT_TBL_BOLETO_SUBTIPO> query;
            query = from t in m_DbContext.AAT_TBL_BOLETO_SUBTIPO
                    where (t.COD_BOLETO_TIPO == pCOD_BOLETO_TIPO || pCOD_BOLETO_TIPO == null)
                    select t;
            return query.ToList();
        }

        public int op_ObtemNossoNumero_PROXIMO()
        {
            int ret;
            string sqlCnab = "SELECT COBO_NR_PROXIMOCODIGO+COBO_NR_INCREMENTO FROM OWN_PLUSOFTCRM.CS_CDTB_CODIGOBOLETO_COBO";
            ret = m_DbContext.Database.SqlQuery<int>(sqlCnab).FirstOrDefault();

            // Atualiza número:
            m_DbContext.Database.ExecuteSqlCommand("UPDATE OWN_PLUSOFTCRM.CS_CDTB_CODIGOBOLETO_COBO " +
                                                   "SET COBO_NR_PROXIMOCODIGO = " + ret.ToString());

            return ret;
        }

        public short FN_CALC_DIGITO_BCO_SANT(int pNossoNumero)           
        {
            short ret;
            string sqlCnab = "select opportunity.FN_CALC_DIGITO_BCO_SANT ('" + pNossoNumero + "') AS RESULT_CALC_DIGITO FROM DUAL";
            string s = m_DbContext.Database.SqlQuery<string>(sqlCnab).FirstOrDefault();
            ret = Util.String2Short(s) ?? 0;
            return ret;
        }

        public string FN_CNAB_CODBAR_BCO_SANT(string p_codCedente, string p_nossonumero, string P_Carteira, decimal p_vlrtitulo, DateTime p_vencimento)
        {
            string ret;
            string sqlCnab = "";
            sqlCnab = "select opportunity.FN_CNAB_CODBAR_BCO_SANT ('" + p_codCedente + "','" + p_nossonumero + "','" + P_Carteira + "'," + p_vlrtitulo.ToString("0.00", CultureInfo.InvariantCulture) + ",";
            sqlCnab += "       to_date('" + p_vencimento.ToString("dd/MM/yyyy") + " 17:00:00','DD/MM/YYYY HH24:MI:SS')) AS RESULT_CNAB FROM DUAL";

            ret = m_DbContext.Database.SqlQuery<string>(sqlCnab).FirstOrDefault();

            return ret;
        }


    }
}
