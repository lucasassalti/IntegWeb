using IntegWeb.Saude.Aplicacao.DAL.Processos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Processos
{
    public class SaudeAnexoIIBLL : SaudeAnexoIIDAL
    {
        public decimal? CalculoValProposto(int codHosp, int? codServ, decimal vPorcProposto, decimal vPorcDescProposto)
        {   
            decimal? valorAtual;
            decimal porcProposto;
            decimal porcDescProposto;
            decimal? valAumentar;
            decimal? valDesconto;
            decimal? valTotalCalculado = 0;

            if (vPorcDescProposto == 0)
            {


                valorAtual = GetServHosp(codHosp, codServ).Select(v => v.VALOR).FirstOrDefault();
                porcProposto = vPorcProposto/100;
                valAumentar = valorAtual * porcProposto;
                valTotalCalculado = valorAtual + valAumentar;

            }
            else
            {
                valorAtual = GetServHosp(codHosp, codServ).Select(v => v.VALOR).FirstOrDefault();
                porcDescProposto = vPorcDescProposto/100;
                valDesconto = valorAtual * porcDescProposto;
                valTotalCalculado = valorAtual - valDesconto;
            
            }

            return Math.Round(valTotalCalculado ?? 0, 2);
                
        }
    }
}
