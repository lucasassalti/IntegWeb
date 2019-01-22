using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Faturamento
{
    public class AdinResumo
    {
        public int id_adinresumo {get;set;}
        public string   descricao{get;set;}
        public string   valor{get;set;}
        public string   mesano{get;set;}
        public string matricula { get; set; }
        public DateTime datainclusao { get; set; }
    }
}
