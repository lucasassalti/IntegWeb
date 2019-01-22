using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Carga
{
    public class CargaDados
    {
        public int? id_carga { get; set; }
        public string carga { get; set; }
        public string titulo { get; set; }
        public string arquivo { get; set; }
        public string carga_extensao { get; set; }
        public int? tipo { get; set; }
        public string pkg_listar { get; set; }
        public string pkg_deletar { get; set; }
        public List<CargaDadosDePara> de_para { get; set; }

        public CargaDados()
        {
            id_carga = null;
            carga = null;
            titulo = null;
            arquivo = null;
            carga_extensao = null;
            tipo = null;
            de_para = new List<CargaDadosDePara>();               
        }

        public int GetCampoOrigemDePara(string origem_campo)
        {
            return de_para.FindIndex(r => r.origem_campo.Equals(origem_campo));
        }

    }
}
