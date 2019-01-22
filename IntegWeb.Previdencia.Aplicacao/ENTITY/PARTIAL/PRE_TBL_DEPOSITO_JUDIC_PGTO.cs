using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class PRE_TBL_DEPOSITO_JUDIC_PGTO
    {

        public PRE_TBL_DEPOSITO_JUDIC_PGTO Clone()
        {
            PRE_TBL_DEPOSITO_JUDIC_PGTO _clone = (PRE_TBL_DEPOSITO_JUDIC_PGTO)this.MemberwiseClone();
            return _clone;
        }

        public bool Comparar(PRE_TBL_DEPOSITO_JUDIC_PGTO _compare)
        {
            return this.TIP_CADASTRO.Equals(_compare.TIP_CADASTRO) &&
                   (this.NUM_PP ?? String.Empty).Equals(_compare.NUM_PP) &&
                   this.TIP_SOLICITACAO.Equals(_compare.TIP_SOLICITACAO) &&
                   this.DTH_PAGAMENTO.Equals(_compare.DTH_PAGAMENTO) &&
                   this.TIP_PAGAMENTO.Equals(_compare.TIP_PAGAMENTO) &&
                   (this.NOM_CREDOR ?? String.Empty).Equals(_compare.NOM_CREDOR ?? "") &&
                   this.VLR_BSPS.Equals(_compare.VLR_BSPS) &&
                   this.VLR_BD.Equals(_compare.VLR_BD) &&
                   this.VLR_CV.Equals(_compare.VLR_CV) &&
                   this.VLR_BSPS_CUSTO.Equals(_compare.VLR_BSPS_CUSTO) &&
                   this.VLR_BD_CUSTO.Equals(_compare.VLR_BD_CUSTO) &&
                   this.VLR_CV_CUSTO.Equals(_compare.VLR_CV_CUSTO) &&
                   (this.DSC_DESCRICAO ?? String.Empty).Equals(_compare.DSC_DESCRICAO) &&
                   this.NUM_CARTA.Equals(_compare.NUM_CARTA) &&
                   this.DAT_CARTA_ENVIO.Equals(_compare.DAT_CARTA_ENVIO) &&
                   this.DAT_EMAIL_ENVIO.Equals(_compare.DAT_EMAIL_ENVIO);
        }

    }
}

