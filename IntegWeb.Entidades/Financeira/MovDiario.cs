using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Financeira.Tesouraria
{
    public class MovDiario
    {

        public string tp_registro { get; set; }
        public string empresa { get; set; }
        public string emp_cadastro { get; set; }
        public string registro { get; set; }
        public string matricula { get; set; }
        public string representante { get; set; }
        public string cpf { get; set; }
        public string nome { get; set; }
        public string vencimento_parcela { get; set; }
        public string contrato { get; set; }
        public string dt_efet_mov { get; set; }
        public string mes_ano_ref { get; set; }
        public string mov_tp { get; set; }
        public string mov_hist { get; set; }
        public string desc_mov_hist { get; set; }
        public string vlr_mov { get; set; }
        public string dt_mov_ref { get; set; }
        public string contrato_mov { get; set; }
        public string sequencial { get; set; }
        public string responsavel { get; set; }
        public string dt_inclusao { get; set; }

        public MovDiario() { }

        public MovDiario(
                            string tp_registro,
                             string empresa,
                             string emp_cadastro,
                             string registro,
                             string matricula,
                             string representante,
                             string cpf,
                             string nome,
                             string vencimento_parcela,
                             string contrato,
                             string dt_efet_mov,
                             string mes_ano_ref,
                             string mov_tp,
                             string mov_hist,
                             string desc_mov_hist,
                             string vlr_mov,
                             string dt_mov_ref,
                             string contrato_mov,
                             string sequencial,
                             string responsavel,
                             string dt_inclusao
                         )
        {
            this.tp_registro = tp_registro;
            this.empresa = empresa;
            this.emp_cadastro = emp_cadastro;
            this.registro = registro;
            this.matricula = matricula;
            this.representante = representante;
            this.cpf = cpf;
            this.nome = nome;
            this.vencimento_parcela = vencimento_parcela;
            this.contrato = contrato;
            this.dt_efet_mov = dt_efet_mov;
            this.mes_ano_ref = mes_ano_ref;
            this.mov_tp = mov_tp;
            this.mov_hist = mov_hist;
            this.desc_mov_hist = desc_mov_hist;
            this.vlr_mov = vlr_mov;
            this.dt_mov_ref = dt_mov_ref;
            this.contrato_mov = contrato_mov;
            this.sequencial = sequencial;
            this.responsavel = responsavel;
            this.dt_inclusao = dt_inclusao;


        }

    }
}
