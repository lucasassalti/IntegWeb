using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IntegWeb.Framework.Base;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class PRE_TBL_ARQ_PAT_DEMONSTRATIVO : BaseEntity
    {

        public PRE_TBL_ARQ_PAT_DEMONSTRATIVO Clone()
        {
            return base.Clone<PRE_TBL_ARQ_PAT_DEMONSTRATIVO>(this);
        }
        
    }
}

