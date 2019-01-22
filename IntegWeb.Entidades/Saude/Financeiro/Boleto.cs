using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Financeiro
{
    [Serializable]
    public class Boleto
    {
        public DateTime DataVencimento { get; set; }


        public Boleto()
        {

            DataVencimento = DateTime.MinValue;

        }
        
    }
}
