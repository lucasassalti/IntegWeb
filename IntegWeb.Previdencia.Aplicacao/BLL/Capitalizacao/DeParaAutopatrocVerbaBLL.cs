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
using IntegWeb.Previdencia.Aplicacao.DAL.Capitalizacao;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao
{
    public class DeParaAutopatrocVerbaBLL : DeParaAutopatrocVerbaDAL
    {

        public Resultado Validar(TB_SCR_DEPARA_AUTOPATROC_VERBA DeParaAutoVerba, bool novo = false)
        {
            Resultado retorno = new Resultado(true);

            if (DeParaAutoVerba.EMPRS_DEST == 0)
            {
                retorno.Erro("O campo Empresa é obrigatório");
                return retorno;
            }

            if (DeParaAutoVerba.PLANO_ORIGEM == 0)
            {
                retorno.Erro("O campo Plano é obrigatório");
                return retorno;
            }

            if (DeParaAutoVerba.NUM_VER_FUND == 0)
            {
                retorno.Erro("O campo Verba Funcesp é obrigatório");
                return retorno;
            }

            if (DeParaAutoVerba.NUM_VER_DEST == 0)
            {
                retorno.Erro("O campo Verba Destino é obrigatório");
                return retorno;
            }

            TB_SCR_DEPARA_AUTOPATROC_VERBA ja_existe = base.GetItem(DeParaAutoVerba.EMPRS_DEST, DeParaAutoVerba.PLANO_ORIGEM, DeParaAutoVerba.NUM_VER_FUND);

            if (ja_existe != null && novo)
            {
                retorno.Erro("Este Plano Origem/Verba Funcesp já existe na base");
                return retorno;
            }

            return retorno;

        }

    }
}
