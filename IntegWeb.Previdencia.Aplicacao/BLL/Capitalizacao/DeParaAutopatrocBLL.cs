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
    public class DeParaAutopatrocBLL : DeParaAutopatrocDAL
    {

        public Resultado Validar(TB_SCR_DEPARA_AUTOPATROC DeParaAutoVerba, bool novo = false)
        {
            Resultado retorno = new Resultado(true);

            if (DeParaAutoVerba.COD_EMPRS_ORIGEM == 0)
            {
                retorno.Erro("O campo Empresa é obrigatório");
                return retorno;
            }

            if (DeParaAutoVerba.NUM_RGTRO_EMPRG == 0)
            {
                retorno.Erro("O campo Matricula é obrigatório");
                return retorno;
            }

            if (DeParaAutoVerba.COD_EMPRS_DESTINO == 0)
            {
                retorno.Erro("O campo Empresa Destino é obrigatório");
                return retorno;
            }

            TB_SCR_DEPARA_AUTOPATROC ja_existe = base.GetItem(short.Parse(DeParaAutoVerba.COD_EMPRS_ORIGEM.ToString()), int.Parse(DeParaAutoVerba.NUM_RGTRO_EMPRG.ToString()), short.Parse(DeParaAutoVerba.COD_EMPRS_DESTINO.ToString()));

            if (ja_existe != null && novo)
            {
                retorno.Erro("Este Plano Origem/Verba Funcesp já existe na base");
                return retorno;
            }

            return retorno;

        }

    }
}
