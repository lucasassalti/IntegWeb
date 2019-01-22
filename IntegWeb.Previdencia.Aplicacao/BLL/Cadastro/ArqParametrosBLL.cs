using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
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

namespace IntegWeb.Previdencia.Aplicacao.BLL
{

    public class ArqParametrosBLL : ArqParametrosDAL
    {


        public Resultado Validar(PRE_TBL_ARQ_PARAM newobj)
        {
            Resultado res = new Resultado();

            res.Sucesso();

            if (String.IsNullOrEmpty(newobj.DCR_PARAM))
            {
                res.Erro("O valor do parâmetro é obrigatório");
            }

            if (newobj.NOM_PARAM == "EMAIL_PATROCINADORA" && newobj.COD_GRUPO_EMPRS == null)
            {
                res.Erro("O campo Patrocinadora é obrigatório");
            }

            if (newobj.NOM_PARAM == "EMAIL_AREA" && newobj.COD_ARQ_AREA == null)
            {
                res.Erro("O campo Área é obrigatório");
            }

            return res;

        }
    }
}
