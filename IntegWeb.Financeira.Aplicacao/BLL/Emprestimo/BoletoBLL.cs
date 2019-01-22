using IntegWeb.Entidades;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Financeira.Aplicacao.DAL;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.BLL
{
    public class BoletoBLL : BoletoDAL
    {

        public Resultado GerarNovoBoleto(short COD_BOLETO_TIPO, short? COD_BOLETO_SUBTIPO, short COD_EMPRS, int NUM_RGTRO_EMPRG, short? NUM_DIVR_EMPRG, int? NUM_IDNTF_RPTANT, long NUM_CPF, int NUM_LOTE, string NOM_EMPR, DateTime DAT_VENCT_LCEMP, decimal VLR_DOCTO, string DCR_OBSERVACAO, string DCR_ENDER_EMPRG, string BAIRRO_EMPRG, string COD_CEP_EMPRG, string NOM_CIDRS_EMPRG, string COD_UNDFD_EMPRG, string LOG_INCLUSAO)
        {
            Resultado res = new Resultado();

            AAT_TBL_BOLETO newBoleto = new AAT_TBL_BOLETO();
            AAT_TBL_BOLETO_TIPO PARAM_FIXO_BOLETO = new AAT_TBL_BOLETO_TIPO();
            PARAM_FIXO_BOLETO = base.GetBoletoTipo(COD_BOLETO_TIPO);

            DateTime dtNow = DateTime.Now;
            int iNossoNumero = base.op_ObtemNossoNumero_PROXIMO();
            short iCALC_DIGITO = base.FN_CALC_DIGITO_BCO_SANT(iNossoNumero);
            string sNossoNumero = iNossoNumero.ToString() + iCALC_DIGITO.ToString();

            decimal Valor = VLR_DOCTO;
            DateTime DtVencimento = DAT_VENCT_LCEMP;

            string RESULT_CNAB = base.FN_CNAB_CODBAR_BCO_SANT(PARAM_FIXO_BOLETO.DCR_CONTA, sNossoNumero, PARAM_FIXO_BOLETO.DCR_CARTEIRA, Valor, DtVencimento).ToString();
            string sLinha = RESULT_CNAB.Substring(44);
            string sCodigoBarras = RESULT_CNAB.Substring(0, 44);

            newBoleto.COD_BOLETO = base.GetMaxPk();
            newBoleto.COD_EMPRS = COD_EMPRS;
            newBoleto.NUM_RGTRO_EMPRG = NUM_RGTRO_EMPRG;
            newBoleto.NUM_DIVR_EMPRG = NUM_DIVR_EMPRG;
            newBoleto.COD_BOLETO_TIPO = PARAM_FIXO_BOLETO.COD_BOLETO_TIPO;
            newBoleto.COD_BOLETO_SUBTIPO = COD_BOLETO_SUBTIPO;
            newBoleto.NUM_DOCTO = "";
            newBoleto.NUM_DCMCOB_BLPGT = Util.String2Int32(sNossoNumero) ?? 0;
            newBoleto.NUM_CPF = NUM_CPF;
            newBoleto.NOM_EMPR = NOM_EMPR;
            newBoleto.DAT_VENCT_LCEMP = DAT_VENCT_LCEMP;
            newBoleto.VLR_DOCTO = VLR_DOCTO;
            newBoleto.CALC_DIGITO = iCALC_DIGITO;
            newBoleto.DT_PROCESSAMENTO = dtNow;
            //newBoleto.TXT_FIX1 { get; set; }
            //newBoleto.TXT_FIX2 { get; set; }
            //newBoleto.TXT_FIX3 { get; set; }
            //newBoleto.TXT_FIX4 { get; set; }
            newBoleto.LOCAL_PAGTO = PARAM_FIXO_BOLETO.DCR_LOCAL_PAGTO;
            newBoleto.COD_BANCO = Util.String2Int32(PARAM_FIXO_BOLETO.DCR_BANCO) ?? 0;
            newBoleto.COD_DIGITO_BANCO = Util.String2Short(PARAM_FIXO_BOLETO.DCR_DIGITOBANCO) ?? 0;
            newBoleto.CEDENTE = PARAM_FIXO_BOLETO.DCR_CEDENTE;
            newBoleto.AGENCIA = PARAM_FIXO_BOLETO.DCR_AGENCIA;
            newBoleto.COD_CEDENTE = PARAM_FIXO_BOLETO.DCR_CONTA;
            newBoleto.DCR_ESPECIEDOC = PARAM_FIXO_BOLETO.DCR_ESPECIEDOC;
            newBoleto.DCR_ACEITE = PARAM_FIXO_BOLETO.DCR_ACEITE;
            newBoleto.DCR_USOBANCO = PARAM_FIXO_BOLETO.DCR_USOBANCO;
            newBoleto.DCR_CARTEIRA = PARAM_FIXO_BOLETO.DCR_CARTEIRA;
            newBoleto.DCR_ESPECIE = PARAM_FIXO_BOLETO.DCR_ESPECIE;
            newBoleto.DCR_QUANTIDADE = PARAM_FIXO_BOLETO.DCR_QUANTIDADE;
            newBoleto.INSTRUCOES1 = PARAM_FIXO_BOLETO.DCR_INSTR1;
            newBoleto.INSTRUCOES2 = PARAM_FIXO_BOLETO.DCR_INSTR2;
            newBoleto.INSTRUCOES3 = PARAM_FIXO_BOLETO.DCR_INSTR3;
            newBoleto.INSTRUCOES4 = PARAM_FIXO_BOLETO.DCR_INSTR4;
            newBoleto.INSTRUCOES5 = DCR_OBSERVACAO;
            newBoleto.DCR_OBSERVACAO = DCR_OBSERVACAO;
            newBoleto.DCR_ENDER_EMPRG = DCR_ENDER_EMPRG;
            newBoleto.BAIRRO_EMPRG = BAIRRO_EMPRG;
            newBoleto.COD_CEP_EMPRG = COD_CEP_EMPRG;
            newBoleto.NOM_CIDRS_EMPRG = NOM_CIDRS_EMPRG;
            newBoleto.COD_UNDFD_EMPRG = COD_UNDFD_EMPRG;
            newBoleto.LIN_DIGITAVEL = sLinha;
            newBoleto.COD_BARRAS = sCodigoBarras;
            //newBoleto.TXT_FIXO_ECT1 { get; set; }
            //newBoleto.TXT_FIXO_ECT2 { get; set; }
            //newBoleto.TXT_FIXO_ECT3 { get; set; }
            //newBoleto.TXT_FIXO_ECT4 { get; set; }
            //newBoleto.TXT_FIXO_ECT5 { get; set; }
            //newBoleto.TXT_FIXO_ECT6 { get; set; }
            newBoleto.NUM_LOTE = NUM_LOTE;
            newBoleto.CODBARRAS_ECT = " ";
            //newBoleto.SEQ_POSTAGEM { get; set; }
            newBoleto.LOG_INCLUSAO = LOG_INCLUSAO;
            newBoleto.DTH_INCLUSAO = dtNow;
            //newBoleto.LOG_EXCLUSAO { get; set; }
            //newBoleto.DTH_EXCLUSAO { get; set; }

            AAT_TBL_BOLETO_ITEM newBoletoItem = new AAT_TBL_BOLETO_ITEM();
            newBoletoItem.COD_BOLETO_ITEM = base.GetMaxPkItem();
            newBoletoItem.COD_BOLETO = newBoleto.COD_BOLETO;    
            newBoletoItem.NUM_SEQ_DETALHE = 1;
            newBoletoItem.COD_DETALHE = newBoleto.COD_BOLETO_TIPO;
            newBoletoItem.DTH_REFERENCIA = dtNow;
            newBoletoItem.DTH_VENCIMENTO = newBoleto.DAT_VENCT_LCEMP;
            newBoletoItem.DSC_DETALHE = PARAM_FIXO_BOLETO.NOM_BOLETO;
            newBoletoItem.VLR_VALOR = newBoleto.VLR_DOCTO;

            newBoleto.AAT_TBL_BOLETO_ITEM.Add(newBoletoItem);

            res = base.SaveData(newBoleto);

            if (res.Ok)
            {
                FIN_TBL_BOL_CRM bol_crm = new FIN_TBL_BOL_CRM();
                bol_crm.COD_EMPRS = COD_EMPRS;
                bol_crm.NUM_RGTRO_EMPRG = NUM_RGTRO_EMPRG;
                bol_crm.NUM_IDNTF_RPTANT = NUM_IDNTF_RPTANT ?? 0;
                bol_crm.NUM_NOSSO_NUMERO = Util.String2Int32(sNossoNumero) ?? 0;
                bol_crm.NUM_SEQ_GER = 1;
                bol_crm.NOM_EMP_REP = NOM_EMPR;
                bol_crm.NUM_CPF = NUM_CPF;
                bol_crm.DTA_VENCIMENTO = DAT_VENCT_LCEMP;
                bol_crm.DTH_INCLUSAO_REG = dtNow;
                bol_crm.VLR_DOCUMENTO = VLR_DOCTO;
                bol_crm.PROG_UTIL = "IntegWeb - Financeira";
                bol_crm.USU_RESP = LOG_INCLUSAO;
                bol_crm.WK_STATION = "";
                bol_crm.USU_UTIL = "OWN_FUNCESP";
                bol_crm.IP_STATION = "";
                bol_crm.ID_BOBA_CD_BOLETO = PARAM_FIXO_BOLETO.COD_BOLETO_TIPO;
                bol_crm.LOBO_TX_OBSERVACAO = DCR_OBSERVACAO;
                bol_crm.FL_ACAO = "I";
                Resultado res2 = base.SaveData(bol_crm);

                if (!res2.Ok)
                {
                    res.Erro(res2.Mensagem);
                }
            }

            return res;

        }
    }
}
