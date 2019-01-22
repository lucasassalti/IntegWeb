using IntegWeb.Framework; 
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using IntegWeb.Entidades;
using IntegWeb.Previdencia.Aplicacao.DAL.Int_Protheus;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Int_Protheus
{
    public class CCustoBLL : CCustoDAL
    {

        public Resultado Validar(CCUSTO_SAU CCusto, bool novo = false)
        {
            Resultado retorno = new Resultado(true);

            if (CCusto.COD_EMPRS == 0)
            {
                retorno.Erro("O campo Empresa é obrigatório");
                return retorno;
            }

            if (CCusto.NUM_ORGAO == 0)
            {
                retorno.Erro("O campo Núm. Orgão é obrigatório");
                return retorno;
            }

            CCUSTO_SAU ja_existe = base.GetCCusto(CCusto.COD_EMPRS, CCusto.NUM_ORGAO, (CCusto.DSP_ADM=="S"));

            if (ja_existe != null && novo)
            {
                retorno.Erro("Este Centro de Custo já existe na base");
                return retorno;
            }

            return retorno;

        }

    }
}
