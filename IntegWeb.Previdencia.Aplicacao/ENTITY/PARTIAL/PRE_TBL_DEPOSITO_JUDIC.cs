using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class PRE_TBL_DEPOSITO_JUDIC
    {

        public PRE_TBL_DEPOSITO_JUDIC Clone()
        {
            PRE_TBL_DEPOSITO_JUDIC _clone = (PRE_TBL_DEPOSITO_JUDIC)this.MemberwiseClone();
            return _clone;
        }

        public bool Comparar(PRE_TBL_DEPOSITO_JUDIC _compare)
        {

            //_compare.PRE_TBL_DEPOSITO_JUDIC_PGTO.(_compare.PRE_TBL_DEPOSITO_JUDIC_PGTO.FirstOrDefault()); 
            //bool equal_childs = (_compare.PRE_TBL_DEPOSITO_JUDIC_PGTO.Count==0);

            //foreach (PRE_TBL_DEPOSITO_JUDIC_PGTO _compare_child in _compare.PRE_TBL_DEPOSITO_JUDIC_PGTO)
            //{
            //    PRE_TBL_DEPOSITO_JUDIC_PGTO compara_filho =
            //        this.PRE_TBL_DEPOSITO_JUDIC_PGTO.FirstOrDefault(p => p.COD_DEPOSITO_JUDIC_PGTO == _compare_child.COD_DEPOSITO_JUDIC_PGTO
            //                                                        && p.DTH_INCLUSAO == _compare_child.DTH_INCLUSAO);
            //    if (compara_filho != null)
            //    {
            //        equal_childs = compara_filho.Comparar(_compare_child);
            //        if (!equal_childs) break;
            //    }
            //}

            return this.COD_EMPRS.Equals(_compare.COD_EMPRS) &&
                   this.NUM_RGTRO_EMPRG.Equals(_compare.NUM_RGTRO_EMPRG) &&
                   this.NUM_MATR_PARTF.Equals(_compare.NUM_MATR_PARTF) &&
                   (this.NRO_PASTA ?? "").Equals(_compare.NRO_PASTA) &&
                   (this.NRO_PROCESSO ?? "").Equals(_compare.NRO_PROCESSO) &&
                   this.POLO_ACJUD.Equals(_compare.POLO_ACJUD) &&
                   (this.COD_VARA_PROC ?? "").Equals(_compare.COD_VARA_PROC) &&
                   this.COD_TIPLTO.Equals(_compare.COD_TIPLTO);
        }

    }
}

