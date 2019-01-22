using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; 
using Intranet.Aplicacao.DAL;
using IntegWeb.Entidades;
using IntegWeb.Framework;

namespace Intranet.Aplicacao

{
    public class CidadesBLL : CidadesDAL
    {        
        public new Cidade Consultar(string cidade, string estado)
        {
            return base.Consultar(cidade, estado);
        }

    }
}