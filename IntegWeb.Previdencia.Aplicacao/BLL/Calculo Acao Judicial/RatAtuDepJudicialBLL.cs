using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Previdencia.Aplicacao.DAL.Calculo_Acao_Judicial;
using System.Data;
using System.Linq;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Framework;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial
{
    public class RatAtuDepJudicialBLL : RatAtuDepJudicialDAL
    {
        public DataTable ExportaRelatorio(DateTime data)
        {
            DataTable dt = new DataTable();
            List<PRE_TBL_JUR_DEP_JUDICIAL> list = new List<PRE_TBL_JUR_DEP_JUDICIAL>();
            list = exportaRel(data).ToList();

            if(list != null)
            {
                dt = list.ToDataTable();
            }

            return dt;
        }


        //public string InserirRateio(PRE_TBL_JUR_DEP_JUDICIAL obj)
        //{
        //    PRE_TBL_DEPOSITO_JUDIC_view View = GetValorRateio(obj.NRO_PASTA.ToString() , Convert.ToDateTime(obj.DAT_PAGAMENTO.ToString()));

        //    string pastaData = "";

        //    if (View != null)
        //    {
        //        decimal? percentualBSPS = View.VALOR_BSPS / obj.VLR_ORI;
        //        decimal? percentualBD = View.VALOR_BD / obj.VLR_ORI;
        //        decimal? percentualCV = View.VALOR_CV / obj.VLR_ORI;

        //        obj.PLANO = View.PLANO;
        //        obj.VLR_BSPS =  obj.VLR_ATUALIZADO * percentualBSPS ;
        //        obj.VLR_BD = obj.VLR_ATUALIZADO * percentualBD ;
        //        obj.VLR_CV = obj.VLR_ATUALIZADO * percentualCV ;

        //        //obj.VLR_BSPS = View.VALOR_BSPS;
        //        //obj.VLR_BD = View.VALOR_BD;
        //        //obj.VLR_CV = View.VALOR_CV;
        //    }
        //    else 
        //    {
        //        pastaData = "Pasta: " + obj.NRO_PASTA + " com Data de Pagamento: " + Convert.ToDateTime(obj.DAT_PAGAMENTO.ToString()).ToShortDateString() + " não encontrada! <br>";
        //    }
            

        //    Inserir(obj);

        //    return pastaData;
        //}

        
    }
}
