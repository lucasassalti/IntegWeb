using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    public class Editora
    {
        public int? id_editora { get; set; }
        public string nome_editora { get; set; }
        public string cgc_cpf_editora { get; set; }
        public string endereco_editora { get; set; }
        public string cidade_editora { get; set; }
        public string bairro_editora { get; set; }
        public string uf_editora { get; set; }
        public string cep_editora { get; set; }
        public string numero_editora { get; set; }
        public string complemento_editora { get; set; }
        public string contato { get; set; }
        public string fone_editora { get; set; }
        public string fax_editora { get; set; }
        public string email_editora { get; set; }
        public string site_editora { get; set; }

        public Editora()
        {

            id_editora = null;
            nome_editora = null;
            cgc_cpf_editora = null;
            endereco_editora = null;
            cidade_editora = null;
            bairro_editora = null;
            uf_editora = null;
            cep_editora = null;
            numero_editora = null;
            complemento_editora = null;
            fone_editora = null;
            fax_editora = null;
            email_editora = null;
            site_editora = null;
            contato = null;
        }
    }


}
