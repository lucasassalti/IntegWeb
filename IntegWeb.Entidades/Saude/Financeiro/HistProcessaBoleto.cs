using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Financeiro
{
    [Serializable]
    public class HistProcessaBoleto
    {

        public int? execucao_id { get; set; }
        public string usuario { get; set; }
        public DateTime? inicio { get; set; }
        public DateTime? fim { get; set; }
        public string mensagem { get; set; }
        public int? lote_consolidado { get; set; }
        public int? lote_nao_consolidado { get; set; }
        public DateTime? dat_vencimento { get; set; }

        public HistProcessaBoleto()
        {

            execucao_id = null;
            usuario = null;
            inicio = null;
            fim = null;
            mensagem = null;
            lote_consolidado = null;
            lote_nao_consolidado = null;
            dat_vencimento = null;

        }


    }
}
