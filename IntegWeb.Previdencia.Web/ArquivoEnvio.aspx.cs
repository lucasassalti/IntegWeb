using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.BLL.Cadastro;
using IntegWeb.Framework;
using System.Collections.Specialized;
using IntegWeb.Entidades;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace IntegWeb.Previdencia.Web
{
    public partial class ArquivoEnvio : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDDLs();
            }
        }

        private void CarregaDDLs()
        {
            ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();
            ListItem SELECIONE = new ListItem();

            //ddlGrupo.DataSource = bll.GetGrupoDdl();
            //ddlGrupo.DataValueField = "COD_GRUPO_EMPRS";
            //ddlGrupo.DataTextField = "DCR_GRUPO_EMPRS";
            //ddlGrupo.DataBind();
            //ddlGrupo.Items.Insert(0, new ListItem("TODOS", ""));
            if (ddlGrupo.Items.Count == 0)
            {
                CarregaDropDowList(ddlGrupo, bll.GetGrupoDdl().ToList<object>(), "DCR_GRUPO_EMPRS", "COD_GRUPO_EMPRS");
                SELECIONE = ddlGrupo.Items.FindByValue("0");
                CloneDropDownList(ddlGrupo, ddlGrupoEnvio);
                SELECIONE.Text = "<TODOS>";
                SELECIONE.Value = "";
                //---
                SELECIONE = ddlGrupoEnvio.Items.FindByValue("0");
                SELECIONE.Text = "";
                SELECIONE.Value = "";
            }

            //ddlStatus.DataSource = bll.GetStatusDdl();
            //ddlStatus.DataValueField = "COD_ARQ_STATUS";
            //ddlStatus.DataTextField = "DCR_ARQ_STATUS";
            //ddlStatus.DataBind();
            //ddlStatus.Items.Insert(0, new ListItem("Todos", ""));

            if (ddlStatus.Items.Count == 0)
            {
                CarregaDropDowList(ddlStatus, bll.GetStatusDdl().ToList<object>(), "DCR_ARQ_STATUS", "COD_ARQ_STATUS");
                SELECIONE = ddlStatus.Items.FindByValue("0");
                CloneDropDownList(ddlStatus, ddlStatusEnvio);
                SELECIONE.Text = "<TODOS>";
                SELECIONE.Value = "";
                //---
                SELECIONE = ddlStatusEnvio.Items.FindByValue("0");
                SELECIONE.Text = "";
                SELECIONE.Value = "";
            }

            //ddlTipoEnvio.DataSource = bll.GetTipoEnvioDdl();
            //ddlTipoEnvio.DataValueField = "COD_ARQ_ENVIO_TIPO";
            //ddlTipoEnvio.DataTextField = "DCR_ARQ_ENVIO_TIPO";
            //ddlTipoEnvio.DataBind();
            //ddlTipoEnvio.Items.Insert(0, new ListItem("Todos", ""));

            if (ddlTipoEnvio.Items.Count == 0)
            {
                CarregaDropDowList(ddlTipoEnvio, bll.GetTipoEnvioDdl().ToList<object>(), "DCR_ARQ_ENVIO_TIPO", "COD_ARQ_ENVIO_TIPO");
                SELECIONE = ddlTipoEnvio.Items.FindByValue("0");
                CloneDropDownList(ddlTipoEnvio, ddlTipoEnvioEnvio);
                SELECIONE.Text = "<TODOS>";
                SELECIONE.Value = "";
                //---
                SELECIONE = ddlTipoEnvioEnvio.Items.FindByValue("0");
                SELECIONE.Text = "";
                SELECIONE.Value = "";
            }

            ConectaAD user = (ConectaAD)Session["objUser"];
            List<ArqPatrocinadoraEnvioBLL.PRE_TBL_ARQ_AREA_View> lista_ddl = bll.GetAreaDdl(user);

            if (ddlArea.Items.Count == 0)
            {
                CarregaDropDowList(ddlArea, lista_ddl.ToList<object>(), "DCR_ARQ_C_AREA_SUB", "COD_ARQ_AREA");
                SELECIONE = ddlArea.Items.FindByValue("0");
                CloneDropDownList(ddlArea, ddlAreaEnvio);
                SELECIONE.Text = "<TODOS>";
                SELECIONE.Value = "";
                //---
                SELECIONE = ddlAreaEnvio.Items.FindByValue("0");
                SELECIONE.Text = "";
                SELECIONE.Value = "";

                if (lista_ddl.Count == 1)
                {
                    ddlArea.Visible = false;
                    lblArea.Visible = false;
                    ddlAreaEnvio.Visible = false;
                    lblArea2.Visible = false;
                    ddlArea.SelectedValue = lista_ddl[0].COD_ARQ_AREA.ToString();
                    ddlAreaEnvio.SelectedValue = ddlArea.SelectedValue;
                    exibeChecklistEnvio(ddlAreaEnvio.SelectedValue);
                }
            }
        }

        #region .: Pesquisa :.
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdEnvio.EditIndex = -1;
            grdEnvio.PageIndex = 0;
            grdEnvio.DataBind();
        }
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos(1);
        }
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();

            grdRepasse.EditIndex = -1;
            grdEnvio.PageIndex = 0;
            grdRepasse.DataSource = null;

            CarregaDDLs();

            LimparCampos(2); //Limpa os campos de inserção
            LimparCampos(3); //Limpa a Gridview e a CheckboxList
            ValidaComando("Novo", 0); //Controla a exibição dos campos para a nova inserção

            ddlAreaEnvio.SelectedValue = ddlArea.SelectedValue;            

            divPesquisa.Visible = false;
            divDetalhesEnvio.Visible = true;
        }
        #endregion        

        #region .: Envio :.
        protected void btnSalvarEnvio_Click(object sender, EventArgs e)
        {
            ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();
            PRE_TBL_ARQ_ENVIO newEnvio = new PRE_TBL_ARQ_ENVIO();
            Resultado res = new Resultado();

            var user = (ConectaAD)Session["objUser"];

            if (ddlStatusEnvio.SelectedValue == "1" && !String.IsNullOrEmpty(hidCodEnvio.Value))
            {
                PRE_TBL_ARQ_ENVIO_HIST envioHist = new PRE_TBL_ARQ_ENVIO_HIST();
                envioHist.COD_ARQ_ENVIO = int.Parse(hidCodEnvio.Value);
                envioHist.COD_ARQ_STATUS = 2;
                envioHist.DTH_INCLUSAO = System.DateTime.Now;
                envioHist.LOG_INCLUSAO = (user != null) ? user.login : "Desenv";
                //envioHist.PRE_TBL_ARQ_ENVIO_STATUS = new PRE_TBL_ARQ_ENVIO_STATUS();
                //envioHist.PRE_TBL_ARQ_ENVIO = newEnvio;
                bll.InsertHistorico(envioHist);
                LimparCampos(2);
                grdEnvio.DataBind();
                divDetalhesEnvio.Visible = false;
                divPesquisa.Visible = true;
            }

            if (ddlTipoEnvioEnvio.SelectedValue == "1") //Tipo relatório
            {
                CheckBoxList chklstRel = new CheckBoxList();
                if (chklstRelCapJoia.Visible) chklstRel = chklstRelCapJoia;
                if (chklstRelCapAutoPatr.Visible) chklstRel = chklstRelCapAutoPatr;
                if (chklstRelCapVol.Visible) chklstRel = chklstRelCapVol;
                if (chklstRelEmprest.Visible) chklstRel = chklstRelEmprest;
                if (chklstRelSaude.Visible) chklstRel = chklstRelSaude;
                if (chklstRelSeguro.Visible) chklstRel = chklstRelSeguro;

                foreach (ListItem check in chklstRel.Items)
                {
                    if (check.Selected)
                    {

                        newEnvio = NovoObjEnvio(0,
                                                Convert.ToInt16(ddlTipoEnvioEnvio.SelectedValue),
                                                Util.String2Short(txtAnoGerarEnvio.Text),
                                                Util.String2Short(txtMesGerarEnvio.Text),
                                                Util.String2Short(ddlAreaEnvio.SelectedValue),
                                                1,
                                                Util.String2Short(ddlGrupoEnvio.SelectedValue),
                                                txtReferenciaEnvio.Text,
                                                System.DateTime.Now,
                                                (user != null) ? user.login : "Desenv");

                        newEnvio.COD_ARQ_SUB_TIPO = int.Parse(check.Value);

                        res = bll.SaveData(newEnvio);

                        if (res.Ok != true)
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro! Entre em contato com o administrador \\n\\n Descrição: " + res.Mensagem);
                        }
                    }
                }

                LimparCampos(2);
                grdEnvio.DataBind();
                divDetalhesEnvio.Visible = false;
                divPesquisa.Visible = true;
            } 
            else if (ddlTipoEnvioEnvio.SelectedValue == "2") //Tipo Arquivo Repasse
            {
                int contador = 0;

                foreach (GridViewRow row in grdRepasse.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkSelect = (row.FindControl("chckRepasse") as CheckBox);
                        int iCOD_ARQ_ENV_REPASSE = (int)grdRepasse.DataKeys[row.RowIndex].Value;

                        if (chkSelect.Checked)
                        {
                            newEnvio = NovoObjEnvio(0,
                                                    Convert.ToInt16(ddlTipoEnvioEnvio.SelectedValue),
                                                    Util.String2Short(txtAnoGerarEnvio.Text),
                                                    Util.String2Short(txtMesGerarEnvio.Text),
                                                    Util.String2Short(ddlAreaEnvio.SelectedValue),
                                                    1,
                                                    Util.String2Short(ddlGrupoEnvio.SelectedValue),
                                                    txtReferenciaEnvio.Text,
                                                    System.DateTime.Now,
                                                    (user != null) ? user.login : "Desenv");

                            newEnvio.COD_ARQ_SUB_TIPO = iCOD_ARQ_ENV_REPASSE;

                            res = bll.SaveData(newEnvio, iCOD_ARQ_ENV_REPASSE); //Util.String2Int32(((Label)row.FindControl("lblCodRepasse")).Text));

                            if (res.Ok != true)
                            {
                                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro! Entre em contato com o administrador \\n\\n Descrição: " + res.Mensagem);
                            }
                            contador++;
                        }
                    }
                }
                if (contador == 0)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Selecione um arquivo de repasse antes de salvar.");
                    grdEnvio.DataBind();
                }
                else
                {
                    if (res.Ok)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Concluído com sucesso!");
                        LimparCampos(2);
                        grdEnvio.DataBind();
                        divDetalhesEnvio.Visible = false;
                        divPesquisa.Visible = true;
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao salvar o envio. " + res.Mensagem);
                    }
                }
            }
            else if (ddlTipoEnvioEnvio.SelectedValue == "3") //Tipo outros arquivos
            {
                if (FileUploadControl.HasFile)
                {
                    //if (FileUploadControl.PostedFile.ContentType.Equals("text/plain"))
                    //{

                        string path = "";

                        try
                        {
                            string filename = Path.GetFileName(FileUploadControl.FileName).ToString();
                            string[] name = filename.Split('.');
                            string UploadFilePath = Server.MapPath("UploadFile\\");

                            path = UploadFilePath + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];

                            if (!Directory.Exists(UploadFilePath))
                            {
                                Directory.CreateDirectory(UploadFilePath);
                            }

                            FileUploadControl.SaveAs(path);
                            //FileUploadControl.PostedFile.InputStream;
                            //DataTable dt = ReadTextFile(path);


                            newEnvio = NovoObjEnvio(0,
                                                    Convert.ToInt16(ddlTipoEnvioEnvio.SelectedValue),
                                                    Util.String2Short(txtAnoGerarEnvio.Text),
                                                    Util.String2Short(txtMesGerarEnvio.Text),
                                                    Util.String2Short(ddlAreaEnvio.SelectedValue),
                                                    null,
                                                    Util.String2Short(ddlGrupoEnvio.SelectedValue),
                                                    txtReferenciaEnvio.Text,
                                                    System.DateTime.Now,
                                                    (user != null) ? user.login : "Desenv");

                            newEnvio.DAT_ARQUIVO = Util.File2Memory(path);
                            newEnvio.DCR_CAMINHO_ARQUIVO = name[0];        
                            newEnvio.DCR_ARQ_EXT = name[1];                            

                            res = bll.SaveData(newEnvio); //Util.String2Int32(((Label)row.FindControl("lblCodRepasse")).Text));

                            if (res.Ok)
                            {
                                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Concluído com sucesso!");
                                LimparCampos(2);
                                grdEnvio.DataBind();
                                divDetalhesEnvio.Visible = false;
                                divPesquisa.Visible = true;
                            }
                            else
                            {
                                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao salvar o envio. " + res.Mensagem);
                            }

                            //string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
                            //string path = Server.MapPath("UploadFile\\") + name[0] + "_" + System.DateTime.Now.ToFileTime() + "." + name[1];
                            //FileUploadControl.SaveAs(path);
                            ////Lê o Excel e converte para DataSet
                            //DataSet ds = ReadExcelFileWork(path);
                            //List<FichaFinanceira> list = ImportDataTableToList(ds);
                            //ficha.InsereVerba(list, out mensagem);
                            //MostraMensagemTelaUpdatePanel(upVerba, mensagem);
                        }
                        catch (Exception ex)
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Atenção\\n\\nO arquivo não pôde ser carregado.\\nMotivo:\\n" + ex.Message);
                        }
                        finally
                        {
                            FileUploadControl.FileContent.Dispose();
                            FileUploadControl.FileContent.Flush();
                            FileUploadControl.PostedFile.InputStream.Flush();
                            FileUploadControl.PostedFile.InputStream.Close();
                        }

                    //}
                    //else
                    //{
                    //    MostraMensagem(lblMensagemImportacao, "Atenção\\n\\nCarregue apenas arquivos texto simples (.txt) ou .CSV!");
                    //}
                }
                else if (FileUploadControl.Visible)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Atenção\\n\\nSelecione um Arquivo para continuar!");
                }
            }
        }

        private PRE_TBL_ARQ_ENVIO NovoObjEnvio(int COD_ARQ_ENVIO,
                                               short COD_ARQ_ENVIO_TIPO,
                                               short? ANO_REF,
                                               short? MES_REF,
                                               short? COD_ARQ_AREA_ORIG,
                                               short? COD_ARQ_AREA_DEST,
                                               short? COD_GRUPO_EMPRS,
                                               string DCR_ARQ_ENVIO,
                                               DateTime DTH_INCLUSAO,
                                               string LOG_INCLUSAO)
        {
            PRE_TBL_ARQ_ENVIO obj = new PRE_TBL_ARQ_ENVIO();
            obj.COD_ARQ_ENVIO = 0; // bll.GetMaxPkEnvio();
            obj.MES_REF = MES_REF;
            obj.ANO_REF = ANO_REF;
            obj.COD_ARQ_AREA_ORIG = COD_ARQ_AREA_ORIG;
            obj.COD_ARQ_AREA_DEST = COD_ARQ_AREA_DEST;
            obj.COD_GRUPO_EMPRS = COD_GRUPO_EMPRS;
            obj.COD_ARQ_ENVIO_TIPO = COD_ARQ_ENVIO_TIPO;
            obj.DCR_ARQ_ENVIO = DCR_ARQ_ENVIO;
            //obj.COD_ARQ_ENV_REPASSE = Util.String2Int32(((Label)row.FindControl("lblCodRepasse")).Text);
            obj.DTH_INCLUSAO = System.DateTime.Now;
            obj.LOG_INCLUSAO = LOG_INCLUSAO;
            return obj;

        }

        protected void btnCancelarEnvio_Click(object sender, EventArgs e)
        {
            //LimparCampos(1);
            LimparCampos(2);
            LimparCampos(3);

            divDetalhesEnvio.Visible = false;
            divPesquisa.Visible = true;
        }
        protected void CarregaEnvio(int codigo, string comando)
        {
            ddlGrupoEnvio.Enabled = false;
            IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraEnvioDAL.PRE_TBL_ARQ_ENVIO_View obj = new IntegWeb.Previdencia.Aplicacao.DAL.Cadastro.ArqPatrocinadoraEnvioDAL.PRE_TBL_ARQ_ENVIO_View();
            ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();

            obj = bll.GetLinha(codigo);

            //ddlGrupoEnvio.DataSource = bll.GetGrupoDdl();
            //ddlGrupoEnvio.DataValueField = "COD_GRUPO_EMPRS";
            //ddlGrupoEnvio.DataTextField = "DCR_GRUPO_EMPRS";
            //ddlGrupoEnvio.DataBind();
            //ddlGrupoEnvio.Items.Insert(0, new ListItem("", ""));

            //ddlTipoEnvioEnvio.DataSource = bll.GetTipoEnvioDdl();
            //ddlTipoEnvioEnvio.DataValueField = "COD_ARQ_ENVIO_TIPO";
            //ddlTipoEnvioEnvio.DataTextField = "DCR_ARQ_ENVIO_TIPO";
            //ddlTipoEnvioEnvio.DataBind();
            //ddlTipoEnvioEnvio.Items.Insert(0, new ListItem("", ""));

            //CarregaDropDowList(ddlArea, bll.GetAreaDdl().ToList<object>(), "DCR_ARQ_C_AREA_SUB", "COD_ARQ_AREA");
            //ListItem SELECIONE = ddlArea.Items.FindByValue("0");
            //CloneDropDownList(ddlArea, ddlAreaEnvio);
            //SELECIONE.Text = "<TODOS>";
            //SELECIONE.Value = "";

            //SELECIONE = ddlAreaEnvio.Items.FindByValue("0");
            //SELECIONE.Text = "";
            //SELECIONE.Value = "";

            ////ddlAreaEnvio.DataSource = bll.GetAreaDdl();
            ////ddlAreaEnvio.DataValueField = "COD_ARQ_AREA";
            ////ddlAreaEnvio.DataTextField = "DCR_ARQ_C_AREA_SUB";
            ////ddlAreaEnvio.DataBind();
            ////ddlAreaEnvio.Items.Insert(0, new ListItem("", ""));

            //ddlGrupoEnvio.SelectedValue = obj.COD_GRUPO_EMPRS.ToString();

            //ddlEmpresaEnvio.DataSource = bll.GetGrupoDdl(Util.String2Short(ddlGrupoEnvio.SelectedValue));
            //ddlEmpresaEnvio.DataValueField = "EMPRESA";
            //ddlEmpresaEnvio.DataTextField = "NOM_ABRVO_EMPRS";
            //ddlEmpresaEnvio.DataBind();
            //ddlEmpresaEnvio.Items.Insert(0, new ListItem("", ""));

            //ddlStatusEnvio.DataSource = bll.GetStatusDdl();
            //ddlStatusEnvio.DataValueField = "COD_ARQ_STATUS";
            //ddlStatusEnvio.DataTextField = "DCR_ARQ_STATUS";
            //ddlStatusEnvio.DataBind();
            //ddlStatusEnvio.Items.Insert(0, new ListItem("", ""));

            CarregaDDLs();

            //Preechimento dos campos
            hidCodEnvio.Value = codigo.ToString();
            txtMesGerarEnvio.Text = obj.MES_REF.ToString();
            txtAnoGerarEnvio.Text = obj.ANO_REF.ToString();
            ddlTipoEnvioEnvio.SelectedValue = obj.COD_ARQ_ENVIO_TIPO.ToString();
            if (ddlAreaEnvio.Items.FindByValue(obj.COD_ARQ_AREA_ORIG.ToString()) != null)
            {
                ddlAreaEnvio.SelectedValue = obj.COD_ARQ_AREA_ORIG.ToString();
            }
            ddlGrupoEnvio.SelectedValue = obj.COD_GRUPO_EMPRS.ToString();
            ddlStatusEnvio.SelectedValue = obj.COD_ARQ_STATUS.ToString();
            txtReferenciaEnvio.Text = obj.DCR_ARQ_ENVIO.ToString();
            chkedItemGrid.Value = (obj.COD_ARQ_SUB_TIPO ?? 0).ToString();

            if (obj.COD_ARQ_STATUS > 1)
            {
                pnlDetalhes.Enabled = false;
                btnSalvarEnvio.Enabled = false;
                btnSalvarEnvioUpload.Enabled = false;
            }
            else
            {
                btnSalvarEnvio.Text = "Enviar";
                btnSalvarEnvioUpload.Text = "Enviar";
            }

            if (ddlTipoEnvioEnvio.SelectedValue == "1") //Se tipo Relatório
            {
                exibePainelTipoEnvio(ddlTipoEnvioEnvio.SelectedValue);
                exibeChecklistEnvio(ddlAreaEnvio.SelectedValue);
            }
            else if (ddlTipoEnvioEnvio.SelectedValue == "2") //Se tipo Arquivo Repasse
            {
                exibePainelTipoEnvio(ddlTipoEnvioEnvio.SelectedValue);
            }
        }
        protected void ddlGrupoEnvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();
            //De acordo com o grupo selecionado a dropdownlist de Empresas é carregada
            short? grupoEmpresa = Util.String2Short(ddlGrupoEnvio.SelectedValue);

            ddlEmpresaEnvio.DataSource = bll.GetGrupoDdl(grupoEmpresa);
            ddlEmpresaEnvio.DataValueField = "EMPRESA";
            ddlEmpresaEnvio.DataTextField = "NOM_ABRVO_EMPRS";
            ddlEmpresaEnvio.DataBind();
            ddlEmpresaEnvio.Items.Insert(0, new ListItem("", ""));
        }        
        protected void grdEnvio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] Arg = e.CommandArgument.ToString().Split(',');

            int iCOD_ARQ_ENVIO = 0;
            int iCOD_ARQ_ENVIO_TIPO = 0;
            int iCOD_ARQ_STATUS = 0;

            if (e.CommandName != "Sort")
            {
                iCOD_ARQ_ENVIO = (Arg.Length) > 0 ? Convert.ToInt32(Arg[0]) : 0;
                iCOD_ARQ_ENVIO_TIPO = (Arg.Length) > 1 ? Convert.ToInt32(Arg[1]) : 0;
                iCOD_ARQ_STATUS = (Arg.Length) > 2 ? Convert.ToInt32(Arg[2]) : 0;

                if (e.CommandName == "Visualizar")
                {
                    ValidaComando(e.CommandName, iCOD_ARQ_ENVIO);//Controla a exibição dos campos e os preenche

                    //GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    //int RowIndex = gvr.RowIndex;

                    //int? codEnvioTipo = Util.String2Int32(Arg[1]);

                    if (iCOD_ARQ_ENVIO_TIPO == 1) //Tipo Relatório
                    {

                    }
                    else if (iCOD_ARQ_ENVIO_TIPO == 2) //Tipo Arquivo Repasse - Marca na gridview o arquivo selecionado para visualização
                    {
                        grdRepasse.DataBind();
                        foreach (GridViewRow row in grdRepasse.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkSelect = (row.FindControl("chckRepasse") as CheckBox);
                                //int? codRepasse = Util.String2Int32(((Label)row.FindControl("lblCodRepasse")).Text);

                                int codEnvio = (int)grdRepasse.DataKeys[row.RowIndex].Value;
                                //int? codEnvio = Util.String2Int32(((Label)row.FindControl("lblCodEnvio")).Text);
                                if (iCOD_ARQ_ENVIO == codEnvio)
                                {
                                    chkSelect.Checked = true;
                                    chkSelect.Enabled = false;
                                }
                                else
                                {
                                    chkSelect.Checked = false;
                                    chkSelect.Enabled = false;
                                }
                            }
                        }
                    }
                    divDetalhesEnvio.Visible = true;
                    divPesquisa.Visible = false;
                }
                else if (e.CommandName == "DeleteEnvio")
                {
                    ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();
                    var user = (ConectaAD)Session["objUser"];
                    Resultado res = bll.ExcluirEnvio(iCOD_ARQ_ENVIO, DateTime.Now, (user != null) ? user.login : "Desenv");
                    if (res.Ok)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Excluido com Sucesso");
                        grdEnvio.PageIndex = 0;
                        grdEnvio.DataBind();
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Ocorreu um erro durante a exclusão.\\nErro: " + res.Mensagem);
                    }
                }
            }
        }

        protected void ddlTipoEnvioEnvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimparCampos(3);//Limpa os campos marcados
            exibePainelTipoEnvio(ddlTipoEnvioEnvio.SelectedValue); //Controla qual painel deve aparecer
            grdRepasse.DataBind();
        }
        protected void ddlAreaEnvio_SelectedIndexChanged(object sender, EventArgs e)
        {
            exibeChecklistEnvio(ddlAreaEnvio.SelectedValue);
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdEnvio.PageIndex = 0;
        }

        protected void chckRepasse_CheckedChanged(object sender, EventArgs e)
        {
            string referencia = txtReferenciaEnvio.Text.Trim(); 

            //Se o campo de referência(título) do arquivo estiver vazio, é preenchido com a descrição do arquivo selecionado. 
            //Também preenche os campos de mes e ano
            foreach (GridViewRow row in grdRepasse.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chckRepasse = (CheckBox)row.FindControl("chckRepasse");
                    Label lblDescricaoArquivo = (Label)row.FindControl("lblDescricaoArquivo");
                    if (chckRepasse.Checked)
                    {
                        if (referencia == "")
                        {
                            txtReferenciaEnvio.Text = lblDescricaoArquivo.Text.ToString(); 
                        }
                        txtMesGerarEnvio.Text = row.Cells[2].Text; 
                        txtAnoGerarEnvio.Text = row.Cells[3].Text;
                        chkedItemGrid.Value = grdRepasse.DataKeys[row.RowIndex].Value.ToString();
                    }
                }
            }
        }
        #endregion        

        #region .: Métodos/Validações :.
        public void LimparCampos(int cod)
        {
            switch (cod)
            {
                case 1: //Limpa campos da Pesquisa
                    txtMesGerar.Text = "";
                    txtAnoGerar.Text = "";
                    ddlGrupo.SelectedValue = "";
                    txtReferencia.Text = "";
                    txtdatIniRef.Text = "";
                    txtdatFimRef.Text = "";
                    ddlStatus.SelectedValue = "";
                    ddlTipoEnvio.SelectedValue = "";

                    grdEnvio.EditIndex = -1;
                    grdEnvio.PageIndex = 0;
                    grdEnvio.DataBind();
                    break;
                case 2: //Limpa campos da Tela de Envio
                    btnSalvarEnvio.Text = "Gravar";
                    btnSalvarEnvioUpload.Text = "Gravar";
                    hidCodEnvio.Value = "";
                    txtMesGerarEnvio.Text = "";
                    txtAnoGerarEnvio.Text = "";
                    ddlTipoEnvioEnvio.SelectedValue = "";
                    ddlGrupoEnvio.SelectedValue = "";
                    ddlEmpresaEnvio.SelectedValue = "";
                    ddlAreaEnvio.SelectedValue = "";
                    txtReferenciaEnvio.Text = "";
                    pnlRelatorios.Visible = false;
                    pnlRelatorios.Enabled = true;
                    pnlRepasse.Visible = false;
                    pnlUpload.Visible = false;
                    foreach (ListItem item in chklstRelCapJoia.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelCapAutoPatr.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelCapVol.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelEmprest.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelSaude.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelSeguro.Items)
                    {
                        item.Selected = false;
                    }
                    break;
                case 3: //Limpa a CheckList de Relatórios e a Gridview de Arquivos de Repasse
                    foreach (ListItem item in chklstRelCapJoia.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelCapAutoPatr.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelCapVol.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelEmprest.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelSaude.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelSeguro.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (GridViewRow row in grdRepasse.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox rdbSelect = (row.FindControl("chckRepasse") as CheckBox);
                            if (rdbSelect.Checked)
                            {
                                rdbSelect.Checked = false;
                            }
                        }
                    }
                    chkedItemGrid.Value = "";
                    break;
            }
        }
        protected void ValidaComando(string comando, int codigo)
        {
            switch (comando)
            {

                case "Visualizar": //Liberação controlada, apenas para visualização
                    ddlTipoEnvioEnvio.Enabled = false;
                    ddlTipoEnvioEnvio.AutoPostBack = false;
                    ddlAreaEnvio.Enabled = false;
                    ddlAreaEnvio.AutoPostBack = false;
                    txtMesGerarEnvio.ReadOnly = true;
                    txtMesGerarEnvio.Enabled = false;
                    txtMesGerarEnvio.AutoPostBack = false;
                    txtAnoGerarEnvio.ReadOnly = true;
                    txtAnoGerarEnvio.Enabled = false;
                    txtAnoGerarEnvio.AutoPostBack = false;
                    ddlGrupoEnvio.Enabled = false;
                    ddlGrupoEnvio.AutoPostBack = false;
                    txtReferenciaEnvio.ReadOnly = true;
                    txtReferenciaEnvio.Enabled = false;
                    pnlDetalhes.Enabled = true;
                    btnSalvarEnvio.Enabled = true;
                    btnSalvarEnvioUpload.Enabled = true;
                    CarregaEnvio(codigo, comando);
                    break;

                case "Novo": //Liberação dos campos para a nova inserção
                    ddlStatusEnvio.Enabled = false;
                    ddlStatusEnvio.SelectedValue = "1";
                    ddlTipoEnvioEnvio.Enabled = true;
                    ddlTipoEnvioEnvio.AutoPostBack = true;
                    ddlAreaEnvio.Enabled = true;
                    ddlAreaEnvio.AutoPostBack = true;
                    txtMesGerarEnvio.ReadOnly = false;
                    txtMesGerarEnvio.Enabled = true;
                    txtMesGerarEnvio.AutoPostBack = true;
                    txtAnoGerarEnvio.ReadOnly = false;
                    txtAnoGerarEnvio.Enabled = true;
                    txtAnoGerarEnvio.AutoPostBack = true;
                    ddlGrupoEnvio.Enabled = true;
                    ddlGrupoEnvio.AutoPostBack = true;
                    txtReferenciaEnvio.ReadOnly = false;
                    txtReferenciaEnvio.Enabled = true;
                    pnlDetalhes.Enabled = true;
                    btnSalvarEnvio.Enabled = true;
                    btnSalvarEnvioUpload.Enabled = true;
                    break;
            }
        }
        protected void exibePainelTipoEnvio(string codigo)
        {
            switch (codigo)
            {
                case "1": //Exibe painel de Relatórios
                    pnlRelatorios.Visible = true;
                    pnlRelatorios.Enabled = true;

                    pnlRepasse.Visible = false;
                    pnlRepasse.Enabled = false;
                    pnlUpload.Visible = false;
                    pnlUpload.Enabled = false;

                    btnSalvarEnvio.Visible = true;
                    btnSalvarEnvioUpload.Visible = false;
                    break;

                case "2": //Exibe painel de Arquivos de repasse

                    pnlRepasse.Visible = true;
                    pnlRepasse.Enabled = true;

                    pnlRelatorios.Visible = false;
                    pnlRelatorios.Enabled = false;
                    pnlUpload.Visible = false;
                    pnlUpload.Enabled = false;

                    btnSalvarEnvio.Visible = true;
                    btnSalvarEnvioUpload.Visible = false;
                    break;

                case "3": //Exibe painel de Upload
                    pnlUpload.Visible = true;
                    pnlUpload.Enabled = true;

                    pnlRelatorios.Visible = false;
                    pnlRelatorios.Enabled = false;
                    pnlRepasse.Visible = false;
                    pnlRepasse.Enabled = false;

                    btnSalvarEnvio.Visible = false;
                    btnSalvarEnvioUpload.Visible = true;
                    break;

                case "": 
                    pnlUpload.Visible = false;
                    pnlUpload.Enabled = false;
                    pnlRelatorios.Visible = false;
                    pnlRelatorios.Enabled = false;
                    pnlRepasse.Visible = false;
                    pnlRepasse.Enabled = false;
                    btnSalvarEnvio.Visible = true;
                    btnSalvarEnvioUpload.Visible = false;
                    break;
            }
            if (ddlAreaEnvio.SelectedValue == "")
            {
                chklstRelCapJoia.Visible = false;
                chklstRelCapAutoPatr.Visible = false;
                chklstRelCapVol.Visible = false;
                chklstRelEmprest.Visible = false;
                chklstRelSaude.Visible = false;
                chklstRelSeguro.Visible = false;
            }
        }
        protected void exibeChecklistEnvio(string codigo)
        {
            //Exibe a checklist selecionada na dropdown de Area, e limpa os campos das demais
            switch (codigo)
            {
                case "3": //Jóia
                case "6": //Folha 
                    chklstRelCapJoia.Visible = true;

                    chklstRelCapAutoPatr.Visible = false;
                    chklstRelCapVol.Visible = false;
                    chklstRelEmprest.Visible = false;
                    chklstRelSaude.Visible = false;
                    chklstRelSeguro.Visible = false;
                    foreach (ListItem item in chklstRelCapJoia.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapAutoPatr.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapVol.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelEmprest.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSaude.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSeguro.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    break;
                case "4":
                    chklstRelCapAutoPatr.Visible = true;

                    chklstRelCapJoia.Visible = false;
                    chklstRelCapVol.Visible = false;
                    chklstRelEmprest.Visible = false;
                    chklstRelSaude.Visible = false;
                    chklstRelSeguro.Visible = false;
                    foreach (ListItem item in chklstRelCapJoia.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapAutoPatr.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapVol.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelEmprest.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSaude.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSeguro.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    break;
                case "5":
                    chklstRelCapVol.Visible = true;

                    chklstRelCapJoia.Visible = false;
                    chklstRelCapAutoPatr.Visible = false;
                    chklstRelEmprest.Visible = false;
                    chklstRelSaude.Visible = false;
                    chklstRelSeguro.Visible = false;
                    foreach (ListItem item in chklstRelCapJoia.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapAutoPatr.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapVol.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelEmprest.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSaude.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSeguro.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    break;
                //case "6":

                //    chklstRelCapJoia.Visible = false;
                //    chklstRelCapAutoPatr.Visible = false;
                //    chklstRelCapVol.Visible = false;
                //    chklstRelEmprest.Visible = false;
                //    chklstRelSaude.Visible = false;
                //    chklstRelSeguro.Visible = false;
                //    foreach (ListItem item in chklstRelCapJoia.Items)
                //    {
                //        item.Selected = false;
                //    }
                //    foreach (ListItem item in chklstRelCapAutoPatr.Items)
                //    {
                //        item.Selected = false;
                //    }
                //    foreach (ListItem item in chklstRelCapVol.Items)
                //    {
                //        item.Selected = false;
                //    }
                //    foreach (ListItem item in chklstRelSaude.Items)
                //    {
                //        item.Selected = false;
                //    }
                //    foreach (ListItem item in chklstRelSeguro.Items)
                //    {
                //        item.Selected = false;
                //    }
                //    break;
                case "7":
                    chklstRelEmprest.Visible = true;

                    chklstRelCapJoia.Visible = false;
                    chklstRelCapAutoPatr.Visible = false;
                    chklstRelCapVol.Visible = false;
                    chklstRelSaude.Visible = false;
                    chklstRelSeguro.Visible = false;
                    foreach (ListItem item in chklstRelCapJoia.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapAutoPatr.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapVol.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelEmprest.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSaude.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSeguro.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    break;
                case "8":
                    chklstRelSaude.Visible = true;

                    chklstRelCapJoia.Visible = false;
                    chklstRelCapAutoPatr.Visible = false;
                    chklstRelCapVol.Visible = false;
                    chklstRelEmprest.Visible = false;
                    chklstRelSeguro.Visible = false;
                    foreach (ListItem item in chklstRelCapJoia.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapAutoPatr.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapVol.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelEmprest.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSaude.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSeguro.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    break;
                case "9":
                    chklstRelSeguro.Visible = true;

                    chklstRelCapJoia.Visible = false;
                    chklstRelCapAutoPatr.Visible = false;
                    chklstRelCapVol.Visible = false;
                    chklstRelEmprest.Visible = false;
                    chklstRelSaude.Visible = false;
                    foreach (ListItem item in chklstRelCapJoia.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapAutoPatr.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelCapVol.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelEmprest.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSaude.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    foreach (ListItem item in chklstRelSeguro.Items)
                    {
                        item.Selected = ((chkedItemGrid.Value ?? "").ToString() == item.Value) ? true : false;
                    }
                    break;

                case "":
                    chklstRelSeguro.Visible = false;
                    chklstRelCapJoia.Visible = false;
                    chklstRelCapAutoPatr.Visible = false;
                    chklstRelCapVol.Visible = false;
                    chklstRelEmprest.Visible = false;
                    chklstRelSaude.Visible = false;
                    foreach (ListItem item in chklstRelCapJoia.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelCapAutoPatr.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelCapVol.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelEmprest.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelSaude.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (ListItem item in chklstRelSeguro.Items)
                    {
                        item.Selected = false;
                    }
                    break;
            }
        }
        #endregion

    }
}