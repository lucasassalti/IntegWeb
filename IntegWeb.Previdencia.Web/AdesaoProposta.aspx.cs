using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Entidades.Cartas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IntegWeb.Framework;

namespace IntegWeb.Previdencia.Web
{
    public partial class AdesaoProposta : BasePage
    {
        #region Atributos
        PropostaAdesaoBLL obj = new PropostaAdesaoBLL();
        #endregion

        #region Eventos

        #region Aba Cadastro Proposta

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Form.DefaultButton = this.btnPesquisar.UniqueID;

            if (drpPesquisa.SelectedValue == "1")
            {
                drpStatus.Style.Remove("display");
                txtPesquisa.Style.Add("display", "none");
            }
            else if (drpPesquisa.SelectedValue == "2")
            {
                txtPesquisa.Style.Remove("display");
                drpStatus.Style.Add("display", "none");
            }

            if (!IsPostBack)
            {
                CarregaTela();
            }



        }

        protected void btnSalvarCad_Click(object sender, EventArgs e)
        {

            try
            {
                string mensagem = "";
                int id = 0;
                bool ret = true;
                ConectaAD user = null;
                obj.tipo_doc = (int)tipoDocumento.Proposta;


                if (Session["objUser"] != null)
                {
                    user = (ConectaAD)Session["objUser"];
                }

                if (!txtRegistro.Text.Equals(""))
                    obj.registro = int.Parse(txtRegistro.Text);
                obj.nome = txtNome.Text;
                obj.perfil = txtPerfil.Text;
                if (!String.IsNullOrEmpty(txtNovoCometario.Text))
                {
                    obj.desc_indeferido = txtIndMotivo.Text + DateTime.Now.ToShortDateString() + " " + user.nome + ": " + txtNovoCometario.Text + "\r\n";
                    txtIndMotivo.Text = obj.desc_indeferido;
                }
                else
                {
                    obj.desc_indeferido = txtIndMotivo.Text;
                }
                txtNovoCometario.Text = "";

                if (!txtVoluntaria.Text.Equals(""))
                    obj.voluntaria = decimal.Parse(txtVoluntaria.Text);

                if (!txtDtInclusao.Text.Equals(""))
                    obj.dt_inclusao = DateTime.Parse(txtDtInclusao.Text);

                if (!txtEmpresa.Text.Equals(""))
                    obj.cod_emprs = int.Parse(txtEmpresa.Text);

                if (drpBenef.SelectedIndex > 0)
                    obj.id_tpbeneficio = int.Parse(drpBenef.SelectedValue);

                if (drpTempoServ.SelectedIndex > 0)
                    obj.id_tpservico = int.Parse(drpTempoServ.SelectedValue);

                obj.sit_cadastral = int.Parse(rdSituacao.SelectedValue);
                obj.desc_motivo_sit = txtMotivoSit.Text;

                int id_status;
                int.TryParse(hidIdStatus.Value, out id_status);
                obj.id_status = id_status;


                if (!hdIdProposta.Value.Equals("0"))
                {
                    obj.id_pradprev = int.Parse(hdIdProposta.Value);
                    ret = obj.Alterar(out mensagem, drpTempoServ.Visible);
                }
                else
                {
                    if (user != null)
                    {
                        obj.matricula = user.login;
                        ret = obj.Inserir(out mensagem, out id, drpTempoServ.Visible);
                    }
                }


                if (ret)
                {
                    if (id > 0)
                    {
                        btnNovo.Visible = (hdIdProposta.Value.Equals("0"));
                        hdIdProposta.Value = id.ToString();
                        RegraTela(1);
                        CarregaServicoBeneficiario(id);
                    } else
                    {
                        RegraTela(obj.id_status);
                    }

                    MostraMensagemTelaUpdatePanel(upAdesao, mensagem);

                }
                else
                    MostraMensagemTelaUpdatePanel(upAdesao, "Atenção !\\n\\n" + mensagem);
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }


        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            try
            {
                LimpaFormProposta();
                hdIdProposta.Value = "0";
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        protected void grdAdesao_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdAdesao"] != null)
            {
                grdAdesao.PageIndex = e.NewPageIndex;
                grdAdesao.DataSource = ViewState["grdAdesao"];
                grdAdesao.DataBind();
            }
        }

        protected void grdAdesao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            string msg = "";
            switch (e.CommandName)
            {
                case "Alterar":


                    hdIdProposta.Value = id.ToString();
                    obj.id_pradprev = id;
                    obj.cod_emprs = null;
                    obj.registro = null;
                    DataTable dt = obj.ListarProposta();
                    if (dt.Rows.Count > 0)
                    {

                        TabContainer1.ActiveTabIndex = 0;

                        txtCodMetrofile.Text = dt.Rows[0]["cod_metrofile"].ToString();
                        txtVoluntaria.Text = dt.Rows[0]["Voluntaria"].ToString();
                        txtDtInclusao.Text = DateTime.Parse(dt.Rows[0]["Dtinclusao"].ToString()).ToString("dd/MM/yyyy");

                        DateTime dtTemp;
                        if (DateTime.TryParse(dt.Rows[0]["dt_perfil"].ToString(), out dtTemp)) {
                            txtDtAtivo.Text = dtTemp.ToString("dd/MM/yyyy");
                        }
                        txtEmpresa.Text = dt.Rows[0]["COD_EMPRS"].ToString();
                        txtPerfil.Text = dt.Rows[0]["PERFIL"].ToString();
                        txtNome.Text = dt.Rows[0]["NOM_PARTICIP"].ToString();
                        txtRegistro.Text = dt.Rows[0]["REGISTRO"].ToString();
                        txtIndMotivo.Text = dt.Rows[0]["desc_indeferido"].ToString();
                        txtNovoCometario.Text = "";
                        hidIdStatus.Value = dt.Rows[0]["id_status"].ToString();
                        lblStatus.Text = dt.Rows[0]["status"].ToString();

                        hidcodPlano.Value = dt.Rows[0]["COD_PLANO"].ToString();

                        //motivoIndeferido.Visible = String.IsNullOrEmpty(txtIndMotivo.Text) ? false : true;

                        if (!dt.Rows[0]["ID_TPBENEFICIO"].ToString().Equals(""))
                            drpBenef.SelectedValue = dt.Rows[0]["ID_TPBENEFICIO"].ToString();

                        if (!dt.Rows[0]["dt_envio_kit"].ToString().Equals(""))
                            txtdtEnvioKit.Text = DateTime.Parse(dt.Rows[0]["dt_envio_kit"].ToString()).ToString("dd/MM/yyyy");

                        if (!dt.Rows[0]["dt_ar"].ToString().Equals(""))
                            txtDtAr.Text = DateTime.Parse(dt.Rows[0]["dt_ar"].ToString()).ToString("dd/MM/yyyy");

                        if (!dt.Rows[0]["dt_metrofile"].ToString().Equals(""))
                            txtDtMetrofile.Text = DateTime.Parse(dt.Rows[0]["dt_metrofile"].ToString()).ToString("dd/MM/yyyy");

                        if (!dt.Rows[0]["ID_TPSERVICO"].ToString().Equals(""))
                        {
                            drpTempoServ.SelectedValue = dt.Rows[0]["ID_TPSERVICO"].ToString();
                        }


                        if (!drpTempoServ.SelectedValue.Equals("4"))
                        {
                            ListItem ls_4 = drpTempoServ.Items.FindByValue("4");
                            if (ls_4!=null)
                            {
                                drpTempoServ.Items.Remove(ls_4);
                            }
                        }

                        rdSituacao.SelectedValue = dt.Rows[0]["SIT_CADASTRAL"].ToString();

                        if (dt.Rows[0]["SIT_CADASTRAL"].ToString().Equals("1"))
                        {
                            tbMotivoSit.Visible = true;
                            txtMotivoSit.Text = dt.Rows[0]["DESC_MOTIVO_SIT"].ToString();
                        }

                        if (!dt.Rows[0]["REGISTRO"].ToString().Equals(""))
                            obj.registro = int.Parse(dt.Rows[0]["REGISTRO"].ToString());

                        obj.cod_emprs = int.Parse(dt.Rows[0]["COD_EMPRS"].ToString());

                        //DataTable dts = obj.ListarParticipante();
                        //CarregaParticipante(dts, true);

                        CarregaServicoBeneficiario(id);

                        RegraTela(int.Parse(dt.Rows[0]["id_status"].ToString()));

                    }

                    DivAcao(divActionProposta, divSelectProposta);
                    break;

                case "Deletar":

                    try
                    {

                        var user = (ConectaAD)Session["objUser"];
                        obj.id_pradprev = id;
                        obj.matricula = user.login;
                        bool ret = obj.Deletar(out msg);
                        if (ret)
                        {
                            obj.id_pradprev = null;
                            CarregaGrid("grdAdesao", obj.ListarProposta(), grdAdesao);
                        }
                        MostraMensagemTelaUpdatePanel(upAdesao, msg);
                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool isErro = ValidaCamposTela(txtPesquisa, drpPesquisa, out msg);


            if (isErro)
            {
                MostraMensagemTelaUpdatePanel(upAdesao, msg.ToString());
            }
            else
            {
                if (drpPesquisa.SelectedValue == "1")
                    obj.perfil = (drpStatus.SelectedValue != "0") ? drpStatus.SelectedItem.Text : "";
                else if (drpPesquisa.SelectedValue == "2")
                    obj.registro = (!String.IsNullOrEmpty(txtPesquisa.Text)) ? int.Parse(txtPesquisa.Text) : 0;


                CarregaGrid("grdAdesao", obj.ListarProposta(), grdAdesao);

            }
            DivAcao(divSelectProposta, divActionProposta);
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtPesquisa.Text = "";
            CarregaTela();
        }

        protected void lnkInserir_Click(object sender, EventArgs e)
        {
            RegraTela(0);
            DivAcao(divActionProposta, divSelectProposta);
            // Inibir tipo de serviço "não possui tempo de serviço anterior" nos novos lanaçamentos:

            obj.ObTipoServico = new TipoServicoBLL();
            CarregaDropDowDT(obj.ObTipoServico.ListaDados("ID_TPSERVICO<>4"), drpTempoServ);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            CarregaTela();
            LimpaFormProposta();
            DivAcao(divSelectProposta, divActionProposta);
        }

        protected void txtEmpresa_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int matricula = 0;
                int empresa = 0;


                if (int.TryParse(txtRegistro.Text, out matricula) && int.TryParse(txtEmpresa.Text, out empresa))
                {
                    obj.registro = matricula;
                    obj.cod_emprs = empresa;
                    DataTable dt = obj.ListarParticipante();
                    CarregaParticipante(dt, false);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upAdesao, "Digite apenas números nos campos Registro ou Empresa ");
                }

            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }

        }

        protected void rdSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdSituacao.SelectedValue == "0")
                tbMotivoSit.Visible = false;
            else
                tbMotivoSit.Visible = true;
        }

        protected void btnEnviarCap_Click(object sender, EventArgs e)
        {
            try
            {
                string mensagem;
                if (Session["objUser"] != null)
                {

                    var user = (ConectaAD)Session["objUser"];

                    obj.matricula = user.login;

                    if (!hdIdProposta.Value.Equals("0"))
                    {
                        obj.id_pradprev = int.Parse(hdIdProposta.Value);
                        obj.id_status = 2;
                        lblStatus.Text = "Enviado";
                        obj.EnviarCap(out mensagem);
                        RegraTela(obj.id_status);
                        MostraMensagemTelaUpdatePanel(upAdesao, mensagem);
                    }
                }

            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }
        #endregion

        #region Aba Beneficiário Recusado
        protected void btnSalvarBenef_Click(object sender, EventArgs e)
        {
            try
            {
                obj.ObjBenefRecusado = new BenefRecusadoBLL();
                var benef = obj.ObjBenefRecusado;
                string msg = "";
                int id = 0;
                bool ret;

                if (!hdIdProposta.Value.Equals("0"))
                {
                    benef.id_pradprev = int.Parse(hdIdProposta.Value);
                    benef.nome = txtBeneficiario.Text;
                    benef.pai = txtNomPai.Text;
                    benef.rg = txtRg.Text;
                    benef.grau = txtGrau.Text;
                    benef.cpf = txtCPF.Text;
                    benef.grau = txtGrau.Text;
                    benef.mae = txtNomMae.Text;
                }


                if (!txtDtNascimento.Text.Equals(""))
                    benef.dtNascimento = DateTime.Parse(txtDtNascimento.Text);

                if (hdIdBenef.Value.Equals("0"))
                {
                    ret = benef.Inserir(out msg , out id);
                }
                else
                {
                    benef.id_benefrecusado = int.Parse(hdIdBenef.Value.ToString());
                    ret = benef.Alterar(out msg);

                }


                if (ret)
                {
                    if (id > 0)
                    {
                        hdIdBenef.Value = id.ToString();
                    }

                    MostraMensagemTelaUpdatePanel(upAdesao, msg);

                }
                else
                    MostraMensagemTelaUpdatePanel(upAdesao, "Atenção !\\n\\n" + msg);
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }


        }

        protected void VoltarBenef_Click(object sender, EventArgs e)
        {
            DivAcao(divSelectBenef, divActionBenef);
            CarregaServicoBeneficiario(int.Parse(hdIdProposta.Value));
            LimpaCamposBenef();
        }

        protected void grdBenef_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            hdIdBenef.Value = id.ToString();
            obj.ObjBenefRecusado = new BenefRecusadoBLL();
            string msg = "";
            switch (e.CommandName)
            {
                case "Alterar":


                    obj.ObjBenefRecusado.id_benefrecusado = id;
                    DataTable dt = obj.ObjBenefRecusado.ListarBenefRecusado();

                    if (dt.Rows.Count > 0)
                    {

                        txtBeneficiario.Text = dt.Rows[0]["nome"].ToString();
                        txtCPF.Text = dt.Rows[0]["cpf"].ToString();
                        txtRg.Text = dt.Rows[0]["rg"].ToString();
                        txtGrau.Text = dt.Rows[0]["grau"].ToString();
                        txtNomMae.Text = dt.Rows[0]["mae"].ToString();
                        txtNomPai.Text = dt.Rows[0]["Pai"].ToString();

                        if (!dt.Rows[0]["dtnascimento"].ToString().Equals(""))
                            txtDtNascimento.Text = DateTime.Parse(dt.Rows[0]["dtnascimento"].ToString()).ToString("dd/MM/yyyy");


                    }

                    DivAcao(divActionBenef, divSelectBenef);
                    break;

                case "Deletar":

                    try
                    {
                        obj.ObjBenefRecusado.id_benefrecusado = id;
                        bool ret = obj.ObjBenefRecusado.Deletar(out msg);
                        if (ret)
                        {
                            if (!hdIdProposta.Value.ToString().Equals("0"))
                                CarregaServicoBeneficiario(int.Parse(hdIdProposta.Value));

                        }
                        MostraMensagemTelaUpdatePanel(upAdesao, msg);
                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        protected void lnkInsereBenef_Click(object sender, EventArgs e)
        {
            DivAcao(divActionBenef, divSelectBenef);
        }




        #endregion

        #region Aba Tempo de Serviço Recusado
        protected void btnSalvarTemp_Click(object sender, EventArgs e)
        {
            try
            {
                obj.ObjTempRecusado = new TempoRecusadoBLL();
                var temp = obj.ObjTempRecusado;
                string msg = "";
                bool ret;
                int id = 0;

                if (!hdIdProposta.Value.Equals("0"))
                {
                    temp.id_pradprev = int.Parse(hdIdProposta.Value);
                    temp.empresa = txtEmprs.Text;
                    if (!txtDemissao.Text.Equals(""))
                        temp.dtdemissao = DateTime.Parse(txtDemissao.Text);

                    if (!txtDtAdmissao.Text.Equals(""))
                        temp.dtadmissao = DateTime.Parse(txtDtAdmissao.Text);

                }

                if (hdIdTemp.Value.Equals("0"))
                {
                    ret = temp.Inserir(out msg, out id);
                }
                else
                {
                    temp.id_temprecusado = int.Parse(hdIdTemp.Value.ToString());
                    ret = temp.Alterar(out msg);

                }

                if (ret)
                {
                    if (id > 0)
                    {
                        hdIdTemp.Value = id.ToString();
                    }

                    MostraMensagemTelaUpdatePanel(upAdesao, msg);

                }
                else
                    MostraMensagemTelaUpdatePanel(upAdesao, "Atenção !\\n\\n" + msg);


                MostraMensagemTelaUpdatePanel(upAdesao, msg);

            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        protected void btnVoltarTemp_Click(object sender, EventArgs e)
        {
            DivAcao(divSelectTemp, divActionTemp);
            LimpaCamposTemp();
            CarregaServicoBeneficiario(int.Parse(hdIdProposta.Value));

        }

        protected void lnkInserirTemp_Click(object sender, EventArgs e)
        {
            DivAcao(divActionTemp, divSelectTemp);
        }

        protected void grdTemp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            hdIdTemp.Value = id.ToString();
            obj.ObjTempRecusado = new TempoRecusadoBLL();
            string msg = "";
            switch (e.CommandName)
            {
                case "Alterar":


                    obj.ObjTempRecusado.id_temprecusado = id;
                    DataTable dt = obj.ObjTempRecusado.ListarTemp();

                    if (dt.Rows.Count > 0)
                    {

                        txtEmprs.Text = dt.Rows[0]["EMPRESA"].ToString();

                        if (!dt.Rows[0]["DTADMISSAO"].ToString().Equals(""))
                            txtDtAdmissao.Text = DateTime.Parse(dt.Rows[0]["DTADMISSAO"].ToString()).ToString("dd/MM/yyyy");

                        if (!dt.Rows[0]["DTDEMISSAO"].ToString().Equals(""))
                            txtDemissao.Text = DateTime.Parse(dt.Rows[0]["DTDEMISSAO"].ToString()).ToString("dd/MM/yyyy");


                    }

                    DivAcao(divActionTemp, divSelectTemp);
                    break;

                case "Deletar":

                    try
                    {
                        obj.ObjTempRecusado.id_temprecusado = id;
                        bool ret = obj.ObjTempRecusado.Deletar(out msg);
                        if (ret)
                        {
                            if (!hdIdProposta.Value.ToString().Equals("0"))
                                CarregaServicoBeneficiario(int.Parse(hdIdProposta.Value));

                        }
                        MostraMensagemTelaUpdatePanel(upAdesao, msg);
                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Aba Envio do Kit
        protected void btnSavalKit_Click(object sender, EventArgs e)
        {
            try
            {
                string mensagem;
                if (txtdtEnvioKit.Text.Equals(""))
                {
                    MostraMensagemTelaUpdatePanel(upAdesao, "Informe a Data de Envio do Kit");
                    return;
                }

                if (!hdIdProposta.Value.Equals("0"))
                {
                    obj.id_pradprev = int.Parse(hdIdProposta.Value);
                    obj.cod_metrofile = txtCodMetrofile.Text;
                    var user = (ConectaAD)Session["objUser"];
                    obj.matricula = user.login;

                    if (!txtdtEnvioKit.Text.Equals(""))
                        obj.dt_envio_kit = DateTime.Parse(txtdtEnvioKit.Text);

                    if (!txtDtAr.Text.Equals(""))
                        obj.dt_ar = DateTime.Parse(txtDtAr.Text);

                    if (!txtDtMetrofile.Text.Equals(""))
                        obj.dt_metrofile = DateTime.Parse(txtDtMetrofile.Text);

                    btnArquivar.Enabled = (!txtCodMetrofile.Text.Equals("") && !txtDtMetrofile.Text.Equals(""));

                    obj.Salvarkit(out mensagem);

                    lblStatus.Text = "Aguardando AR";

                    MostraMensagemTelaUpdatePanel(upAdesao, mensagem);
                }

            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        protected void btnArquivar_Click(object sender, EventArgs e)
        {
            try
            {
                string mensagem;
                if (Session["objUser"] != null)
                {

                    var user = (ConectaAD)Session["objUser"];

                    obj.matricula = user.login;

                    if (!hdIdProposta.Value.Equals("0"))
                    {
                        obj.id_pradprev = int.Parse(hdIdProposta.Value);
                        obj.ArquivarProposta(out mensagem);
                        RegraTela(6);
                        MostraMensagemTelaUpdatePanel(upAdesao, mensagem);
                    }
                }

            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upAdesao, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }
        #endregion
        #endregion

        #region Métodos

        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {

            divExibir.Visible = true;
            divOcultar.Visible = false;

        }

        private void CarregaTela()
        {
            RegraTela(0);
            CarregaDrop();
            CarregaGrid("grdAdesao", obj.ListarProposta(), grdAdesao);
        }

        private void CarregaDrop()
        {

            obj.ObTipoServico = new TipoServicoBLL();
            obj.ObjTipoBeneficio = new TipoBeneficioBLL();
            CarregaDropDowDT(obj.ObTipoServico.ListaDados(), drpTempoServ);
            CarregaDropDowDT(obj.ObjTipoBeneficio.ListaDados(), drpBenef);
            CarregaDropDowDT(obj.ListarStatus(), drpStatus);
            drpStatus.Items[0].Text = "<TODOS>";
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
                hidcodPlano.Value = row["COD_PLANO"].ToString();
                lblDtAdesao.Text = DateTime.Parse(row["DATA_DA_ADESAO"].ToString()).ToString("dd/MM/yyyy");
                lblDtAdmissao.Text = DateTime.Parse(row["DATA_DE_ADMISSAO"].ToString()).ToString("dd/MM/yyyy");

                //Para os planos PAB/FUNCESP e PPCPFL não são cadastrados o tempo de serviço anterior. Desta forma, a carta deverá conter apenas as informações de beneficiários. 
                if (hidcodPlano.Value.ToString().Equals("16") || hidcodPlano.Value.ToString().Equals("17"))
                {
                    drpTempoServ.SelectedValue = "0";
                    drpTempoServ.Visible = false;
                    lblTempoServico.Visible = false;
                }
                else {
                    drpTempoServ.Visible = true;
                    lblTempoServico.Visible = true;                
                }


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
                //txtNome.Text = "";
                //txtVoluntaria.Text = "";
                //txtPerfil.Text = "";
                //txtDtInclusao.Text = "";
                //txtDtAtivo.Text = "";
                divCap.Visible = false;
                MostraMensagemTelaUpdatePanel(upAdesao, "Matrícula ou Empresa não existe no banco de dados ou Participante não tem plano previdenciário!");
            }
        }
        private bool ValidaCamposTela(TextBox text, DropDownList drp, out string mensagem)
        {

            StringBuilder msg = new StringBuilder();
            msg.Append("ERRO!\\n");
            bool isErro = false;
            //if (drp.SelectedIndex < 1)
            //{
            //    msg.Append("1 Selecione uma opção de busca.\\n");
            //    isErro = true;
            //}

            if (text.Text == "" && drp.SelectedValue == "2")
            {
                msg.Append("Entre com o nº registro");
                isErro = true;
            }
            mensagem = msg.ToString();
            return isErro;
        }
        private void LimpaFormProposta()
        {

            txtPesquisa.Text = "";
            txtMotivoSit.Text = "";
            txtNome.Text = "";
            txtVoluntaria.Text = "";
            txtRegistro.Text = "";
            txtPerfil.Text = "";
            txtEmpresa.Text = "";
            tbMotivoSit.Visible = false;
            divCap.Visible = false;
            txtDtInclusao.Text = "";
            txtDtAtivo.Text = "";
            drpBenef.SelectedValue = "0";
            drpTempoServ.SelectedValue = "0";


        }
        private void LimpaCamposBenef()
        {

            txtBeneficiario.Text = "";
            txtCPF.Text = "";
            txtRg.Text = "";
            txtGrau.Text = "";
            txtNomMae.Text = "";
            txtNomPai.Text = "";
            txtDtNascimento.Text = "";
            hdIdBenef.Value = "0";

        }
        public void LimpaCamposTemp()
        {

            txtDemissao.Text = "";
            txtDtAdmissao.Text = "";
            txtEmprs.Text = "";
            hdIdTemp.Value = "0";


        }

        public void CamposLeitura(bool chave)
        {

            txtMotivoSit.Enabled = chave;
            txtNome.Enabled = chave;
            txtVoluntaria.Enabled = chave;
            txtRegistro.Enabled = chave;
            txtPerfil.Enabled = false;
            txtEmpresa.Enabled = chave;
            txtDtAtivo.Enabled = chave;
            txtDtInclusao.Enabled = chave;
            txtNovoCometario.Enabled = chave;
            drpBenef.Enabled = chave;
            drpTempoServ.Enabled = chave;
            lnkInsereBenef.Visible = chave;
            lnkInserirTemp.Visible = chave;
            btnEnviarCap.Visible = chave;
            btnSalvarCad.Visible = chave;
            rdSituacao.Enabled = chave;

            if (grdBenef.Rows.Count > 0)
            {
                grdBenef.Columns[7].Visible = chave;
                grdBenef.Columns[8].Visible = chave;
            }
            if (grdTemp.Rows.Count > 0)
            {
                grdTemp.Columns[3].Visible = chave;
                grdTemp.Columns[4].Visible = chave;
            }

        }

        private void RegraTela(int id)
        {

            switch (id)//Tela para Inserir
            {
                case 0:

                    TbBenefRecusado.Visible = false;
                    TbServico.Visible = false;
                    TbEnvioKit.Visible = false;
                    rdSituacao.SelectedIndex = 0;
                    tbMotivoSit.Visible = false;
                    btnEnviarCap.Visible = false;
                    hdIdProposta.Value = "0";
                    btnSalvarCad.Visible = true;
                    motivoIndeferido.Visible = false;
                    divSelectBenef.Visible = true;
                    divSelectTemp.Visible = true;
                    divActionBenef.Visible = false;
                    divActionTemp.Visible = false;
                    CamposLeitura(true);
                    drpTempoServ.Visible = ((!hidcodPlano.Value.ToString().Equals("16") && !hidcodPlano.Value.ToString().Equals("17")));
                    lblTempoServico.Visible = drpTempoServ.Visible;
                    btnEnviarCap.Visible = false;
                    break;
                case 1: //Status Criado
                    TbBenefRecusado.Visible = true;

                    drpTempoServ.Visible = ((!hidcodPlano.Value.ToString().Equals("16") && !hidcodPlano.Value.ToString().Equals("17")));
                    TbServico.Visible = drpTempoServ.Visible;
                    lblTempoServico.Visible = drpTempoServ.Visible;
                    btnEnviarCap.Enabled = (txtPerfil.Text.IndexOf("01-Ativo 01-Ativo") > -1 ||
                                            txtPerfil.Text.IndexOf("01-Ativo 04-Desligado") > -1 ||
                                            txtPerfil.Text.IndexOf("01-Ativo 03-Afastado") > -1 ||
                                            txtPerfil.Text.IndexOf("01-Ativo 02-Falecido") > -1);
                    TbEnvioKit.Visible = false;
                    motivoIndeferido.Visible = false;
                    divSelectBenef.Visible = true;
                    divSelectTemp.Visible = true;
                    divActionBenef.Visible = false;
                    divActionTemp.Visible = false;
                    CamposLeitura(true);

                    break;
                case 2: //Status Enviado
                    TbBenefRecusado.Visible = true;
                    drpTempoServ.Visible = ((!hidcodPlano.Value.ToString().Equals("16") && !hidcodPlano.Value.ToString().Equals("17")));
                    TbServico.Visible = drpTempoServ.Visible;
                    lblTempoServico.Visible = drpTempoServ.Visible;
                    TbEnvioKit.Visible = false;
                    btnEnviarCap.Enabled = false;
                    motivoIndeferido.Visible = !string.IsNullOrEmpty(txtIndMotivo.Text);
                    divSelectBenef.Visible = true;
                    divSelectTemp.Visible = true;
                    divActionBenef.Visible = false;
                    divActionTemp.Visible = false;
                    CamposLeitura(true);
                    break;
                case 3: //Status Enviar KIT - Deferido
                    TbEnvioKit.Visible = true;
                    TbBenefRecusado.Visible = true;
                    TbServico.Visible = true;
                    motivoIndeferido.Visible = true;
                    txtIndMotivo.Visible = true;
                    txtNovoCometario.Visible = true;
                    btnEnviarCap.Enabled = false;
                    txtdtEnvioKit.Enabled = true;
                    txtDtAr.Enabled = true;
                    txtDtMetrofile.Enabled = true;
                    txtCodMetrofile.Enabled = true;
                    btnSavalKit.Visible = true;
                    btnArquivar.Visible = true;
                    btnArquivar.Enabled = (!txtCodMetrofile.Text.Equals("") && !txtDtMetrofile.Text.Equals(""));
                    CamposLeitura(false);
                    break;
                case 4: //Status Indeferido
                    TbEnvioKit.Visible = false;
                    TbBenefRecusado.Visible = true;
                    TbServico.Visible = true;
                    motivoIndeferido.Visible = true;
                    txtIndMotivo.Visible = true;
                    txtNovoCometario.Visible = true;
                    divSelectBenef.Visible = true;
                    divSelectTemp.Visible = true;
                    divActionBenef.Visible = false;
                    divActionTemp.Visible = false;
                    btnEnviarCap.Enabled = false;
                    CamposLeitura(true);
                    break;
                case 5: //Status Aguardando AR
                    TbEnvioKit.Visible = true;
                    TbBenefRecusado.Visible = true;
                    TbServico.Visible = true;
                    motivoIndeferido.Visible = true;
                    txtIndMotivo.Visible = true;
                    txtNovoCometario.Visible = true;
                    btnEnviarCap.Enabled = false;
                    txtdtEnvioKit.Enabled = true;
                    txtDtAr.Enabled = true;
                    txtDtMetrofile.Enabled = true;
                    txtCodMetrofile.Enabled = true;
                    btnSavalKit.Visible = true;
                    btnArquivar.Visible = true;
                    btnArquivar.Enabled = (!txtCodMetrofile.Text.Equals("") && !txtDtMetrofile.Text.Equals(""));
                    CamposLeitura(false);
                    break;
                case 6: //Arquivado
                    TbEnvioKit.Visible = true;
                    TbBenefRecusado.Visible = true;
                    TbServico.Visible = true;
                    TbEnvioKit.Visible = true;
                    motivoIndeferido.Visible = !String.IsNullOrEmpty(txtIndMotivo.Text);
                    txtIndMotivo.Visible = !String.IsNullOrEmpty(txtIndMotivo.Text);
                    txtNovoCometario.Visible = false;
                    btnEnviarCap.Enabled = false;

                    txtdtEnvioKit.Enabled = false;
                    txtDtAr.Enabled = false;
                    txtDtMetrofile.Enabled = false;
                    txtCodMetrofile.Enabled = false;
                    btnSavalKit.Visible = false;
                    btnArquivar.Visible = false;

                    CamposLeitura(false);

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

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        #endregion

    }
}