using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    public class AssinaturaArea
    {
        public int?    id_assinatura { get; set; }
        public int?    id_area { get; set; }

        public AssinaturaArea() { 
          
         id_assinatura =null;
         id_area = null;
        }
    }
}
