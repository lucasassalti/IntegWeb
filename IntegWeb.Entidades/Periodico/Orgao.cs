using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    public class Orgao
    {

        public string cod_orgao { get; set; }
        public string setor { get; set; }
        public string responsavel { get; set; }
        public Orgao()
        {
            cod_orgao = null;
            setor = null;
            responsavel = null;

        }

    }

}
