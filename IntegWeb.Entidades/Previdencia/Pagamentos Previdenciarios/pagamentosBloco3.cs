using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegWeb.Entidades.Previdencia.Pagamentos
{
    public class pagamentosBloco3
    {

        private string COD_EMPRS;
        private string NUM_RGTRO_EMPRG;
        private string NUM_DIGVR_EMPRG;
        private string NUM_IDNTF_DPDTE;
        private string NUM_IDNTF_RPTANT;
        private string ANO_REFERENCIA;

        private string DRC_VRBFSS;

        private decimal? SLD_ANTERIOR;
        private decimal? MOVTO_MES;
        private decimal? SLD_ATUAL;

        private string asabono;
        private string asquadro;

        private string anqtdeaviso;
        private string astipoaviso;

        public string Aviso_anqtdeaviso { get { return anqtdeaviso; } set { anqtdeaviso = value; } }
        public string Aviso_astipoaviso { get { return astipoaviso; } set { astipoaviso = value; } }

        public string AVISO_ANO_REFERENCIA { get { return ANO_REFERENCIA; } set { ANO_REFERENCIA = value; } }
        public string AVISO_COD_EMPRS { get { return COD_EMPRS; } set { COD_EMPRS = value; } }
        public string AVISO_NUM_RGTRO_EMPRG { get { return NUM_RGTRO_EMPRG; } set { NUM_RGTRO_EMPRG = value; } }
        public string AVISO_NUM_DIGVR_EMPRG { get { return NUM_DIGVR_EMPRG; } set { NUM_DIGVR_EMPRG = value; } }
        public string AVISO_NUM_IDNTF_DPDTE { get { return NUM_IDNTF_DPDTE; } set { NUM_IDNTF_DPDTE = value; } }
        public string AVISO_NUM_IDNTF_RPTANT { get { return NUM_IDNTF_RPTANT; } set { NUM_IDNTF_RPTANT = value; } }

        public string AVISO_DRC_VRBFSS { get { return DRC_VRBFSS; } set { DRC_VRBFSS = value; } }

        public decimal AVISO_SLD_ANTERIOR { get; set; }
        public decimal AVISO_MOVTO_MES { get; set; }
        public decimal AVISO_SLD_ATUAL { get; set; }

        public string AVISO_asabono { get { return asabono; } set { asabono = value; } }
        public string AVISO_asquadro { get { return asquadro; } set { asquadro = value; } }

        public pagamentosBloco3()
        {
            DRC_VRBFSS = string.Empty;
            SLD_ANTERIOR = null;
            MOVTO_MES = null;
            SLD_ATUAL = null;


        }
    }
}
