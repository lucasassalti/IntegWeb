using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using IntegWeb.Saude.Aplicacao.DAL;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Saude;
using IntegWeb.Framework;

namespace IntegWeb.Saude.Aplicacao.BLL

{
    public class CidadesBLL : CidadesDAL
    {        
        public new Cidade Consultar(string cidade, string estado)
        {
            return base.Consultar(cidade, estado);
        }

        public new Cidade ConsultarPorCodigo(string cod_cidade, string estado_sigla)
        {
            return base.ConsultarPorCodigo(cod_cidade, estado_sigla);
        }

        public new Cidade ConsultarPorIBGE(int COD_MUNICI_IBGE)
        {
            return base.ConsultarPorIBGE(COD_MUNICI_IBGE);
        }

    }
}