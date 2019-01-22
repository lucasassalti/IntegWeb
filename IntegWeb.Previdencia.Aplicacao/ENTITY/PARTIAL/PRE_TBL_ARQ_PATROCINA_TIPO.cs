using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IntegWeb.Framework.Base;
//using System.Runtime.Serialization;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class PRE_TBL_ARQ_PATROCINA_TIPO : BaseEntity
    {

        public PRE_TBL_ARQ_PATROCINA_TIPO Clone()
        {
            return base.Clone<PRE_TBL_ARQ_PATROCINA_TIPO>(this);
        }

        //public PRE_TBL_ARQ_PATROCINA_TIPO(int pNUM_TAM_REGISTRO)
        //{
        //    if (pNUM_TAM_REGISTRO > 1327)
        //    {
        //        COD_TIPO = 1; //Cadastral - Empregados
        //    }
        //    else if (pNUM_TAM_REGISTRO > 285)
        //    {
        //        COD_TIPO = 3; //Orgão Lotação
        //    }
        //    else if (pNUM_TAM_REGISTRO > 50)
        //    {
        //        COD_TIPO = 4; //Financeiro
        //    }
        //    else if (pNUM_TAM_REGISTRO > 40)
        //    {
        //        COD_TIPO = 2; //Afastamento
        //    }
        //}

        //public bool Comparar(PRE_TBL_RECADASTRAMENTO _compare)
        //{
        //    return this.DAT_PGTO.Equals(_compare.DAT_PGTO) &&
        //           this.DAT_VENCIMENTO.Equals(_compare.DAT_VENCIMENTO) &&
        //           this.VLR_CONTRIB.Equals(_compare.DAT_VENCIMENTO) &&
        //           this.COD_BOLETO.Equals(_compare.COD_BOLETO);
        //}

    }
}

