using System.Data;
using System.Linq;
using System.Collections.Generic;
using IntegWeb.Framework;
using IntegWeb.Entidades.Saude.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;

namespace IntegWeb.Saude.Aplicacao.BLL.Processos
{
    public class ConvenenteBLL : ConvenenteDAL
    {
        public new TB_CONVENENTE Consultar(decimal COD_CONVENENTE)
        {
            return base.Consultar(COD_CONVENENTE);
        }
    }
}
