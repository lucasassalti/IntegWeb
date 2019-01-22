
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;

namespace IntegWeb.Framework.Base
{
    public class BaseEntity
    {
        public T Clone<T>(T obj) where T : class
        {
            DataContractSerializer dcSer = new DataContractSerializer(this.GetType());
            MemoryStream memoryStream = new MemoryStream();
            dcSer.WriteObject(memoryStream, this);
            memoryStream.Position = 0;
            T newObject = (T)dcSer.ReadObject(memoryStream);
            return newObject;
        }
    }

}