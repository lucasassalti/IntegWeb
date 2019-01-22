using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Previdencia.Aplicacao.ENTITY;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    class LAY_AFASTAMENTO
    {
        public LAY_CAMPO LAY_COD_EMPRS = new LAY_CAMPO() { pos = 1, tam = 4, nome_amigavel = "CÓD. EMPRESA", nome = "COD_EMPRS" };
        public LAY_CAMPO LAY_NUM_RGTRO_EMPRG = new LAY_CAMPO() { pos = 4, tam = 10, nome_amigavel = "MATRÍCULA", nome = "NUM_RGTRO_EMPRG" };
        public LAY_CAMPO LAY_DAT_INAFT_AFAST = new LAY_CAMPO() { pos = 14, tam = 8, nome_amigavel = "DATA INÍCIO AFASTAMENTO", nome = "DAT_INAFT_AFAST" };
        public LAY_CAMPO LAY_DAT_PRVFA_AFAST = new LAY_CAMPO() { pos = 22, tam = 8, nome_amigavel = "DATA PREVISTA RETORNO", nome = "DAT_PRVFA_AFAST" };
        public LAY_CAMPO LAY_DAT_FMAFT_AFAST = new LAY_CAMPO() { pos = 30, tam = 8, nome_amigavel = "DATA FINAL AFASTAMENTO", nome = "DAT_FMAFT_AFAST" };
        public LAY_CAMPO LAY_COD_TIPAFT = new LAY_CAMPO() { pos = 38, tam = 4, nome_amigavel = "CÓD. TIPO AFASTAMENTO", nome = "COD_TIPAFT" };

        public AFASTAMENTO DePara(string pCOD_EMPRS, string pNUM_RGTRO_EMPRG, string pDADOS)
        {
            AFASTAMENTO ret = new AFASTAMENTO();
            //ret._NAO_ATUALIZAR = new List<string>();

            ret.COD_EMPRS = short.Parse(pCOD_EMPRS);
            ret.NUM_RGTRO_EMPRG = int.Parse(pNUM_RGTRO_EMPRG);
            ret.DAT_INAFT_AFAST = LAYOUT_UTIL.GetData(pDADOS, LAY_DAT_INAFT_AFAST);
            ret.DAT_PRVFA_AFAST = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_PRVFA_AFAST);
            ret.DAT_FMAFT_AFAST = LAYOUT_UTIL.GetDataNull(pDADOS, LAY_DAT_FMAFT_AFAST);
            ret.COD_TIPAFT = LAYOUT_UTIL.GetShort(pDADOS, LAY_COD_TIPAFT);
            //ret.COD_GRUPO_CID = null;
            //ret.COD_ITEM_CID = null;
            return ret;
        }
    }
}
