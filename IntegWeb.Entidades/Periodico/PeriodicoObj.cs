using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    public class PeriodicoObj
    {

        public int? id_periodico { get; set; }
        public int? id_editora { get; set; }
        public string nome_periodico { get; set; }
        public string codigo { get; set; }

        public PeriodicoObj
()
        {

            id_periodico = null;
            id_editora = null;
            nome_periodico = null;
            codigo = null;

        }

    }

}
