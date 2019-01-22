using IntegWeb.Entidades;
using IntegWeb.Entidades.Periodico;
using IntegWeb.Framework;
using IntegWeb.Periodico.Aplicacao;
using IntegWeb.Saude.Aplicacao;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Periodico.Web
{
    public partial class AssinaturaTela : BasePage
    {
        #region Atributos
        AssinaturaPeriodicoBLL objB = new AssinaturaPeriodicoBLL();
        Assinatura objM = new Assinatura();
        ArrayList arrayPagAcessos = new ArrayList();
        ArrayList arrayPagGrupos = new ArrayList();
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

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            if (Session["objUser"] != null)
            {
                var user = (ConectaAD)Session["objUser"];
                string mensagem = "";
                int id = 0;

                objM.cod_assinatura = txtCodigo.Text;
                objM.dt_inicio_assinat = txtDtInicio.Text.Equals("") ? DateTime.MinValue : DateTime.Parse(txtDtInicio.Text);
                objM.dt_pagto_assinat = txtDtPagamento.Text.Equals("") ? DateTime.MinValue : DateTime.Parse(txtDtPagamento.Text);
                objM.dt_vecto_assinat = txtDtVencimento.Text.Equals("") ? DateTime.MinValue : DateTime.Parse(txtDtVencimento.Text);
                objM.dt_vigencia = txtDtVigencia.Text.Equals("") ? DateTime.MinValue : DateTime.Parse(txtDtVigencia.Text);
                objM.id_periodico = int.Parse(drpPeriodico.SelectedValue.ToString());
                objM.id_periodo = int.Parse(drpPeriodo.SelectedValue.ToString());
                objM.dist_assinat = int.Parse(drpDistribuicao.SelectedValue.ToString());
                objM.matricula = user.login;
                objM.qtde_assinat = txtQuantidade.Text.Equals("") ? 0 : int.Parse(txtQuantidade.Text);
                objM.valor_assinat = txtValorAssinatura.Text.Equals("") ? 0 : decimal.Parse(txtValorAssinatura.Text);
                bool isUpdate = false;

                if (Session["id"] != null)
                {
                    objM.id_assinatura = int.Parse(Session["id"].ToString());
                    isUpdate = true;
                }
                if (objB.ValidaCampos(out mensagem, objM, isUpdate, out id))
                {
                    MostraMensagemTelaUpdatePanel(upAssinatura, mensagem);
                    hdVigencia.Value = txtDtVigencia.Text;
                    txtDtVigencia.Enabled = false;
                    txtValorAssinatura.Enabled = false;
                    CarregaPeriodico(objM.id_periodico);
                    DivAcao(divAction, divSelect);
                    divEditora.Visible = true;
                    btnAlt.Visible = true;
                    lnkVEditora.Visible = true;
                    btnCanc.Visible = false;
                    btnSalv.Visible = false;
                    if (id > 0)
                    {
                   
                 DataTable dts = objB.ListaValores(new Assinatura() { id_assinatura = id });
                 CarregaGrid("grdValores", dts, grdValores);

                        Session["id"] = id;
                        CarregaArea(id);
                    }

                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upAssinatura, mensagem);
                    DivAcao(divAction, divSelect);
                    divEditora.Visible = Session["id"] != null;
                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(divSelect, divAction);
            divEditora.Visible = false;
            DataTable dts = objB.ListaTodos(new Assinatura());
            CarregaGrid("gridAssinatura", dts, gridAssinatura);



            if (Session["id"] != null)
                Session.Remove("id");
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool isErro = ValidaCamposTela(txtAssinatura, drpAssinatura, out msg);


            if (isErro)
            {
                MostraMensagemTelaUpdatePanel(upAssinatura, msg.ToString());
            }
            else
            {
                if (drpAssinatura.SelectedValue == "1")
                    objM.cod_assinatura = txtAssinatura.Text;
                else if (drpAssinatura.SelectedValue == "2")
                    objM.ano_mes = txtAssinatura.Text;

                DataTable dt = objB.ListaTodos(objM);

                CarregaGrid("gridAssinatura", dt, gridAssinatura);

            }
            DivAcao(divSelect, divAction);
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            DataTable dts = objB.ListaTodos(new Assinatura());
            CarregaGrid("gridAssinatura", dts, gridAssinatura);
        }

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        protected void lnkInserirGrupo_Click(object sender, EventArgs e)
        {
            if (Session["id"] != null)
                Session.Remove("id");
            txtDtVigencia.Text = DateTime.Now.ToShortDateString();
            LimpaCampos();
            DivAcao(divAction, divSelect);
            divEditora.Visible = false;
            txtDtVigencia.Enabled = true;
            txtValorAssinatura.Enabled = true;
            btnAlt.Visible = false;
            btnCanc.Visible = false;
            btnSalv.Visible = false;

            lnkVEditora.Visible = false;
        }

        protected void gridAssinatura_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Atualizar":

                    DataTable dt = objB.ListaTodos(new Assinatura() { id_assinatura = int.Parse(e.CommandArgument.ToString()) });

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];


                        string dt1 = row["DT_PAGTO"].ToString();

                        txtCodigo.Text = row["cod_assinatura"].ToString();
                        txtQuantidade.Text = row["QUANTIDADE"].ToString();
                        txtValorAssinatura.Text = row["VALOR_ASSINAT"].ToString();
                        txtDtInicio.Text = DateTime.Parse(row["DT_INICIO"].ToString()).ToShortDateString();
                        txtDtPagamento.Text = DateTime.Parse(row["DT_PAGTO"].ToString()).ToShortDateString();
                        txtDtVencimento.Text = DateTime.Parse(row["DT_VECTO"].ToString()).ToShortDateString();
                        txtDtVigencia.Text = DateTime.Parse(row["DT_VIGENCIA"].ToString()).ToShortDateString();
                        drpDistribuicao.SelectedValue = row["DISTRIBUICAO"].ToString();
                        drpPeriodico.SelectedValue = row["ID_PERIODICO"].ToString();
                        drpPeriodo.SelectedValue = row["PERIODO"].ToString();
                        hdVigencia.Value = txtDtVigencia.Text;
                        txtDtVigencia.Enabled = false;
                        txtValorAssinatura.Enabled = false;
                        btnCanc.Visible = false;
                        btnSalv.Visible = false;


                        Session["id"] = e.CommandArgument.ToString();
                        CarregaPeriodico(int.Parse(row["ID_PERIODICO"].ToString()));
                        CarregaArea(int.Parse(row["ID_ASSINATURA"].ToString()));
                        DivAcao(divAction, divSelect);
                        divEditora.Visible = true;
                        btnAlt.Visible = true;
                        lnkVEditora.Visible = true;


                        DataTable dts = objB.ListaValores(new Assinatura() { id_assinatura = int.Parse(e.CommandArgument.ToString()) });
                        CarregaGrid("grdValores", dts, grdValores);
                   
                    }
                    break;

                case "Cancelar":
                    Session["id_delete"] = e.CommandArgument.ToString();
                    DivAcao(DivDelete, divSelect);
                    break;
                case "Email":
                    try
                    {
                        DataTable dts = objB.ListaTodos(new Assinatura() { id_assinatura = int.Parse(e.CommandArgument.ToString()) });

                        if (dts.Rows.Count > 0)
                        {
                            DataRow row = dts.Rows[0];

                            string template = ConfigurationManager.AppSettings["htmlEmail"];
                            template = template.ToString().Replace("cod_periodico", row["cod_periodico"].ToString()).Replace("DT_VECTO", DateTime.Parse(row["DT_VECTO"].ToString()).ToShortDateString()).Replace("NOME_PERIODICO", row["NOME_PERIODICO"].ToString()).Replace("COD_PERIODICO", row["cod_periodico"].ToString()).Replace("DESC_PERIODO", row["DESC_PERIODO"].ToString()).Replace("VALOR_ASSINAT", row["VALOR_ASSINAT"].ToString()).Replace("DT_VIGENCIA", DateTime.Parse(row["DT_VIGENCIA"].ToString()).ToShortDateString());
                            string mailBody = String.Format("'mailto:?Subject={0}&body={1}'", ConfigurationManager.AppSettings["assuntoEmail"], template);

                            ScriptManager.RegisterStartupScript(this, typeof(string), "SendMail", "document.location =" + mailBody + ";", true);
                        }
                    }
                    catch (System.Exception ex)
                    {

                        MostraMensagemTelaUpdatePanel(upAssinatura, ex.Message);
                    }

                    break;
                default:
                    break;
            }
        }

        protected void gridAssinatura_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["gridAssinatura"] != null)
            {
                gridAssinatura.PageIndex = e.NewPageIndex;
                gridAssinatura.DataSource = ViewState["gridAssinatura"];
                gridAssinatura.DataBind();
            }
        }

        protected void grdArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdArea"] != null)
            {
                grdArea.PageIndex = e.NewPageIndex;
                grdArea.DataSource = ViewState["grdArea"];
                grdArea.DataBind();
            }
        }

        protected void grdArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void drpAssinatura_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpAssinatura.SelectedIndex > -1)
            {
                if (drpAssinatura.SelectedValue == "2")
                    txtAssinatura.Attributes.Add("placeholder", " FORMATO DE BUSCA MM/YYYY");
                else
                    txtAssinatura.Attributes.Add("placeholder", "");
            }
        }

        protected void btnEnvio_Click(object sender, EventArgs e)
        {
            lblPagina.Visible = false;
            if (lstPagina.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstPagina.Items.Count; i++)
                {
                    if (lstPagina.Items[i].Selected)
                    {
                        if (!arrayPagAcessos.Contains(lstPagina.Items[i]))
                        {
                            arrayPagAcessos.Add(lstPagina.Items[i]);

                        }
                    }
                }
                for (int i = 0; i < arrayPagAcessos.Count; i++)
                {
                    if (!lstPaginaAcesso.Items.Contains(((ListItem)arrayPagAcessos[i])))
                    {
                        lstPaginaAcesso.Items.Add(((ListItem)arrayPagAcessos[i]));
                    }
                    lstPagina.Items.Remove(((ListItem)arrayPagAcessos[i]));
                }
                lstPaginaAcesso.SelectedIndex = -1;
            }
            else
            {
                lblPagina.Visible = true;
                lblPagina.Text = "Selecione uma opção da Área/Responsável para mover";
            }
        }

        protected void btnEnvioTodos_Click(object sender, EventArgs e)
        {
            lblPagina.Visible = false;
            while (lstPagina.Items.Count != 0)
            {
                for (int i = 0; i < lstPagina.Items.Count; i++)
                {
                    if (!lstPaginaAcesso.Items.Contains(lstPagina.Items[i]))
                    {
                        lstPaginaAcesso.Items.Add(lstPagina.Items[i]);
                    }
                    lstPagina.Items.Remove(lstPagina.Items[i]);
                }
            }
        }

        protected void btnRemovs_Click(object sender, EventArgs e)
        {
            lblPagina.Visible = false;
            if (lstPaginaAcesso.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstPaginaAcesso.Items.Count; i++)
                {
                    if (lstPaginaAcesso.Items[i].Selected)
                    {
                        if (!arrayPagGrupos.Contains(lstPaginaAcesso.Items[i]))
                        {
                            arrayPagGrupos.Add(lstPaginaAcesso.Items[i]);
                        }
                    }
                }
                for (int i = 0; i < arrayPagGrupos.Count; i++)
                {
                    if (!lstPagina.Items.Contains(((ListItem)arrayPagGrupos[i])))
                    {
                        lstPagina.Items.Add(((ListItem)arrayPagGrupos[i]));
                    }
                    lstPaginaAcesso.Items.Remove(((ListItem)arrayPagGrupos[i]));
                }
                lstPagina.SelectedIndex = -1;
            }
            else
            {
                lblPagina.Visible = true;
                lblPagina.Text = "Selecione uma opção da lista Área/Assinante para mover";
            }
        }

        protected void btnRemovsTodos_Click(object sender, EventArgs e)
        {
            RemoveTodosListaPagina();
        }

        protected void btnArea_Click(object sender, EventArgs e)
        {
            DivAcao(DivActionArea, divAction);
            if (Session["id"] != null)
            {
                CarregaListBox(new AreaBLL().ListaTodos(new Area()), lstPagina);
                CarregaListBox(new AssinaturaPeriodicoBLL().ListaAreaViculada(new Assinatura() { id_assinatura = int.Parse(Session["id"].ToString()) }), lstPaginaAcesso);
            }
        }

        protected void btVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(divAction, DivActionArea);
        }

        protected void btnSalvarArea_Click(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                string msg = "";
                if (lstPaginaAcesso.Items.Count > 0)
                {
                    objM.listarea = String.Join(",", lstPaginaAcesso.Items
                           .Cast<ListItem>()
                                   .Select(li => li.Value)
                                   .ToArray());
                }
                objM.id_assinatura = int.Parse(Session["id"].ToString());

                bool ret = objB.VincularArea(out msg, objM);

                if (ret)
                {
                    MostraMensagemTelaUpdatePanel(upAssinatura, msg);
                    CarregaArea(int.Parse(Session["id"].ToString()));
                    DivAcao(divAction, DivActionArea);
                }
                else
                    MostraMensagemTelaUpdatePanel(upAssinatura, "Problemas contate o administrador do sistema");
            }

        }

        protected void btnSalv_Click(object sender, EventArgs e)
        {

            if (Session["objUser"] != null)
            {
                var user = (ConectaAD)Session["objUser"];
                string mensagem = "";
                if (ValidaVigencia(out mensagem))
                {

                    objM.dt_vigencia = txtDtVigencia.Text.Equals("") ? DateTime.MinValue : DateTime.Parse(txtDtVigencia.Text);
                    objM.matricula = user.login;
                    objM.valor_assinat = txtValorAssinatura.Text.Equals("") ? 0 : decimal.Parse(txtValorAssinatura.Text);

                    if (Session["id"] != null)
                    {
                        objM.id_assinatura = int.Parse(Session["id"].ToString());

                    }
                    if (objB.InsertVigencia(out mensagem, objM))
                    {
                        MostraMensagemTelaUpdatePanel(upAssinatura, mensagem);
                        RegraTela(false, true);

                        DataTable dts = objB.ListaValores(objM);
                        CarregaGrid("grdValores", dts, grdValores);
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upAssinatura, mensagem);
                        DivAcao(divAction, divSelect);
                        RegraTela(true, false);
                    }

                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upAssinatura, mensagem);
                    DivAcao(divAction, divSelect);
                    RegraTela(true, false);
                }
            }
        }

        protected void btnCanc_Click(object sender, EventArgs e)
        {
            RegraTela(false, true);
        }

        protected void btnAlt_Click(object sender, EventArgs e)
        {

            RegraTela(true, false);
        }

        protected void btnSalvObs_Click(object sender, EventArgs e)
        {
            if (Session["id_delete"] != null && Session["objUser"] != null)
            {
                var user = (ConectaAD)Session["objUser"];

                if (!txtobs.Text.Equals(""))
                {
                    string mensagem = "";
                    bool ret = objB.Deletar(out mensagem, new Assinatura()
                    {
                        id_assinatura = int.Parse(Session["id_delete"].ToString()),
                        matricula = user.login,
                        obs = txtobs.Text
                    });

                    if (ret)
                    {
                        DivAcao(divSelect, DivDelete);

                        MostraMensagemTelaUpdatePanel(upAssinatura, mensagem);

                        DataTable dts = objB.ListaTodos(new Assinatura());

                        CarregaGrid("gridAssinatura", dts, gridAssinatura);

                        Session["id_delete"] = null;
                    }
                }
                else
                {
                    DivAcao(DivDelete, divSelect);
                    MostraMensagemTelaUpdatePanel(upAssinatura, "Digite o motivo do cancelamento");
                }
            }
        }

        protected void butonVoltar_Click(object sender, EventArgs e)
        {
            DivAcao(divSelect, DivDelete);
        }

        protected void lnkVEditora_Click(object sender, EventArgs e)
        {

            ModalBox(Page, lnkEditora.ClientID);
        }


        protected void grdValores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdValores"] != null)
            {
                grdValores.PageIndex = e.NewPageIndex;
                grdValores.DataSource = ViewState["grdValores"];
                grdValores.DataBind();
            }
        }

        #endregion

        #region Métodos
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

        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {

            divExibir.Visible = true;
            divOcultar.Visible = false;

        }

        private void LimpaCampos()
        {
            drpDistribuicao.ClearSelection();
            drpPeriodo.ClearSelection();
            drpPeriodico.ClearSelection();

            txtCodigo.Text = "";
            txtDtInicio.Text = "";
            txtDtVencimento.Text = "";
            txtDtPagamento.Text = "";
            txtQuantidade.Text = ""; ;
            txtValorAssinatura.Text = "";

        }

        private void CarregaTela()
        {

            DivAcao(divSelect, divAction);
            CarregaDrop();
            DataTable dts = objB.ListaTodos(new Assinatura());
            CarregaGrid("gridAssinatura", dts, gridAssinatura);
            divEditora.Visible = false;
        }

        private void RemoveTodosListaPagina()
        {

            while (lstPaginaAcesso.Items.Count != 0)
            {
                for (int i = 0; i < lstPaginaAcesso.Items.Count; i++)
                {
                    if (!lstPagina.Items.Contains(lstPaginaAcesso.Items[i]))
                    {
                        lstPagina.Items.Add(lstPaginaAcesso.Items[i]);
                    }
                    lstPaginaAcesso.Items.Remove(lstPaginaAcesso.Items[i]);
                }
            }

            OrderByListBox(lstPagina);

        }

        private void OrderByListBox(ListBox list)
        {

            Dictionary<string, string> slBrowser = new Dictionary<string, string>();

            foreach (ListItem lstItem in list.Items)
            {
                slBrowser.Add(lstItem.Value, lstItem.Text);
            }

            var sortedDic = (from dic in slBrowser orderby dic.Value ascending select dic);

            list.DataTextField = "Value";
            list.DataValueField = "Key";
            list.DataSource = slBrowser;
            list.DataBind();

        }

        private void CarregaDrop()
        {

            CarregaDropDowDT(new PeriodoPeriodicoBLL().ListaTodos(new PeriodoPeriodico()), drpPeriodo);
            CarregaDropDowDT(new PeriodoPeriodicoBLL().ListaTodos(new PeriodoPeriodico()), drpDistribuicao);
            CarregaDropDowDT(new PeriodicoBLL().ListaTodos(new PeriodicoObj()), drpPeriodico);
            CarregaListBox(new AreaBLL().ListaTodos(new Area()), lstPagina);

        }

        private void CarregaPeriodico(int? id)
        {

            DataTable dt = new PeriodicoBLL().ListaTodos(new PeriodicoObj() { id_periodico = id });

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lblCgc.Text = row["CGC_CPF"].ToString();
                lblBairro.Text = row["BAIRRO"].ToString();
                lblCep.Text = row["CEP"].ToString();
                lblContato.Text = row["CONTATO"].ToString();
                lblCidade.Text = row["CIDADE"].ToString();
                lblComplemento.Text = row["COMPLEMENTO"].ToString();
                lblEmail.Text = row["EMAIL"].ToString();
                lblFax.Text = row["FAX"].ToString();
                lblFone.Text = row["FONE"].ToString();
                lblNome.Text = row["NOME"].ToString();
                lblNumero.Text = row["NUMERO"].ToString();
                lblRua.Text = row["RUA"].ToString();
                lblUF.Text = row["UF"].ToString();
                link.HRef = row["SITE"].ToString();
                link.InnerText = row["SITE"].ToString();

            }

        }

        private void CarregaArea(int id)
        {

            CarregaGrid("grdArea", new AssinaturaPeriodicoBLL().ListaAreaViculada(new Assinatura() { id_assinatura = id }), grdArea);
        }

        private void RegraTela(bool enable, bool disable)
        {

            btnCanc.Visible = enable;
            btnSalv.Visible = enable;
            txtDtVigencia.Enabled = enable;
            txtValorAssinatura.Enabled = enable;
            btnAlt.Visible = disable;


        }

        private bool ValidaVigencia(out string mensagem)
        {

            bool ret = true;
            mensagem = "";
            if (hdVigencia.Value != "")
            {
                if (hdVigencia.Value == txtDtVigencia.Text)
                {
                    mensagem = "Digite uma data de vigência diferente " + hdVigencia.Value;
                    ret = false;
                }

                if (DateTime.Parse(txtDtVigencia.Text) < DateTime.Parse(hdVigencia.Value))
                {
                    mensagem = "Digite uma data de vigência maior que " + hdVigencia.Value;
                    ret = false;
                }

            }
            return ret;
        }
        #endregion


    }
}