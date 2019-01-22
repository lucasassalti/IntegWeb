using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.DAL.Faturamento;
using IntegWeb.Saude.Aplicacao.BLL.Faturamento;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades;
using System.Data;
using System.Data.OleDb;
using System.IO;
using IntegWeb.Framework;

namespace IntegWeb.Saude.Web
{

    public partial class CargaEquipSimpro : BasePage
    {
        #region .: Propriedades :.
        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            Processo_Mensagem.Visible = false;
            if (!IsPostBack)
            {
                CargaEquipSimproBLL bll = new CargaEquipSimproBLL();


                ddlConvenente.DataSource = bll.GetConvenente();
                ddlConvenente.DataValueField = "COD_CONVENENTE";
                ddlConvenente.DataTextField = "NOM_CONVENENTE";
                ddlConvenente.DataBind();
                ddlConvenente.Items.Insert(0, new ListItem("---Selecione---", ""));
                ddlTipoPreco.Items.Insert(0, new ListItem("---Selecione---", ""));

                lblContrat.Visible = false;
                ddlContrat.Visible = false;
                divgGridview.Visible = false;


            }
        }

        #region .: ABA 1 :.

        protected void ddlConvenente_SelectedIndexChanged(object sender, EventArgs e)
        {

            CargaEquipSimproBLL bll = new CargaEquipSimproBLL();

            lblContrat.Visible = true;
            ddlContrat.Visible = true;

            decimal valorddl = Convert.ToDecimal(ddlConvenente.SelectedValue);

            ddlContrat.DataSource = bll.GetCondicaoContratual(valorddl);
            ddlContrat.DataValueField = "COD_TIPO_COND_CONT_VALUE";
            ddlContrat.DataTextField = "COD_DES_TIPO_COND_CONT";
            ddlContrat.DataBind();


        }

        protected void btnProcessar_Click(object sender, EventArgs e)
        {
            //verifica se o campo data é superior a data do banco 
            if (ValidarData(Convert.ToDateTime(txtValidade.Text)) == true)
            {

                // verifica se tem arquivo
                if (fileExcel.HasFile)
                {

                    if (fileExcel.PostedFile.ContentType.Equals("application/vnd.ms-excel") //formato superior ao 2003  
                       || fileExcel.PostedFile.ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))//formato 2012) 
                    {
                        CargaEquipSimproBLL bll = new CargaEquipSimproBLL();

                        string path_distribuicao = "";

                        divgGridview.Visible = true;
                        divInserir.Visible = true;

                        try
                        {

                            string filename = Path.GetFileName(fileExcel.FileName).ToString();

                            string[] name = filename.Split('.');

                            string UploadFilePath = Server.MapPath(@"UploadFile\");

                            path_distribuicao = UploadFilePath + name[0] + "-" + DateTime.Now.ToFileTime() + "." + name[1];

                            //salva o arquivo na pasta
                            fileExcel.SaveAs(path_distribuicao);

                            //Lê o Excel e converte para DataSet
                            DataTable dt = ReadExcelFile(path_distribuicao, 1);


                            dt.Columns["COD_TUSS"].DataType = typeof(String);
                            dt.Columns.Add("taxa_aplicada", typeof(Decimal));
                            dt.Columns.Add("tipo_preco", typeof(String));
                            dt.Columns.Add("dat_validade", typeof(DateTime));

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                String campoCodTuss = dt.Rows[i]["COD_TUSS"].ToString();
                                if (campoCodTuss.Length != 8)
                                {
                                    dt.Rows[i]["COD_TUSS"] = dt.Rows[i]["COD_TUSS"].ToString().PadLeft(10, '0');
                                }

                            }

                            GridEquip.DataSource = dt;

                            foreach (DataRow dr in dt.Rows)
                            {

                                dr["taxa_aplicada"] = txtTaxa.Text;
                                dr["tipo_preco"] = ddlTipoPreco.SelectedValue;
                                dr["dat_validade"] = txtValidade.Text;
                            }

                            GridEquip.DataBind();

                            // joga o equipamentos do excel para tabela temporária
                            Resultado res = new Resultado();

                            res = bll.ImportaDados(dt);


                            bll.GetEquipSimpro();

                        }

                        catch (NullReferenceException ec)
                        {

                            MostraMensagem(Processo_Mensagem, "Atenção! O arquivo não pôde ser carregado. Motivo:\\n" + "O arquivo não contém as informações necessárias ");
                        }
                        catch (Exception ex)
                        {

                            MostraMensagem(Processo_Mensagem, "Atenção! O arquivo não pôde ser carregado. Motivo:\\n" + ex.Message, "n_error");
                        }
                        finally
                        {
                            fileExcel.FileContent.Dispose();
                            fileExcel.FileContent.Flush();
                            fileExcel.PostedFile.InputStream.Flush();
                            fileExcel.PostedFile.InputStream.Close();
                            File.Delete(path_distribuicao);

                        }
                    }
                    else
                    {
                        MostraMensagem(Processo_Mensagem, "Atenção! Carregue apenas arquivos com extensão .xls");
                    }

                }

                else
                {
                    MostraMensagem(Processo_Mensagem, "Atenção! Selecione um Arquivo para continuar");
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Selecione uma data de validade igual ou superior a data do tipo de condição contratrual");
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {

            IncluirEquip();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            ddlConvenente.SelectedValue = "";
            ddlContrat.Visible = false;
            lblContrat.Visible = false;
            ddlTipoPreco.SelectedValue = "";
            txtTaxa.Text = "";
            txtValidade.Text = "";
            GridEquip.DataSource = null;
            GridEquip.DataBind();
            divgGridview.Visible = false;
        }
        #endregion

        #region .: ABA 2 :.
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (gridProcedimentos.Visible == false)
            {
                gridProcedimentos.Visible = true;
            }

            if (divEquip.Visible == true)
            {
                divEquip.Visible = false;
            }

            CargaEquipSimproBLL bll = new CargaEquipSimproBLL();


            gridProcedimentos.DataSource = bll.GetProcedimento(txtMaterial.Text);
            gridProcedimentos.DataBind();
            gridProcedimentos.Columns[0].Visible = false;
            gridProcedimentos.Columns[2].Visible = false;


        }

        protected void gridProcedimentos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "SelectAb")
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                lblMaterial.Text = gridProcedimentos.DataKeys[RowIndex].Values["rcocodprocedimento"].ToString();
                lblDescri.Text = gridProcedimentos.Rows[RowIndex].Cells[1].Text;
                lblRecurso.Text = gridProcedimentos.DataKeys[RowIndex].Values["COD_RECURSO"].ToString();

                CarregaGrid();

                VerificaGrid();
                
                txtDtFim1.Text = "";
                txtDtInicio1.Text = "";
                txtQtde1.Text = "";

                divEquip.Visible = true;
            }
        }

        protected void gridEmb_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            CargaEquipSimproBLL bll = new CargaEquipSimproBLL();
            decimal recurso = Convert.ToDecimal(lblRecurso.Text);
            DateTime? fim = bll.GetLastFimMatMed(recurso);
            DateTime inicio = bll.GetLastInicioMatMed(recurso);

            if (e.CommandName == "UpdateNovo")
            {

                if (Convert.ToDateTime(((TextBox)gridEmb.FooterRow.FindControl("txtDtInicioFooter")).Text) < inicio)
                {
                    if ( ((TextBox)gridEmb.FooterRow.FindControl("txtDtFimFooter")).Text != "")
                    {
                        if (Convert.ToDateTime(((TextBox)gridEmb.FooterRow.FindControl("txtDtFimFooter")).Text) < inicio)
                        {
                            NovoEquipMatMed();
                        }
                        else
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "A Data Fim precisa ser menor que última Data de vigência cadastrada");
                        }
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "A Data Fim precisa ser preenchida");
                    }

                }
                else
                {
                    if (fim != null)
                    {
                        if (Convert.ToDateTime(((TextBox)gridEmb.FooterRow.FindControl("txtDtInicioFooter")).Text) > inicio)
                        {
                            if (String.IsNullOrEmpty(((TextBox)gridEmb.FooterRow.FindControl("txtDtFimFooter")).Text) || Convert.ToDateTime(((TextBox)gridEmb.FooterRow.FindControl("txtDtInicioFooter")).Text) < Convert.ToDateTime(((TextBox)gridEmb.FooterRow.FindControl("txtDtFimFooter")).Text))
                            {
                                NovoEquipMatMed();
                            }
                            else
                            {
                                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Data Fim precisa ser Maior que a Data Início de Vigência! ");
                            }
                        }
                        else
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, " A data de Vigência precisa ser maior que a ultima data de vigência cadastrada");
                        }
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "  A última Data de Vigência não possui uma Data Fim");
                        gridEmb.ShowFooter = false;
                        CarregaGrid();
                    }
                }



            }
            else if (e.CommandName == "UpdateAb")
            {
                if (Convert.ToDateTime(((TextBox)gridEmb.Rows[gridEmb.EditIndex].FindControl("txtDtInicio")).Text) < inicio)
                {
                    if (((TextBox)gridEmb.Rows[gridEmb.EditIndex].FindControl("txtDtFim")).Text != "")
                    {
                        if (Convert.ToDateTime(((TextBox)gridEmb.Rows[gridEmb.EditIndex].FindControl("txtDtFim")).Text) < inicio )
                        {
                            AtualizarEquipMatMed();
                        }
                        else
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "A Data Fim precisa ser menor que última Data de vigência cadastrada");
                        }
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "A Data Fim precisa ser preenchida");
                    }
                }
                else
                {
                     if (String.IsNullOrEmpty(((TextBox)gridEmb.Rows[gridEmb.EditIndex].FindControl("txtDtFim")).Text) || Convert.ToDateTime(((TextBox)gridEmb.Rows[gridEmb.EditIndex].FindControl("txtDtInicio")).Text) < Convert.ToDateTime(((TextBox)gridEmb.Rows[gridEmb.EditIndex].FindControl("txtDtFim")).Text))
                {
                    AtualizarEquipMatMed();
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Data Fim precisa ser Maior que a Data Início de Vigência! ");
                }
            }

                }

               

            else if (e.CommandName == "DeleteAb")
            {
                try
                {


                    SAU_TBL_QTDE_MATMED obj = new SAU_TBL_QTDE_MATMED();

                    Resultado res = new Resultado();
                    DateTime dhNow = DateTime.Now;

                    var user = (ConectaAD)Session["objUser"];
                    GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    int RowIndex = gvr.RowIndex;

                    obj.DTH_ACAO = dhNow;
                    obj.COD_RECURSO = Convert.ToDecimal(lblRecurso.Text);
                    obj.QTD = Convert.ToInt32(((Label)gridEmb.Rows[RowIndex].FindControl("lblEmbalagem")).Text);
                    obj.DAT_INIVIG = Convert.ToDateTime(((Label)gridEmb.Rows[RowIndex].FindControl("lblDtInicio")).Text);
                    obj.COD_ACAO = "D";
                    obj.DESC_USERAPL = "IntegWeb";
                    obj.DESC_USERAPL = "Web";
                    obj.DESC_MAQREDE = "FCESP\\" + System.Environment.MachineName;
                    obj.DESC_MAQTERM = System.Environment.MachineName;
                    obj.DESC_USEROS = user.login;


                    if (((Label)gridEmb.Rows[RowIndex].FindControl("lblDtFim")).Text == "")
                    {
                        obj.DAT_FIMVIG = null;
                    }
                    else
                    {
                        obj.DAT_FIMVIG = Convert.ToDateTime(((Label)gridEmb.Rows[RowIndex].FindControl("lblDtFim")).Text);
                    }
                    res = bll.InserirMatMed(obj);

                    if (res.Ok)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Data de vigência Excluida");
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao tentar Excluir a Data de vigência");
                    }


                    CarregaGrid();

                    gridEmb.ShowFooter = false;

                    VerificaGrid();

                    LimparCampos();

                }
                catch (Exception ex)
                {

                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro : " + ex.Message);

                }
            }
            else if (e.CommandName == "CancelNovo")
            {
                gridEmb.ShowFooter = false;
                CarregaGrid();
            }
        }

        protected void gridEmb_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gridEmb.EditIndex = e.NewEditIndex;
            gridEmb.ShowFooter = false;
            CarregaGrid();
        }

        protected void gridEmb_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridEmb.EditIndex = -1;
            CarregaGrid();
        }

        protected void btnSalvarEquip_Click(object sender, EventArgs e)
        {
            IncluirEquipMatMed();
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            gridEmb.ShowFooter = true;
            gridEmb.EditIndex = -1;
            CarregaGrid();
        }

        protected void btnLimpar_Click1(object sender, EventArgs e)
        {
            Limpar();
        }
        #endregion

        #endregion

        #region .: Métodos :.

        #region .: ABA 1 :.

        public void IncluirEquip()
        {
            CargaEquipSimproBLL bll = new CargaEquipSimproBLL();

            try
            {
                if (bll.GetEquipSimproList().Count() > 0)
                {

                    Resultado res = new Resultado();
                    DateTime dhNow = DateTime.Now;
                    decimal prioridade = 3;
                    int naoInserida = 0;
                    int inserida = 0;

                    var user = (ConectaAD)Session["objUser"];

                    FATTBLRSP newFatPriori = new FATTBLRSP();
                    newFatPriori.COD_TIPO_COND_CONT = Convert.ToDecimal(ddlContrat.SelectedValue.Split('-')[1]);
                    newFatPriori.DAT_VALIDADE = Convert.ToDateTime(ddlContrat.SelectedValue.Split('-')[0]);
                    newFatPriori.RSPDATVALIDINI = Convert.ToDateTime(txtValidade.Text);
                    prioridade = bll.GetMaxPrioridade(newFatPriori);

                    if (prioridade == 0)
                    {
                        prioridade = 2;
                    }

                    foreach (CargaEquipSimproDAL.TB_EQUIP_SIMPRO_view fat in bll.GetEquipSimproList())
                    {
                        string[] ddlvalor;
                        
                        ddlvalor = ddlContrat.SelectedValue.Split('-');

                        prioridade++;

                        FATTBLRSP newFat = new FATTBLRSP();

                        newFat.HDRDATHOR = dhNow.ToString();
                        newFat.HDRCODUSU = user.login;
                        newFat.HDRCODETC = "CargaWeb";
                        newFat.HDRCODPGR = "a_fat";
                        newFat.COD_TIPO_COND_CONT = Convert.ToDecimal(ddlvalor[1]);
                        newFat.DAT_VALIDADE = Convert.ToDateTime(ddlvalor[0]);
                        newFat.RSPDATVALIDINI = fat.DAT_VALIDADE;
                        newFat.RSPTIPPRECO = fat.TIPO_PRECO;
                        newFat.RSPVALTAXA = fat.TAXA_APLICADA;
                        newFat.RSPVALADICAO = 0;
                        newFat.RSPCODSIMPROINI = fat.COD_SIMPRO;
                        newFat.RSPCODSIMPROFIM = fat.COD_SIMPRO;
                        newFat.FSOSEQ = null;
                        newFat.RSPCODMERCADO = null;
                        newFat.RSPCNDCAIXASELECAO = null;

                        newFat.RSPVALPRIORIDADE = prioridade;

                        //prioridade++;

                        res = bll.InserirFattblrsp(newFat);

                        if (res.Ok)
                        {
                            bll.Savechanges();
                            inserida++;

                        }
                        else
                        {
                            naoInserida++;
                        }

                    }

                    IncluirEquipPadrao();
                    
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, inserida + " Equipamentos Inseridos e " + naoInserida + " 'Equipamentos já estão cadastrados'");

                    //limpar campos depois da inserçao
                    ddlConvenente.SelectedValue = "";
                    ddlContrat.Visible = false;
                    lblContrat.Visible = false;
                    ddlTipoPreco.SelectedValue = "";
                    txtTaxa.Text = "";
                    txtValidade.Text = "";
                    GridEquip.DataSource = null;
                    GridEquip.DataBind();
                    divgGridview.Visible = false;


                }
                else
                {

                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Os dados do Excel não foram Processados");
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro : " + ex.Message);
            }
            finally
            {

                bll.DeleteEquipSimpro();

            }
        }

        public bool ValidarData(DateTime datValidini)
        {

            CargaEquipSimproBLL bll = new CargaEquipSimproBLL();

            string[] ddlvalor = ddlContrat.SelectedValue.Split('-');
            decimal codTipoConvenente = Convert.ToDecimal(ddlvalor[1]);
            decimal codConvenente = Convert.ToDecimal(ddlConvenente.SelectedValue);


            if (datValidini >= bll.GetDatValidade(codTipoConvenente, codConvenente))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void IncluirEquipPadrao()
        {

            CargaEquipSimproBLL bll = new CargaEquipSimproBLL();
            Resultado res = new Resultado();
            DateTime dhNow = DateTime.Now;
            var user = (ConectaAD)Session["objUser"];

            string[] ddlvalor;
            ddlvalor = ddlContrat.SelectedValue.Split('-');

            FATTBLRSP newFat = new FATTBLRSP();

            newFat.HDRDATHOR = dhNow.ToString();
            newFat.HDRCODUSU = user.login;
            newFat.HDRCODETC = "CargaWeb";
            newFat.HDRCODPGR = "a_fat";
            newFat.COD_TIPO_COND_CONT = Convert.ToDecimal(ddlvalor[1]);
            newFat.DAT_VALIDADE = Convert.ToDateTime(ddlvalor[0]);
            newFat.RSPDATVALIDINI = Convert.ToDateTime(txtValidade.Text);
            newFat.RSPVALPRIORIDADE = 1;
            newFat.RSPTIPPRECO = "FF";
            newFat.RSPVALTAXA = 1;
            newFat.RSPVALADICAO = 0;
            newFat.RSPCODSIMPROINI = null;
            newFat.RSPCODSIMPROFIM = null;
            newFat.FSOSEQ = null;
            newFat.RSPCODMERCADO = null;
            newFat.RSPCNDCAIXASELECAO = null;

            res = bll.InserirFattblrsp(newFat);

            if (res.Ok)
            {
                bll.Savechanges();

            }

            FATTBLRSP newFat1 = new FATTBLRSP();

            newFat1.HDRDATHOR = dhNow.ToString();
            newFat1.HDRCODUSU = user.login;                //user.login;
            newFat1.HDRCODETC = "CargaWeb";
            newFat1.HDRCODPGR = "a_fat";
            newFat1.COD_TIPO_COND_CONT = Convert.ToDecimal(ddlvalor[1]);
            newFat1.DAT_VALIDADE = Convert.ToDateTime(ddlvalor[0]);
            newFat1.RSPDATVALIDINI = Convert.ToDateTime(txtValidade.Text);
            newFat1.RSPVALPRIORIDADE = 2;
            newFat1.RSPTIPPRECO = "FE";
            newFat1.RSPVALTAXA = 1;
            newFat1.RSPVALADICAO = 0;
            newFat1.RSPCODSIMPROINI = null;
            newFat1.RSPCODSIMPROFIM = null;
            newFat1.FSOSEQ = null;
            newFat1.RSPCODMERCADO = null;
            newFat1.RSPCNDCAIXASELECAO = null;

            res = bll.InserirFattblrsp(newFat1);

            if (res.Ok)
            {
                bll.Savechanges();

            }
        }

        #endregion

        #region .: ABA 2 :.

        public void CarregaGrid()
        {
            CargaEquipSimproBLL bll = new CargaEquipSimproBLL();
            decimal recurso = Convert.ToDecimal(lblRecurso.Text);
            gridEmb.DataSource = bll.GetMatMed(recurso);
            gridEmb.DataBind();
        }

        public void VerificaGrid()
        {
            GridViewEditEventArgs e = new GridViewEditEventArgs(0);

            if (gridEmb.Rows.Count != 0)
            {
                if (gridEmb.Visible == false)
                {
                    gridEmb.Visible = true;
                    tbEquip.Visible = false;
                    btnNovo.Visible = true;
                }

                CarregaGrid();
            }
            else
            {
                btnNovo.Visible = false;
                gridEmb.Visible = false;
                tbEquip.Visible = true;
            }
        }

        public void IncluirEquipMatMed()
        {

            if (String.IsNullOrEmpty(txtDtFim1.Text) || Convert.ToDateTime(txtDtInicio1.Text) < Convert.ToDateTime(txtDtFim1.Text))
            {
                try
                {
                    SAU_TBL_QTDE_MATMED obj = new SAU_TBL_QTDE_MATMED();

                    Resultado res = new Resultado();
                    DateTime dhNow = DateTime.Now;

                    var user = (ConectaAD)Session["objUser"];

                    obj.DTH_ACAO = dhNow;
                    obj.COD_RECURSO = Convert.ToDecimal(lblRecurso.Text);
                    obj.QTD = Convert.ToInt32(txtQtde1.Text);
                    obj.DAT_INIVIG = Convert.ToDateTime(txtDtInicio1.Text);
                    obj.COD_ACAO = "I";
                    obj.DESC_USERAPL = "IntegWeb";
                    obj.DESC_USERAPL = "Web";
                    obj.DESC_MAQREDE = "FCESP\\" + System.Environment.MachineName;
                    obj.DESC_MAQTERM = System.Environment.MachineName;
                    obj.DESC_USEROS = user.login;


                    if (txtDtFim1.Text == "")
                    {
                        obj.DAT_FIMVIG = null;
                    }
                    else
                    {
                        obj.DAT_FIMVIG = Convert.ToDateTime(txtDtFim1.Text);
                    }


                    CargaEquipSimproBLL bll = new CargaEquipSimproBLL();

                    res = bll.InserirMatMed(obj);

                    if (res.Ok)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Equipamento Cadastrado com Sucesso");
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao tentar cadastrar equipamento");
                    }

                    CarregaGrid();
                    VerificaGrid();
                }
                catch (Exception ex)
                {

                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro : " + ex.Message);
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, " Data Fim precisa ser Maior que a Data Início de Vigência!");
            }
        }

        public void AtualizarEquipMatMed()
        {
            try
            {
                CargaEquipSimproBLL bll = new CargaEquipSimproBLL();
                SAU_TBL_QTDE_MATMED obj = new SAU_TBL_QTDE_MATMED();

                Resultado res = new Resultado();
                DateTime dhNow = DateTime.Now;

                var user = (ConectaAD)Session["objUser"];

                obj.DTH_ACAO = dhNow;
                obj.COD_RECURSO = Convert.ToDecimal(lblRecurso.Text);
                obj.QTD = Convert.ToInt32(((TextBox)gridEmb.Rows[gridEmb.EditIndex].FindControl("txtQtde")).Text);
                obj.DAT_INIVIG = Convert.ToDateTime(((TextBox)gridEmb.Rows[gridEmb.EditIndex].FindControl("txtDtInicio")).Text);
                obj.COD_ACAO = "U";
                obj.DESC_USERAPL = "IntegWeb";
                obj.DESC_USERAPL = "Web";
                obj.DESC_MAQREDE = "FCESP\\" + System.Environment.MachineName;
                obj.DESC_MAQTERM = System.Environment.MachineName;
                obj.DESC_USEROS = user.login;


                if (((TextBox)gridEmb.Rows[gridEmb.EditIndex].FindControl("txtDtFim")).Text == "")
                {
                    obj.DAT_FIMVIG = null;
                }
                else
                {
                    obj.DAT_FIMVIG = Convert.ToDateTime(((TextBox)gridEmb.Rows[gridEmb.EditIndex].FindControl("txtDtFim")).Text);
                }
                res = bll.AtualizarMatMed(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Equipamento Alterado com sucesso");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao tentar Alterar o equipamento");
                }

                gridEmb.EditIndex = -1;
                CarregaGrid();
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro : " + ex.Message);
            }
        }

        public void NovoEquipMatMed()
        {
            try
            {
                CargaEquipSimproBLL bll = new CargaEquipSimproBLL();

                SAU_TBL_QTDE_MATMED obj = new SAU_TBL_QTDE_MATMED();

                Resultado res = new Resultado();
                DateTime dhNow = DateTime.Now;

                var user = (ConectaAD)Session["objUser"];

                obj.DTH_ACAO = dhNow;
                obj.COD_RECURSO = Convert.ToDecimal(lblRecurso.Text);
                obj.QTD = Convert.ToInt32(((TextBox)gridEmb.FooterRow.FindControl("txtQtdeFooter")).Text);
                obj.DAT_INIVIG = Convert.ToDateTime(((TextBox)gridEmb.FooterRow.FindControl("txtDtInicioFooter")).Text);
                obj.COD_ACAO = "I";
                obj.DESC_USERAPL = "IntegWeb";
                obj.DESC_USERAPL = "Web";
                obj.DESC_MAQREDE = "FCESP\\" + System.Environment.MachineName;
                obj.DESC_MAQTERM = System.Environment.MachineName;
                obj.DESC_USEROS = user.login;


                if (((TextBox)gridEmb.FooterRow.FindControl("txtDtFimFooter")).Text == "")
                {
                    obj.DAT_FIMVIG = null;
                }
                else
                {
                    obj.DAT_FIMVIG = Convert.ToDateTime(((TextBox)gridEmb.FooterRow.FindControl("txtDtFimFooter")).Text);
                }

                res = bll.InserirMatMed(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Data de Vigência Cadastrada");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro ao tentar cadastrar nova Data de Vigência");
                }

                gridEmb.ShowFooter = false;
                CarregaGrid();

            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro : " + ex.Message);
            }
        }

        public void LimparCampos()
        {
            txtDtFim1.Text = "";
            txtDtInicio1.Text = "";
            txtQtde1.Text = "";
        }

        public void Limpar()
        {
            divEquip.Visible = false;
            gridProcedimentos.DataBind();
            gridProcedimentos.Visible = false;

            gridEmb.DataBind();
            txtMaterial.Text = "";
        }

        #endregion

        #endregion















    }
}