using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Entidades
{
    [Serializable]
    public class CadastroFlag
    {
        private int cod_emprs;
        private int num_rgtro_emprg;
        private int num_idntf_rptant;        
        private string nom_emprg_repres;        
        private DateTime dt_inclusao;        
        private string nom_solic_inclusao;        
        private string flag_judicial;
        
        public int Cod_emprs
        {
            get { return cod_emprs; }
            set { cod_emprs = value; }
        }
        public int Num_rgtro_emprg
        {
            get { return num_rgtro_emprg; }
            set { num_rgtro_emprg = value; }
        }
        public int Num_idntf_rptant
        {
            get { return num_idntf_rptant; }
            set { num_idntf_rptant = value; }
        }
        public string Nom_emprg_repres
        {
            get { return nom_emprg_repres; }
            set { nom_emprg_repres = value; }
        }
        public DateTime Dt_inclusao
        {
            get { return dt_inclusao; }
            set { dt_inclusao = value; }
        }
        public string Nom_solic_inclusao
        {
            get { return nom_solic_inclusao; }
            set { nom_solic_inclusao = value; }
        }
        public string Flag_judicial
        {
            get { return flag_judicial; }
            set { flag_judicial = value; }
        }

        public CadastroFlag()
        {
            cod_emprs = int.MinValue;
            num_rgtro_emprg = int.MinValue;
            Num_idntf_rptant = int.MinValue;
            nom_emprg_repres = string.Empty;
            dt_inclusao = DateTime.MinValue;
            nom_solic_inclusao = string.Empty;
            flag_judicial = string.Empty;

        }

    }
}
