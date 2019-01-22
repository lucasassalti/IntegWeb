using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Previdencia.Aplicacao.ENTITY;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    class LAY_ORGAO
    {

        public LAY_CAMPO LAY_NUM_ORGAO = new LAY_CAMPO() { pos = 1, tam = 15, nome_amigavel = "NÚM. ORGÃO", nome = "NUM_ORGAO" };
        //public LAY_CAMPO LAY_NUM_ORGAO = new LAY_CAMPO() { pos = 0, tam = 6 };
        public LAY_CAMPO LAY_NOM_ORGAO = new LAY_CAMPO() { pos = 16, tam = 50, nome_amigavel = "NOME ORGÃO", nome = "NOM_ORGAO" };
        public LAY_CAMPO LAY_COD_ORGAO = new LAY_CAMPO() { pos = 66, tam = 7, nome_amigavel = "CÓD. ORGÃO", nome = "COD_ORGAO" };
        public LAY_CAMPO LAY_COD_EMPRS = new LAY_CAMPO() { pos = 73, tam = 3, nome_amigavel = "CÓD. EMPRESA", nome = "COD_EMPRS" };
        public LAY_CAMPO LAY_NUM_FILIAL = new LAY_CAMPO() { pos = 76, tam = 2, nome_amigavel = "NÚM. FILIAL", nome = "NUM_FILIAL" };
        public LAY_CAMPO LAY_COD_ESTADO = new LAY_CAMPO() { pos = 78, tam = 2, nome_amigavel = "CÓD. ESTADO 1", nome = "COD_ESTADO" };
        public LAY_CAMPO LAY_COD_MUNICI = new LAY_CAMPO() { pos = 80, tam = 7, nome_amigavel = "CÓD. MUNICÍPIO", nome = "COD_MUNICI" };
        public LAY_CAMPO LAY_DCR_ENDER_ORGAO = new LAY_CAMPO() { pos = 87, tam = 80, nome_amigavel = "ENDEREÇO", nome = "DCR_ENDER_ORGAO" };
        public LAY_CAMPO LAY_NUM_ENDER_ORGAO = new LAY_CAMPO() { pos = 167, tam = 6, nome_amigavel = "NÚM. ENDEREÇO", nome = "NUM_ENDER_ORGAO" };
        public LAY_CAMPO LAY_DCR_COMPL_ORGAO = new LAY_CAMPO() { pos = 173, tam = 50, nome_amigavel = "COMPL. ENDEREÇO", nome = "DCR_COMPL_ORGAO" };
        public LAY_CAMPO LAY_NOM_CIDRS_ORGAO = new LAY_CAMPO() { pos = 223, tam = 40, nome_amigavel = "NOME MUNICÍPIO", nome = "NOM_CIDRS_ORGAO" };
        public LAY_CAMPO LAY_COD_UNDFD_ORGAO = new LAY_CAMPO() { pos = 263, tam = 2, nome_amigavel = "CÓD. ESTADO 2", nome = "COD_UNDFD_ORGAO" };
        public LAY_CAMPO LAY_COD_CEP_ORGAO = new LAY_CAMPO() { pos = 265, tam = 8, nome_amigavel = "CEP", nome = "COD_CEP_ORGAO" };
        public LAY_CAMPO LAY_COD_DDD_ORGAO = new LAY_CAMPO() { pos = 273, tam = 4, nome_amigavel = "DDD", nome = "COD_DDD_ORGAO" };
        public LAY_CAMPO LAY_NUM_TELEF_ORGAO = new LAY_CAMPO() { pos = 277, tam = 8, nome_amigavel = "TELEFONE", nome = "NUM_TELEF_ORGAO" };
        public LAY_CAMPO LAY_NOM_BAIRRO_ORGAO = new LAY_CAMPO() { pos = 285, tam = 25, nome_amigavel = "BAIRRO", nome = "NOM_BAIRRO_ORGAO" };

        public ORGAO DePara(string pCOD_EMPRS, string pNUM_RGTRO_EMPRG, string pDADOS)
        {
            ORGAO ret = new ORGAO();

            //ret.NUM_ORGAO = LAYOUT_UTIL.GetInt(pDADOS, LAY_NUM_ORGAO);
            ret._NUM_ORGAO_ARQUIVO = pDADOS.Substring(LAY_NUM_ORGAO.pos, LAY_NUM_ORGAO.tam).Trim();            
            ret.NOM_ORGAO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_ORGAO);
            ret.COD_ORGAO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_ORGAO);
            ret.COD_EMPRS = LAYOUT_UTIL.GetShort(pDADOS, LAY_COD_EMPRS);
            ret.NUM_FILIAL = LAYOUT_UTIL.GetShort(pDADOS, LAY_NUM_FILIAL);            
            ret.COD_ESTADO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_ESTADO);
            ret.COD_MUNICI = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_MUNICI);
            ret.DCR_ENDER_ORGAO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_ENDER_ORGAO);
            ret.NUM_ENDER_ORGAO  = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_ENDER_ORGAO);
            ret.DCR_COMPL_ORGAO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_DCR_COMPL_ORGAO);
            ret.NOM_CIDRS_ORGAO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_CIDRS_ORGAO);
            ret.COD_UNDFD_ORGAO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_UNDFD_ORGAO);
            ret.COD_CEP_ORGAO = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_COD_CEP_ORGAO);
            ret.COD_DDD_ORGAO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_COD_DDD_ORGAO);
            ret.NUM_TELEF_ORGAO = LAYOUT_UTIL.GetIntNull(pDADOS, LAY_NUM_TELEF_ORGAO);
            ret.NOM_BAIRRO_ORGAO = LAYOUT_UTIL.GetStringNull(pDADOS, LAY_NOM_BAIRRO_ORGAO);
            //public string COD_ORIGEM_ORGAO { get; set; }
            //public Nullable<short> ATOCODATRIBUTOORGAO { get; set; }
            //public Nullable<int> PAICOD { get; set; }
            //public string NOM_PAIS_ORGAO { get; set; }

           return ret;
        }
        
    }
}
