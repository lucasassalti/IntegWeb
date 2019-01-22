using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Entidades
{
    public class ArquivoDownload
    {
        public string nome_arquivo { get; set; }
        public string caminho_arquivo { get; set; }        
        public object dados { get; set; }
        public string modo_abertura { get; set; }
        public string opcao_arquivo { get; set; }          //Padrão do Newline DOS ou UNIX

    }
}
