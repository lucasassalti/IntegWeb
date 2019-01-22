using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Relatorio
{
    public class Relatorio
    {
        public int? id_relatorio { get; set; }
        public string relatorio { get; set; }
        public string titulo { get; set; }
        public string arquivo { get; set; }
        public string relatorio_extensao { get; set; }
        public int? tipo { get; set; }
        public List<Parametro> parametros { get; set; }

        public Relatorio() {
            id_relatorio = null;
            relatorio = null;
            titulo = null;
            arquivo = null;
            relatorio_extensao = null;
            tipo = null;
            parametros = new List<Parametro>();                
        }

        public Parametro get_parametro(string nome_param)
        {
            foreach (var param in parametros)
            {
                if (nome_param == param.parametro)
                {
                    return param;
                }               
            }
            return null;
        }

    }
}
