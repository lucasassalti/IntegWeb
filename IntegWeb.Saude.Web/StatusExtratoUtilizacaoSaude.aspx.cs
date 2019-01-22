using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.BLL.Processos;
using IntegWeb.Saude.Aplicacao.DAL.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Framework;
using IntegWeb.Entidades;

namespace IntegWeb.Saude.Web
{
    public partial class StatusExtratoUtilizacaoSaude : BasePage
    {
        #region .: Propriedades :.
        #endregion

        #region .: Eventos :.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gridExtrato.Visible = false;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

            StatusExtratoUtilizacaoSaudeBLL bll = new StatusExtratoUtilizacaoSaudeBLL();

            gridExtrato.DataBind();
            gridExtrato.Visible = true;


        }

        protected void gridExtrato_RowEditing(object sender, GridViewEditEventArgs e)
        {
            StatusExtratoUtilizacaoSaudeBLL bll = new StatusExtratoUtilizacaoSaudeBLL();
            gridExtrato.EditIndex = e.NewEditIndex;
            gridExtrato.DataBind();

                  
        }

        protected void gridExtrato_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "UpdateAb")
            {
                StatusExtratoUtilizacaoSaudeBLL bll = new StatusExtratoUtilizacaoSaudeBLL();
                SAU_TBL_EXT_UTIL_DADGER obj = new SAU_TBL_EXT_UTIL_DADGER();

                DropDownList ddlInibi = ((DropDownList)gridExtrato.Rows[0].FindControl("ddlInibicao"));

                string inibeAtual = ddlInibi.SelectedValue;

                //update na tabela
                obj.NOM_RESP = gridExtrato.Rows[0].Cells[0].Text;
                obj.COD_EMPRS = Convert.ToInt16(gridExtrato.Rows[0].Cells[1].Text);
                obj.NUM_RGTRO_EMPRG = Convert.ToInt32(gridExtrato.Rows[0].Cells[2].Text);
                obj.DAT_REF = gridExtrato.Rows[0].Cells[3].Text;
                obj.DAT_MOVIMENTO = Convert.ToDateTime(gridExtrato.Rows[0].Cells[4].Text);
                obj.NUM_IDNTF_RPTANT = Convert.ToInt32(gridExtrato.Rows[0].Cells[5].Text);
                obj.IDC_INIBE_EXT = inibeAtual;

                bll.UpdateData(obj, inibeAtual);


                //log do update
                SAU_TBL_EXT_UTIL_DADGER_LOG log = new SAU_TBL_EXT_UTIL_DADGER_LOG();
                DateTime dhNow = DateTime.Now;
                var user = (ConectaAD)Session["objUser"];
                string inibeAntigo = ((Label)gridExtrato.Rows[0].FindControl("lblInibir2")).Text;

                log.DAT_REF = gridExtrato.Rows[0].Cells[3].Text;
                log.COD_EMPRS = Convert.ToInt16(gridExtrato.Rows[0].Cells[1].Text);
                log.NUM_RGTRO_EMPRG = Convert.ToInt32(gridExtrato.Rows[0].Cells[2].Text);
                log.DAT_MOVIMENTO = Convert.ToDateTime(gridExtrato.Rows[0].Cells[4].Text);
                log.NUM_IDNTF_RPTANT = Convert.ToInt32(gridExtrato.Rows[0].Cells[5].Text);
                log.HDRDATHOR = dhNow;
                log.HDRCODUSU = user.login;
                log.IDC_INIBE_EXT_ANTIGO = inibeAntigo;
                log.IDC_INIBE_EXT_ATUAL = inibeAtual;

                Resultado res = bll.InsertLog(log);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Status Alterado com Sucesso");
                    gridExtrato.EditIndex = -1;
                    gridExtrato.DataBind();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro ao tentar alterar o status.\\nErro:" + res.Mensagem);
                }

            }
            
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtEmpresa.Text = "";
            txtMatricula.Text = "";
            txtRepresentante.Text = "";
            txtDataMovimentacao.Text = "";
            gridExtrato.Visible = false;


        }


        #endregion

        #region .: Métodos :.
        #endregion
    }
}