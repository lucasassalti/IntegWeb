using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao;

namespace IntegWeb.Saude.Web.Financeiro
{
    public partial class CadastroFlagTela : BasePage
    {

        CadastroFlag ObjFlag = new CadastroFlag();

        private void CarregaGrid(string nameView, List<Object> list, GridView grid)
        {

            ViewState[nameView] = list;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }
        
        private void ApresentarMensagem(string mensagem)
        {
            ClientScript.RegisterStartupScript(
                Type.GetType("System.String"),
                "mensagem",
                "alert('" + mensagem.Replace("\r", "").Replace("\n", "")
                                       .Replace("'", "") + "')",
                true);
        }
        
        private void CarregarLista()
        {
            grvListaFlag.DataSource = new CadastroFlagBLL().Listar();
            grvListaFlag.DataBind();
        }

              
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // CarregarLista();

                mvwCadastroFlag.ActiveViewIndex = 0; //vwLista
            }
        }


        protected void grvListaFlag_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CadastroFlag objItem = (CadastroFlag)e.Row.DataItem;

                e.Row.Cells[4].Text = Util.FormatarData(objItem.Dt_inclusao);
                
            }
        }

        protected void grvListaFlag_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int empresa;
            int registro;
            int repres;

            if (e.CommandName == "EDITAR")
            {
               
            }
            else if (e.CommandName == "EXCLUIR")
            {

                empresa = Convert.ToByte(e.CommandArgument);
                registro = Convert.ToByte(e.CommandArgument);
                repres = Convert.ToByte(e.CommandArgument);



                CadastroFlagBLL bll = new CadastroFlagBLL();
                               
                
            }
        }

        //protected void btnConsultarFlag_Click(object sender, CommandEventArgs e)
        //{

        //    int COD_EMPRS = Convert.ToInt32(e.CommandArgument);


        //    CadastroFlagBLL bll = new CadastroFlagBLL();
        //    CadastroFlag objConsulta = bll.Consultar();

        //}

        protected void btnConsultarFlag_Click(object sender, EventArgs e)
        {
            try
            {
                // bool isValid = ValidaCamposTela(txtUsuario, drpUsuario, out msg);

                ObjFlag.Cod_emprs = Convert.ToInt32(txtCodEmprs.Text);
                ObjFlag.Num_rgtro_emprg = Convert.ToInt32(txtNumRgtroEmprg.Text);
                ObjFlag.Num_idntf_rptant = Convert.ToInt32(txtNumRgtroRptant.Text);

                CarregaGrid("grvListaFlag", new CadastroFlagBLL().ConsultarFlag(ObjFlag).ToList<Object>(), grvListaFlag);
            }
            catch (Exception ex)
            {
                MostraMensagemTela(Page,"Problemas Na Validação dos Campos... //n" + ex.Message);
            }
           
             //objB.MostraMensagemTelaUpdatePanel(upUsuarioAd, msg);
        }

              

    }
}