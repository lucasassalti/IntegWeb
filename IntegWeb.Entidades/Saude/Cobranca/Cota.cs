using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Cobranca
{
    [Serializable]
    public class Cota
    {
        public int? id { get; set; }
        public DateTime? dtmov { get; set; }
        public DateTime? dtinigeracao { get; set; }
        public string username { get; set; }
        public DateTime? dtfimgeracao { get; set; }


        public Cota()
        {
            id = null;
            dtmov = null;
            dtinigeracao = null;
            username = null;
            dtfimgeracao = null;
        }
    }

}
