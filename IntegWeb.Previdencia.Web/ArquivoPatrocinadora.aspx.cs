using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace IntegWeb.Previdencia.Web
{

    public partial class ArquivoPatrocinadora : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //init_SSO_Session();

            TbUpload_Mensagem.Visible = false;

            if (!IsPostBack)
            {
                grdArqEnviados.Sort("DTH_INCLUSAO", SortDirection.Ascending);
                grdCriticasLinhas.Sort("NUM_LINHA", SortDirection.Descending);
                grdCriticasTodas.Sort("TIP_CRITICA", SortDirection.Descending);
                //ddlMesRef.SelectedValue = DateTime.Now.Month.ToString("00");
                //ddlAnoRef.SelectedValue = DateTime.Now.Year.ToString();

                if (sso_session != null && sso_session.ListaGrupos != null)
                {
                    ddlGrupoNovo.Items.Clear();
                    List<string> lsGrupos = sso_session.ListaGrupos.Where(g => g.ToUpper().Contains("DOWN")).ToList();
                    foreach (string grupo in lsGrupos)
                    {
                        ddlGrupoNovo.Items.Add(new ListItem(grupo, grupo));
                    }
                    hidGrupos.Value = String.Join(",", lsGrupos);
                }

                Session["lst_uploads"] = null;
                //CarregaTela();

                if (Session["rExibir.SelectedValue"] != null)
                {
                    rExibir.SelectedValue = Session["rExibir.SelectedValue"].ToString();
                    Exibir();
                }

                GrupoAdmPatrocinadoraBLL Grupo_PortalBll = new GrupoAdmPatrocinadoraBLL();
                CarregaDropDowList(ddlGrupo, Grupo_PortalBll.Search("down", null).ToList<object>(), "GRUPO", "GRUPO");
                
                ListItem SELECIONE = ddlGrupo.Items.FindByValue("0");
                CloneDropDownList(ddlGrupo, ddlGrupoNovo);
                SELECIONE.Text = "<TODOS>";
                SELECIONE.Value = "9999";
                hidGrupos.Value = "9999";
            }

            if (ddlGrupoNovo.Items.Count == 1)
            {
                Label5.Style.Add("display", "none");
                ddlGrupoNovo.Style.Add("display", "none");
                grdArqEnviados.Columns[GetColumnIndex(grdArqEnviados, "Grupo Portal")].Visible = false;

            }

            ScriptManager.RegisterStartupScript(upArqPatrocinadoras,
                   upArqPatrocinadoras.GetType(),
                   "script",
                   "chkSelect_click();",
                   true);

            txtDtRepasse.Text = txtDtRepasse.Text.Split(',')[0];
            txtDtCredito.Text = txtDtCredito.Text.Split(',')[0];
        }

        protected void rExibir_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["rExibir.SelectedValue"] = rExibir.SelectedValue;
            Exibir();
            //grdArqEnviados.Visible = (rAgrupa.SelectedValue == "1");
            //grdArqEnviadosPorEmpresa.Visible = (rAgrupa.SelectedValue == "2");
            //FileUploadControl.Visible = grdArqEnviados.Visible;
            //chkSobreporTodos.Visible = grdArqEnviados.Visible;
            //btnValidar.Visible = grdArqEnviados.Visible;
            //btnCarregar.Visible = grdArqEnviados.Visible;
        }

        protected void rAgrupa_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdArqEnviados.Visible = (rAgrupa.SelectedValue == "1");
            grdArqEnviadosPorEmpresa.Visible = (rAgrupa.SelectedValue == "2");
            FileUploadControl.Visible = grdArqEnviados.Visible;
            chkSobreporTodos.Visible = grdArqEnviados.Visible;
            btnValidar.Visible = grdArqEnviados.Visible;
            btnCarregar.Visible = grdArqEnviados.Visible;
        }

        protected void Exibir()
        {
            if (!grdArqEnviados.Visible)
            {
                if (String.IsNullOrEmpty(rExibir.SelectedValue))
                {
                    rExibir.SelectedValue = "1";
                }
                btnValidar.Visible = true;
                //btnExibir.Visible = false;
                btnCarregar.Visible = true;
                //btnDemonstrativo.Visible = true;
            }

            grdArqEnviados.Visible = true;
        }

        protected void btnExibir_Click(object sender, EventArgs e)
        {
            Exibir();
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdArqEnviados.Visible = (rAgrupa.SelectedValue == "1");
            grdArqEnviadosPorEmpresa.Visible = (rAgrupa.SelectedValue == "2");
            btnValidar.Visible = grdArqEnviados.Visible;
            btnCarregar.Visible = grdArqEnviados.Visible;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrocaArquivos.aspx");
        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            List<PRE_TBL_ARQ_PATROCINA> _lst_uploads = null;

            try
            {
                UsuarioPortal user = new UserEngineBLL()
                                        .GetCurrentUser((ConectaAD)Session["objUser"], 
                                                        (Singlesignon)Session["SingleSignOn"]);
                
                //rAgrupa
                if (Session != null && Session["lst_uploads"] != null)
                {
                    ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
                    _lst_uploads = (List<PRE_TBL_ARQ_PATROCINA>)Session["lst_uploads"];
                    bool novos_uploads = false;
                    bool sobreposicoes = false;
                    string lista_sobreposicoes = "";
                    foreach (PRE_TBL_ARQ_PATROCINA ap in _lst_uploads)
                    {
                        DataTable dt = ReadTextFile(ap._CAMINHO_COMPLETO_ARQUIVO, Encoding.GetEncoding("iso-8859-1"));
                        Resultado resDePara = apBLL.DePara(ap, dt, short.Parse(ddlAnoRef.SelectedValue), short.Parse(ddlMesRef.SelectedValue), ddlGrupoNovo.SelectedValue, user.login);
                        if (resDePara.Ok)
                        {
                            PRE_TBL_ARQ_PATROCINA ap_ja_existe = apBLL.GetDataByHASH(ap.NUM_HASH, ddlGrupoNovo.SelectedValue);

                            if (ap_ja_existe != null && 
                                //ap_ja_existe.NOM_ARQUIVO.ToUpper().Equals(ap.NOM_ARQUIVO.ToUpper()) && 
                                !chkSobreporTodos.Checked)
                            {
                                if (ap_ja_existe.COD_STATUS < 8)
                                {
                                    lista_sobreposicoes += ap.NOM_ARQUIVO + ",";
                                    sobreposicoes = true;
                                }
                                else
                                {
                                    MostraMensagem(TbUpload_Mensagem, "O arquivo "  + ap.NOM_ARQUIVO + " já foi processado e carregado anteriormente. Impossivel enviar novamente.<br>Cód. Status: " + ap.COD_STATUS);
                                }

                            }
                            else
                            {
                                novos_uploads = true;
                                ap._PROCESSADO = true;
                                Resultado res = apBLL.Persistir(ap, ap_ja_existe, ddlGrupoNovo.SelectedValue, user.login);
                                if (!res.Ok)
                                {
                                    MostraMensagem(TbUpload_Mensagem, res.Mensagem);
                                }
                            }
                        }
                        else
                        {
                            MostraMensagem(TbUpload_Mensagem, resDePara.Mensagem, "n_error");
                        }
                    }

                    if (sobreposicoes && !chkSobreporTodos.Checked)
                    {
                        // No inicio do html, logo depois do <form>:
                        ScriptManager.RegisterClientScriptBlock(upArqPatrocinadoras,
                               upArqPatrocinadoras.GetType(),
                               "script",
                               String.Format("pergunta_sobrepor('{0}');", lista_sobreposicoes),
                               true);

                        // No fim do html:
                        //ScriptManager.RegisterStartupScript(upArqPatrocinadoras,
                        //       upArqPatrocinadoras.GetType(),
                        //       "script",
                        //       String.Format("pergunta_sobrepor('{0}');", lista_sobreposicoes),
                        //       true);
                    }

                    if (novos_uploads)
                    {
                        Exibir();
                        grdArqEnviados.DataBind();
                        grdArqEnviadosPorEmpresa.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! O arquivo não pôde ser processado. Motivo:\\n" + Util.GetInnerException(ex), "n_error");
            }
            finally
            {
                FileUploadControl.FileContent.Dispose();
                FileUploadControl.FileContent.Flush();
                if (FileUploadControl.PostedFile!=null)
                {
                    FileUploadControl.PostedFile.InputStream.Flush();
                    FileUploadControl.PostedFile.InputStream.Close();
                }
                LimparUploads(_lst_uploads);
            }
        }

        private void LimparUploads(List<PRE_TBL_ARQ_PATROCINA> _lst_uploads)
        {
            if (_lst_uploads != null && _lst_uploads.Count > 0)
            {
                for (int i = _lst_uploads.Count - 1; i >= 0; i--)
                {
                    if (_lst_uploads[i]._PROCESSADO)
                    {
                        File.Delete(_lst_uploads[i]._CAMINHO_COMPLETO_ARQUIVO);
                        _lst_uploads.RemoveAt(i);
                    }
                }
                if (_lst_uploads.Count == 0)
                {
                    chkSobreporTodos.Checked = false;
                    grdArqEnviados.DataBind();
                    grdArqEnviadosPorEmpresa.DataBind();
                    Session["lst_uploads"] = null;
                }
            }      
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (Session != null && Session["lst_uploads"] != null)
            {
                List<PRE_TBL_ARQ_PATROCINA> _lst_uploads = (List<PRE_TBL_ARQ_PATROCINA>)Session["lst_uploads"];
                _lst_uploads.ForEach(a => { a._PROCESSADO = true; });
                LimparUploads((List<PRE_TBL_ARQ_PATROCINA>)Session["lst_uploads"]);
            }
        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            List<ArqPatrocinadoraDAL.PRE_VIEW_ARQ_PATROCINA> lst_validar = GetRowSelected();            

            if (lst_validar.Count==0)
            {              
                MostraMensagem(TbUpload_Mensagem, "Atenção! Selecione ao menos um arquivo para validar");
                return;
            };

            if (lst_validar.FirstOrDefault(a => (a.TIP_ARQUIVO == 4)) != null)
            {
                if (!ValidarFichaFinanceira()) return;
            }

            if (lst_validar.Count > 0)
            {
                //var user = (ConectaAD)Session["objUser"];
                UsuarioPortal user = new UserEngineBLL()
                        .GetCurrentUser((ConectaAD)Session["objUser"],
                                        (Singlesignon)Session["SingleSignOn"]);
                ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
                Resultado res = new Resultado();
                foreach(PRE_TBL_ARQ_PATROCINA ap in lst_validar)
                {
                    //res = apBLL.Criticar(ap, short.Parse(ddlMesRef.SelectedValue), 
                    //Validação solicitada:
                     res = apBLL.Criticar_etapa_1(ap, short.Parse(ddlMesRef.SelectedValue),
                                             short.Parse(ddlAnoRef.SelectedValue),
                                             txtDtRepasse.Text,
                                             txtDtCredito.Text,
                                             ddlGrupoNovo.SelectedValue,
                                             user);

                     //res = apBLL.Criticar_etapa_2(ap, short.Parse(ddlMesRef.SelectedValue),
                     //                        short.Parse(ddlAnoRef.SelectedValue),
                     //                        txtDtRepasse.Text,
                     //                        txtDtCredito.Text);
                     if (chkImediato.Checked && res.Ok)
                     {
                         //string retorno = apBLL.Processar_Todos_Arquivos_Por_Status(3);
                         apBLL.UpdateStatus(ap.COD_ARQ_PAT, 4);
                         res = apBLL.Criticar_etapa_2(ap, short.Parse(ap.MES_REF.ToString()), short.Parse(ap.ANO_REF.ToString()), txtDtRepasse.Text, txtDtCredito.Text, 0);
                         if (!res.Ok && !res.Alerta)
                         {
                             //Rollback de status:
                             //apBLL.m_DbContext = new PREV_Entity_Conn();
                             apBLL.UpdateStatus(ap.COD_ARQ_PAT, 1);
                         }
                         break;
                     }                     
                }
                if (res.Ok)
                {
                    grdArqEnviados.DataBind();
                    MostraMensagem(TbUpload_Mensagem, res.Mensagem);
                    btnCarregar.Enabled = true;
                }
                else
                {
                    grdArqEnviados.DataBind();
                    MostraMensagem(TbUpload_Mensagem, res.Mensagem, "n_error");
                }
            }
                        
        }

        private void AutoRefreshDaPagina(int segundos)
        {
            ScriptManager.RegisterClientScriptBlock(upArqPatrocinadoras,
                    upArqPatrocinadoras.GetType(),
                    "script",
                    String.Format("self.setInterval('window.location.reload(true)', {0});", segundos * 1000),
                    true);
        }

        protected void btnAjustes_Click(object sender, EventArgs e)
        {

            List<ArqPatrocinadoraDAL.PRE_VIEW_ARQ_PATROCINA> lst_validar = GetRowSelected();

            if ((lst_validar.Count == 0) ||
                (lst_validar.FirstOrDefault(a => a.TIP_ARQUIVO == 4) == null))
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Selecione ao menos um arquivo do tipo FINANCEIRO para gerar o demonstrativo");
                return;
            };

            //if (!ValidarFichaFinanceira()) return;

            //if (lst_validar.FirstOrDefault(a => (a.TIP_ARQUIVO == 4)==null))
            //{
            //    MostraMensagem(TbUpload_Mensagem, "Atenção! Selecione ao menos um arquivo para validar");
            //    return;
            //};

            //var user = (ConectaAD)Session["objUser"];
            UsuarioPortal user = new UserEngineBLL()
                        .GetCurrentUser((ConectaAD)Session["objUser"],
                                        (Singlesignon)Session["SingleSignOn"]);

            if (lst_validar.Count > 0)
            {
                ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
                Resultado res = new Resultado();
                //String sCOD_ARQ_PATS = "";
                //foreach (PRE_TBL_ARQ_PATROCINA ap in lst_validar)
                //{
                //    //res = apBLL.GeraDemonstrativo(ap, DateTime.Parse(txtDtRepasse.Text), DateTime.Parse(txtDtCredito.Text), user.login);                    
                //    sCOD_ARQ_PATS += ap.COD_ARQ_PAT + ",";
                //}

                //sCOD_ARQ_PATS = sCOD_ARQ_PATS.Substring(0, sCOD_ARQ_PATS.Length - 1);

                string sCOD_ARQ_PATS = String.Join(",", lst_validar.Select(c => c.COD_ARQ_PAT));
                string sCOD_EMPRS = String.Join(",", lst_validar.Select(c => c.COD_EMPRS));
                string sCOD_STATUS = String.Join(",", lst_validar.Select(c => c.COD_STATUS));

                hidCodArquivos.Value = sCOD_ARQ_PATS;
                hidCodEmprs.Value = sCOD_EMPRS;
                hidCodStatus.Value = sCOD_STATUS;
                hidParamAcertos.Value = ddlMesRef.SelectedValue + "," + ddlAnoRef.SelectedValue + "," + txtDtRepasse.Text + "," + txtDtCredito.Text;

                Server.Transfer("AcertoDemonstrativo.aspx");

            }

        }

        protected void btnImprimirDemonstrativo_Click(object sender, EventArgs e)
        {

            List<ArqPatrocinadoraDAL.PRE_VIEW_ARQ_PATROCINA> lst_validar = GetRowSelected();

            if ((lst_validar.Count == 0) ||
                (lst_validar.FirstOrDefault(a => a.TIP_ARQUIVO == 4) == null))
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Selecione ao menos um arquivo do tipo FINANCEIRO para gerar o demonstrativo");
                return;
            };

            if (!ValidarFichaFinanceira()) return;

            //if (lst_validar.FirstOrDefault(a => (a.TIP_ARQUIVO == 4)==null))
            //{
            //    MostraMensagem(TbUpload_Mensagem, "Atenção! Selecione ao menos um arquivo para validar");
            //    return;
            //};

            UsuarioPortal user = new UserEngineBLL()
                        .GetCurrentUser((ConectaAD)Session["objUser"],
                                        (Singlesignon)Session["SingleSignOn"]);

            if (lst_validar.Count > 0)
            {
                ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
                Resultado res = new Resultado();
                //String sCOD_ARQ_PATS = "";
                //foreach (PRE_TBL_ARQ_PATROCINA ap in lst_validar)
                //{
                //    //res = apBLL.GeraDemonstrativo(ap, DateTime.Parse(txtDtRepasse.Text), DateTime.Parse(txtDtCredito.Text), user.login);                    
                //    sCOD_ARQ_PATS += ap.COD_ARQ_PAT + ",";
                //}

                string sCOD_ARQ_PATS = String.Join(",", lst_validar.Select(c => c.COD_ARQ_PAT));
                string sCOD_EMPRS = String.Join(",", lst_validar.Select(c => c.COD_EMPRS));
                DownloadDemonstrativo(short.Parse(ddlAnoRef.SelectedValue), short.Parse(ddlMesRef.SelectedValue), DateTime.Parse(txtDtRepasse.Text), DateTime.Parse(txtDtCredito.Text), sCOD_EMPRS, sCOD_ARQ_PATS, ddlGrupoNovo.SelectedValue);
                //grdReembolsoAjustes.DataBind();
            }
        }

        protected void btnCarregar_Click(object sender, EventArgs e)
        {
            List<ArqPatrocinadoraDAL.PRE_VIEW_ARQ_PATROCINA> lst_carregar = GetRowSelected();

            if (lst_carregar.Count == 0)
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Selecione ao menos um arquivo para carregar");
                return;
            };

            if (lst_carregar.FirstOrDefault(a => (a.TIP_ARQUIVO == 4)) != null)
            {
                if (!ValidarFichaFinanceira()) return;
            }

            //var user = (ConectaAD)Session["objUser"];
            UsuarioPortal user = new UserEngineBLL()
                        .GetCurrentUser((ConectaAD)Session["objUser"],
                                        (Singlesignon)Session["SingleSignOn"]);

            if (lst_carregar.Count > 0)
            {
                ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
                Resultado res = new Resultado();
                foreach (PRE_TBL_ARQ_PATROCINA ap in lst_carregar.OrderBy(o => o.PRE_TBL_ARQ_PATROCINA_TIPO.NUM_ORDEM_PROCESSA))
                {
                    //res = apBLL.Carregar(ap, user.login, null, txtDtRepasse.Text);
                    //res = apBLL.Carregar_async(ap.COD_ARQ_PAT, user.login); //Carregamento solicitado
                    res = apBLL.Carregar_async(ap, user.login); //Carregamento solicitado
                    if (chkImediato.Checked && res.Ok)
                    {
                        //string retorno = apBLL.Processar_Todos_Arquivos_Por_Status(6);
                        apBLL.UpdateStatus(ap.COD_ARQ_PAT, 7);
                        res = apBLL.Carregar(ap, ap.LOG_INCLUSAO, null, txtDtRepasse.Text, 0);
                        if (!res.Ok && !res.Alerta)
                        {
                            //Rollback de status:
                            //apBLL.m_DbContext = new PREV_Entity_Conn();
                            apBLL.UpdateStatus(ap.COD_ARQ_PAT, 5);
                        }
                        //Registra_LOG(7, ap.COD_ARQ_PAT, ap.LOG_INCLUSAO);
                        break;
                    }
                }
                if (res.Ok)
                {
                    MostraMensagem(TbUpload_Mensagem, res.Mensagem);
                }
                else
                {
                    MostraMensagem(TbUpload_Mensagem, res.Mensagem,  res.Alerta ? "n_warning" :"n_error");
                }
                grdArqEnviados.DataBind();
            }

        }

        private bool ValidarFichaFinanceira()
        {

            DateTime DtRepasse;
            DateTime DtCredito;

            if (ddlMesRef.SelectedValue=="0")
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Entre com o Mês de refêrencia.");
                return false;
            }

            if (ddlAnoRef.SelectedValue == "0")
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Entre com o Ano de refêrencia.");
                return false;
            }

            if (String.IsNullOrEmpty(txtDtRepasse.Text))
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Entre com a data de repasse.");
                return false;
            }
            else if (!DateTime.TryParse(txtDtRepasse.Text, out DtRepasse))
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Data de Repasse inválida.");
                return false;
            }

            if (String.IsNullOrEmpty(txtDtCredito.Text))
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Entre com a Data de Crédito.");
                return false;
            }
            else if (!DateTime.TryParse(txtDtCredito.Text, out DtCredito))
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Data de Crédito inválida.");
                return false;
            }

            int MesRef = Convert.ToInt32(ddlMesRef.SelectedValue);
            int AnoRef = Convert.ToInt32(ddlAnoRef.SelectedValue);

            if (MesRef > 12) MesRef = 12;

            //if (MesRef != DtRepasse.Month)
            //{
            //    MostraMensagem(TbUpload_Mensagem, "Atenção! Mês Ref. diferente do mês da Data de Repasse.");
            //    return false;
            //}

            if (MesRef != DtCredito.Month)
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Mês Ref. diferente do mês da Data de Crédito.");
                return false;
            }

            //if (AnoRef != DtRepasse.Year)
            //{
            //    MostraMensagem(TbUpload_Mensagem, "Atenção! Ano Ref. diferente do ano da Data de Repasse.");
            //    return false;
            //}

            if (AnoRef != DtCredito.Year)
            {
                MostraMensagem(TbUpload_Mensagem, "Atenção! Ano Ref. diferente do mês da Data de Crédito.");
                return false;
            }

            return true;

        }

        private void DownloadDemonstrativo(short? ANO_REF, 
                                           short? MES_REF, 
                                           DateTime DAT_REPASSE, 
                                           DateTime DAT_CREDITO, 
                                           String COD_EMPRS = "",
                                           String COD_ARQ_PATS = "", 
                                           String GRUPO_PORTAL = "")
        {
            Relatorio rel = new Relatorio();
            RelatorioBLL relBLL = new RelatorioBLL();
            String relatorio_nome = "Rel_Demo_Repasse.rpt";
            rel = relBLL.Listar(relatorio_nome);

            if (String.IsNullOrEmpty(COD_EMPRS))
            {
                COD_EMPRS = "0";
            }

            rel.get_parametro("COD_EMPRS").valor = COD_EMPRS;
            rel.get_parametro("COD_ARQ_PATS").valor = COD_ARQ_PATS;
            rel.get_parametro("GRUPO_PORTAL").valor = GRUPO_PORTAL;
            rel.get_parametro("ANO_REF").valor = ANO_REF.ToString();
            rel.get_parametro("MES_REF").valor = MES_REF.ToString();

            Session[rel.relatorio] = rel;
            AbrirNovaAba(upArqPatrocinadoras, "RelatorioWeb.aspx?Relatorio_nome=" + rel.relatorio + "&PromptParam=false&Popup=true&Alert=false", rel.relatorio);

            //if (ReportCrystal.Visible) ReportCrystal.VisualizaRelatorio();
        }

        protected void btnVoltarCriticas_Click(object sender, EventArgs e)
        {
            //PanelUploadControles.Visible = true;
            PanelUpload.Visible = true;
            PanelCriticas.Visible = false;
            //PanelReembolsoAjustes.Visible = false;
        }

        protected void btnImprimirCriticas_Click(object sender, EventArgs e)
        {
            short TIP_CRITICA;
            short.TryParse(rTIP_CRITICA.SelectedValue, out TIP_CRITICA);
            InicializaRelatorioCriticas(Int32.Parse(hidCOD_ARQ_PAT_CRITICA.Value), TIP_CRITICA);
        }

        private void InicializaRelatorioCriticas(int pCOD_ARQ_PAT, short pTIP_CRITICA)
        {
            Relatorio rel = new Relatorio();
            RelatorioBLL relBLL = new RelatorioBLL();
            String relatorio_nome = "Rel_Critica_Arquivo.rpt";
            rel = relBLL.Listar(relatorio_nome);

            rel.get_parametro("COD_ARQ_PAT").valor = pCOD_ARQ_PAT.ToString();
            rel.get_parametro("TIP_CRITICA").valor = pTIP_CRITICA.ToString();

            Session[rel.relatorio] = rel;
            AbrirNovaAba(upArqPatrocinadoras, "RelatorioWeb.aspx?Relatorio_nome=" + rel.relatorio + "&PromptParam=false&Popup=false&Alert=false", rel.relatorio);

            //if (ReportCrystal.Visible) ReportCrystal.VisualizaRelatorio();
        }

        protected void grdArqEnviados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int COD_ARQ_PAT = 0;
            switch (e.CommandName)
            {
                case "Criticas":
                    string[] Args = e.CommandArgument.ToString().Split(',');
                    hidCOD_ARQ_PAT_CRITICA.Value = Args[0];

                    DataControlField colVerba = grdCriticasTodas.Columns[GetColumnIndex(grdCriticasTodas, "Verba")];
                    colVerba.Visible = true;
                    //grdCriticasTodas.Columns[GetColumnIndex(grdCriticasTodas, "Verba")]

                    if (Args[1] != "4")
                    {
                        colVerba.Visible = false;
                    }

                    rAgrupaCritica.SelectedValue = "1";
                    rTIP_CRITICA.SelectedValue = Args[2];

                    //grdCriticas.Sort("NUM_LINHA", SortDirection.Descending);
                    grdCriticasLinhas.Sort("NUM_LINHA", SortDirection.Descending);
                    grdCriticasTodas.Sort("TIP_CRITICA", SortDirection.Descending);

                    BindGridCriticas();

                    //grdCriticasTodas.DataBind();
                    //grdCriticas.DataBind();
                    //grdCriticasLinhas.DataBind();

                    //PanelUploadControles.Visible = false;
                    PanelUpload.Visible = false;
                    PanelCriticas.Visible = true;
                    break;

                case "Excluir":
                    COD_ARQ_PAT = int.Parse(e.CommandArgument.ToString());
                    ExcluiArqPatrocinadora(COD_ARQ_PAT);
                    break;

                case "Demonstrativo":
                    //int COD_ARQ_PAT = int.Parse(e.CommandArgument.ToString());
                    //GerarDemonstrativo(COD_ARQ_PAT);
                    break;

                case "NotaDebito":
                    Args = e.CommandArgument.ToString().Split(',');
                    GerarNotaDebito(int.Parse(Args[0]), short.Parse(Args[1]), short.Parse(Args[2]), Args[3]);
                    //GerarDemonstrativo(COD_ARQ_PAT);
                    break;

                case "CancelarProcessamento":
                    Args = e.CommandArgument.ToString().Split(',');
                    CancelarProcessamento(int.Parse(Args[0]), short.Parse(Args[1]));
                    break;
                default:
                    break;
            }
        }

        private void ExcluiArqPatrocinadora(int iCOD_ARQ_PAT)
        {
            Resultado res = new Resultado();
            ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
            PRE_TBL_ARQ_PATROCINA ap = apBLL.GetDataByCod(iCOD_ARQ_PAT);
            if (ap != null)
            {
                res = apBLL.Delete(ap);

                if (res.Ok)
                {
                    grdArqEnviados.DataBind();
                    MostraMensagem(TbUpload_Mensagem, res.Mensagem, "n_ok");
                }
                else
                {
                    MostraMensagem(TbUpload_Mensagem, res.Mensagem, "n_error");
                }
            }
        }

        private void CancelarProcessamento(int iCOD_ARQ_PAT, short sCOD_STATUS)
        {
            Resultado res = new Resultado();
            ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();


            if (sCOD_STATUS == 4)
            {
                sCOD_STATUS = 1;
            }
            else if (sCOD_STATUS == 7)
            {
                sCOD_STATUS = 5;
            }

            res = apBLL.UpdateStatus(iCOD_ARQ_PAT, sCOD_STATUS);

            if (res.Ok)
            {
                grdArqEnviados.DataBind();
                MostraMensagem(TbUpload_Mensagem, res.Mensagem, "n_ok");
            }
            else
            {
                MostraMensagem(TbUpload_Mensagem, res.Mensagem, "n_error");
            }
        }

        private void GerarNotaDebito(int iCOD_ARQ_PAT, short sANO_REF, short sMES_REF, string pGRUPO_PORTAL)
        {

            ArqPatrocinaNotaDebitoBLL apBLL = new ArqPatrocinaNotaDebitoBLL();
            Resultado res = apBLL.CarregarNotaDebito(sANO_REF, sMES_REF, pGRUPO_PORTAL);
            //Resultado res = apBLL.ProcessarNotaDebito(sANO_REF, sMES_REF, pGRUPO_PORTAL, true);

            if (res.Ok)
            {
                Relatorio rel = new Relatorio();
                RelatorioBLL relBLL = new RelatorioBLL();
                String relatorio_nome = "Rel_Nota_Debito.rpt";
                rel = relBLL.Listar(relatorio_nome);
                //rel.get_parametro("COD_NOTA_DEBITO").valor = iCOD_ARQ_PAT.ToString();
                rel.get_parametro("COD_NOTA_DEBITO").valor = res.CodigoCriado.ToString();
                rel.get_parametro("ANO_REF").valor = "0"; // sANO_REF.ToString();
                rel.get_parametro("MES_REF").valor = "0"; // sMES_REF.ToString();
                //rel.get_parametro("GRUPO_PORTAL").valor = GRUPO_PORTAL;
                Session[rel.relatorio] = rel;
                AbrirNovaAba(upArqPatrocinadoras, "RelatorioWeb.aspx?Relatorio_nome=" + rel.relatorio + "&PromptParam=false&Popup=true&Alert=false", rel.relatorio);
                grdArqEnviados.DataBind();
                MostraMensagem(TbUpload_Mensagem, "Nota emitida com sucesso!", "n_ok");
            }
            else
            {
                MostraMensagem(TbUpload_Mensagem, res.Mensagem, "n_error");
            }
        }

        //private void GerarDemonstrativo(int iCOD_ARQ_PAT)
        //{

        //    ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
        //    PRE_TBL_ARQ_PATROCINA ArqPat = apBLL.GetDataByCod(iCOD_ARQ_PAT);
        //    if (ArqPat != null)
        //    {

        //    }
        //    var user = (ConectaAD)Session["objUser"];

        //    if (lst_carregar.Count > 0)
        //    {
        //        ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
        //        Resultado res = new Resultado();
        //        foreach (PRE_TBL_ARQ_PATROCINA ap in lst_carregar)
        //        {
        //            //res = apBLL.Criticar(ap, txtDtRepasse.Text);
        //            res = apBLL.Carregar(ap, (user != null) ? user.login : "DESENV");
        //        }
        //        if (res.Ok)
        //        {
        //            grdArqEnviados.DataBind();
        //            MostraMensagem(TbUpload_Mensagem, res.Mensagem);
        //        }
        //        else
        //        {
        //            MostraMensagem(TbUpload_Mensagem, res.Mensagem, "n_error");
        //        }
        //    }
        //}

        protected void grdArqEnviados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == 0)
                {
                    CheckBox chkSelect = (e.Row.FindControl("chkSelect") as CheckBox);

                    short COD_STATUS;
                    short.TryParse(chkSelect.Attributes["Cod_Status"], out COD_STATUS);

                    short[] aStatus = new short[] {3,4,6,7};

                    if (Array.IndexOf(aStatus, COD_STATUS) > -1)
                    {
                        //chkSelect.Style.Add("display", "none");
                        chkSelect.Enabled = false;
                        chkSelect.ToolTip = "Arquivo em fila de processamento. Favor aguardar conclusão.";
                        AutoRefreshDaPagina(60);
                    }

                    Label lstCriticas = (Label)e.Row.FindControl("lstAcoes");
                    int COD_ARQ_PAT = (int)grdArqEnviados.DataKeys[e.Row.RowIndex].Value;
                    ArqPatrocinadoraBLL ArqPatBLL = new ArqPatrocinadoraBLL();

                    string strCrit = "";
                    List<PRE_TBL_ARQ_PATROCINA_LOG> lsLOG = ArqPatBLL.GetLogBy(COD_ARQ_PAT);
                    foreach (PRE_TBL_ARQ_PATROCINA_LOG log in lsLOG)
                    {
                        strCrit += String.Format("<tr><td>{0}</td><td>{1}</td></tr>", Util.Date2String(log.DTH_INCLUSAO), log.DCR_ACAO);
                    }
                    if (lsLOG.Count() > 0)
                    {
                        lstCriticas.Text = String.Format("<table>{0}</table>", strCrit);
                    }
                }
            }

        }

        protected void rAgrupaCritica_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridCriticas();
        }

        protected void rTIP_CRITICA_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridCriticas();
        }

        private void BindGridCriticas()
        {
            grdCriticas.PageIndex = 0;
            grdCriticasLinhas.PageIndex = 0;
            grdCriticasTodas.PageIndex = 0;
            grdCriticas.Visible = (rAgrupaCritica.SelectedValue == "2");
            grdCriticasLinhas.Visible = (rAgrupaCritica.SelectedValue == "2");
            grdCriticasTodas.Visible = (rAgrupaCritica.SelectedValue == "1");
            if (grdCriticas.Visible) grdCriticasTodas.DataBind();
            if (grdCriticasLinhas.Visible) grdCriticasLinhas.DataBind();
            if (grdCriticasTodas.Visible) grdCriticasTodas.DataBind();
        }

        protected void grdCriticasLinhas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) == 0)
                {
                    Label lstCriticas = (Label)e.Row.FindControl("lstCriticas");
                    PRE_TBL_ARQ_PATROCINA_LINHA retDebConta = (PRE_TBL_ARQ_PATROCINA_LINHA)e.Row.DataItem;
                    string strCrit = "";
                    foreach (PRE_TBL_ARQ_PATROCINA_CRITICA crit in retDebConta.PRE_TBL_ARQ_PATROCINA_CRITICA)
                    {
                        strCrit += String.Format("<li>{0} - {1}</li>", crit.COD_CRITICA, crit.DCR_CRITICA);
                    }
                    lstCriticas.Text = String.Format("<ul style='margin: 0px;'>{0}</ul>", strCrit);

                    Label lblCritica = (Label)e.Row.FindControl("lblCritica");
                    retDebConta = (PRE_TBL_ARQ_PATROCINA_LINHA)e.Row.DataItem;
                    strCrit = "";
                    PRE_TBL_ARQ_PATROCINA_CRITICA crit2 = retDebConta.PRE_TBL_ARQ_PATROCINA_CRITICA.FirstOrDefault();
                    if (crit2 != null)
                    {
                        strCrit += String.Format("{0} - {1}", crit2.COD_CRITICA, crit2.DCR_CRITICA);
                        lblCritica.Text = strCrit;
                    }

                }
            }

        }

        private List<ArqPatrocinadoraDAL.PRE_VIEW_ARQ_PATROCINA> GetRowSelected()
        {
            List<ArqPatrocinadoraDAL.PRE_VIEW_ARQ_PATROCINA> lst_validar = new List<ArqPatrocinadoraDAL.PRE_VIEW_ARQ_PATROCINA>();
            ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
            foreach (GridViewRow row in grdArqEnviados.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSelect = (row.FindControl("chkSelect") as CheckBox);
                    if (chkSelect.Checked)
                    {
                        int iCOD_ARQ_PAT = int.Parse(grdArqEnviados.DataKeys[row.RowIndex].Value.ToString());

                        PRE_TBL_ARQ_PATROCINA ArqPat = apBLL.GetDataByCod(iCOD_ARQ_PAT);
                        if (ArqPat != null)
                        {
                            ArqPatrocinadoraDAL.PRE_VIEW_ARQ_PATROCINA newViewArqPat = new ArqPatrocinadoraDAL.PRE_VIEW_ARQ_PATROCINA(ArqPat);
                            newViewArqPat.DAT_REPASSE = Util.String2Date(chkSelect.Attributes["DAT_REPASSE"]);
                            newViewArqPat.DAT_CREDITO = Util.String2Date(chkSelect.Attributes["DAT_CREDITO"]);
                            newViewArqPat.COD_EMPRS = chkSelect.Attributes["COD_EMPRS"];
                            lst_validar.Add(newViewArqPat);
                        }
                    }
                }
            }
            return lst_validar;
        }
    }
}