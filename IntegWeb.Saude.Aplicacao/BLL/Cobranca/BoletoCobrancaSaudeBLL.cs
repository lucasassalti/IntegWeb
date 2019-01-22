using IntegWeb.Saude.Aplicacao.DAL.Cobranca;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace IntegWeb.Saude.Aplicacao.BLL.Cobranca
{
     public class BoletoCobrancaSaudeBLL : BoletoCobrancaSaudeDAL
    {

         public void ProcessarCobrancaSaude(DateTime dtVenc, DateTime dtTolerancia, decimal numLote, out decimal numLoteOut) 
        {
            base.ProcessarCobrancaSaude(dtVenc, dtTolerancia, numLote, out numLoteOut);
        }

        public void ProcessarFlagInsucesso(decimal numLote) 
        {
            base.ProcessarFlagInsucesso(numLote);
        }

        public void ProcessarInadimplentes(decimal tipoRotina, DateTime dtVenc, decimal numLote) 
        {
            base.ProcessarInadimplentes(tipoRotina, dtVenc, numLote);
        }

        public void ProcessarAvisoDeCancelamento(DateTime dtVenc, decimal numLote) 
        {
            base.ProcessarAvisoDeCancelamento(dtVenc, numLote);
        }

        public DataTable ProcessarRelatorios(decimal tipoRel, DateTime dtVenc, decimal numLote) 
        {
            return base.ProcessarRelatorios(tipoRel, dtVenc, numLote);
        }

        public void ProcessarArquivosTxts(decimal tipoTxt, decimal numLote, out string nomeArquivo)
         {
             base.ProcessarArquivosTxts(tipoTxt, numLote, out nomeArquivo);
         }

        public void ProcessarExtraJudicial(decimal codemprs, decimal matricula, decimal numRepres, decimal numlote, DateTime dtVenc, DateTime dtVencAnt, out decimal contador) 
        {
            base.ProcessarExtraJudicial(codemprs.ToString(),  matricula.ToString(), numRepres,  numlote, dtVenc, dtVencAnt, out contador) ;
        
        }
    }
}
