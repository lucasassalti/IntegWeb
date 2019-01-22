using IntegWeb.Entidades;
using IntegWeb.Intranet.Aplicacao.ENTITY;
using Intranet.Entidades;
using Intranet.Aplicacao;
using Intranet.Aplicacao.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net.Mime;
using IntegWeb.Framework;

namespace IntegWeb.Intranet.Web
{
    public partial class PriorizaChamado : BasePage
    {
        private string sUsuario = "Anônimo";
        private int? _perfil = null;
        private string _cod_supervisor = "504";

        public int? perfil
        {
            get
            {
                return _perfil ?? GetPerfil();                
            }
        }

        private int? GetPerfil()
        {
            _perfil = 0;
            switch (sUsuario.ToUpper())
            {
                case "F02514":
                case "F02527":
                    _perfil = 1;
                    break;
                default:
                    foreach (ListItem liANALISTA in ddlPesqANALISTAS.Items)
                    {
                        if (liANALISTA.Text.IndexOf(sUsuario.ToUpper()) > -1)
                        {
                            _perfil = 2;
                            break;
                        }
                    }
                    break;
            }
            return _perfil;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] aUsers = User.Identity.Name.Split('\\');
            if (aUsers.Length > 0)
            {
                sUsuario = aUsers.Last().ToUpper();
            }

            if (!IsPostBack || Session["MostrarObs"] == null)
            {

                lblUsuario.Text = sUsuario;
                UsuariosBLL userBLL = new UsuariosBLL();
                CarregaDropDowList(ddlPesqANALISTAS,
                                   userBLL.ListarCustom("Desenvolvimento de Sistemas"), "NOME", "ID_USUARIO");
                ddlPesqANALISTAS.Items[0].Text = "<TODOS>";

                Session["MostrarObs"] = false;                                    

                if (perfil > 0)
                {
                    grdPriorizaChamados.Columns[GetColumnIndex(grdPriorizaChamados,"")].Visible = true;
                    if (perfil == 1)
                    {
                        btnNovo.Visible = true;
                        btnEmailAlerta.Visible = true;
                    }
                }

            }
            else
            {
                if (Request.Form["__EVENTTARGET"].IndexOf("ddlPesqANALISTAS") > -1 ||
                    Request.Form["__EVENTTARGET"].IndexOf("ddlPesqSTATUS") > -1 ||
                    Request.Form["__EVENTTARGET"].IndexOf("ddlSiglaArea") > -1)
                {
                    grdPriorizaChamados.EditIndex = -1;
                    grdPriorizaChamados.PageIndex = 0; 
                }
            }

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (txtNumChamado.Text.Equals("") &&
                ddlPesqSTATUS.SelectedValue.Equals("0") &&
                ddlSiglaArea.SelectedValue.Equals("0") &&
                ddlPesqANALISTAS.SelectedValue.Equals("0"))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Prencha um campo de pesquisa para continuar");
            }
            else grdPriorizaChamados.PageIndex = 0; 
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            btnLimpar_Click(sender, e);
            grdPriorizaChamados.ShowFooter = true;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNumChamado.Text = "";
            ddlPesqSTATUS.SelectedValue = "0";
            ddlSiglaArea.SelectedValue = "0";
            ddlPesqANALISTAS.SelectedValue = "0";
            grdPriorizaChamados.PageIndex = 0; 
            grdPriorizaChamados.EditIndex = -1;
            grdPriorizaChamados.ShowFooter = false;
        }

        protected void btnEmailAlerta_Click(object sender, EventArgs e)
        {
            PriorizaChamadoBLL pcBLL = new PriorizaChamadoBLL();
            foreach(VW_PRIORIZACHAMADO pc in pcBLL.GetWhere(txtNumChamado.Text, 
                                                                  ddlSiglaArea.SelectedValue, 
                                                                  ddlPesqSTATUS.SelectedValue, 
                                                                  int.Parse(ddlIdadeStatus.SelectedValue), 
                                                                  ddlPesqANALISTAS.SelectedValue).ToList())
            {                
                if (pc.STATUS != "CONCLUÍDO")
                {
                    UsuariosBLL uBLL = new UsuariosBLL();
                    EnviaEmailDiasStatus(uBLL.Carregar(pc.ID_USUARIO.ToString()), pc.CHAMADO.ToString(), pc.TITULO, pc.STATUS, DateTime.Now.Subtract(((DateTime)pc.DT_INCLUSAO)).Days);
                }
            }
            
        }

        protected void grdPriorizaChamados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdPriorizaChamados.EditIndex = -1;
            //grdPriorizaChamados.PageIndex = 0; 
        }

        protected void grdPriorizaChamados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdPriorizaChamados.EditIndex = e.NewEditIndex;
        }

        protected void grdPriorizaChamados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    //TextBox txtCHAMADO = (TextBox)e.Row.FindControl("txtChamado");
                    DropDownList ddlArea = (DropDownList)e.Row.FindControl("ddlAREA");
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlANALISTA");
                    HiddenField hidID_USUARIO = (HiddenField)e.Row.FindControl("hidID_USUARIO");
                    HiddenField hidAREA = (HiddenField)e.Row.FindControl("hidAREA");
                    DropDownList ddlSTATUS = (DropDownList)e.Row.FindControl("ddlSTATUS");
                    //HiddenField hidSTATUS = (HiddenField)e.Row.FindControl("hidSTATUS");

                    CloneDropDownList(ddlPesqANALISTAS, ddList);
                    ddList.SelectedValue = hidID_USUARIO.Value;
                    ddList.Items[0].Text = "";
                    ddList.Items[0].Value = null;

                    CloneDropDownList(ddlSiglaArea, ddlArea);
                    ddlArea.SelectedValue = hidAREA.Value;
                    ddlArea.Items[0].Text = "";
                    ddlArea.Items[0].Value = null;

                    TextBox txtOBS = (TextBox)e.Row.FindControl("txtOBS");
                    txtOBS.Attributes.Add("maxlength", "300");

                    if (bool.Parse(Session["MostrarObs"].ToString())  &&
                         !String.IsNullOrEmpty(((TextBox)e.Row.FindControl("txtOBS")).Text))
                    {
                        //((TextBox)e.Row.FindControl("txtOBS")).Visible = true;
                        txtOBS.Style["display"] = "inherit";
                        ((Button)e.Row.FindControl("btObs")).Visible = false;
                    }

                    ddlSTATUS.Attributes.Add("onchange", "if($('#ddlSTATUS').val()=='PAUSADO') { $('#btObs').hide(); $('#txtOBS').show(); }");

                    if (perfil != 1)
                    {
                        //ddlSTATUS.Items.FindByValue("DESPRIORIZADO").Enabled = false;
                        if (ddlSTATUS.SelectedValue == "DESPRIORIZADO")
                        {
                            ddlSTATUS.Enabled = false;
                            ((TextBox)e.Row.FindControl("txtDT_TERMINO")).BorderStyle = BorderStyle.None;
                            ((TextBox)e.Row.FindControl("txtDT_TERMINO")).ReadOnly = true;
                            e.Row.Cells[6].Enabled = false;
                        }
                        ((TextBox)e.Row.FindControl("txtTITULO")).BorderStyle = BorderStyle.None;
                        ((TextBox)e.Row.FindControl("txtTITULO")).ReadOnly = true;
                        ddlArea.BorderStyle = BorderStyle.None;
                        ddList.BorderStyle = BorderStyle.None;
                        ((TextBox)e.Row.FindControl("txtDT_INCLUSAO")).BorderStyle = BorderStyle.None;
                        ((TextBox)e.Row.FindControl("txtDT_INCLUSAO")).ReadOnly = true;
                        e.Row.Cells[1].Enabled = false;
                        e.Row.Cells[2].Enabled = false;
                        e.Row.Cells[3].Enabled = false;
                        e.Row.Cells[5].Enabled = false;
                    }
                }
                else
                {

                    //string strObs = ((Label)e.Row.FindControl("txtOBS")).Text;
                    //strObs = strObs.Replace("\n", "<br>");
                    //((ImageButton)e.Row.FindControl("lnkOBS")).OnClientClick = String.Format("$('#lblObs').html('{0}');", strObs);
                    //((ImageButton)e.Row.FindControl("lnkOBS")).OnClientClick = "$('#lblObs').html('" + @strObs + "');";

                    if (bool.Parse(Session["MostrarObs"].ToString()))
                    {
                        //((Label)e.Row.FindControl("lblOBS")).Visible = true;
                        ((Label)e.Row.FindControl("lblOBS")).Style["display"] = "inherit";
                        ((Button)e.Row.FindControl("btObs")).Visible = false;
                    } else if (!String.IsNullOrEmpty(((Label)e.Row.FindControl("lblOBS")).Text))
                    {
                        ((Button)e.Row.FindControl("btObs")).Visible = true;
                    }
                    
                    if (perfil == 1)
                    {
                        ((Button)e.Row.FindControl("btEditar")).Visible = true;
                        ((Button)e.Row.FindControl("btExcluir")).Visible = true;
                    } else if (perfil == 2) {
                        Label lblANALISTA = (Label)e.Row.FindControl("lblANALISTA");
                        ((Button)e.Row.FindControl("btEditar")).Visible = (lblANALISTA.Text.IndexOf(sUsuario.ToUpper()) > -1);              
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                TextBox txtCHAMADO = (TextBox)e.Row.FindControl("txtChamado");
                DropDownList ddList = (DropDownList)e.Row.FindControl("ddlANALISTA");
                DropDownList ddlArea = (DropDownList)e.Row.FindControl("ddlAREA");
                TextBox txtDT_INCLUSAO = (TextBox)e.Row.FindControl("txtDT_INCLUSAO");
                DropDownList ddlSTATUS = (DropDownList)e.Row.FindControl("ddlSTATUS");

                txtCHAMADO.Text = "";

                CloneDropDownList(ddlPesqANALISTAS, ddList);
                ddList.Items[0].Text = "";
                ddList.Items[0].Value = null;
                ddList.SelectedValue = null;

                CloneDropDownList(ddlSiglaArea, ddlArea);
                ddlArea.Items[0].Text = "";
                ddlArea.Items[0].Value = null;
                ddlArea.SelectedValue = null;

                if (perfil != 1)
                {
                    ddlSTATUS.Items.FindByValue("DESPRIORIZADO").Enabled = false;
                }

                txtDT_INCLUSAO.Text = DateTime.Now.ToShortDateString();

                // }
            }
        }

        protected void grdPriorizaChamados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddNew")
            {
                string CHAMADO = ((TextBox)grdPriorizaChamados.FooterRow.FindControl("txtChamado")).Text;
                string TITULO = ((TextBox)grdPriorizaChamados.FooterRow.FindControl("txtTITULO")).Text;
                string AREA = ((DropDownList)grdPriorizaChamados.FooterRow.FindControl("ddlAREA")).SelectedValue;
                string ID_USUARIO = ((DropDownList)grdPriorizaChamados.FooterRow.FindControl("ddlANALISTA")).SelectedValue;
                string STATUS = ((DropDownList)grdPriorizaChamados.FooterRow.FindControl("ddlSTATUS")).SelectedValue;
                string DT_INCLUSAO = ((TextBox)grdPriorizaChamados.FooterRow.FindControl("txtDT_INCLUSAO")).Text;
                string DT_TERMINO = ((TextBox)grdPriorizaChamados.FooterRow.FindControl("txtDT_TERMINO")).Text;
                string OBS = ((TextBox)grdPriorizaChamados.FooterRow.FindControl("txtOBS")).Text;
                if (ValidaCampos(CHAMADO, ID_USUARIO))
                {

                    //odsPriorizaChamados.InsertParameters.Add("CHAMADO", CHAMADO);
                    //odsPriorizaChamados.InsertParameters.Add("TITULO", TITULO);
                    //odsPriorizaChamados.InsertParameters.Add("AREA", AREA);
                    //odsPriorizaChamados.InsertParameters.Add("ID_USUARIO", ID_USUARIO);
                    //odsPriorizaChamados.InsertParameters.Add("STATUS", STATUS);
                    //odsPriorizaChamados.InsertParameters.Add("DT_INCLUSAO", DT_INCLUSAO);
                    //odsPriorizaChamados.InsertParameters.Add("DT_TERMINO", DT_TERMINO);
                    //odsPriorizaChamados.InsertParameters.Add("OBS", OBS);
                    //if (odsPriorizaChamados.Insert() > 0)

                    PriorizaChamadoBLL PriorBLL = new PriorizaChamadoBLL();
                    Resultado res = PriorBLL.InsertData(TITULO,
                                                        AREA,
                                                        int.Parse(ID_USUARIO),
                                                        STATUS,
                                                        DT_INCLUSAO,
                                                        DT_TERMINO,
                                                        OBS,
                                                        decimal.Parse(CHAMADO));

                    if (res.Ok)
                    {
                        UsuariosBLL uBLL = new UsuariosBLL();
                        EnviaEmailPriorizacao(uBLL.Carregar(ID_USUARIO), CHAMADO, TITULO);
                        grdPriorizaChamados.ShowFooter = false;
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir a nova priorização.\\nErro: " + res.Mensagem + "\\n\\n(Contate o Chuck Norris)");
                    }
                }
            }
            else if (e.CommandName == "Update")
            {
                string CHAMADO = ((Label)grdPriorizaChamados.Rows[grdPriorizaChamados.EditIndex].FindControl("lblCHAMADO")).Text;
                string TITULO = ((TextBox)grdPriorizaChamados.Rows[grdPriorizaChamados.EditIndex].FindControl("txtTITULO")).Text;
                TextBox DT_TERMINO = (TextBox)grdPriorizaChamados.Rows[grdPriorizaChamados.EditIndex].FindControl("txtDT_TERMINO");
                string ID_USUARIO = ((DropDownList)grdPriorizaChamados.Rows[grdPriorizaChamados.EditIndex].FindControl("ddlANALISTA")).SelectedValue;
                string STATUS_ANTERIOR = ((HiddenField)grdPriorizaChamados.Rows[grdPriorizaChamados.EditIndex].FindControl("hidSTATUS")).Value;                
                DropDownList ddlSTATUS = (DropDownList)grdPriorizaChamados.Rows[grdPriorizaChamados.EditIndex].FindControl("ddlSTATUS");

                if (ddlSTATUS.SelectedValue!= "CONCLUÍDO" && !String.IsNullOrEmpty(DT_TERMINO.Text))
                {
                    ddlSTATUS.SelectedValue = "CONCLUÍDO";
                }
                
                string STATUS = ddlSTATUS.SelectedValue;                

                if (ValidaCampos(CHAMADO, ID_USUARIO))
                {
                    UsuariosBLL uBLL = new UsuariosBLL();
                    //EnviaEmailPriorizacao(uBLL.Carregar(ID_USUARIO), CHAMADO, TITULO);
                    if (!STATUS.Equals(STATUS_ANTERIOR))
                    {

                        if (STATUS == "CONCLUÍDO" && String.IsNullOrEmpty(DT_TERMINO.Text))
                        {
                            DT_TERMINO.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        }

                        if (STATUS == "PAUSADO")
                        {
                            Session["MostrarObs"] = true;
                        }

                        EnviaEmailConclusaoChamado(uBLL.Carregar(_cod_supervisor), uBLL.Carregar(ID_USUARIO), CHAMADO, TITULO, STATUS, STATUS_ANTERIOR);
                    }
                }
            }
            else if (e.CommandName == "CancelAdd")
            {
                grdPriorizaChamados.ShowFooter = false;
            }
        }

        private bool ValidaCampos(string CHAMADO, string ID_USUARIO)
        {
            if (String.IsNullOrEmpty(CHAMADO))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Nº Chamado é obrigatório.\\n");
                return false;
            }

            if (String.IsNullOrEmpty(ID_USUARIO))
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nCampo Analista é obrigatório.\\n");
                return false;
            }

            return true;
        }

        private void EnviaEmailDiasStatus(FUN_TBL_USUARIO Usuario, string CHAMADO, string TITULO, string STATUS, int iDias)
        {
            string emailRemetente = "IntegraWeb Funcesp <atendimento@funcesp.com.br>";
            string emailAssunto = "ALERTA DE TEMPO DECORRIDO DE CHAMADO: " + CHAMADO + " - " + TITULO;

            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;
            TimeSpan periodo_dia = new TimeSpan(12, 0, 0);

            string str_periodo_dia = "";

            if (horarioAtual < periodo_dia)
                str_periodo_dia = "Bom Dia ";
            else
                str_periodo_dia = "Boa Tarde ";

            string emailCorpo = "<p style='font-family:Verdana; font-size:12px'>" +
                                str_periodo_dia + Usuario.NOME.Split(' ')[0] + ",<br/><br/>" +
                                "Você tem um chamado priorizado, com status " + STATUS + " a <span style='font-weight: bold; font-size:14px'>" + iDias.ToString() + " dias</span>: Nº " + CHAMADO + " - " + TITULO + ".<br/>" +
                                "Lembre-se que o acúmulo/atraso de priorizações impactará na meta anual.<br/>" +
                                "<a href='http://intradesenv/Desenv/PriorizaChamado.aspx'>Clique aqui</a> para acessar a tela de priorização de chamados.<br/><br/>" +
                                "ATENÇÃO! Isso é uma mensagem automática, favor não responder.<br/><br/>" +
                                "Obrigado!</p>";

            //EnviaEmail(emailRemetente, "guilherme.provenzano@funcesp.com.br", emailAssunto, emailCorpo);
            EnviaEmail(emailRemetente, Usuario.EMAIL, emailAssunto, emailCorpo);            

        }

        private void EnviaEmailPriorizacao(FUN_TBL_USUARIO Usuario, string CHAMADO, string TITULO)
        {
            string emailRemetente = "IntegraWeb Funcesp <atendimento@funcesp.com.br>";
            string emailAssunto = "ALERTA DE PRIORIZAÇÃO DE CHAMADO " + CHAMADO + " - " + TITULO;

            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;
            TimeSpan periodo_dia = new TimeSpan(12, 0, 0);

            string str_periodo_dia = "";

            if (horarioAtual < periodo_dia)
                str_periodo_dia = "Bom Dia ";
            else
                str_periodo_dia = "Boa Tarde ";

            string emailCorpo = "<p style='font-family:Verdana; font-size:12px'>" +
                                str_periodo_dia + Usuario.NOME.Split(' ')[0] + ",<br/><br/>" +
                                "Há um chamado priorizado para você de número " + CHAMADO + " - " + TITULO + ".<br/>" +
                                "<a href='http://intradesenv/Desenv/PriorizaChamado.aspx'>Clique aqui</a> para acessar a tela de priorização de chamados.<br/><br/>" +
                                "ATENÇÃO! Isso é uma mensagem automática, favor não responder.<br/><br/>" +
                                "Obrigado!</p>";

            EnviaEmail(emailRemetente, Usuario.EMAIL, emailAssunto, emailCorpo);

        }

        private void EnviaEmailConclusaoChamado(FUN_TBL_USUARIO Supervisor, FUN_TBL_USUARIO Usuario, string CHAMADO, string TITULO, string STATUS, string STATUS_ANTERIOR)
        {
            string emailRemetente = "IntegraWeb Funcesp <atendimento@funcesp.com.br>";
            string emailAssunto = "ALERTA DE ALTERAÇÃO NA PRIORIDADE DO CHAMADO: " + CHAMADO + " - " + TITULO;

            TimeSpan horarioAtual = DateTime.Now.TimeOfDay;
            TimeSpan periodo_dia = new TimeSpan(12, 0, 0);

            string str_periodo_dia = "";

            if (horarioAtual < periodo_dia)
                str_periodo_dia = "Bom Dia ";
            else
                str_periodo_dia = "Boa Tarde ";

            string emailCorpo = "<p style='font-family:Verdana; font-size:12px'>" +
                                str_periodo_dia + Supervisor.NOME.Split(' ')[0] + ",<br/><br/>" +
                                "O seguinte chamado foi marcado como '" + STATUS.ToUpper() + "' pelo usuário " + Usuario.NOME + ":</p>" +
                                "<p style='font-family:Verdana; font-size:12px; font-weight: bold;'>" + CHAMADO + " - " + TITULO +
                                "<br/><span style='font-family:Verdana; font-size: 9px; font-weight: normal;'>(STATUS ANTERIOR: '" + STATUS_ANTERIOR + "')</span></p>" +
                                "<p style='font-family:Verdana; font-size:12px'><a href='http://intradesenv/Desenv/PriorizaChamado.aspx'>Clique aqui</a> para acessar a tela de priorização de chamados.<br/><br/>" +
                                "ATENÇÃO! Isso é uma mensagem automática, favor não responder.<br/><br/>" +
                                "Obrigado!</p>";

            EnviaEmail(emailRemetente, Supervisor.EMAIL, emailAssunto, emailCorpo);

        }

        private void EnviaEmail(string Remetente, string Para, string Assunto, string Corpo)
        {
            using (var message = new MailMessage(Remetente, Para))
            {
                try
                {                    
                    //---------------------------------------------------------------------
                    //---------------------------------------------------------------------
                    message.IsBodyHtml = true;
                    message.Subject = Assunto;
                    message.Body = Corpo;
                    // ENVIAR COM CÓPIA
                    // MailAddress copy = new MailAddress("");
                    // message.CC.Add(copy);

                    // ENVIAR COM COPIA OCULTA
                    // MailAddress bcc = new MailAddress("");
                    // message.Bcc.Add(bcc);

                    new Email().EnviaEmailMensagem(message);
                    //MostraMensagemTelaUpdatePanel(UpdatePanel, "E-Mail enviado com sucesso");
                }
                catch (Exception ex)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Atenção\\n\\nO E-mail NÃO foi enviado.\\nMotivo:\\n" + ex.Message);
                }
            }
        }
    }
}