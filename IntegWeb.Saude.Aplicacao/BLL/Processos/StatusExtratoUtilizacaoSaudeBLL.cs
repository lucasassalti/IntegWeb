using IntegWeb.Entidades;
using IntegWeb.Saude.Aplicacao.DAL.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Processos
{
   public class StatusExtratoUtilizacaoSaudeBLL : StatusExtratoUtilizacaoSaudeDAL
    {
        public List<SAU_TBL_EXT_UTIL_DADGER_VIEW> GetExtrato(short codEmpresa, int matricula, int numRepresen, DateTime dataMotiv) 
        {
            return base.GetExtrato(codEmpresa,matricula,numRepresen,dataMotiv);
        }

        public Resultado UpdateData(SAU_TBL_EXT_UTIL_DADGER obj, string inibir) 
        {
            return base.UpdateData(obj,inibir);
        }

        public string GetResultInibir(short codEmpresa, int matricula, int numRepresen, DateTime dataMotiv)
        {
            return base.GetResultInibir(codEmpresa,matricula,numRepresen,dataMotiv);

        }

        public Resultado InsertLog(SAU_TBL_EXT_UTIL_DADGER_LOG obj) 
        {
            return base.InsertLog(obj);
        }
    }
}
