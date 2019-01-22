using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class ControlCapitalizacao : BasePage
    {
        #region Atributos
        PropostaAdesaoBLL obj = new PropostaAdesaoBLL();
        string mensagem = "";
        #endregion

        #region Enventos
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Form.DefaultButton = this.btnPesquisar.UniqueID;

            ScriptManager.RegisterStartupScript(upAdesao,
                   upAdesao.GetType(),
                   "script",
                   "_client_side_script()",
                    true);

            if (!IsPostBack)
            {
                CarregaTela();
            }
        }

        protected void ddlStatus_TextChanged(object sender, EventArgs e)
        {
            btnDeferirTodas.Enabled = (ddlStatus.SelectedValue == "2");
        }

        private void CarregaTela()
        {
            CarregaDrop();
            //CarregaGrid("grdAdesao", obj.ListarControle(), grdAdesao);

        }

        protected void grdAdesao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdAdesao"] != null)
            {
                grdAdesao.PageIndex = e.NewPageIndex;
                //grdAdesao.DataSource = ViewState["grdAdesao"];
                grdAdesao.DataBind();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (ValidarCampos()){                
                DivAcao(divSelectProposta, divActionProposta);
                grdAdesao.DataBind();
            }
            
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCodEmpresa.Text = "";
            txtCodMatricula.Text = "";
            txtDtIni.Text = "";
            txtDtFim.Text = "";
            ddlStatus.SelectedValue = "2";
            btnDeferirTodas.Enabled = true;
            grdAdesao.PageIndex = 0;
            grdAdesao.EditIndex = -1;
            grdAdesao.ShowFooter = false;
            grdAdesao.DataBind();
        }

        protected void lnkInserir_Click(object sender, EventArgs e)
        {
            DivAcao(divActionProposta, divSelectProposta);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {

            CarregaTela();
            txtMotivoInd.Text = "";
            DivAcao(divSelectProposta, divActionProposta);
            grdAdesao.DataBind();
        }

        protected void btnDeferirTodas_Click(object sender, EventArgs e)
        {
            bool selecionado = false;
            string mensagem_retorno = "Proposta(s) Deferida(s) com sucesso!";
            string mensagem_erro = "";

            foreach (GridViewRow row in grdAdesao.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelect = (row.FindControl("chkSelect") as CheckBox);
                    if (chkSelect.Checked)
                    {
                        selecionado = true;
                        LinkButton lnkAlterar = (row.FindControl("lnkAlterar") as LinkButton);

                        int id = int.Parse(lnkAlterar.CommandArgument.ToString());

                        if (Session["objUser"] != null)
                        {
                            var user = (ConectaAD)Session["objUser"];
                            obj.matricula = user.login;
                            obj.id_pradprev = id;
                            if (!obj.Deferir(out mensagem))
                            {
                                mensagem_erro += "\\n" + mensagem;
                            }
                        }
                    }
                }
            }

            if (!selecionado)
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção\\n\\nSelecione ao menos uma proposta para deferir");
            }
            else
            {
                if (String.IsNullOrEmpty(mensagem_erro))
                {
                    btnLimpar_Click(sender, e);
                    MostraMensagemTelaUpdatePanel(upAdesao, mensagem_retorno);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upAdesao, mensagem_erro);
                }
            }
        }

        protected void grdAdesao_RowCommand(object sender, GridViewCommandEventArgs e)
        {            

            switch (e.CommandName)
            {
                case "Alterar":

                    int id = int.Parse(e.CommandArgument.ToString());

                    hdIdProposta.Value = id.ToString();
                    obj.id_pradprev = id;
                    obj.cod_emprs = null;
                    obj.registro = null;
                    //DataTable dt = obj.ListarProposta();
                    DataTable dt = obj.ListarControles();
                    if (dt.Rows.Count > 0)
                    {

                        txtVoluntaria.Text = dt.Rows[0]["Voluntaria"].ToString();
                        txtDtInclusao.Text = DateTime.Parse(dt.Rows[0]["Dtinclusao"].ToString()).ToString("dd/MM/yyyy");

                        if (dt.Rows[0]["dt_deferimento"].ToString() != "")
                        {
                            txtDtDeferimento.Text = DateTime.Parse(dt.Rows[0]["dt_deferimento"].ToString()).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            txtDtDeferimento.Text = "";
                        }

                        txtIdDeferimento.Text = dt.Rows[0]["mat_def"].ToString();

                        DateTime dtTemp;
                        if (DateTime.TryParse(dt.Rows[0]["DT_ATIVO_ATIVO"].ToString(), out dtTemp))
                        {
                            txtDtAtivo.Text = dtTemp.ToString("dd/MM/yyyy");                        
                        }
                        txtEmpresa.Text = dt.Rows[0]["COD_EMPRS"].ToString();
                        txtStatus.Text = dt.Rows[0]["status"].ToString();
                        txtPerfil.Text = dt.Rows[0]["PERFIL"].ToString();
                        txtNome.Text = dt.Rows[0]["NOM_PARTICIP"].ToString();
                        txtRegistro.Text = dt.Rows[0]["REGISTRO"].ToString();
                        txtNovoCometario.Text = "";

                        if (!dt.Rows[0]["ID_TPBENEFICIO"].ToString().Equals(""))
                            drpBenef.SelectedValue = dt.Rows[0]["ID_TPBENEFICIO"].ToString();

                        if (!dt.Rows[0]["ID_TPSERVICO"].ToString().Equals(""))
                            drpTempoServ.SelectedValue = dt.Rows[0]["ID_TPSERVICO"].ToString();

                        rdSituacao.SelectedValue = dt.Rows[0]["SIT_CADASTRAL"].ToString();

                        if (dt.Rows[0]["SIT_CADASTRAL"].ToString().Equals("1"))
                        {
                            tbMotivoSit.Visible = true;
                            txtMotivoSit.Text = dt.Rows[0]["DESC_MOTIVO_SIT"].ToString();
                        }

                        if (!dt.Rows[0]["REGISTRO"].ToString().Equals(""))
                            obj.registro = int.Parse(dt.Rows[0]["REGISTRO"].ToString());

                        obj.cod_emprs = int.Parse(dt.Rows[0]["COD_EMPRS"].ToString());

                        DataTable dts = obj.ListarParticipante();
                        CarregaParticipante(dts, true);

                        CarregaServicoBeneficiario(id);

                        RegraTela(int.Parse(dt.Rows[0]["id_status"].ToString()));
                        txtIndMotivo.Text = dt.Rows[0]["desc_indeferido"].ToString();

                    }

                    DivAcao(divActionProposta, divSelectProposta);
                    break;

                default:
                    break;
            }
        }

        protected void btnIndeferir_Click(object sender, EventArgs e)
        {
            DivAcao(divIndefirir, divActionProposta);
        }

        protected void btnDeferir_Click(object sender, EventArgs e)
        {
            try
            {
                bool ret = false;

                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.matricula = user.login;
                    if (!string.IsNullOrEmpty(txtNovoCometario.Text))
                    {
                        obj.desc_indeferido = txtIndMotivo.Text + DateTime.Now.ToShortDateString() + " " + user.nome + ": " + txtNovoCometario.Text + "\r\n";
                        txtIndMotivo.Text = obj.desc_indeferido;
                        txtNovoCometario.Text = "";
                    }
                    if (!hdIdProposta.Value.Equals("0"))
                    {
                        obj.id_pradprev = int.Parse(hdIdProposta.Value);
                        ret = obj.Deferir(out mensagem);
                    }
                }

                if (ret)
                {
                    RegraTela(3);
                }
                MostraMensagemTelaUpdatePanel(upAdesao, mensagem);


            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                bool ret = false;
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.matricula = user.login;
                    if (!string.IsNullOrEmpty(txtNovoCometario.Text))
                    {
                        obj.desc_indeferido = txtIndMotivo.Text + DateTime.Now.ToShortDateString() + " " + user.nome + ": " + txtNovoCometario.Text + "\r\n";
                        txtIndMotivo.Text = obj.desc_indeferido;
                        txtNovoCometario.Text = "";
                    }

                    if (!txtMotivoInd.Text.Equals(""))
                    {
                        if (!hdIdProposta.Value.Equals("0"))
                        {
                            obj.id_pradprev = int.Parse(hdIdProposta.Value);
                            obj.desc_indeferido = txtMotivoInd.Text + DateTime.Now.ToShortDateString() + " " + user.nome + ": " + txtMotivoInd.Text + "\r\n";
                            ret = obj.Indeferir(out mensagem);
                        }

                        if (ret)
                        {
                            txtIndMotivo.Text = obj.desc_indeferido;
                            RegraTela(4);
                            DivAcao(divActionProposta, divIndefirir);
                        }
                        MostraMensagemTelaUpdatePanel(upAdesao, mensagem);
                    }
                    else
                        MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Informe o motivo para continuar.");
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            DivAcao(divActionProposta, divIndefirir);
        }

        protected void grdBenef_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdBenef"] != null)
            {
                grdBenef.PageIndex = e.NewPageIndex;
                grdBenef.DataSource = ViewState["grdBenef"];
                grdBenef.DataBind();
            }
        }

        protected void grdTemp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdTemp"] != null)
            {
                grdTemp.PageIndex = e.NewPageIndex;
                grdTemp.DataSource = ViewState["grdTemp"];
                grdTemp.DataBind();
            }
        }

        #endregion

        #region Métodos
        private void RegraTela(int id)
        {

            switch (id)
            {
                case 2: //enviado
                    btnIndeferir.Visible = true;
                    btnDeferir.Visible = true;
                    motivoIndeferido.Visible = false;
                    break;

                case 3://deferido
                    btnIndeferir.Visible = false;
                    btnDeferir.Visible = false;
                    txtNovoCometario.Visible = false; 
                    break;
                case 4://Indeferido
                    motivoIndeferido.Visible = true;
                    btnIndeferir.Visible = false;
                    btnDeferir.Visible = true;
                    txtNovoCometario.Visible = true; 
                    break;
                default:
                    break;
            }
        }

        private void CarregaServicoBeneficiario(int id)
        {

            obj.ObjBenefRecusado = new BenefRecusadoBLL();
            obj.ObjBenefRecusado.id_pradprev = id;
            obj.ObjBenefRecusado.id_benefrecusado = null;
            CarregaGrid("grdBenef", obj.ObjBenefRecusado.ListarBenefRecusado(), grdBenef);

            obj.ObjTempRecusado = new TempoRecusadoBLL();
            obj.ObjTempRecusado.id_pradprev = id;
            obj.ObjTempRecusado.id_temprecusado = null;
            CarregaGrid("grdTemp", obj.ObjTempRecusado.ListarTemp(), grdTemp);

        }

        private void CarregaParticipante(DataTable dt, bool isupdate)
        {

            if (dt.Rows.Count > 0)
            {
                divCap.Visible = true;
                DataRow row = dt.Rows[0];
                lblNumMatri.Text = row["num_matr_partf"].ToString();
                lblRegime.Text = row["REGIME_DE_TRIBUTACAO"].ToString();
                lblPlano.Text = row["PLANO"].ToString();
                lblDtAdesao.Text = DateTime.Parse(row["DATA_DA_ADESAO"].ToString()).ToString("dd/MM/yyyy");
                lblDtAdmissao.Text = DateTime.Parse(row["DATA_DE_ADMISSAO"].ToString()).ToString("dd/MM/yyyy");

                if (!isupdate)
                {
                    txtPerfil.Text = row["PERFIL"].ToString();
                    //txtDtInclusao.Text = DateTime.Parse(row["DATA_INCLI_PERFIL"].ToString()).ToString("dd/MM/yyyy");
                    txtDtInclusao.Text = DateTime.Parse(row["Dtinclusao"].ToString()).ToString("dd/MM/yyyy");
                    txtDtAtivo.Text = DateTime.Parse(row["DATA_INCLI_PERFIL"].ToString()).ToString("dd/MM/yyyy");
                    txtNome.Text = row["nom_emprg"].ToString();
                    txtEmpresa.Text = row["cod_emprs"].ToString();
                    txtVoluntaria.Text = row["PERCENTUAL"].ToString();
                }

            }
            else
            {
                txtNome.Text = "";
                txtEmpresa.Text = "";
                txtVoluntaria.Text = "";
                txtPerfil.Text = "";
                txtDtInclusao.Text = "";
                txtDtAtivo.Text = "";
                divCap.Visible = false;
            }
        }

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        private bool ValidarCampos()
        {
            int CodEmpresa, CodMatricula;
            DateTime DtIni, DtFim;

            if (!String.IsNullOrEmpty(txtCodEmpresa.Text) && !int.TryParse(txtCodEmpresa.Text, out CodEmpresa))
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção\\n\\nCampo Empresa inválido");
                return false;
            }

            if (!String.IsNullOrEmpty(txtCodMatricula.Text) && !int.TryParse(txtCodMatricula.Text, out CodMatricula))
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção\\n\\nCampo Matrícula inválido");
                return false;
            }

            if (!String.IsNullOrEmpty(txtDtIni.Text) && !DateTime.TryParse(txtDtIni.Text, out DtIni))
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção\\n\\nPeríodo inicial de inclusão inválido");
                return false;
            }

            if (!String.IsNullOrEmpty(txtDtFim.Text) && !DateTime.TryParse(txtDtFim.Text, out DtFim))
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção\\n\\nPeríodo final de inclusão inválido");
                return false;
            }

            //if (String.IsNullOrEmpty(txtDtIni.Text) || String.IsNullOrEmpty(txtDtFim.Text))
            //{
            //    MostraMensagemTelaUpdatePanel(upAdesao, "Atenção\\n\\nOs campos do Período de emissão são obrigatórios");
            //    return false;
            //}
            //else if (!DateTime.TryParse(txtDtIni.Text, out DtIni) || !DateTime.TryParse(txtDtFim.Text, out DtFim))
            //{
            //    MostraMensagemTelaUpdatePanel(upAdesao, "Atenção\\n\\nPeríodo de emissão inválido");
            //    return false;
            //}

            return true;
        }

        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {

            divExibir.Visible = true;
            divOcultar.Visible = false;

        }

        private void CarregaDrop()
        {

            obj.ObTipoServico = new TipoServicoBLL();
            obj.ObjTipoBeneficio = new TipoBeneficioBLL();
            CarregaDropDowDT(obj.ObTipoServico.ListaDados(), drpTempoServ);
            CarregaDropDowDT(obj.ObjTipoBeneficio.ListaDados(), drpBenef);

        }
        #endregion

    }
}