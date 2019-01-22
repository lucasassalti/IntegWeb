//using IntegWeb.Framework;
using IntegWeb.Entidades;
//using IntegWeb.Entidades.Relatorio;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial
{
    public class DepositoJudicialBLL : DepositoJudicialDAL
    {
        public Resultado Validar(PRE_TBL_DEPOSITO_JUDIC DebJudicial, PRE_TBL_DEPOSITO_JUDIC_PGTO newDebContaPgto)
        {
            Resultado retorno = new Resultado(true);

            if (DebJudicial.COD_EMPRS==null || DebJudicial.COD_EMPRS == 0)
            {
                retorno.Erro("O campo Empresa é obrigatório");
                return retorno;
            }

            if (DebJudicial.NUM_RGTRO_EMPRG == null || DebJudicial.NUM_RGTRO_EMPRG == 0)
            {
                retorno.Erro("O campo Matrícula é obrigatório");
                return retorno;
            }

            if (DebJudicial.NUM_MATR_PARTF == null || DebJudicial.NUM_MATR_PARTF == 0)
            {
                retorno.Erro("O campo Matrícula é obrigatório");
                return retorno;
            }

            if (DebJudicial.COD_TIPLTO == null || DebJudicial.COD_TIPLTO == 0)
            {
                retorno.Erro("O campo Assunto (Pleito) é obrigatório");
                return retorno;
            }

            if (newDebContaPgto!=null)
            {
                //PRE_TBL_DEPOSITO_JUDIC_PGTO valPgto = DebJudicial.PRE_TBL_DEPOSITO_JUDIC_PGTO.FirstOrDefault();

                if (newDebContaPgto.DTH_PAGAMENTO == null)
                {
                    retorno.Erro("O campo Dt. Pagamento é obrigatório");
                    return retorno;
                }
            }

            return retorno;

        }

        public new Resultado SaveData(PRE_TBL_DEPOSITO_JUDIC newDebConta)
        {
            return SaveData(newDebConta, null);
        }

        public Resultado SaveData(PRE_TBL_DEPOSITO_JUDIC newDebConta, PRE_TBL_DEPOSITO_JUDIC_PGTO newDebContaPgto)
        {
            Resultado retorno = new Resultado(true);

            retorno = base.SaveData(newDebConta);
            if (retorno.Ok && newDebContaPgto != null)
            {
                newDebContaPgto.COD_DEPOSITO_JUDIC = newDebConta.COD_DEPOSITO_JUDIC;
                retorno = base.SaveDataPgto(newDebContaPgto);
            }

            return retorno;
        }


    }
}
