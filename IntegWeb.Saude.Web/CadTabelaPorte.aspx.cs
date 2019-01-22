using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.DAL.Cadastro;
using System.Data;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Framework;

namespace IntegWeb.Saude.Web
{
    public partial class CadTabelaPorte : BasePage
    {
        CadTabelaPorteDAL dao = new CadTabelaPorteDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CarregarDdlConv();
                
                CarregarDdlClasse();


                ddlConv.Items.Insert(0, new ListItem("Selecione o prestador", ""));   
                ddlTipoCadastro.Items.Insert(0, new ListItem("Selecione o Tipo de Cadastro", ""));
                ddlTipoCadastro2.Items.Insert(0, new ListItem("Selecione o Tipo de Consulta", ""));
                ddlClasse.Items.Insert(0, new ListItem("Selecione a Classe", ""));
            }
        }

        protected void TabContainer_ActiveTabChanged(object sender, EventArgs e)
        {
            divConv.Visible = false;
            divClasse.Visible = false;
            divConv2.Visible = false;
            divClasse2.Visible = false;
            divSucesso.Visible = false;
            divCadastro.Visible = true;

            gridViewLista.Visible = false;
            gridViewListaClasse.Visible = false;
            gridViewListaPorte.Visible = false;

            ddlTipoCadastro.SelectedIndex = 0;
            ddlTipoCadastro2.SelectedIndex = 0;

            ddlConv.SelectedIndex = 0;
            //ddlDtVig.SelectedIndex = 0;
            txtIniDatVigencia.Text = "";
            txtFimDatVigencia.Text = "";

            ddlClasse.SelectedIndex = 0;
            //ddlDtVig2.SelectedIndex = 0;
            txtIniDatVigencia2.Text = "";
            txtFimDatVigencia2.Text = "";

            txtCodConv.Text = "";
            txtCodClasse.Text = "";

            txtDtVig.Text = "";

        }


        #region .:ABA 01:.

        #region .:EVENTOS:.

        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {

                var user = (ConectaAD)Session["objUser"];


                    SAU_TBL_CONV_PORTE_VIG obj = new SAU_TBL_CONV_PORTE_VIG();
                    obj.NUM_SEQ = dao.GetMaxNumSeqConv() + 1;
                    obj.COD_CONVENENTE = Convert.ToDecimal(ddlConv.SelectedValue);
                    obj.DT_VIG_PORTE = Convert.ToDateTime(ddlDtVig.SelectedValue);
                    obj.COD_TAB_RECURSO = null;
                    obj.DT_INI_VIG = Convert.ToDateTime(txtIniDatVigencia.Text);
                    obj.DT_INCLUSAO = Convert.ToDateTime(DateTime.Now);
                    obj.DT_ATU = null;
                    obj.USUARIO = user.login;
                    if (txtFimDatVigencia.Text != "")
                    {
                        obj.DT_FIM_VIG = Convert.ToDateTime(txtFimDatVigencia.Text);
                    }
                    else
                    {
                        obj.DT_FIM_VIG = null;
                    }

                    if (dao.VerificarVigFimConv(Convert.ToDecimal(ddlConv.SelectedValue)) == 1)
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, " O prestador possui uma vigência aberta, vá na aba CONSULTAR VIGÊNCIA e inclua uma DATA FIM DE VIGÊNCIA para que possa ser realizado um novo cadastro. ");
                    }
                    else
                    {
                        if (dao.VerificaInicioVigConv(obj.COD_CONVENENTE, obj.DT_INI_VIG) == 0)
                        {
                            dao.CadastrarConv(obj);
                            divConv.Visible = false;
                            divCadastro.Visible = false;
                            divSucesso.Visible = true;
                        }
                        else 
                        {
                            MostraMensagemTelaUpdatePanel(upUpdatePanel, "Selecione uma data de inicio de vigência que não esteja entre as vigências cadastradas anteriormente");
                        }
                        
                    }

                }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro na Etapa de cadastro: " + ex.Message);
            }
        }
        
        protected void ddlTipoCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoCadastro.SelectedValue == "1")
            {
                divConv.Visible = true;
                divClasse.Visible = false;
                ddlDtVig.Items.Insert(0, new ListItem("Selecione a Data de Vigência do porte", ""));

            }
            else if (ddlTipoCadastro.SelectedValue == "2")
            {
                divConv.Visible = false;
                divClasse.Visible = true;
                ddlDtVig2.Items.Insert(0, new ListItem("Selecione a Data de Vigência do porte", ""));
            }

            CarregarDdlDatVig();
        }

        protected void btnCadClasse_Click(object sender, EventArgs e)
        {
             var user = (ConectaAD)Session["objUser"];

            try
            {
                SAU_TBL_CLASSE_PORT_VIG obj = new SAU_TBL_CLASSE_PORT_VIG();
                obj.NUM_SEQ = dao.GetMaxNumSeqClasse() + 1  ;
                obj.COD_CLASSE = Convert.ToDecimal(ddlClasse.SelectedValue);
                obj.DT_VIG_PORTE = Convert.ToDateTime(ddlDtVig2.SelectedValue);
                obj.DT_INI_ATEND = Convert.ToDateTime(txtIniDatVigencia2.Text);
                obj.DT_INCLUSAO = Convert.ToDateTime(DateTime.Now);
                obj.DT_ATU = null;
                obj.USUARIO = user.login;

                if (txtCobTab.Text != "")
                {
                    obj.COD_TAB_RECURSO = Convert.ToDecimal(txtCobTab.Text);
                }
                else 
                {
                    obj.COD_TAB_RECURSO = null;
                }

                if (txtFimDatVigencia2.Text != "")
                {
                    obj.DT_FIM_ATEND = Convert.ToDateTime(txtFimDatVigencia2.Text);

                }
                else
                {
                    obj.DT_FIM_ATEND = null;
                }

                if (dao.VerificaVigFimClasse(Convert.ToDecimal(ddlClasse.SelectedValue), txtCobTab.Text) != 1)
                {
                    if (dao.VerificaInicioVigClasse(Convert.ToDecimal(ddlClasse.SelectedValue), txtCobTab.Text, Convert.ToDateTime(txtIniDatVigencia2.Text)) == 0)
                    {
   
                                dao.CadastrarClasse(obj);
                                divClasse.Visible = false;
                                divCadastro.Visible = false;
                                divSucesso.Visible = true;
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upUpdatePanel, "Selecione uma data de inicio de vigência que não esteja entre as vigências cadastradas anteriormente");
                    }
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, " A classe possui uma vigência aberta, vá na aba CONSULTAR VIGÊNCIA e inclua uma DATA FIM DE VIGÊNCIA para que possa ser realizado um novo cadastro. ");
                }
                    

            }
            catch(Exception ex) 
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Erro na Etapa de cadastro: " + ex.Message);
            }
        }
        
        #endregion

        #region .:MÉTODOS:.

        public void CarregarDdlConv()
        {
            ddlConv.DataSource = dao.GetConvenente();
            ddlConv.DataValueField = "COD_CONVENENTE";
            ddlConv.DataTextField = "NOM_CONVENENTE";
            ddlConv.DataBind();
        }

        public void CarregarDdlDatVig()
        {
            List<DateTime> lista = dao.GetDatVig();
            var datas = lista.Select(d => d.ToString("dd/MM/yyyy"));

            if (ddlTipoCadastro.SelectedValue == "1")
            {
                ddlDtVig.DataSource = datas;
                ddlDtVig.DataBind();
                ddlDtVig.Items.Insert(0, new ListItem("Selecione a Data de Vigência do porte", ""));
            }
            else if (ddlTipoCadastro.SelectedValue == "2")
            {
                ddlDtVig2.DataSource = datas;
                ddlDtVig2.DataBind();
                ddlDtVig2.Items.Insert(0, new ListItem("Selecione a Data de Vigência do porte", ""));
            }
        }

        public void CarregarDdlClasse()
        {
            ddlClasse.DataSource = dao.GetClasse();
            ddlClasse.DataValueField = "COD_CLASSE";
            ddlClasse.DataTextField = "DES_CLASSE";
            ddlClasse.DataBind();
        }

        #endregion
        
        #endregion


        #region .:ABA 02:.

        #region .:EVENTOS:.
        #region .:Botões:.
        protected void btnListar_Click(object sender, EventArgs e)
        {
            gridViewLista.Visible = false;
            CarregarGridViewLista();
            gridViewLista.Visible = true;
            gridViewListaClasse.Visible = false;
            lblButtonSel.Text = "2";
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            gridViewLista.Visible = false;
            CarregarGridViewConv();
            gridViewLista.Visible = true;
            gridViewListaClasse.Visible = false;
            lblButtonSel.Text = "1";
        }

        protected void btnPesquisarPorte_Click(object sender, EventArgs e)
        {
            CarregarGridViewPorte();
            gridViewListaPorte.Visible = true;
        }

        protected void btnPesquisarClasse_Click(object sender, EventArgs e)
        {
            gridViewListaClasse.Visible = false;
            CarregarGridViewClasse();
            gridViewListaClasse.Visible = true;
            gridViewLista.Visible = false;
            lblButtonSel.Text = "1";
        }

        protected void btnListarClasse_Click(object sender, EventArgs e)
        {
            gridViewListaClasse.Visible = false;
            CarregarGridViewListaClasse();
            gridViewListaClasse.Visible = true;
            gridViewLista.Visible = false;
            lblButtonSel.Text = "2";
        }
        #endregion

        #region .:DropDownLists:.
        protected void ddlTipoCadastro2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoCadastro2.SelectedValue == "1")
            {
                divConv2.Visible = true;
                divClasse2.Visible = false;
                gridViewLista.Visible = false;
                gridViewListaClasse.Visible = false;

            }
            else if (ddlTipoCadastro2.SelectedValue == "2")
            {
                divConv2.Visible = false;
                divClasse2.Visible = true;
                gridViewLista.Visible = false;
                gridViewListaClasse.Visible = false;

            }
        }
        #endregion

        #region .:GridViews:.

        #region .:GridView Convenente:.
        protected void gridViewLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gridViewLista.DataSource = dao.GetListaConvDatVig();
            gridViewLista.PageIndex = e.NewPageIndex;
            gridViewLista.DataBind();

        }

        protected void gridViewLista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbl = (Label)e.Row.FindControl("lblDtFimVigGrid");
            Button btn = (Button)e.Row.FindControl("btnEditar");

            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (lbl.Text == "" || lbl.Text == null)
                    {

                        btn.Visible = true;
                    }
                    else
                    {
                        btn.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                btn.Visible = true;
            }

        }

        protected void gridViewLista_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var user = (ConectaAD)Session["objUser"];

            if (e.CommandName == "Update")
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                SAU_TBL_CONV_PORTE_VIG obj = new SAU_TBL_CONV_PORTE_VIG();
                obj.NUM_SEQ = Convert.ToDecimal(gridViewLista.DataKeys[RowIndex]["NUM_SEQ"].ToString()); //Convert.ToDecimal(gridViewLista.Rows[RowIndex].Cells[0].Text);
                obj.COD_CONVENENTE = Convert.ToDecimal(gridViewLista.Rows[RowIndex].Cells[1].Text);
                obj.DT_VIG_PORTE = Convert.ToDateTime(gridViewLista.Rows[RowIndex].Cells[3].Text);
                obj.COD_TAB_RECURSO = null;
                obj.DT_INI_VIG = Convert.ToDateTime(gridViewLista.Rows[RowIndex].Cells[4].Text);
                obj.DT_ATU = Convert.ToDateTime(DateTime.Now);
                obj.USUARIO = user.login;

                if (Convert.ToDateTime(((TextBox)gridViewLista.Rows[RowIndex].FindControl("TxtDtFimVigGrid")).Text) >= Convert.ToDateTime(gridViewLista.Rows[RowIndex].Cells[4].Text))
                {
                    obj.DT_FIM_VIG = Convert.ToDateTime(((TextBox)gridViewLista.Rows[RowIndex].FindControl("TxtDtFimVigGrid")).Text);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Insira uma data fim igual ou posterior á data de inicio de vigência");
                }

                dao.AtualizarConv(obj);


            }


        }

        protected void gridViewLista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewLista.EditIndex = e.NewEditIndex;

            if (lblButtonSel.Text == "1")
            {
                CarregarGridViewConv();

            }
            else if (lblButtonSel.Text == "2")
            {
                CarregarGridViewLista();

            }


            Button btnEdit = (Button)gridViewLista.Rows[gridViewLista.EditIndex].FindControl("btnEditar");
            Button btnAtu = (Button)gridViewLista.Rows[gridViewLista.EditIndex].FindControl("btnAtualizar");
            Button btnCancel = (Button)gridViewLista.Rows[gridViewLista.EditIndex].FindControl("btnCancelar");

            btnEdit.Visible = false;
            btnAtu.Visible = true;
            btnCancel.Visible = true;
        }

        protected void gridViewLista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewLista.EditIndex = -1;

            if (lblButtonSel.Text == "1")
            {
                CarregarGridViewConv();
            }
            else if (lblButtonSel.Text == "2")
            {
                CarregarGridViewLista();
            }

        }

        protected void gridViewLista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            gridViewLista.EditIndex = -1;

            if (lblButtonSel.Text == "1")
            {
                CarregarGridViewConv();
            }
            else if (lblButtonSel.Text == "2")
            {
                CarregarGridViewLista();
            }
        }
        #endregion

        #region .:GridView Classe:.
        protected void gridViewListaClasse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbl = (Label)e.Row.FindControl("lblDtFimVigGridClasse");
            Button btn = (Button)e.Row.FindControl("btnEditarClasse");

            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (lbl.Text == "" || lbl.Text == null)
                    {

                        btn.Visible = true;
                    }
                    else
                    {
                        btn.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                btn.Visible = true;
            }
        }

        protected void gridViewListaClasse_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewListaClasse.EditIndex = e.NewEditIndex;

            if (lblButtonSel.Text == "1")
            {
                CarregarGridViewClasse();
            }
            else if (lblButtonSel.Text == "2")
            {
                CarregarGridViewListaClasse();
            }


            Button btnEdit = (Button)gridViewListaClasse.Rows[gridViewListaClasse.EditIndex].FindControl("btnEditarClasse");
            Button btnAtu = (Button)gridViewListaClasse.Rows[gridViewListaClasse.EditIndex].FindControl("btnAtualizarClasse");
            Button btnCancel = (Button)gridViewListaClasse.Rows[gridViewListaClasse.EditIndex].FindControl("btnCancelarClasse");

            btnEdit.Visible = false;
            btnAtu.Visible = true;
            btnCancel.Visible = true;
        }

        protected void gridViewListaClasse_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var user = (ConectaAD)Session["objUser"];

            if (e.CommandName == "Update")
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                SAU_TBL_CLASSE_PORT_VIG obj = new SAU_TBL_CLASSE_PORT_VIG();
                obj.NUM_SEQ = Convert.ToDecimal(gridViewListaClasse.DataKeys[RowIndex]["NUM_SEQ"].ToString());//Convert.ToDecimal(gridViewListaClasse.Rows[RowIndex].Cells[0].Text);
                obj.COD_CLASSE = Convert.ToDecimal(gridViewListaClasse.Rows[RowIndex].Cells[1].Text);
                obj.DT_VIG_PORTE = Convert.ToDateTime(gridViewListaClasse.Rows[RowIndex].Cells[4].Text);
                obj.DT_INI_ATEND = Convert.ToDateTime(gridViewListaClasse.Rows[RowIndex].Cells[5].Text);
                obj.DT_ATU = Convert.ToDateTime(DateTime.Now);
                obj.USUARIO = user.login;

                if (Convert.ToDateTime(((TextBox)gridViewListaClasse.Rows[RowIndex].FindControl("TxtDtFimVigGridClasse")).Text) >= Convert.ToDateTime(gridViewListaClasse.Rows[RowIndex].Cells[4].Text))
                {
                    obj.DT_FIM_ATEND = Convert.ToDateTime(((TextBox)gridViewListaClasse.Rows[RowIndex].FindControl("TxtDtFimVigGridClasse")).Text);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Insira uma data fim igual ou posterior á data de inicio de vigência");
                }

                if (gridViewListaClasse.Rows[RowIndex].Cells[3].Text == "&nbsp;")
                {
                    obj.COD_TAB_RECURSO = null;
                }
                else
                {
                    obj.COD_TAB_RECURSO = Convert.ToDecimal(gridViewListaClasse.Rows[RowIndex].Cells[3].Text); ;
                }

                dao.AtualizarClasse(obj);
            }
        }

        protected void gridViewListaClasse_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewListaClasse.EditIndex = -1;

            if (lblButtonSel.Text == "1")
            {
                CarregarGridViewClasse();
            }
            else if (lblButtonSel.Text == "2")
            {
                CarregarGridViewListaClasse();
            }

        }

        protected void gridViewListaClasse_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            gridViewListaClasse.EditIndex = -1;

            if (lblButtonSel.Text == "1")
            {
                CarregarGridViewClasse();
            }
            else if (lblButtonSel.Text == "2")
            {
                CarregarGridViewListaClasse();
            }
        }

        protected void gridViewListaClasse_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridViewListaClasse.DataSource = dao.GetClasseDatVig(Convert.ToDecimal(txtCodClasse.Text));
            gridViewListaClasse.PageIndex = e.NewPageIndex;
            gridViewListaClasse.DataBind();

        }
        #endregion

        #endregion

        #endregion

        #region .:MÉTODOS:.
        public void CarregarGridViewLista()
        {
            gridViewLista.DataSource = dao.GetListaConvDatVig();
            gridViewLista.DataBind();

        }

        public void CarregarGridViewConv()
        {
            gridViewLista.DataSource = dao.GetConvDatVig(Convert.ToDecimal(txtCodConv.Text));
            gridViewLista.DataBind();
        }

        public void CarregarGridViewListaClasse()
        {
            gridViewListaClasse.DataSource = dao.GetListaClasseDatVig();
            gridViewListaClasse.DataBind();

        }

        public void CarregarGridViewClasse()
        {
            gridViewListaClasse.DataSource = dao.GetClasseDatVig(Convert.ToDecimal(txtCodClasse.Text));
            gridViewListaClasse.DataBind();
        }

        #endregion
        #endregion


        #region .:ABA 03:.
        #region .:EVENTOS:.
        protected void gridViewListaPorte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gridViewListaPorte.DataSource = dao.GetPorte(Convert.ToDateTime(txtDtVig.Text));
            gridViewListaPorte.PageIndex = e.NewPageIndex;
            gridViewListaPorte.DataBind();

        }
        #endregion

        #region .:MÉTODOS:.
        public void CarregarGridViewPorte()
        {
            gridViewListaPorte.DataSource = dao.GetPorte(Convert.ToDateTime(txtDtVig.Text));
            gridViewListaPorte.DataBind();
        }
        #endregion

       

        #endregion

     
       






    }
}