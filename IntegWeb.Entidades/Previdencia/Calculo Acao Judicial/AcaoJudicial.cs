using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial
{
    public class AcaoJudicial
    {
        public int? num_matr_partf { get; set; }
        public int? num_seq_prc { get; set; }
        public int? matricula { get; set; }
        public int? cod_emprs { get; set; }
        public string flag_abn { get; set; }
        public string num_processo { get; set; }
        public string num_pasta { get; set; }
        public string obs_src { get; set; }
        public string desc_mensagem { get; set; }
        public int? id_acao_processo { get; set; }
        public int? cod_tiplto { get; set; }
        public int? cod_tipatlz { get; set; }
        public string cod_vara { get; set; }
        public string flag_acao_Jud { get; set; }
        public string nome_benficiario { get; set; }
        public string cpf_benficiario { get; set; }
        public DateTime? data_atualiz { get; set; }
        public string responsavel { get; set; }
        public DateTime? dt_ini_pgto { get; set; }
        public DateTime? dt_fin_pgto { get; set; }
        public DateTime? dt_ajuizamento { get; set; }
        public decimal? vl_inicial { get; set; }
        public decimal? vl_sempleito { get; set; }
        public decimal? vl_compleito { get; set; }
        public string mrc_ant_rsv { get; set; }
        public string mrc_atlz_igpdi { get; set; }
        public string mrc_atlz_trab { get; set; }
        public string mrc_catlz_civ { get; set; }        
        public string mrc_cad_bnf { get; set; }
        public string mrc_cad_rsv_splto { get; set; }
        public string mrc_cad_rsv_cplto { get; set; }
        public int tip_bnf { get; set; }

        public AcaoJudicial()
        {
            responsavel = null;
            desc_mensagem = null;
            id_acao_processo = null;
            num_matr_partf = null;
            num_seq_prc = null;
            flag_abn = null;
            data_atualiz = null;
            num_processo = null;
            num_pasta = null;
            obs_src = null;
            cod_tiplto = null;
            cod_vara = null;
            flag_acao_Jud = null;
            dt_ini_pgto = null;
            dt_fin_pgto = null;
            dt_ajuizamento = null;
            nome_benficiario = null;
            vl_inicial = 0;
            vl_sempleito = 0;
            vl_compleito = 0;
            mrc_ant_rsv = "N";
            mrc_atlz_igpdi = "N";
            mrc_atlz_trab = "N";
            mrc_catlz_civ = "N";

        }
    }
}
