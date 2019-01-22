using IntegWeb.Entidades;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Saude.Aplicacao.DAL;

namespace IntegWeb.Saude.Web
{
    public partial class ControleUnimed : BasePage
    {

        #region .: Propriedades :.

        string relatorio_nome_etq = "EtiquetasUnimed";
        string relatorio_titulo = "Relatório Etiquetas Unimed";
        string relatorio_caminho_etq = @"~/Relatorios/ETQ_unimed.rpt";


        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ControleUnimedBLL bll = new ControleUnimedBLL();
                var user = (ConectaAD)Session["objUser"];

                ddlNomeUnimed.DataSource = bll.GetUnimed();
                ddlNomeUnimed.DataValueField = "COD_PLANO";
                ddlNomeUnimed.DataTextField = "DES_PLANO";
                ddlNomeUnimed.DataBind();
                ddlNomeUnimed.Items.Insert(0, new ListItem("---Selecione---", ""));

                ddlNomeUnimedAB2.DataSource = bll.GetUnimed();
                ddlNomeUnimedAB2.DataValueField = "COD_PLANO";
                ddlNomeUnimedAB2.DataTextField = "DES_PLANO";
                ddlNomeUnimedAB2.DataBind();
                ddlNomeUnimedAB2.Items.Insert(0, new ListItem("---Selecione---", ""));

                ddlUnimedInc.DataSource = bll.GetUnimed();
                ddlUnimedInc.DataValueField = "COD_PLANO";
                ddlUnimedInc.DataTextField = "DES_PLANO";
                ddlUnimedInc.DataBind();

                ddlUnimedInc.Items.Insert(0, new ListItem("---Selecione---", ""));
                ddlMovimentacao.Items.Insert(0, new ListItem("---Selecione---", ""));
                ddlMovimentacaoAB2.Items.Insert(0, new ListItem("---Selecione---", ""));

                // Retirar após termino desenvolvimento
                //if (user.login == "F02565")
                //{
                //    TbAtualizacaoTabela.Visible = false;
                //}
                //   TbExtracaoRelatorio.Visible = false;
                // 
                //   btnInclusaoLote.Visible = false;

                List<CAD_TBL_CONTROLEUNIMED> list = new List<CAD_TBL_CONTROLEUNIMED>();

                list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();
                DataTable dt = list.ToDataTable();

                grdControleUnimed.DataSource = dt;
                grdControleUnimed.DataBind();



            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (ReportCrystal != null)
            {
                ReportCrystal.RelatorioID = null;
                ReportCrystal = null;
            }
        }


        #region .: Aba 1 :.

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            ControleUnimedBLL bll = new ControleUnimedBLL();

            List<CAD_TBL_CONTROLEUNIMED> list = new List<CAD_TBL_CONTROLEUNIMED>();

            list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();
            DataTable dt = list.ToDataTable();

            grdControleUnimed.DataSource = dt;
            grdControleUnimed.DataBind();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtEmpresa.Text = "";
            txtMatricula.Text = "";
            txtSubMatricula.Text = "";
            txtNome.Text = "";
            txtCodIdentificacao.Text = "";
            ddlNomeUnimed.SelectedValue = "";
            ddlMovimentacao.SelectedValue = "";
            txtDatMovimentacaoIni.Text = "";
            txtDatMovimentacaoFim.Text = "";
            txtDatSaidaIni.Text = "";
            txtDatSaidaFim.Text = "";
           

            ControleUnimedBLL bll = new ControleUnimedBLL();

            List<CAD_TBL_CONTROLEUNIMED> list = new List<CAD_TBL_CONTROLEUNIMED>();

            list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();
            DataTable dt = list.ToDataTable();

            grdControleUnimed.DataSource = dt;
            grdControleUnimed.DataBind();
        }

        protected void btnInclusaoLote_Click(object sender, EventArgs e)
        {
            divPesquisar.Visible = false;
            divInclusaoLote.Visible = true;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            divPesquisar.Visible = true;
            divInclusaoLote.Visible = false;
            btnLimpar_Click(null, null);
            grdControleUnimed.DataBind();
        }

        protected void grdControleUnimed_RowEditing(object sender, GridViewEditEventArgs e)
        {

            ControleUnimedBLL bll = new ControleUnimedBLL();

            List<CAD_TBL_CONTROLEUNIMED> list = new List<CAD_TBL_CONTROLEUNIMED>();

            list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();
            DataTable dt = list.ToDataTable();

            grdControleUnimed.DataSource = dt;
            grdControleUnimed.EditIndex = e.NewEditIndex;
            grdControleUnimed.DataBind(); 
        }

        protected void grdControleUnimed_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateAB1")
            {
                ControleUnimedBLL bll = new ControleUnimedBLL();
                CAD_TBL_CONTROLEUNIMED obj = new CAD_TBL_CONTROLEUNIMED();

                obj.COD_CONTROLEUNIMED = Convert.ToInt64(((Label)grdControleUnimed.Rows[grdControleUnimed.EditIndex].FindControl("lblCodControleUnimed")).Text);
                obj.ENVIO_UNIMED = Util.String2Date(((TextBox)grdControleUnimed.Rows[grdControleUnimed.EditIndex].FindControl("txtDatSaida")).Text);
                obj.COBRANCA_MEMORIAL = ((DropDownList)grdControleUnimed.Rows[grdControleUnimed.EditIndex].FindControl("ddlMemorial")).Text;
                obj.NUMERO_UNIMED = Convert.ToString(((TextBox)grdControleUnimed.Rows[grdControleUnimed.EditIndex].FindControl("txtNumeroUnimed")).Text);
                obj.NUMERO_VIA = Util.String2Int32(((TextBox)grdControleUnimed.Rows[grdControleUnimed.EditIndex].FindControl("txtNumeroVia")).Text);

                Resultado res = bll.AtualizaTabelaControleUnimed(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Atualizado com Sucesso");
                    grdControleUnimed.EditIndex = -1;
                    grdControleUnimed.PageIndex = 0;
                    List<CAD_TBL_CONTROLEUNIMED> list = new List<CAD_TBL_CONTROLEUNIMED>();

                    list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();
                    DataTable dt = list.ToDataTable();

                    grdControleUnimed.DataSource = dt;
                    grdControleUnimed.DataBind();
                }
            }
        }

        protected void grdControleUnimed_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            foreach (GridViewRow row in grdControleUnimed.Rows)
            {
                var chkSelect = row.FindControl("chkSelect") as CheckBox;
                IDataItemContainer container = (IDataItemContainer)chkSelect.NamingContainer;

                if (chkSelect.Checked)
                {
                    PersistRowIndex(container.DataItemIndex);
                }
                else
                {
                    RemoveRowIndex(container.DataItemIndex);

                }

            }

            ControleUnimedBLL bll = new ControleUnimedBLL();

            grdControleUnimed.PageIndex = e.NewPageIndex;
            List<CAD_TBL_CONTROLEUNIMED> list = new List<CAD_TBL_CONTROLEUNIMED>();
            list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();

            DataTable dt = list.ToDataTable();

            grdControleUnimed.DataSource = dt;
            grdControleUnimed.DataBind();
            RePopulateCheckBoxes();


        }

        protected void grdControleUnimed_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ControleUnimedBLL bll = new ControleUnimedBLL();

            List<CAD_TBL_CONTROLEUNIMED> list = new List<CAD_TBL_CONTROLEUNIMED>();
            list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();

            DataTable dt = list.ToDataTable();

            grdControleUnimed.DataSource = dt;
            grdControleUnimed.EditIndex = -1;
            grdControleUnimed.DataBind();
            RePopulateCheckBoxes();


        }

        protected void grdControleUnimed_Sorting(object sender, GridViewSortEventArgs e)
        {
            ControleUnimedBLL bll = new ControleUnimedBLL();

            List<CAD_TBL_CONTROLEUNIMED> list = new List<CAD_TBL_CONTROLEUNIMED>();

            list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();

            DataTable dt = list.ToDataTable();

            dt.DefaultView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);

            grdControleUnimed.DataSource = dt;
            grdControleUnimed.DataBind();
            RePopulateCheckBoxes();

        }
    

        protected void btnEdicaoLote_Click(object sender, EventArgs e)
        {
            

            ModalPopupExtender1.Show();

            
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ControleUnimedBLL bll = new ControleUnimedBLL();

            DataTable dt = bll.ListarDadosParaExcel(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text,
                                                    ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text),
                                                    Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text));

            //Download do Excel 
            ArquivoDownload adXlsControleUnimed = new ArquivoDownload();
            adXlsControleUnimed.nome_arquivo = "Controle_Unimed.xls";
            //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
            adXlsControleUnimed.dados = dt;
            Session[ValidaCaracteres(adXlsControleUnimed.nome_arquivo)] = adXlsControleUnimed;
            string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsControleUnimed.nome_arquivo);
            //AdicionarAcesso(fUrl);
            AbrirNovaAba(upUpdatePanel, fUrl, adXlsControleUnimed.nome_arquivo);


        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            Resultado res = new Resultado();

            if (FileUploadControl.HasFile)
            {
                if (FileUploadControl.PostedFile.ContentType.Equals("application/vnd.ms-excel") || // xls
                   FileUploadControl.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))//xlsx
                {
                    string Excel = "";

                    try
                    {
                        string[] name = Path.GetFileName(FileUploadControl.FileName).ToString().Split('.');
                        string UploadFilePath = Server.MapPath("UploadFile\\");
                        Excel = UploadFilePath + name[0] + "." + name[1];

                        if (!Directory.Exists(UploadFilePath))
                        {
                            Directory.CreateDirectory(UploadFilePath);
                        }

                        FileUploadControl.SaveAs(Excel);
                        DataTable dtExcel = ReadExcelFile(Excel);

                        foreach (DataRow drExcelLinha in dtExcel.Rows)
                        {
                            if (!drExcelLinha.Table.Columns.Contains("DT_MOV"))
                            {
                                throw new Exception("A Coluna Data Movimento não existe, Favor verificar");
                            }
                            if (!drExcelLinha.Table.Columns.Contains("COD_EMPRS"))
                            {
                                throw new Exception("A Coluna Empresa não existe, Favor verificar");
                            }
                            if (!drExcelLinha.Table.Columns.Contains("NUM_MATRICULA"))
                            {
                                throw new Exception("A Coluna Matricula não existe, Favor verificar");
                            }
                            if (!drExcelLinha.Table.Columns.Contains("SUB_MATRICULA"))
                            {
                                throw new Exception("A Coluna Sub não existe, Favor verificar");
                            }
                            if (!drExcelLinha.Table.Columns.Contains("COD_IDENTIFICACAO"))
                            {
                                throw new Exception("A Coluna Código Identificação não existe, Favor verificar");
                            }
                            if (!drExcelLinha.Table.Columns.Contains("NOM_PARTICIP"))
                            {
                                throw new Exception("A Coluna Nome não existe, Favor verificar");
                            }
                            if (!drExcelLinha.Table.Columns.Contains("NUMERO_UNIMED"))
                            {
                                throw new Exception("A Coluna Código Unimed não existe, Favor verificar");
                            }
                            if (!drExcelLinha.Table.Columns.Contains("DES_PLANO"))
                            {
                                throw new Exception("A Coluna Nome Unimed não existe, Favor verificar");
                            }
                            //if (!drExcelLinha.Table.Columns.Contains("Acomodação"))
                            //{
                            //    throw new Exception("A Coluna Acomodação não existe, Favor verificar");
                            //}

                            if (!drExcelLinha.Table.Columns.Contains("TIPO"))
                            {
                                throw new Exception("A Coluna Tipo Movimento não existe, Favor verificar");
                            }

                            //if (!drExcelLinha.Table.Columns.Contains("Código Plano Saúde"))
                            //{
                            //    throw new Exception("A Coluna Código Plano Saúde não existe, Favor verificar");
                            //}
                        }


                        foreach (DataRow drExcel in dtExcel.Rows)
                        {
                            CAD_TBL_CONTROLEUNIMED obj = new CAD_TBL_CONTROLEUNIMED();
                            ControleUnimedBLL bll = new ControleUnimedBLL();

                            var user = (ConectaAD)Session["objUser"];

                            obj.DAT_GERACAO = Convert.ToDateTime(drExcel["DT_MOV"].ToString());
                            obj.COD_IDENTIFICACAO = drExcel["COD_IDENTIFICACAO"].ToString().Trim();
                            obj.NOM_PARTICIP = drExcel["NOM_PARTICIP"].ToString().ToUpper();
                            //obj.DAT_NASCIMENTO = Util.String2Date(drExcel["DAT_NASCIMENTO"].ToString());
                            //obj.COD_SEXO = drExcel["COD_SEXO"].ToString();
                            //obj.CPF = Util.String2Int64(drExcel["CPF"].ToString());
                            //obj.ACOMODACAO = drExcel["Acomodação"].ToString().ToUpper();
                            obj.DES_PLANO = drExcel["DES_PLANO"].ToString().ToUpper();
                            //obj.ENVIO_UNIMED = Util.String2Date(drExcel["DT_SAIDA"].ToString());
                            obj.TIPO = drExcel["TIPO"].ToString().ToUpper();
                            //obj.DT_HIST = Util.String2Date(drExcel["DT_HIST"].ToString());
                            obj.COD_UNIMED = drExcel["NUMERO_UNIMED"].ToString();
                            //obj.DAT_ADESAO = Util.String2Date(drExcel["DAT_ADESAO"].ToString());
                            obj.COD_PLANO_CESP = bll.GetPlanoFcesp(Util.String2Short(drExcel["COD_EMPRS"].ToString()), drExcel["NUM_MATRICULA"].ToString(), drExcel["SUB_MATRICULA"].ToString());
                            obj.COD_EMPRS = Convert.ToInt16(drExcel["COD_EMPRS"].ToString());
                            obj.NUM_MATRICULA = Convert.ToInt32(drExcel["NUM_MATRICULA"].ToString());
                            obj.SUB_MATRICULA = drExcel["SUB_MATRICULA"].ToString();
                            //obj.DAT_CANCELAMENTO = Util.String2Date(drExcel["DAT_CANCELAMENTO"].ToString());
                            obj.COBRANCA_MEMORIAL = "N";
                            //obj.NUMERO_UNIMED = drExcel["NUMERO_UNIMED"].ToString();
                            //obj.NUMERO_VIA = Util.String2Int32(drExcel["NUMERO_VIA"].ToString());
                            obj.USU_GERACAO = user.login.ToString();

                            res = bll.Inserir(obj);

                            if (!res.Ok)
                            {
                                MostraMensagemTelaUpdatePanel(upUpdatePanel, res.Mensagem);
                                return;
                            }

                        }

                        MostraMensagemTelaUpdatePanel(upUpdatePanel, res.Mensagem);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Atenção carrega arquivos xls ou xlsx");
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Atenção carrega a planilha para continuar");
            }

        }

        protected void btnInclusao_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Show();
        }

        protected void btnEtq_Click(object sender, EventArgs e)
        {
            ControleUnimedBLL bll = new ControleUnimedBLL();   
            
            var user = (ConectaAD)Session["objUser"];

            bll.DeleteTabelaEtq(user.login);

            foreach (GridViewRow row in grdControleUnimed.Rows)
            {
                var chkBox = row.FindControl("chkSelect") as CheckBox;
                IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;

                if (chkBox.Checked)
                {
                    PersistRowIndex(container.DataItemIndex);
                }
                else
                {
                    RemoveRowIndex(container.DataItemIndex);

                }


            }

            List<CAD_TBL_CONTROLEUNIMED> list = new List<CAD_TBL_CONTROLEUNIMED>();
            list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();

            DataTable dt = list.ToDataTable();

            grdControleUnimed.DataSource = dt;
            //grdControleUnimed.EditIndex = -1;
            grdControleUnimed.AllowPaging = false;
           grdControleUnimed.DataBind();
            RePopulateCheckBoxes();


            for (int i = 0; i < grdControleUnimed.Rows.Count; i++)
            {
                CheckBox chkSelect = grdControleUnimed.Rows[i].FindControl("chkSelect") as CheckBox;

                if (chkSelect.Checked)
                {
                    CAD_TBL_CONTROLEUNIMED_ETQ obj = new CAD_TBL_CONTROLEUNIMED_ETQ();

                    obj.COD_EMPRS = Convert.ToInt16(((Label)grdControleUnimed.Rows[i].FindControl("lblCodEmpresa")).Text);
                    obj.NUM_MATRICULA = Convert.ToInt32(((Label)grdControleUnimed.Rows[i].FindControl("lblNumMatricula")).Text);
                    obj.NOM_PARTICIP = Convert.ToString(((Label)grdControleUnimed.Rows[i].FindControl("lblNome")).Text);
                    obj.TIP_MOV = Convert.ToString(((Label)grdControleUnimed.Rows[i].FindControl("lblMovimentacao")).Text);
                    obj.USU_GERACAO = user.login;

                   
                    Resultado res = bll.InserirTabelaEtq(obj);
                }

            }


            if (InicializaRelatorioEtq(user.login))
            {
                ArquivoDownload adRelEtqPdf = new ArquivoDownload();
                adRelEtqPdf.nome_arquivo = relatorio_nome_etq + "_" + DateTime.Now.ToFileTime() + ".pdf";
                adRelEtqPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adRelEtqPdf.nome_arquivo;
                adRelEtqPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                ReportCrystal.ExportarRelatorioPdf(adRelEtqPdf.caminho_arquivo);

                Session[ValidaCaracteres(adRelEtqPdf.nome_arquivo)] = adRelEtqPdf;
                string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adRelEtqPdf.nome_arquivo);
                AdicionarAcesso(fullUrl);
                AbrirNovaAba(upUpdatePanel, fullUrl, adRelEtqPdf.nome_arquivo);

            }

     
            list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();

            DataTable dt1 = list.ToDataTable();

            grdControleUnimed.DataSource = dt1;
            //grdControleUnimed.EditIndex = -1;
            grdControleUnimed.AllowPaging = true;
            grdControleUnimed.DataBind();

        }


        #region .: Pop Up Edição em Lote :.

        protected void btnAtualizarLote_Click(object sender, EventArgs e)
        {
            ControleUnimedBLL bll = new ControleUnimedBLL();
            CAD_TBL_CONTROLEUNIMED obj = new CAD_TBL_CONTROLEUNIMED();


            foreach (GridViewRow row in grdControleUnimed.Rows)
            {
                var chkBox = row.FindControl("chkSelect") as CheckBox;
                IDataItemContainer container = (IDataItemContainer)chkBox.NamingContainer;

                if (chkBox.Checked)
                {
                    PersistRowIndex(container.DataItemIndex);
                }
                else
                {
                    RemoveRowIndex(container.DataItemIndex);

                }

               




            }

            List<CAD_TBL_CONTROLEUNIMED> list = new List<CAD_TBL_CONTROLEUNIMED>();
            list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();

            DataTable dt = list.ToDataTable();

            grdControleUnimed.DataSource = dt;
            //grdControleUnimed.EditIndex = -1;
            grdControleUnimed.AllowPaging = false;
            grdControleUnimed.DataBind();
            RePopulateCheckBoxes();

       
                foreach (GridViewRow row in grdControleUnimed.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        CheckBox chkSelect = (row.FindControl("chkSelect") as CheckBox);

                        if (chkSelect.Checked)
                        {
                            obj.COD_CONTROLEUNIMED = Convert.ToInt64(((Label)row.FindControl("lblCodControleUnimed")).Text);
                            obj.ENVIO_UNIMED = Util.String2Date(txtDatSaidaPopUp.Text);
                            obj.COBRANCA_MEMORIAL = ddlMemorialPopUp.SelectedValue;


                            Resultado res = bll.AtualizaTabelaControleUnimed(obj);

                            if (!res.Ok)
                            {
                                MostraMensagemTelaUpdatePanel(upUpdatePanel, res.Mensagem);
                                grdControleUnimed.EditIndex = -1;
                                grdControleUnimed.PageIndex = 0;
                                grdControleUnimed.DataBind();
                                LimpaCampoPopUp();
                            }
                        }
                    }
                }

            
            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registros Atualizados com Sucesso");
            grdControleUnimed.EditIndex = -1;
            grdControleUnimed.PageIndex = 0;
            grdControleUnimed.DataBind();
            LimpaCampoPopUp();

            list = bll.GetWhere(Util.String2Short(txtEmpresa.Text), Util.String2Int32(txtMatricula.Text), txtSubMatricula.Text, txtNome.Text, txtCodIdentificacao.Text, ddlNomeUnimed.SelectedValue, ddlMovimentacao.SelectedValue, Util.String2Date(txtDatMovimentacaoIni.Text), Util.String2Date(txtDatMovimentacaoFim.Text), Util.String2Date(txtDatSaidaIni.Text), Util.String2Date(txtDatSaidaFim.Text)).ToList();

            DataTable dt1 = list.ToDataTable();

            grdControleUnimed.DataSource = dt1;
            //grdControleUnimed.EditIndex = -1;
            grdControleUnimed.AllowPaging = true;
            grdControleUnimed.DataBind();

        }

        protected void btnCancelarPopUp_Click(object sender, EventArgs e)
        {
            LimpaCampoPopUp();
        }

        #endregion

        #region .: Pop Up Inclusão Individual :.

        protected void btnInserirIn_Click(object sender, EventArgs e)
        {
            ControleUnimedBLL bll = new ControleUnimedBLL();
            CAD_TBL_CONTROLEUNIMED obj = new CAD_TBL_CONTROLEUNIMED();
            var user = (ConectaAD)Session["objUser"];

            obj.DAT_GERACAO = Convert.ToDateTime(txtDataAdesaoInc.Text);
            obj.COD_IDENTIFICACAO = txtIdenticacaoInc.Text;
            obj.NOM_PARTICIP = txtNomeInc.Text.ToUpper();
            obj.DAT_NASCIMENTO = Convert.ToDateTime(txtDatNascInc.Text);
            //obj.COD_SEXO = ddlSexoInc.SelectedValue;
            //obj.CPF = Convert.ToInt64(txtCPFInc.Text);
            obj.ACOMODACAO = ddlAcomodInc.SelectedValue;
            obj.DES_PLANO = ddlUnimedInc.SelectedItem.Text;
            obj.ENVIO_UNIMED = null;
            obj.TIPO = ddlTipoInc.SelectedValue;
            obj.USU_GERACAO = user.login;
            obj.DT_HIST = System.DateTime.Now;
            obj.COD_UNIMED = ddlUnimedInc.SelectedValue;
            //obj.DAT_ADESAO = Convert.ToDateTime(txtDataAdesaoInc.Text);
            obj.COD_PLANO_CESP = bll.GetPlanoFcesp(Util.String2Short(txtEmpresaInc.Text), txtMatriculaInc.Text, txtSubInc.Text);
            obj.COD_EMPRS = Convert.ToInt16(txtEmpresaInc.Text);
            obj.NUM_MATRICULA = Convert.ToInt32(txtMatriculaInc.Text);
            obj.SUB_MATRICULA = txtSubInc.Text;
            obj.DAT_CANCELAMENTO = Util.String2Date(txtDatCancInc.Text);
            obj.COBRANCA_MEMORIAL = "N";
            obj.NUMERO_UNIMED = txtNumeroUnimedInc.Text;
            obj.NUMERO_VIA = Util.String2Int32(txtNumeroViaInc.Text);


            Resultado res = bll.Inserir(obj);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Inserido com Sucesso");
                grdControleUnimed.EditIndex = -1;
                grdControleUnimed.PageIndex = 0;
                grdControleUnimed.DataBind();
                LimpaCampoPopUpInc();

            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, res.Mensagem);
                grdControleUnimed.EditIndex = -1;
                grdControleUnimed.PageIndex = 0;
                grdControleUnimed.DataBind();
                LimpaCampoPopUpInc();
            }
        }

        protected void btnCancelarInc_Click(object sender, EventArgs e)
        {
            LimpaCampoPopUpInc();

        }

        protected void txtSubInc_TextChanged(object sender, EventArgs e)
        {
            ControleUnimedBLL bll = new ControleUnimedBLL();

            lblCodPlanSaude.Text = bll.GetPlanoFcesp(Util.String2Short(txtEmpresaInc.Text), txtMatriculaInc.Text, txtSubInc.Text);

            ModalPopupExtender2.Show();
        }


        #endregion

        #endregion

        #region .: Aba 2 :.

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            ControleUnimedBLL bll = new ControleUnimedBLL();

            DateTime vDatFim = DateTime.Parse(txtDatFim.Text);
            DateTime vDatIni = DateTime.Parse(txtDatIni.Text);
            int vNomeUnimedAB2 = int.Parse(ddlNomeUnimedAB2.SelectedValue);
            string mov = ddlMovimentacaoAB2.SelectedValue == "" ? "TODOS" : ddlMovimentacaoAB2.SelectedValue;
            DataTable dt = new DataTable();


            if (ddlMovimentacaoAB2.SelectedValue == "INCLUSÃO")
            {
                dt = bll.ExtRelInclusao(vNomeUnimedAB2, vDatFim, vDatIni);

            }

            else if (ddlMovimentacaoAB2.SelectedValue == "TROCA")
            {

                dt = bll.ExtRelTroca(vNomeUnimedAB2, vDatFim, vDatIni);

            }

            else if (ddlMovimentacaoAB2.SelectedValue == "SEGUNDA_VIA")
            {
                dt = bll.ExtRelSegVia(vNomeUnimedAB2, vDatFim, vDatIni);

            }

            else if (ddlMovimentacaoAB2.SelectedValue == "CANCELAMENTO")
            {
                dt = bll.ExtRelCancelamento(vNomeUnimedAB2, vDatFim, vDatIni);
            }


            //Download do Excel 
            ArquivoDownload adXlsControleUnimed = new ArquivoDownload();
            adXlsControleUnimed.nome_arquivo = "Controle_Unimed_" + mov + "_" + DateTime.Now.ToString("ddmmyyyy") + ".xls";
            //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
            adXlsControleUnimed.dados = dt;
            Session[ValidaCaracteres(adXlsControleUnimed.nome_arquivo)] = adXlsControleUnimed;
            string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsControleUnimed.nome_arquivo);
            //AdicionarAcesso(fUrl);
            AbrirNovaAba(upUpdatePanel, fUrl, adXlsControleUnimed.nome_arquivo);

        }

        #endregion

        #region .: Aba 3 :.

        protected void grdAtualizaTabela_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var user = (ConectaAD)Session["objUser"];

            if (e.CommandName == "UpdateAB3")
            {
                ControleUnimedBLL bll = new ControleUnimedBLL();
                SAU_TBL_VALORCARTUNIMED obj = new SAU_TBL_VALORCARTUNIMED();

                obj.ID_REG = Convert.ToDecimal(((Label)grdAtualizaTabela.Rows[grdAtualizaTabela.EditIndex].FindControl("lblIdReg")).Text);
                obj.COD_PLANO = (((Label)grdAtualizaTabela.Rows[grdAtualizaTabela.EditIndex].FindControl("lblCodPlanoAB3")).Text);
                obj.DES_PLANO = (((Label)grdAtualizaTabela.Rows[grdAtualizaTabela.EditIndex].FindControl("lblPlanoAB3")).Text);
                obj.INCLUSAO = Convert.ToDecimal(((TextBox)grdAtualizaTabela.Rows[grdAtualizaTabela.EditIndex].FindControl("txtInclusaoAB3")).Text);
                obj.SEGUNDA_VIA = Convert.ToDecimal(((TextBox)grdAtualizaTabela.Rows[grdAtualizaTabela.EditIndex].FindControl("txtSegViaAB3")).Text);
                obj.RENOVACAO = Convert.ToDecimal(((TextBox)grdAtualizaTabela.Rows[grdAtualizaTabela.EditIndex].FindControl("txtRenovacaoAB3")).Text);
                obj.USU_INCLUSAO = user.login;

                Resultado res = bll.AtualizaTabelaCI(obj);
                Resultado res1 = bll.InserirTabelaCI(obj);

                if (res.Ok && res1.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Atualizado com Sucesso");
                    grdAtualizaTabela.EditIndex = -1;
                    grdAtualizaTabela.PageIndex = 0;
                    grdAtualizaTabela.DataBind();
                }
            }
        }

        #endregion

        #region .: Aba 4 :.

        protected void btnParametrizacaoUnimed_Click(object sender, EventArgs e)
        {
            ModalPopupExtender3.Show();
        }

        protected void btnInserirAB4_Click(object sender, EventArgs e)
        {
            ControleUnimedBLL bll = new ControleUnimedBLL();
            CAD_TBL_UNIMEDARQUIVO obj = new CAD_TBL_UNIMEDARQUIVO();
            SAU_TBL_VALORCARTUNIMED objNew = new SAU_TBL_VALORCARTUNIMED();
            var user = (ConectaAD)Session["objUser"];

            // Tabela Parametrização
            obj.COD_PLANO = Convert.ToInt32(txtCodUnimedAB4.Text);
            obj.DES_PLANO = txtDesUnimedAB4.Text.ToUpper();

            //Tabela Valor CI 
            objNew.COD_PLANO = txtCodUnimedAB4.Text;
            objNew.DES_PLANO = txtDesUnimedAB4.Text.ToUpper();
            objNew.INCLUSAO = Util.String2Decimal(txtVlInclusaoAB4.Text) == null ? 0 : Convert.ToDecimal(txtVlInclusaoAB4.Text);
            objNew.SEGUNDA_VIA = Util.String2Decimal(txtVlSegViaAB4.Text) == null ? 0 : Convert.ToDecimal(txtVlSegViaAB4.Text); ;
            objNew.RENOVACAO = Util.String2Decimal(txtVlRenovacaoAB4.Text) == null ? 0 : Convert.ToDecimal(txtVlRenovacaoAB4.Text); ;
            objNew.USU_INCLUSAO = user.login;

            Resultado resUni = bll.InsereTabelaUnimed(obj);
            Resultado resVlCI = bll.InserirTabelaCI(objNew);

            if (resUni.Ok && resVlCI.Ok)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Inserido Com Sucesso");
                grdUnimedExistente.DataBind();
                grdAtualizaTabela.DataBind();
                LimpaCampoPopUpIncUnimed();
            }
            else
            {
                if (!resUni.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, resUni.Mensagem);
                    grdUnimedExistente.DataBind();
                    grdAtualizaTabela.DataBind();
                }
                if (!resVlCI.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, resVlCI.Mensagem);
                    grdUnimedExistente.DataBind();
                    grdAtualizaTabela.DataBind();
                }

            }

        }

        protected void btnCancelarAB4_Click(object sender, EventArgs e)
        {
            LimpaCampoPopUpIncUnimed();
        }

        protected void grdUnimedExistente_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAB4")
            {
                ControleUnimedBLL bll = new ControleUnimedBLL();
                CAD_TBL_UNIMEDARQUIVO obj = new CAD_TBL_UNIMEDARQUIVO();
                SAU_TBL_VALORCARTUNIMED objNew = new SAU_TBL_VALORCARTUNIMED();

                obj.COD_PLANO = Convert.ToDecimal(e.CommandArgument);
                objNew.COD_PLANO = e.CommandArgument.ToString();

                Resultado resDelete = bll.DeleteUnimedExistente(obj);
                Resultado resDeleteTabelaCI = bll.DeleteTabelaCI(objNew);

                if (resDelete.Ok && resDeleteTabelaCI.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro Deletado Com Sucesso");
                    grdUnimedExistente.EditIndex = -1;
                    grdUnimedExistente.PageIndex = 0;
                    grdUnimedExistente.DataBind();
                    grdAtualizaTabela.EditIndex = -1;
                    grdAtualizaTabela.PageIndex = 0;
                    grdAtualizaTabela.DataBind();
                }
                else
                {
                    if (!resDelete.Ok)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, resDelete.Mensagem);
                        grdUnimedExistente.EditIndex = -1;
                        grdUnimedExistente.PageIndex = 0;
                        grdUnimedExistente.DataBind();
                        grdAtualizaTabela.EditIndex = -1;
                        grdAtualizaTabela.PageIndex = 0;
                        grdAtualizaTabela.DataBind();
                    }
                    if (!resDeleteTabelaCI.Ok)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, resDeleteTabelaCI.Mensagem);
                        grdUnimedExistente.EditIndex = -1;
                        grdUnimedExistente.PageIndex = 0;
                        grdUnimedExistente.DataBind();
                        grdAtualizaTabela.EditIndex = -1;
                        grdAtualizaTabela.PageIndex = 0;
                        grdAtualizaTabela.DataBind();
                    }
                }
            }

        }

        #endregion

        #endregion

        #region .: Métodos :.

        public void LimpaCampoPopUp()
        {
            txtDatSaidaPopUp.Text = "";
            ddlMemorialPopUp.SelectedValue = "N";
        }

        public void LimpaCampoPopUpInc()
        {
            txtEmpresaInc.Text = "";
            txtMatriculaInc.Text = "";
            txtSubInc.Text = "";
            txtDataAdesaoInc.Text = "";
            txtIdenticacaoInc.Text = "";
            txtNomeInc.Text = "";
            txtCPFInc.Text = "";
            //txtCodPlanSaude.Text = "";
            txtDatNascInc.Text = "";
            txtNumeroViaInc.Text = "";
            txtNumeroUnimedInc.Text = "";
            ddlUnimedInc.SelectedValue = "";
            ddlTipoInc.SelectedValue = "INCLUSÃO";
            lblCodPlanSaude.Text= "";
        }

        public void LimpaCampoPopUpIncUnimed()
        {
            txtCodUnimedAB4.Text = "";
            txtDesUnimedAB4.Text = "";
            txtVlInclusaoAB4.Text = "";
            txtVlSegViaAB4.Text = "";
            txtVlRenovacaoAB4.Text = "";
        }

        private string ConvertSortDirectionToSql(SortDirection sortDirection)
        {
            string newSortDirection = String.Empty;

            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    newSortDirection = "ASC";
                    break;

                case SortDirection.Descending:
                    newSortDirection = "DESC";
                    break;
            }

            return newSortDirection;
        }

        public bool InicializaRelatorioEtq(string user)
        {
            Relatorio relatorio = new Relatorio();
            Session[relatorio_nome_etq] = null;

            relatorio.titulo = relatorio_titulo;
            relatorio.parametros = new List<Parametro>();

            relatorio.parametros.Add(new Parametro() { parametro = "user", valor = user });

            relatorio.arquivo = relatorio_caminho_etq;
            Session[relatorio_nome_etq] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome_etq;
            return true;
        }

        private void PersistRowIndex(int index)
        {

            if (!SelectedUnimedIndex.Exists(i => i == index))
            {

                SelectedUnimedIndex.Add(index);

            }

        }

        private void RemoveRowIndex(int index)
        {
            SelectedUnimedIndex.Remove(index);
        }

        private List<Int32> SelectedUnimedIndex
        {

            get
            {

                if (Session["SELECTED_UNIMED_INDEX"] == null)
                {

                    Session["SELECTED_UNIMED_INDEX"] = new List<Int32>();

                }



                return (List<Int32>)Session["SELECTED_UNIMED_INDEX"];

            }

        }

        private void RePopulateCheckBoxes()
        {

            foreach (GridViewRow row in grdControleUnimed.Rows)
            {

                var chkSelect = row.FindControl("chkSelect") as CheckBox;
                IDataItemContainer container = (IDataItemContainer)chkSelect.NamingContainer;

                if (SelectedUnimedIndex != null)
                {

                    if (SelectedUnimedIndex.Exists(i => i == container.DataItemIndex))
                    {

                        chkSelect.Checked = true;

                    }
                }
            }
        }

      

        #endregion

      
    
    }



}


