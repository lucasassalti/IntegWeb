using System;
using System.Collections.Generic;

namespace IntegWeb.Entidades.Saude.Processos
{
    public class Prestador
    {
        public int    COD_CONVENENTE { get; set; }
        public string CNPJ { get; set; }
        public string CNES { get; set; }
        public string Razao_Social { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public bool   PJ { get; set; }
        public string REGISTRO_PLANO { get; set; }
        public string CODIGO_PLANO_OPERADORA { get; set; }
        public string RELACAO_OPERADORA { get; set; }
        public string TIPO_CONTRATUALIZACAO { get; set; }
        public string REGISTRO_ANS_OPERADORA_INTERMEDIARIA { get; set; }
        public string DATA_CONTRATUALIZACAO { get; set; }
        public Prestador CHAVE_ALTERACAO { get; set; }

    }
}
