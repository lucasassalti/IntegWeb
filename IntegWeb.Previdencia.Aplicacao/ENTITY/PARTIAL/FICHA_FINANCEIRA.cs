using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IntegWeb.Framework.Base;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class FICHA_FINANCEIRA : BaseEntity
    {

        public FICHA_FINANCEIRA Clone()
        {
            return base.Clone<FICHA_FINANCEIRA>(this);
        }

        //public List<string> _NAO_ATUALIZAR { get; set; }

        //public bool Comparar(PRE_TBL_RECADASTRAMENTO _compare)
        //{
        //    return this.DAT_PGTO.Equals(_compare.DAT_PGTO) &&
        //           this.DAT_VENCIMENTO.Equals(_compare.DAT_VENCIMENTO) &&
        //           this.VLR_CONTRIB.Equals(_compare.DAT_VENCIMENTO) &&
        //           this.COD_BOLETO.Equals(_compare.COD_BOLETO);
        //}

    }
}

