using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Previdencia
{
    public class ArquivoSisOb
    {

        public string livro { get; set; }
        public string folha { get; set; }
        public string termo { get; set; }
        public string dtcertidao { get; set; }
        public string nbeneficio { get; set; }
        public string nomefalecido { get; set; }
        public string nomemae { get; set; }
        public string dtnascimento { get; set; }
        public string dtobito { get; set; }
        public string numcpf { get; set; }
        public string nit { get; set; }
        public string tipoidcartorio { get; set; }
        public string idcartorio { get; set; }
        public string filler { get; set; }
        public string mesanoref { get; set; }
        public string matricula { get; set; }
        public string dat_importacao { get; set; }

        public ArquivoSisOb
                           (
                                string livro,
                                string folha,
                                string termo,
                                string dtcertidao,
                                string nbeneficio,
                                string nomefalecido,
                                string nomemae,
                                string dtnascimento,
                                string dtobito,
                                string numcpf,
                                string nit, 
                                string tipoidcartorio, 
                                string idcartorio, 
                                string filler, 
                                string mesanoref,
                                 string matricula,
                                string dat_importacao
                           )
        {
            this.livro = livro;
            this.folha = folha;
            this.termo = termo;
            this.dtcertidao = dtcertidao;
            this.nbeneficio = nbeneficio;
            this.nomefalecido = nomefalecido;
            this.nomemae = nomemae;
            this.dtnascimento = dtnascimento;
            this.dtobito = dtobito;
            this.numcpf = numcpf;
            this.nit = nit;
            this.tipoidcartorio = tipoidcartorio;
            this.idcartorio = idcartorio;
            this.filler = filler;
            this.mesanoref = mesanoref;
            this.matricula = matricula;
            this.dat_importacao = dat_importacao;
        }

        public ArquivoSisOb() { }
    }
}
