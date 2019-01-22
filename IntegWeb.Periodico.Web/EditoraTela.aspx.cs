using IntegWeb.Entidades;
using IntegWeb.Periodico.Aplicacao;
using IntegWeb.Saude.Aplicacao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Periodico.Web
{
    public partial class EditoraTela : BasePage
    {
        #region Atributos
        Editora objM = new Editora();
        EditoraBLL objB = new EditoraBLL();
        #endregion

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                CarregaTela();
                if (Session["id"] != null)
                    Session.Remove("id");
         
            }
          
        }

        protected void btncEP_Click(object sender, EventArgs e)
        {
            string mensagem = "";
            if (!txtCep.Text.Equals(""))
            {
                DataTable dt = new CepBLL().ValidaCep(Regex.Replace(txtCep.Text, "[^.0-9]", ""), out  mensagem);

                if (dt.Rows.Count > 0)
                {
                    txtcidade.Text = dt.Rows[0]["DES_MUNICI"].ToString();
                    txtEstado.Text = dt.Rows[0]["COD_ESTADO"].ToString();
                    txtRua.Text = dt.Rows[0]["NOM_LOGRADOURO"].ToString();
                    txtBairro.Text = dt.Rows[0]["DES_BAIRRO"].ToString();
                }
                else
                {
                    txtcidade.Text = "";
                    txtEstado.Text = "";
                    txtRua.Text = "";
                    txtBairro.Text = "";
                    MostraMensagemTelaUpdatePanel(upEditora, mensagem);

                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upEditora, "Digite o cep!");
            }
                DivAcao(divAction, divSelect);
           
                divPeriodico.Visible = Session["id"] !=null;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            string mensagem = "";
            bool isUpdate = false;
            int id = 0;
            objM.bairro_editora = txtBairro.Text;
            objM.cidade_editora = txtcidade.Text;
            objM.uf_editora = txtEstado.Text;
            objM.complemento_editora = txtComplemento.Text;
            objM.cep_editora = Regex.Replace(txtCep.Text, "[^.0-9]", "");
            objM.cgc_cpf_editora = txtCpfCnpj.Text;
            objM.endereco_editora = txtRua.Text;
            objM.nome_editora = txtNome.Text;
            objM.numero_editora = txtNumero.Text;

            objM.fone_editora = txtFone.Text;
            objM.email_editora = txtEmail.Text;
            objM.fax_editora = txtFax.Text;
            objM.site_editora = txtSite.Text;
            objM.contato = txtContato.Text;

            if (Session["id"] != null)
            {
                objM.id_editora = int.Parse(Session["id"].ToString());
                isUpdate = true;
            }
            if (objB.ValidaCampos(out mensagem,  objM, isUpdate, out id))
            {
                MostraMensagemTelaUpdatePanel(upEditora, mensagem);
                if (id>0)
                {
                    Session["id"] = id;
                    BindGrid();
                }
               
                DivAcao(divAction, divSelect);
                divPeriodico.Visible = true;

            }
            else
            {
                MostraMensagemTelaUpdatePanel(upEditora, mensagem);
                DivAcao(divAction, divSelect);
                divPeriodico.Visible = Session["id"]!=null;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(divSelect, divAction);
            divPeriodico.Visible = false;
            if(Session["id"]!=null)
            Session.Remove("id");
            CarregaTela();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool isErro = ValidaCamposTela(txtEditora, drpEditora, out msg);


            if (isErro)
            {
                MostraMensagemTelaUpdatePanel(upEditora, msg.ToString());
            }
            else
            {
                if (drpEditora.SelectedValue == "1")
                    objM.cgc_cpf_editora = txtEditora.Text;
                else if (drpEditora.SelectedValue == "2")
                    objM.nome_editora = txtEditora.Text;

                DataTable dt = objB.ListaTodos(objM);

                CarregaGrid("gridEditora", dt, gridEditora);

            }
            DivAcao(divSelect, divAction);
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            DataTable dts = objB.ListaTodos(new Editora());
            CarregaGrid("gridEditora", dts, gridEditora);
        }

        protected void lnkInserirGrupo_Click(object sender, EventArgs e)
        {
            if (Session["id"] != null)
                Session.Remove("id");
            LimpaCampos();
            DivAcao(divAction, divSelect);
            divPeriodico.Visible = false;
        }

        protected void gridEditora_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Atualizar":

                    DataTable dt = objB.ListaTodos(new Editora() { id_editora = int.Parse(e.CommandArgument.ToString()) });

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        txtcidade.Text = row["CIDADE"].ToString();
                        txtEstado.Text = row["UF"].ToString();
                        txtRua.Text = row["RUA"].ToString();
                        txtBairro.Text = row["BAIRRO"].ToString();
                        txtComplemento.Text = row["COMPLEMENTO"].ToString();
                        txtNumero.Text = row["NUMERO"].ToString();
                        txtNome.Text = row["NOME"].ToString();
                        txtCpfCnpj.Text = row["CGC_CPF"].ToString();
                        txtCep.Text = row["CEP"].ToString();

                        txtFone.Text = row["FONE"].ToString();
                        txtFax.Text = row["FAX"].ToString();
                        txtContato.Text = row["CONTATO"].ToString();
                        txtSite.Text = row["SITE"].ToString();
                        txtEmail.Text = row["EMAIL"].ToString();

                        Session["id"] = e.CommandArgument.ToString();
                        DataTable dts = new PeriodicoBLL().ListaTodos(new PeriodicoObj() { id_editora = int.Parse(e.CommandArgument.ToString()) });
                        CarregaGrid("grdPeriodico", dts, grdPeriodico);
                        DivAcao(divAction, divSelect);
                        divPeriodico.Visible = true;
                    }
                    break;

                case "Deletar":

                    string mensagem = "";
                    bool ret = objB.Deletar(new Editora() { id_editora = int.Parse(e.CommandArgument.ToString()) }, out mensagem);
                    MostraMensagemTelaUpdatePanel(upEditora, mensagem);

                    if (ret)
                    {
                        DataTable dts = objB.ListaTodos(new Editora());
                        CarregaGrid("gridEditora", dts, gridEditora);

                    }
                    break;
                default:
                    break;
            }
        }

        protected void gridEditora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["gridEditora"] != null)
            {
                gridEditora.PageIndex = e.NewPageIndex;
                gridEditora.DataSource = ViewState["gridEditora"];
                gridEditora.DataBind();
            }
        }

        protected void grdPeriodico_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (Session["id"] != null)
            {

                int index = e.RowIndex;
                int id = int.Parse(grdPeriodico.DataKeys[e.RowIndex].Value.ToString());
                string mensagem = "";
                TextBox txtP = grdPeriodico.Rows[index].FindControl("txtPer") as TextBox;
                TextBox txtC = grdPeriodico.Rows[index].FindControl("txtCodigo") as TextBox;

                PeriodicoBLL bll = new PeriodicoBLL();

                bool ret = bll.ValidaCampos(out mensagem, new PeriodicoObj()
                {
                    id_editora = int.Parse(Session["id"].ToString()),
                    id_periodico = id,
                    codigo= txtC.Text,
                    nome_periodico = txtP.Text
                },
                                                                   true);
                MostraMensagemTelaUpdatePanel(upEditora, mensagem);
                if (ret)
                {
                    grdPeriodico.EditIndex = -1;
                    BindGrid();
                }
                else
                    MostraMensagemTelaUpdatePanel(upEditora, "Problemas contate o administrador do sistema!");
            }
        }

        protected void grdPeriodico_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdPeriodico"] != null)
            {
                grdPeriodico.PageIndex = e.NewPageIndex;
                grdPeriodico.DataSource = ViewState["grdPeriodico"];
                grdPeriodico.DataBind();
            }
        }

        protected void btnInserirPeriodico_Click(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                string mensagem = "";
                PeriodicoObj objP = new PeriodicoObj();
                PeriodicoBLL objB = new PeriodicoBLL();

                objP.id_editora = int.Parse(Session["id"].ToString());
                objP.nome_periodico = txtPeriodico.Text;
                objP.codigo = txtCod.Text;

                if (objB.ValidaCampos(out mensagem, objP, false))
                {
                    objP = new PeriodicoObj();
                    objP.id_editora = int.Parse(Session["id"].ToString());
                    CarregaGrid("grdPeriodico", objB.ListaTodos(objP), grdPeriodico);
                }
                DivAcao(divAction, divSelect);
                divPeriodico.Visible = true;
                txtCod.Text = "";
                txtPeriodico.Text = "";
                MostraMensagemTelaUpdatePanel(upEditora, mensagem);
            }
        }

        protected void grdPeriodico_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdPeriodico.EditIndex = -1;
            BindGrid();
        }

        protected void grdPeriodico_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            int id = int.Parse(grdPeriodico.DataKeys[e.RowIndex].Value.ToString());

            string mensagem = "";
            bool ret = new PeriodicoBLL().Deletar(out mensagem, new PeriodicoObj() { id_periodico = id });
            MostraMensagemTelaUpdatePanel(upEditora, mensagem);
            DivAcao(divAction, divSelect);
            BindGrid();
 

        }

        protected void grdPeriodico_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdPeriodico.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        #endregion

        #region Métodos
        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {

            divExibir.Visible = true;
            divOcultar.Visible = false;

        }

        private void CarregaTela()
        {

            DivAcao(divSelect, divAction);
            DataTable dts = objB.ListaTodos(new Editora());
            CarregaGrid("gridEditora", dts, gridEditora);
            divPeriodico.Visible = false;
        }

        private void LimpaCampos()
        {

            txtBairro.Text = txtEmail.Text = txtcidade.Text = txtNumero.Text = txtNome.Text = txtEstado.Text = txtComplemento.Text = txtCep.Text = txtCpfCnpj.Text = txtRua.Text = txtFone.Text = txtFax.Text = txtContato.Text = txtSite.Text = "";

        }

        private bool ValidaCamposTela(TextBox text, DropDownList drp, out string mensagem)
        {

            StringBuilder msg = new StringBuilder();
            msg.Append("ERRO!\\n");
            bool isErro = false;
            if (drp.SelectedIndex < 1)
            {
                msg.Append("1 Selecione uma opção de busca.\\n");
                isErro = true;
            }

            if (text.Text == "")
            {
                msg.Append("2 Digite no campo de busca! ");
                isErro = true;
            }
            mensagem = msg.ToString();
            return isErro;
        }

        private void BindGrid() {

            if (Session["id"]!=null)
            {
                DataTable dts = new PeriodicoBLL().ListaTodos(new PeriodicoObj() { id_editora = int.Parse(Session["id"].ToString()) });
                CarregaGrid("grdPeriodico", dts, grdPeriodico);
                DivAcao(divAction, divSelect);
                divPeriodico.Visible = true;
            }


        }
        #endregion

    }
}