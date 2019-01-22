using IntegWeb.Entidades;
using IntegWeb.Intranet.Aplicacao.DAL;
using IntegWeb.Intranet.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Intranet.Aplicacao
{
   public class ExtratoSaudeCorreioBLL : ExtratoSaudeCorreioDAL
    {
       public List<FCESP_EXT_AMH_EXCECAO> GetData(short codEmp, Int64 numRgtroEmp)
       {
           return base.GetData(codEmp, numRgtroEmp);
       }

       public void SaveLog(FCESP_EXT_AMH_EXCECAO_LOG parametroLog)
       {
            base.SaveLog(parametroLog);       
       }

       public Resultado DeleteData(short codEmp, Int64 numRgtroEmp)
       {
           return base.DeleteData(codEmp, numRgtroEmp);       
       }

   
    }
}
