using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegWeb.Entidades.Previdencia.Pagamentos
{
    public class pagamentosBloco2
    {
        private string COD_EMPRS;
        private string NUM_RGTRO_EMPRG;
        private string NUM_DIGVR_EMPRG;
        private string NUM_IDNTF_DPDTE;
        private string NUM_IDNTF_RPTANT;
        private string ANO_REFERENCIA;
        private string HISTORICO;

        private decimal? VENCIMENTO;
        private decimal? DESCONTO;
        private decimal? TOT_VENCIMENTO;
        private decimal? TOT_DESCONTO;
        private decimal? TOT_LIQUIDO;

        private string asabono;
        private string asquadro;

        private string anqtdeaviso;
        private string astipoaviso;

        public string Aviso_anqtdeaviso { get { return anqtdeaviso; } set { anqtdeaviso = value; } }
        public string Aviso_astipoaviso { get { return astipoaviso; } set { astipoaviso = value; } }

        public string AVISO_COD_EMPRS { get { return COD_EMPRS; } set { COD_EMPRS = value; } }
        public string AVISO_NUM_RGTRO_EMPRG { get { return NUM_RGTRO_EMPRG; } set { NUM_RGTRO_EMPRG = value; } }
        public string AVISO_NUM_DIGVR_EMPRG { get { return NUM_DIGVR_EMPRG; } set { NUM_DIGVR_EMPRG = value; } }
        public string AVISO_NUM_IDNTF_DPDTE { get { return NUM_IDNTF_DPDTE; } set { NUM_IDNTF_DPDTE = value; } }
        public string AVISO_NUM_IDNTF_RPTANT { get { return NUM_IDNTF_RPTANT; } set { NUM_IDNTF_RPTANT = value; } }
        public string AVISO_ANO_REFERENCIA { get { return ANO_REFERENCIA; } set { ANO_REFERENCIA = value; } }

        public string AVISO_HISTORICO { get { return HISTORICO; } set { HISTORICO = value; } }

        public decimal AVISO_VENCIMENTO { get; set; }
        public decimal AVISO_DESCONTO { get; set; }

        public decimal AVISO_TOT_VENCIMENTO { get; set; }
        public decimal AVISO_TOT_DESCONTO { get; set; }
        public decimal AVISO_TOT_LIQUIDO { get; set; }

        public string Aviso_asabono { get { return asabono; } set { asabono = value; } }
        public string Aviso_asquadro { get { return asquadro; } set { asquadro = value; } }

        public pagamentosBloco2()
        {

            HISTORICO = string.Empty;
            VENCIMENTO = null;
            DESCONTO = null;
            TOT_VENCIMENTO = null;
            TOT_DESCONTO = null;
            TOT_LIQUIDO = null;

        }
    }
}