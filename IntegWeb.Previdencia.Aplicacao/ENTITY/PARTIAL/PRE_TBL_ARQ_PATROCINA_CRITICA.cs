using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IntegWeb.Framework.Base;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class PRE_TBL_ARQ_PATROCINA_CRITICA : BaseEntity
    {

        public PRE_TBL_ARQ_PATROCINA_CRITICA()
        {
            this.TIP_CRITICA = 1;
        }

        public PRE_TBL_ARQ_PATROCINA_CRITICA Clone()
        {
            return base.Clone<PRE_TBL_ARQ_PATROCINA_CRITICA>(this);
        }

    }
}

