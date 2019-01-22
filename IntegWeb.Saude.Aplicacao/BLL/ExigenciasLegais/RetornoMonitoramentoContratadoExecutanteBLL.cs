using IntegWeb.Entidades;
using IntegWeb.Saude.Aplicacao.DAL.ExigenciasLegais;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IntegWeb.Saude.Aplicacao.BLL.ExigenciasLegais
{
    public class RetornoMonitoramentoContratadoExecutanteBLL
    {
        #region Métodos
        public Resultado Inserir(DataTable dt)
        {
            return new RetornoMonitoramentoContratadoExecutanteDAL().InserirViaProcedure(dt);
        }
        #endregion
    }
}
