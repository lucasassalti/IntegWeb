using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegWeb.Entidades.Saude.ExigenciasLegais.MonitoramentoTISS;
using IntegWeb.Saude.Aplicacao.DAL.ExigenciasLegais;
using IntegWeb.Entidades;
using System.Xml.Linq;

namespace IntegWeb.Saude.Aplicacao.BLL.ExigenciasLegais
{
    public class RetornoMonitoramentoResumoProcessamentoBLL
    {
        #region Métodos
        public Resultado Inserir(DataTable dt)
        {
            return new RetornoMonitoramentoResumoProcessamentoDAL().Inserir(dt);
        }
        #endregion
    }
}
