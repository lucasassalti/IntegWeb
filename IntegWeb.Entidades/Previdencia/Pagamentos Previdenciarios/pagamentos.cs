using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegWeb.Entidades.Previdencia.Pagamentos
{
    public class pagamentos
    {
        private string COD_EMPRS;
        private string NOM_RZSOC_EMPRS;
        private string NUM_RGTRO_EMPRG;
        private string NUM_DIGVR_EMPRG;
        private string NUM_IDNTF_DPDTE;
        private string NUM_IDNTF_RPTANT;
        private string NOM_EMPRG;
        private string MES_REFERENCIA;
        private string ANO_REFERENCIA;
        private string NOM_RZSOC_BANCO;
        private string NOM_AGBCO;
        private string TIP_CTCOR_HISCAD;
        private string NUM_CTCOR_HISCAD;
        private string DAT_PAGTO_PCPGBF;
        private decimal? ADIANT_PREVIST;
        private string DCR_PLBNF;

        private string TXTFIXO31;
        private string TXTFIXO24;
        private string TXTFIXO25;
        private string RODAPE1;
        private string RODAPE2;
        private string RODAPE3;

        private string asabono;
        private string asquadro;

        private string anqtdeaviso;
        private string astipoaviso;

        public string Aviso_anqtdeaviso { get { return anqtdeaviso; } set { anqtdeaviso = value; } }
        public string Aviso_astipoaviso { get { return astipoaviso; } set { astipoaviso = value; } }

        public string AVISO_COD_EMPRS { get { return COD_EMPRS; } set { COD_EMPRS = value; } }
        public string AVISO_NOM_RZSOC_EMPRS { get { return NOM_RZSOC_EMPRS; } set { NOM_RZSOC_EMPRS = value; } }
        public string AVISO_NUM_RGTRO_EMPRG { get { return NUM_RGTRO_EMPRG; } set { NUM_RGTRO_EMPRG = value; } }
        public string AVISO_NUM_DIGVR_EMPRG { get { return NUM_DIGVR_EMPRG; } set { NUM_DIGVR_EMPRG = value; } }
        public string AVISO_NUM_IDNTF_DPDTE { get { return NUM_IDNTF_DPDTE; } set { NUM_IDNTF_DPDTE = value; } }
        public string AVISO_NUM_IDNTF_RPTANT { get { return NUM_IDNTF_RPTANT; } set { NUM_IDNTF_RPTANT = value; } }
        public string AVISO_NOM_EMPRG { get { return NOM_EMPRG; } set { NOM_EMPRG = value; } }
        public string AVISO_MES_REFERENCIA { get { return MES_REFERENCIA; } set { MES_REFERENCIA = value; } }
        public string AVISO_ANO_REFERENCIA { get { return ANO_REFERENCIA; } set { ANO_REFERENCIA = value; } }
        public string AVISO_NOM_RZSOC_BANCO { get { return NOM_RZSOC_BANCO; } set { NOM_RZSOC_BANCO = value; } }
        public string AVISO_NOM_AGBCO { get { return NOM_AGBCO; } set { NOM_AGBCO = value; } }
        public string AVISO_TIP_CTCOR_HISCAD { get { return TIP_CTCOR_HISCAD; } set { TIP_CTCOR_HISCAD = value; } }
        public string AVISO_NUM_CTCOR_HISCAD { get { return NUM_CTCOR_HISCAD; } set { NUM_CTCOR_HISCAD = value; } }
        public string AVISO_DAT_PAGTO_PCPGBF { get { return DAT_PAGTO_PCPGBF; } set { DAT_PAGTO_PCPGBF = value; } }
        public decimal AVISO_ADIANT_PREVIST { get; set; }
        public string AVISO_DCR_PLBNF { get { return DCR_PLBNF; } set { DCR_PLBNF = value; } }
        public string Aviso_asabono { get { return asabono; } set { asabono = value; } }
        public string Aviso_asquadro { get { return asquadro; } set { asquadro = value; } }




        public string AVISO_TXTFIXO31 { get { return TXTFIXO31; } set { TXTFIXO31 = value; } }
        public string AVISO_TXTFIXO24 { get { return TXTFIXO24; } set { TXTFIXO24 = value; } }
        public string AVISO_TXTFIXO25 { get { return TXTFIXO25; } set { TXTFIXO25 = value; } }
        public string AVISO_RODAPE1 { get { return RODAPE1; } set { RODAPE1 = value; } }
        public string AVISO_RODAPE2 { get { return RODAPE2; } set { RODAPE2 = value; } }
        public string AVISO_RODAPE3 { get { return RODAPE3; } set { RODAPE3 = value; } }




        public pagamentos()
        {
            COD_EMPRS = string.Empty;
            NOM_RZSOC_EMPRS = string.Empty;
            NUM_RGTRO_EMPRG = string.Empty;
            NUM_DIGVR_EMPRG = string.Empty;
            NUM_IDNTF_DPDTE = string.Empty;
            NUM_IDNTF_RPTANT = string.Empty;
            NOM_EMPRG = string.Empty;
            MES_REFERENCIA = string.Empty;
            ANO_REFERENCIA = string.Empty;
            NOM_RZSOC_BANCO = string.Empty;
            NOM_AGBCO = string.Empty;
            TIP_CTCOR_HISCAD = string.Empty;
            NUM_CTCOR_HISCAD = string.Empty;
            DAT_PAGTO_PCPGBF = string.Empty;
            ADIANT_PREVIST = null;
            DCR_PLBNF = string.Empty;

            TXTFIXO31 = string.Empty;
            TXTFIXO24 = string.Empty;
            TXTFIXO25 = string.Empty;
            RODAPE1 = string.Empty;
            RODAPE2 = string.Empty;
            RODAPE3 = string.Empty;

        }
    }
      
}