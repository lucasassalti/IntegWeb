using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IntegWeb.Framework.Base;
//using System.Runtime.Serialization;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class PRE_TBL_RECADASTRAMENTO : BaseEntity
    {
        //public PRE_TBL_RECADASTRAMENTO Clone()
        //{
        //    PRE_TBL_RECADASTRAMENTO _clone = (PRE_TBL_RECADASTRAMENTO)this.MemberwiseClone();
        //    return _clone;
        //}

        //public PRE_TBL_RECADASTRAMENTO Clone3()
        //{
        //    DataContractSerializer dcSer = new DataContractSerializer(this.GetType());
        //    MemoryStream memoryStream = new MemoryStream();
        //    dcSer.WriteObject(memoryStream, this);
        //    memoryStream.Position = 0;
        //    PRE_TBL_RECADASTRAMENTO newObject = (PRE_TBL_RECADASTRAMENTO)dcSer.ReadObject(memoryStream);
        //    return newObject;
        //}

        public PRE_TBL_RECADASTRAMENTO Clone()
        {
            return base.Clone<PRE_TBL_RECADASTRAMENTO>(this);
        }

        //public bool Comparar(PRE_TBL_RECADASTRAMENTO _compare)
        //{
        //    return this.DAT_PGTO.Equals(_compare.DAT_PGTO) &&
        //           this.DAT_VENCIMENTO.Equals(_compare.DAT_VENCIMENTO) &&
        //           this.VLR_CONTRIB.Equals(_compare.DAT_VENCIMENTO) &&
        //           this.COD_BOLETO.Equals(_compare.COD_BOLETO);
        //}

    }
}

