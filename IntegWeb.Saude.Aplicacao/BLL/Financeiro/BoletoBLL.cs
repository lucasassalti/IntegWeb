using IntegWeb.Saude.Aplicacao.DAL.Financeiro;
using IntegWeb.Entidades.Saude.Financeiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Financeiro
{
    public class BoletoBLL
    {
        public int ProcessaBoleto(Boleto dataVencimento) 
        {
            return new BoletoDAL().ProcessaBoleto(dataVencimento);                            
        }
    }
}
