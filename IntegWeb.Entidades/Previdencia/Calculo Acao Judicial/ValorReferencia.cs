using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial
{
    public class ValorReferencia
    {
        public int?       num_matr_partf { get; set; }
        public int?       num_rgtro_emprg { get; set; }
        public int?       num_sqncl_prc { get; set; }
        public string     num_proc { get; set; }
        public string     num_pasta { get; set; }
        public DateTime?  dat_prescr { get; set; }
        public string     polo_acjud { get; set; }
        public int?       cod_hstplto { get; set; }
        public string     cod_vara_proc { get; set; }
        public int?       cod_tiplto { get; set; }
        public int?       cod_emprs { get; set; }
        public string     nome_emprg { get; set; }
        public Int64?     cpf_emprg { get; set; }
        public DateTime?  data_admissao { get; set; }
        public DateTime?  data_demissao { get; set; }
        public DateTime?  data_nascto { get; set; }
        public DateTime?  data_adesao { get; set; }
        public DateTime?  dib { get; set; }
        public string     plano { get; set; }
        public string     perfil { get; set; }
        public int?       id_acao_processo { get; set; }
        public string     desc_processo { get; set; }
        public DateTime?  dta_status { get; set; }
        public decimal?   bsps_dib_splto { get; set; }
        public decimal?   bd_dib_splto { get; set; }
        public decimal?   cv_dib_splto { get; set; }
        public decimal?   bsps_atu_splto { get; set; }
        public decimal?   bd_atu_splto { get; set; }
        public decimal?   cv_atu_splto { get; set; }
        public decimal?   bsps_dib_cplto { get; set; }
        public decimal?   bd_dib_cplto { get; set; }
        public decimal?   cv_dib_cplto { get; set; }
        public decimal?   bsps_atu_cplto { get; set; }
        public decimal?   bd_atu_cplto { get; set; }
        public decimal?   cv_atu_cplto { get; set; }
        public decimal?   cntr_part_at_bsps { get; set; }
        public decimal?   bnf_part_ret_bsps { get; set; }
        public decimal?   cntr_part_ret_bsps { get; set; }
        public decimal?   resmat_part_bsps { get; set; }
        public decimal?   resmat_ant_part_bsps { get; set; }
        public decimal?   cntr_part_at_bd { get; set; }
        public decimal?   bnf_part_ret_bd { get; set; }
        public decimal?   cntr_part_ret_bd { get; set; }
        public decimal?   resmat_part_bd { get; set; }
        public decimal?   cntr_part_at_cv { get; set; }
        public decimal?   bnf_part_ret_cv { get; set; }
        public decimal?   prc_part_resmat_bsps { get; set; }
        public decimal?   prc_part_resmat_bd { get; set; }
        public decimal?   total_part { get; set; }
        public decimal?   cntr_patr_at_bsps { get; set; }
        public decimal?   bnf_patr_ret_bsps { get; set; }
        public decimal?   resmat_patr_bsps { get; set; }
        public decimal?   resmat_ant_patr_bsps { get; set; }
        public decimal?   cntr_patr_at_bd { get; set; }
        public decimal?   bnf_patr_ret_bd { get; set; }
        public decimal?   resmat_patr_bd { get; set; }
        public decimal?   cntr_patr_at_cv { get; set; }
        public decimal?   bnf_patr_ret_cv { get; set; }
        public decimal?   total_patr { get; set; }
        public decimal?   prc_patr_resmat_bsps { get; set; }
        public decimal?   prc_patr_resmat_bd { get; set; }
        public string     nota { get; set; }
        public string     obs { get; set; }
        public string     pasta { get; set; }
        public DateTime?  dta_retr_atlz { get; set; }
        public int?       cod_tip_atlz { get; set; }
        public string     responsavel { get; set; }
        public string     cod_origem_dados { get; set; }
        public string     cod_situacao { get; set; }

        public ValorReferencia()
        {
            //responsavel = null;
            //desc_mensagem = null;
            //id_acao_processo = null;
            //num_matr_partf = null;
            //vl_inicial = 0;
            //vl_sempleito = 0;
            //vl_compleito = 0;
            //mrc_ant_rsv = "N";
            //mrc_atlz_igpdi = "N";
            //mrc_atlz_trab = "N";
            //mrc_catlz_civ = "N";
        }
    }
}
