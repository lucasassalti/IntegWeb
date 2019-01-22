using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Framework
{
    public class UsuarioPortal
    {
        public Nullable<long> CPF { get; set; }
        public string Descricao { get; set; }
        public string NomeCompleto { get; set; }
        public string Sobrenome { get; set; }
        public string Nome { get; set; }
        public string login { get; set; }
        public string Apelido { get; set; }
        public short COD_EMPRS { get; set; }
        public int NUM_RGTRO_EMPRG { get; set; }
        public Nullable<short> NUM_DIGVR_EMPRG { get; set; }
        public Nullable<int> NUM_IDNTF_DPDTE { get; set; }
        public int NUM_IDNTF_RPTANT { get; set; }
        public string ENDERECO { get; set; }
        public string BAIRRO { get; set; }
        public string CEP { get; set; }
        public string CIDADE { get; set; }
        public string ESTADO { get; set; }
        public short[] ListaEmpresas { get; set; }

    }
}
