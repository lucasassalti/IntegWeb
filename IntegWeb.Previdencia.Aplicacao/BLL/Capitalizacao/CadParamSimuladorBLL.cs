using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.DAL.Capitalizacao;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao
{
    public class CadParamSimuladorBLL : CadParamSimuladorDAL
    {
        public Resultado InserirDataReferencia(PRE_TBL_POR_UREF objUref)
        {
            Resultado res = new Resultado();
            CadParamSimuladorBLL bll = new CadParamSimuladorBLL();
            List<PRE_TBL_POR_UREF> lista = bll.GetAllData().ToList();


            foreach (PRE_TBL_POR_UREF obj in lista)
            {
                //Novo objeto que será inserido
                PRE_TBL_POR_UREF objInserir = new PRE_TBL_POR_UREF();

                //Atributos do objeto da lista são tribuidos para o objeto a ser inserido
                objInserir.EMPRESA = obj.EMPRESA;
                objInserir.PLANO = obj.PLANO;
                objInserir.TB_ATUARIAL = obj.TB_ATUARIAL;
                objInserir.SEXO = obj.SEXO;
                objInserir.CODIGO_UM = obj.CODIGO_UM;
                objInserir.DESCRICAO_UM = obj.DESCRICAO_UM;
                objInserir.VALOR = obj.VALOR;
                objInserir.VALOR_MEDIO = obj.VALOR_MEDIO;
                objInserir.TETO_INSS = obj.TETO_INSS;
                objInserir.PERC_MINIMO = obj.PERC_MINIMO;
                objInserir.PERC_INV = obj.PERC_INV;
                objInserir.LIM_PERC = obj.LIM_PERC;
                objInserir.DT_REFERENCIA = obj.DT_REFERENCIA;
                objInserir.UQP = obj.UQP;
                objInserir.JUROSA = obj.JUROSA;
                objInserir.JUROSPADRAP = obj.JUROSPADRAP;
                objInserir.JUROSMAX = obj.JUROSMAX;

                                 
                /* atributos do objeto(parametro) que possui a data de referencia, a data de inclusão e o log de inclusão,
                são atribuidos ao objeto a ser inserido*/
                objInserir.DT_REFERENCIA = objUref.DT_REFERENCIA;
                objInserir.DTH_INCLUSAO = objUref.DTH_INCLUSAO;
                objInserir.LOG_INCLUSAO = objUref.LOG_INCLUSAO;              

                res = bll.InserirUref(objInserir);
            }
            return res;
        }

        public decimal CalculaValorMedio(short? codUm, DateTime DataReferencia)
        {
            decimal ret = 0;
            List<COTACAO_MES_UM> indice_cor = base.GetHistUnidadeMonetaria(codUm, DataReferencia);
            List<COTACAO_MES_UM> igpdi = base.GetHistUnidadeMonetaria(3, DataReferencia);

            for (int i = 0; i < igpdi.Count(); i++)
            {
                decimal indice = (igpdi[i].VLR_CMESUM ?? 0);
                if (i>0)
                {
                    indice = (igpdi[i].VLR_CMESUM ?? 0) * (igpdi[i-1].VLR_CMESUM ?? 0);
                    igpdi[i].VLR_CMESUM = indice;
                }
                if (indice > 0 && indice < 1)
                {
                    indice = 1;
                }

                if (i < indice_cor.Count())
                {
                    indice_cor[i].VLR_CMESUM = (indice_cor[i].VLR_CMESUM ?? 0) * indice;
                }
                //System.Diagnostics.Debug.WriteLine(Math.Round(indice_cor[i].VLR_CMESUM, 2));
            }
            if (indice_cor.Count() > 0)
            {
                ret = indice_cor.Average(m => m.VLR_CMESUM ?? 0);
            }
            return ret;
        }

    }
}
