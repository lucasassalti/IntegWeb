using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntegWeb.Entidades;
using System.Data;
using IntegWeb.Saude.Aplicacao.ENTITY;

namespace IntegWeb.Saude.Aplicacao.BLL
{
   public class CadAnaliseSuSBLL : CadAnaliseSusDAL
   {
        public Resultado GravaDados(DataTable dt)
        {
            return gravaTabela(dt);
        }

        public new List<object> ConsultarABI()
        {

            return new CadAnaliseSusDAL().ConsultarABI();                
       
        }

       public new List<object> ConsultarAIHPorUsuario(string paramCodigoUsuario)
       {
            return base.ConsultarAIHPorUsuario(paramCodigoUsuario);
       }

        public List<SAU_TBL_IMPUGNACAOSUS> GetImpugSus(int startRowIndex, int maximumRows, string paramCodigoUsuario, string paramBuscaAIHAPAC, string paramCompetencia, string sortParameter)
        {
            return base.GetData(startRowIndex, maximumRows, paramCodigoUsuario, paramBuscaAIHAPAC, paramCompetencia, sortParameter);
        }

        public int SelectCount(string paramCodigoUsuario, string paramBuscaAIHAPAC, string paramCompetencia)
        {
            return base.GetDataCount(paramCodigoUsuario, paramBuscaAIHAPAC, paramCompetencia);
        }

        public Resultado UpdateData(string CCUSTO_DEB_UTIL, string CCUSTO_CRE_UTIL, string CCUSTO_DEB_GLOSA, string CCUSTO_CRE_GLOSA, string AUX_DEB_UTIL, string AUX_CRE_UTIL, string AUX_DEB_GLOSA, string AUX_CRE_GLOSA, decimal NUM_ORGAO, string COD_PLANO)
        {
            return base.SaveData(CCUSTO_DEB_UTIL, CCUSTO_CRE_UTIL, CCUSTO_DEB_GLOSA, CCUSTO_CRE_GLOSA, AUX_DEB_UTIL, AUX_CRE_UTIL, AUX_DEB_GLOSA, AUX_CRE_GLOSA, NUM_ORGAO, COD_PLANO);
        }



       

    }


    

}
