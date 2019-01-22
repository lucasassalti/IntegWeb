using IntegWeb.Saude.Aplicacao.DAL.Financeiro;
using IntegWeb.Entidades.Saude.Financeiro;
using IntegWeb.Entidades.Framework;
using IntegWeb.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace IntegWeb.Saude.Aplicacao.BLL.Financeiro
{
    public class HistoricoBoletoBLL
    {
        public List<HistProcessaBoleto> ConsultarHistorico( )
        {
            return new HistoricoBoletoDAL().ConsultarProcessamento();
        }

        public DataTable ListaCobrancas()
        {
            return new HistoricoBoletoDAL().ListaCobrancas();
        }

        public DataTable ListaFlagAtivo()
        {
            return new HistoricoBoletoDAL().ListaFlagAtivo();
        }

        public DataTable ListaInadimplentes()
        {
            return new HistoricoBoletoDAL().ListaInadimplentes();
        }

        public DataTable ListaEnderecoNulo()
        {
            return new HistoricoBoletoDAL().ListaEnderecoNulo();
        }
    }
}
