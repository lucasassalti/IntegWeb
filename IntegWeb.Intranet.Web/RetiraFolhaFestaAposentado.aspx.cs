using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Intranet.Aplicacao;
using System.Data;

namespace IntegWeb.Intranet.Web
{
    public partial class RetiraFolhaFestaAposentado : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RetiraFolhaFestaAposentadosBLL objBLL = new RetiraFolhaFestaAposentadosBLL();
                DataTable dt = objBLL.consultaNomeUsuario();


                grdUsuariosDisque.DataSource = dt;
                grdUsuariosDisque.DataBind();
                grdUsuariosDisque.Visible = true;

            }
            

        }

        protected void btnConsultarGrid_Click(object sender, EventArgs e)
        {
            RetiraFolhaFestaAposentadosBLL objBLL = new RetiraFolhaFestaAposentadosBLL();
            DataTable dt = new DataTable();
            try
            {
                if (!String.IsNullOrEmpty(txtMatricula.Text))
                {
                    dt = objBLL.retornaUsuarioMatricula(txtMatricula.Text.ToUpper());
                }
                else if (!String.IsNullOrEmpty(txtNomeUsuario.Text))
                {
                    dt = objBLL.retornaUsuarioNome(txtNomeUsuario.Text);
                }
                else
                {
                    dt = objBLL.consultaNomeUsuario();
                }


                grdUsuariosDisque.DataSource = dt;
                grdUsuariosDisque.DataBind();
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }

        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            RetiraFolhaFestaAposentadosBLL objBLL = new RetiraFolhaFestaAposentadosBLL();
            try
            {
                if (!String.IsNullOrEmpty(txtMatricula.Text))
                {
                    if (!String.IsNullOrEmpty(txtNomeUsuario.Text))
                    {
                        if (objBLL.retornaUsuarioMatricula(txtMatricula.Text.ToUpper()).Rows.Count == 0)
                        {
                            objBLL.insereUsuarioDisque(txtMatricula.Text, txtNomeUsuario.Text);
                            MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro inserido com sucesso!");
                            grdUsuariosDisque.DataSource = objBLL.consultaNomeUsuario();
                            grdUsuariosDisque.DataBind();
                            txtMatricula.Text = "";
                            txtNomeUsuario.Text = "";
                        }
                        else
                        {
                            MostraMensagemTelaUpdatePanel(UpdatePanel, "Erro: Usuário já está na tabela");
                        }
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(UpdatePanel, "Favor preencher campo do nome do usuário");
                        txtNomeUsuario.Focus();
                    }
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Favor preencher campo de matrícula");
                    txtMatricula.Focus();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }

         
        }

        protected void grdUsuariosDisque_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Exclude")
            {
                try
                {
                    int rowIndex = Convert.ToInt32(e.CommandArgument);
                    RetiraFolhaFestaAposentadosBLL objBLL = new RetiraFolhaFestaAposentadosBLL();
                    GridViewRow row = grdUsuariosDisque.Rows[rowIndex];
                    Label matricula = (Label)row.FindControl("lblCodigoMatricula");
                    objBLL.excluiUsuarioBLL(matricula.Text);
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Usuário excluído com sucesso");

                    DataTable dt = new DataTable();
                    dt = objBLL.consultaNomeUsuario();
                    grdUsuariosDisque.DataSource = dt;
                    grdUsuariosDisque.DataBind();
                }
                catch (Exception ex)
                {
                    throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
                }
            }
        }

        protected void excluiUsuarioDisque_Click(object sender, EventArgs e)
        {

        }

        protected void grdUsuariosDisque_RowEditing(object sender, GridViewEditEventArgs e)
        {
            RetiraFolhaFestaAposentadosBLL objBLL = new RetiraFolhaFestaAposentadosBLL();
            grdUsuariosDisque.EditIndex = e.NewEditIndex;
            //DataTable dt = objBLL.consultaNomeUsuario();
            //grdUsuariosDisque.DataSource = dt;
            //grdUsuariosDisque.DataBind();

        }

        void showGridDisque()
        {
            DataTable dt = new DataTable();
            RetiraFolhaFestaAposentadosBLL objBLL = new RetiraFolhaFestaAposentadosBLL();

            // DataView dv = new DataView(objBL);
            grdUsuariosDisque.DataSource = objBLL.consultaNomeUsuario();
            grdUsuariosDisque.DataBind();
        }

        protected void grdUsuariosDisque_PreRender(object sender, EventArgs e)
        {
            if (grdUsuariosDisque.Rows.Count > 0)
            {
                grdUsuariosDisque.UseAccessibleHeader = true;
                grdUsuariosDisque.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void grdUsuariosDisque_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdUsuariosDisque_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = grdUsuariosDisque.Rows[e.NewSelectedIndex];
        }

    }
}