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
    public class CadAssociacaoVerbaBLL : CadAssociacaoVerbaDAL
    {

        public Resultado Validar(ASSOCIACAO_VERBA CCusto, bool novo = false)
        {
            Resultado retorno = new Resultado(true);

            if (CCusto.COD_ASSOC == 0)
            {
                retorno.Erro("O campo Associado é obrigatório");
                return retorno;
            }

            if (CCusto.NUM_VRBFSS == 0)
            {
                retorno.Erro("O campo Verba é obrigatório");
                return retorno;
            }

            ASSOCIACAO_VERBA ja_existe = base.GetCCusto(CCusto.COD_ASSOCIACAO_VERBA);

            if (ja_existe != null && novo)
            {
                retorno.Erro("Este Centro de Custo já existe na base");
                return retorno;
            }

            return retorno;

        }

    }
}
