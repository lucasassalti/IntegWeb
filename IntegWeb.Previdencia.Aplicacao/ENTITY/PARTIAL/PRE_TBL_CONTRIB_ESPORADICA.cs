using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class PRE_TBL_CONTRIB_ESPORADICA
    {

        public PRE_TBL_CONTRIB_ESPORADICA Clone()
        {
            PRE_TBL_CONTRIB_ESPORADICA _clone = (PRE_TBL_CONTRIB_ESPORADICA)this.MemberwiseClone();
            return _clone;
        }

        public bool Comparar(PRE_TBL_CONTRIB_ESPORADICA _compare)
        {
            return this.DAT_PGTO.Equals(_compare.DAT_PGTO) &&
                   this.DAT_VENCIMENTO.Equals(_compare.DAT_VENCIMENTO) &&
                   this.VLR_CONTRIB.Equals(_compare.DAT_VENCIMENTO) &&
                   this.COD_BOLETO.Equals(_compare.COD_BOLETO);
        }

    }
}

