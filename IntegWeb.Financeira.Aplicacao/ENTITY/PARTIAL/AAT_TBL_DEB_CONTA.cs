using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.ENTITY
{
    public partial class AAT_TBL_DEB_CONTA
    {
        public AAT_TBL_DEB_CONTA()
        {
            this.DTH_INCLUSAO = DateTime.Now;
        }

        public AAT_TBL_DEB_CONTA Clone()
        {
            AAT_TBL_DEB_CONTA _clone = (AAT_TBL_DEB_CONTA)this.MemberwiseClone();
            return _clone;
        }

        public bool Comparar(AAT_TBL_DEB_CONTA _compare)
        {
            return this.COD_EMPRS.Equals(_compare.COD_EMPRS) &&
                   this.NUM_RGTRO_EMPRG.Equals(_compare.NUM_RGTRO_EMPRG) &&
                   this.NUM_IDNTF_RPTANT.Equals(_compare.NUM_IDNTF_RPTANT) &&
                   this.COD_PRODUTO.Equals(_compare.COD_PRODUTO) &&
                   //this.DTH_INCLUSAO.Equals(_compare.DTH_INCLUSAO) &&
                   this.NUM_CPF.Equals(_compare.NUM_CPF) &&
                   this.ID_DEB_BANC.Equals(_compare.ID_DEB_BANC) &&
                   this.IND_ATIVO.Equals(_compare.IND_ATIVO) &&
                   this.DCR_NOM_ARQ.Equals(_compare.DCR_NOM_ARQ) &&
                   this.NUM_SEQ_LINHA.Equals(_compare.NUM_SEQ_LINHA) &&
                   this.LOG_INCLUSAO.Equals(_compare.LOG_INCLUSAO);
                   //this.DTH_EXCLUSAO.Equals(_compare.DTH_EXCLUSAO) &&
        }

    }
}

