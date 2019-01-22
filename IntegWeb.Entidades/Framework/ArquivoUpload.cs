using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Framework
{
    public class ArquivoUpload
    {
        public string nome_arquivo { get; set; }
        public string caminho_arquivo { get; set; }
        public bool processado { get; set; }      
    }
}
