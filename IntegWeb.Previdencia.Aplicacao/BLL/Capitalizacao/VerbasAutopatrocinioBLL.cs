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
    public class VerbasAutopatrocinioBLL : VerbasAutopatrocinioDAL
    {

        public Resultado Validar(TB_SCR_VERBAS_AUTOPATROC DeParaAutoVerba, bool novo = false)
        {
            Resultado retorno = new Resultado(true);

            if (DeParaAutoVerba.COD_EMPRS == 0)
            {
                retorno.Erro("O campo Empresa é obrigatório");
                return retorno;
            }

            if (DeParaAutoVerba.NUM_VRBFSS == 0)
            {
                retorno.Erro("O campo Plano é obrigatório");
                return retorno;
            }

            TB_SCR_VERBAS_AUTOPATROC ja_existe = base.GetItem(DeParaAutoVerba.COD_EMPRS, DeParaAutoVerba.NUM_VRBFSS);

            if (ja_existe != null && novo)
            {
                retorno.Erro("Este Plano Origem/Verba Funcesp já existe na base");
                return retorno;
            }

            return retorno;

        }

    }
}
