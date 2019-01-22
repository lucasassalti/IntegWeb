using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Previdencia.Aplicacao.ENTITY;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{

    class LAY_FICHA_FINANCEIRA_CAB
    {
        public LAY_CAMPO LAY_DT_CREDITO = new LAY_CAMPO() { pos = 4, tam = 8, nome_amigavel = "DT. CREDITO FOLHA", nome = "DT_CREDITO" };
        public LAY_CAMPO LAY_DT_REPASSE = new LAY_CAMPO() { pos = 12, tam = 8, nome_amigavel = "DT. REPASSE", nome = "DT_REPASSE" };
        public LAY_CAMPO LAY_VLR_TOTAL_REPASSADO = new LAY_CAMPO() { pos = 20, tam = 14, nome_amigavel = "VLR. TOTAL REPASSADO", nome = "VLR_TOTAL_REPASSADO" };
        public LAY_CAMPO LAY_NUM_TOTAL_REGISTROS = new LAY_CAMPO() { pos = 34, tam = 6, nome_amigavel = "TOTAL REGISTROS", nome = "NUM_TOTAL_REGISTROS" };
    }

    class LAY_FICHA_FINANCEIRA_RODAPE
    {
        public LAY_CAMPO LAY_DT_CREDITO = new LAY_CAMPO() { pos = 4, tam = 8, nome_amigavel = "DT. CREDITO FOLHA", nome = "DT_CREDITO" };
        public LAY_CAMPO LAY_DT_REPASSE = new LAY_CAMPO() { pos = 12, tam = 8, nome_amigavel = "DT. REPASSE", nome = "DT_REPASSE" };
        public LAY_CAMPO LAY_VLR_TOTAL_REPASSADO = new LAY_CAMPO() { pos = 20, tam = 13, nome_amigavel = "VLR. TOTAL REPASSADO", nome = "VLR_TOTAL_REPASSADO" };
        public LAY_CAMPO LAY_NUM_TOTAL_REGISTROS = new LAY_CAMPO() { pos = 33, tam = 6, nome_amigavel = "TOTAL REGISTROS", nome = "NUM_TOTAL_REGISTROS" };
    }

    class LAY_FICHA_FINANCEIRA
    {

        public LAY_CAMPO LAY_COD_EMPRS = new LAY_CAMPO() { pos = 1, tam = 4, nome_amigavel = "CÓD. EMPRESA", nome = "COD_EMPRS" };
        public LAY_CAMPO LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 4, tam = 10, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };
        public LAY_CAMPO LAY_COD_VERBA = new LAY_CAMPO() { pos = 14, tam = 10, nome_amigavel = "CÓD. VERBA", nome = "COD_VERBA" };
        public LAY_CAMPO LAY_ANO_REFER_VERFIN = new LAY_CAMPO() { pos = 24, tam = 4, nome_amigavel = "ANO REFERÊNCIA", nome = "ANO_REFER_VERFIN" };
        public LAY_CAMPO LAY_MES_REFER_VERFIN = new LAY_CAMPO() { pos = 28, tam = 2, nome_amigavel = "MÊS REFERÊNCIA", nome = "MES_REFER_VERFIN" };
        public LAY_CAMPO LAY_VLR_VERFIN = new LAY_CAMPO() { pos = 30, tam = 17, nome_amigavel = "VLR. VERBA", nome = "VLR_VERFIN" };
        //public LAY_CAMPO LAY_ANO_COMPET_VERFIN = new LAY_CAMPO() { pos = 10, tam = 4 };
        //public LAY_CAMPO LAY_MES_COMPET_VERFIN = new LAY_CAMPO() { pos = 14, tam = 2 };
        //public LAY_CAMPO LAY_VLR_VERFIN = new LAY_CAMPO() { pos = 16, tam = 17 };
        public LAY_CAMPO LAY_ANO_PAGTO_VERFIN = new LAY_CAMPO() { pos = 47, tam = 4, nome_amigavel = "ANO COMPETÊNCIA", nome = "ANO_PAGTO_VERFIN" };
        public LAY_CAMPO LAY_MES_PAGTO_VERFIN = new LAY_CAMPO() { pos = 51, tam = 2, nome_amigavel = "MES COMPETÊNCIA", nome = "MES_PAGTO_VERFIN" };
        public LAY_CAMPO LAY_DAT_PAGTO_VERFIN = new LAY_CAMPO() { pos = 1, tam = 0, nome_amigavel = "DATA PAGTO", nome = "DAT_PAGTO_VERFIN" };
        public LAY_CAMPO LAY_NUM_MATFNC_EMPRG = new LAY_CAMPO() { pos = 1, tam = 0, nome_amigavel = "NÚM. MÁTRICULA", nome = "NUM_MATFNC_EMPRG" };
        public LAY_CAMPO LAY_NUM_SIAPE_EMPRG = new LAY_CAMPO() { pos = 1, tam = 0, nome = "NUM_SIAPE_EMPRG" };
        public LAY_CAMPO LAY_NUM_AR_VERFIN = new LAY_CAMPO() { pos = 1, tam = 0, nome = "NUM_AR_VERFIN" };

        public LAY_FICHA_FINANCEIRA_CAB CABECALHO = new LAY_FICHA_FINANCEIRA_CAB();
        public LAY_FICHA_FINANCEIRA_RODAPE RODAPE = new LAY_FICHA_FINANCEIRA_RODAPE();

        public FICHA_FINANCEIRA DePara(string pCOD_EMPRS, string pNUM_RGTRO_EMPRG, string pDADOS)
        {
            FICHA_FINANCEIRA ret = new FICHA_FINANCEIRA();
            //ret._NAO_ATUALIZAR = new List<string>();

            ret.COD_EMPRS = short.Parse(pCOD_EMPRS);
            ret.NUM_RGTRO_EMPRG = int.Parse(pNUM_RGTRO_EMPRG);
            ret.COD_VERBA = LAYOUT_UTIL.GetInt(pDADOS, LAY_COD_VERBA);
            ret.ANO_COMPET_VERFIN = LAYOUT_UTIL.GetShort(pDADOS, LAY_ANO_REFER_VERFIN);
            ret.MES_COMPET_VERFIN = LAYOUT_UTIL.GetShort(pDADOS, LAY_MES_REFER_VERFIN);
            ret.VLR_VERFIN = LAYOUT_UTIL.GetDecimal(pDADOS, LAY_VLR_VERFIN);
            ret.ANO_PAGTO_VERFIN = LAYOUT_UTIL.GetShort(pDADOS, LAY_ANO_PAGTO_VERFIN);
            ret.MES_PAGTO_VERFIN = LAYOUT_UTIL.GetShort(pDADOS, LAY_MES_PAGTO_VERFIN);
            //ret.DAT_PAGTO_VERFIN = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_PAGTO_VERFIN);
            //ret.NUM_MATFNC_EMPRG = pDADOS.Substring(LAY_NUM_MATFNC_EMPRG.pos, LAY_NUM_MATFNC_EMPRG.tam);
            //ret.NUM_SIAPE_EMPRG = pDADOS.Substring(LAY_NUM_SIAPE_EMPRG.pos, LAY_NUM_SIAPE_EMPRG.tam);
            //ret.NUM_AR_VERFIN = LAYOUT_UTIL.GetShort(pDADOS, LAY_NUM_AR_VERFIN);
            return ret;
        }

        public PRE_TBL_ARQ_PAT_DEMONSTRA DePara_Demonstrativo(string pDADOS)
        {
            PRE_TBL_ARQ_PAT_DEMONSTRA ret = new PRE_TBL_ARQ_PAT_DEMONSTRA();

            //ret.COD_ARQ_PAT_DEMO = 0;
            ret.ANO_REF = LAYOUT_UTIL.GetShort(pDADOS, LAY_ANO_REFER_VERFIN);
            ret.MES_REF = LAYOUT_UTIL.GetShort(pDADOS, LAY_MES_REFER_VERFIN);
            //ret.DAT_REPASSE = null; 
            //ret.DAT_CREDITO = null; 
            //ret.GRUPO_PORTAL = null; 
            //ret.DTH_INCLUSAO = null; 
            //ret.LOG_INCLUSAO = null; 
            //ret.DTH_EXCLUSAO = null; 
            //ret.LOG_EXCLUSAO = null; 

            PRE_TBL_ARQ_PAT_DEMONSTRA_DET demo_det = new PRE_TBL_ARQ_PAT_DEMONSTRA_DET();

            demo_det.COD_VERBA = LAYOUT_UTIL.GetInt(pDADOS, LAY_COD_VERBA);
            demo_det.VLR_LANCAMENTO = LAYOUT_UTIL.GetDecimal(pDADOS, LAY_VLR_VERFIN);

            ret.PRE_TBL_ARQ_PAT_DEMONSTRA_DET.Add(demo_det);
   
            return ret;
        }
    }
}
