using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades.Saude.Cobranca
{
    public class CisaoFusao
    {
        public Int32? Cod_Emprs_Ant { get; set; }
        public Int32? Num_Rgtro_Emprg_Ant { get; set; }
        public Int32? Cod_Emprs_Atu { get; set; }
        public Int32? Num_Rgtro_Emprg_Atu { get; set; }
        public Int32? Num_Digver_Atu { get; set; }
        public DateTime? Dat_Base_Cisao { get; set; }
        public DateTime? Dat_Atualizacao { get; set; }
        public string matricula { get; set; }
        public int? mes { get; set; }
        public int? ano { get; set; }



        public CisaoFusao()
        {

            Cod_Emprs_Ant = null;
            Num_Rgtro_Emprg_Ant = null;
            Cod_Emprs_Atu = null;
            Num_Rgtro_Emprg_Atu = null;
            Num_Digver_Atu = null;
            Dat_Base_Cisao = DateTime.MinValue;
            Dat_Atualizacao = DateTime.MinValue;
            matricula = null;
            mes = null;
            ano = null;
        }
    }

}
