using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IntegWeb.Framework; 
using IntegWeb.Framework.Base;
//using System.Runtime.Serialization;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class PRE_TBL_ARQ_PATROCINA_LINHA : BaseEntity
    {

        public PRE_TBL_ARQ_PATROCINA_LINHA Clone()
        {
            return base.Clone<PRE_TBL_ARQ_PATROCINA_LINHA>(this);
        }

        public PRE_TBL_ARQ_PATROCINA_LINHA(short? pCOD_TIP_ARQUIVO, short pTIP_LINHA, string _data)
        {

            _data = _data.PadRight(1400, ' ');
            TIP_LINHA = pTIP_LINHA;
            NUM_HASH_LINHA = Convert.ToInt32(Util.HashSHA_to_long(_data));
            PRE_TBL_ARQ_PATROCINA_CRITICA = new List<PRE_TBL_ARQ_PATROCINA_CRITICA>();
            switch (pCOD_TIP_ARQUIVO)
            {
                case 1: //Cadastral - Empregados
                    COD_EMPRS = _data.Substring(0, 3);
                    NUM_RGTRO_EMPRG = _data.Substring(3, 10);
                    //DADOS = _data.Substring(0, 1330); //Empres. 1 é maior
                    DADOS = _data.Substring(0, 1331);
                    break;
                case 2: //Afastamento
                    COD_EMPRS = _data.Substring(0, 3);
                    NUM_RGTRO_EMPRG = _data.Substring(3, 10);
                    DADOS = _data.Substring(0, 44);
                    break;
                case 3: //Orgão Lotação
                    COD_EMPRS = _data.Substring(72, 3);
                    //NUM_RGTRO_EMPRG = _data.Substring(3, 10);
                    DADOS = _data;
                    break;
                case 4: //Financeiro
                    switch (pTIP_LINHA)
                    {
                        case 1:
                            COD_EMPRS = "000";
                            NUM_RGTRO_EMPRG = "000000";
                            DADOS = _data.Substring(0, 39);
                            break;
                        case 2:
                        default:
                            COD_EMPRS = _data.Substring(0, 3);
                            NUM_RGTRO_EMPRG = _data.Substring(3, 10);
                            DADOS = _data.Substring(0, 52);
                            break;
                        case 3:
                            COD_EMPRS = "000";
                            NUM_RGTRO_EMPRG = "000000";
                            DADOS = _data.Substring(0, 38);
                            break;
                    }
                    break;
            }
            DADOS = pCOD_TIP_ARQUIVO + DADOS;
        }

        //public bool Comparar(PRE_TBL_RECADASTRAMENTO _compare)
        //{
        //    return this.DAT_PGTO.Equals(_compare.DAT_PGTO) &&
        //           this.DAT_VENCIMENTO.Equals(_compare.DAT_VENCIMENTO) &&
        //           this.VLR_CONTRIB.Equals(_compare.DAT_VENCIMENTO) &&
        //           this.COD_BOLETO.Equals(_compare.COD_BOLETO);
        //}

    }
}

