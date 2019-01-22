using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IntegWeb.Framework.Base;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class PRE_TBL_ARQ_PATROCINA : BaseEntity
    {

        public PRE_TBL_ARQ_PATROCINA Clone()
        {
            return base.Clone<PRE_TBL_ARQ_PATROCINA>(this);
        }

        public string _CAMINHO_COMPLETO_ARQUIVO { get; set; }
        public int _TAMANHO_ARQUIVO { get; set; }
        public bool _PROCESSADO { get; set; }
        //public bool _ARQUIVO_SUBSTITUIDO { get; set; }
        public PRE_TBL_ARQ_PATROCINA_LINHA _LINHA_HEADER { get; set; }

        //public bool Comparar(PRE_TBL_RECADASTRAMENTO _compare)
        //{
        //    return this.DAT_PGTO.Equals(_compare.DAT_PGTO) &&
        //           this.DAT_VENCIMENTO.Equals(_compare.DAT_VENCIMENTO) &&
        //           this.VLR_CONTRIB.Equals(_compare.DAT_VENCIMENTO) &&
        //           this.COD_BOLETO.Equals(_compare.COD_BOLETO);
        //}
        
    }
}

