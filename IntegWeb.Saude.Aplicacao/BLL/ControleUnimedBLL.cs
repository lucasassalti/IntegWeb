using IntegWeb.Saude.Aplicacao.DAL;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Framework;
using IntegWeb.Entidades;

namespace IntegWeb.Saude.Aplicacao.BLL
{
    public class ControleUnimedBLL : ControleUnimedDAL
    {

        public void Validar(DataTable dt)
        { 
        
        }
        
        public DataTable ListarDadosParaExcel(short? emp, int? matricula, string sub, string nome, string codIdentificacao, string codUnimed, string tipMovimentacao, DateTime? datMovimentacaoIni, DateTime? datMovimentacaoFim, DateTime? datSaidaIni, DateTime? datSaidaFim)
        {
            DataTable dt = new DataTable();
            List<CAD_TBL_CONTROLEUNIMED_view> list = new List<CAD_TBL_CONTROLEUNIMED_view>();
            list = GetDataExportar(emp, matricula, sub, nome, codIdentificacao, codUnimed, tipMovimentacao, datMovimentacaoIni, datMovimentacaoFim, datSaidaIni, datSaidaFim).ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }
    
    }
}
