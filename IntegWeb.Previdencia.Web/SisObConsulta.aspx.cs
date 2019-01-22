using IntegWeb.Framework;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Previdencia.Aplicacao.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class SisObConsulta : BasePage
    {
        #region Atributos
        ArquivoSisObBLL objbll = new ArquivoSisObBLL();
        ArquivoSisOb obj = new ArquivoSisOb();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaTela();
        }
        protected void btnFind_Click(object sender, EventArgs e)
        {
            grdOb.Visible = true;
 
            string msg = "";
            bool isErro = ValidaCamposTela(out msg);

            if (isErro)
            {
                MostraMensagemTelaUpdatePanel(upSys, msg.ToString());
            }
            else
            {
                obj.nomefalecido = !String.IsNullOrEmpty(txtNomeFalecido.Text) ? txtNomeFalecido.Text.Trim() : null;
                obj.numcpf = !String.IsNullOrEmpty(txtCpf.Text) ? txtCpf.Text.Trim() : null;
                obj.nomemae = !String.IsNullOrEmpty(txtNomeMae.Text) ? txtNomeMae.Text.Trim() : null;
                if (Util.String2Date(txtDtNasc.Text) != null)
                {
                    obj.dtnascimento = DateTime.Parse(txtDtNasc.Text).ToString("yyyyMMdd");
                }
                CarregaGrid("grdOb", objbll.ListaSysOb(obj), grdOb);

            }
        }
 
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!txtArquivo.Text.Equals(""))
            {
                CarregaGrid("grdArquivo", objbll.ListaTodos(new ArquivoSisOb() { mesanoref = txtArquivo.Text.Trim() }), grdArquivo);
            }
            else
            {

                MostraMensagemTelaUpdatePanel(upSys, "Digite o mês/ano referência!");

            }
        }
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtArquivo.Text = "";
            obj.nomefalecido = "";
            obj.numcpf = "";
            obj.nomemae = "";
            obj.dtnascimento = "";
            grdOb.Visible = false;
            CarregaGrid("grdArquivo", objbll.ListaTodos(new ArquivoSisOb()), grdArquivo);
        }

        protected void grdArquivo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdArquivo"] != null)
            {
                grdArquivo.PageIndex = e.NewPageIndex;
                grdArquivo.DataSource = ViewState["grdArquivo"];
                grdArquivo.DataBind();
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            txtArquivo.Text = "";
        }
        protected void grdOb_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdOb"] != null)
            {
                grdOb.PageIndex = e.NewPageIndex;
                grdOb.DataSource = ViewState["grdOb"];
                grdOb.DataBind();
            }
        }


#endregion

        #region  Métodos
        private void CarregaTela()
        {

            CarregaGrid("grdArquivo", objbll.ListaTodos(obj), grdArquivo);

        }
        private bool ValidaCamposTela(out string mensagem)
        {

            StringBuilder msg = new StringBuilder();
            msg.Append("ERRO!\\n");
            bool isErro = false;

            if (String.IsNullOrEmpty(txtNomeFalecido.Text) &&
                String.IsNullOrEmpty(txtCpf.Text) &&
                String.IsNullOrEmpty(txtNomeMae.Text) &&
                String.IsNullOrEmpty(txtDtNasc.Text)){
                msg.Append("Entre com pelo menos um campo para a pesquisa! ");
                isErro = true;
            }
            mensagem = msg.ToString();
            return isErro;
        }
        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }
        #endregion
    }
}