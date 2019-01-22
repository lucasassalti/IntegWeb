using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.ENTITY
{
    public partial class AAT_TBL_BOLETO
    {
        //public AAT_TBL_BOLETO()
        //{
        //    this.DTH_INCLUSAO = DateTime.Now;
        //}

        public AAT_TBL_BOLETO Clone()
        {
            AAT_TBL_BOLETO _clone = (AAT_TBL_BOLETO)this.MemberwiseClone();
            return _clone;
        }

        public bool Comparar(AAT_TBL_BOLETO _compare)
        {
            return this.COD_EMPRS.Equals(_compare.COD_EMPRS) &&
                   this.NUM_RGTRO_EMPRG.Equals(_compare.NUM_RGTRO_EMPRG) &&
                   this.NUM_DCMCOB_BLPGT.Equals(_compare.NUM_DCMCOB_BLPGT) &&
                   this.VLR_DOCTO.Equals(_compare.VLR_DOCTO);
        }

    }
}

