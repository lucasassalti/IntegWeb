using IntegWeb.Entidades;
using IntegWeb.Saude.Aplicacao.DAL.ExigenciasLegais;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IntegWeb.Saude.Aplicacao.BLL.ExigenciasLegais
{
    public class RetornoMonitoramentoIdentificacaoTransacaoBLL
    {
        #region Métodos
        public Resultado Inserir(DataTable dt)
        {
            //return new RetornoMonitoramentoIdentificacaoTransacaoDAL().Inserir(dt);
            return new RetornoMonitoramentoIdentificacaoTransacaoDAL().InserirViaProcedure(dt);
        }
        #endregion
    }
}
