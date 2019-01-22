using IntegWeb.Entidades;
using IntegWeb.Financeira.Aplicacao.BLL.Tesouraria;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Financeira.Web
{
    public partial class TravaDataCorte : BasePage
    {
        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CarregaParametro();
            }
        }

        protected void btnHabilitarTrava_Click(object sender, EventArgs e)
        {
            TravaDataCorteBLL bll = new TravaDataCorteBLL();
            Resultado res = new Resultado();
            string valParametro = bll.GetParameter().VALOR_PARAMETRO;

            // Alteração Status 
            if (valParametro == "0")
            {
                res = bll.Update("1");
                SalvarLog();
            }
            if (valParametro == "1")
            {
                res = bll.Update("0");
                SalvarLog();
            }


            if (res.Ok)
            {
                MostraMensagem(lblMsg, "Status Atualizado com Sucesso", "n_ok");
                CarregaParametro();
            }
            else
            {
                MostraMensagem(lblMsg, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
            }

        }

        #endregion

        #region .: Métodos :.

        private void CarregaParametro()
        {

            TravaDataCorteBLL bll = new TravaDataCorteBLL();
            string valParametro = bll.GetParameter().VALOR_PARAMETRO;

            if (valParametro == "0")
            {
                lblStatus.Text = "Desativado";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            if (valParametro == "1")
            {
                lblStatus.Text = "Ativado";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }

        }

        private void SalvarLog()
        {
            TravaDataCorteBLL bll = new TravaDataCorteBLL();
            FUN_TBL_PARAMETRO_LOG obj = new FUN_TBL_PARAMETRO_LOG();
            var parametro = bll.GetParameter();

            if (Session["objUser"] != null)
            {
                var user = (ConectaAD)Session["objUser"];
                obj.LOGIN = user.login;
            }

            obj.ID_PARAMETRO = parametro.ID_PARAMETRO;
            obj.COLUNA_ALTERACAO = parametro.GetType().GetProperty("VALOR_PARAMETRO").Name.ToString();
            obj.VALOR_ALTERACAO = parametro.VALOR_PARAMETRO;
            bll.SaveLog(obj);

        }

        #endregion
    }
}