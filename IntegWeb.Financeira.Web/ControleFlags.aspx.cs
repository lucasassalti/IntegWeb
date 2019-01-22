using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Financeira.Aplicacao.BLL.Tesouraria;
using System.Data;
using IntegWeb.Entidades.Framework;
using System.Drawing;
using System.Text.RegularExpressions;
using IntegWeb.Framework;




namespace IntegWeb.Financeira.Web
{
    public partial class ControleFlags : BasePage
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                CarregaGridGeral();
                
            }
        }
        //Validação
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

            ControleFlagsBLL objBLL = new ControleFlagsBLL();

            var user = (ConectaAD)Session["objUser"];


            Regex validaNomeSemEspecial = new Regex("[a-zA-Z]");


            if (txtEmp.Text == String.Empty & txtMatr.Text == String.Empty & txtNumRepr.Text == String.Empty & txtNome.Text == String.Empty)
            {
                txtEmp.Focus();
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Todos os campos vazios \n");
                txtEmp.BackColor = Color.LightGray;
                txtMatr.BackColor = Color.LightGray;
                txtNumRepr.BackColor = Color.LightGray;
                txtNome.BackColor = Color.LightGray;
            } 

            else if (txtEmp.Text == String.Empty & txtMatr.Text == String.Empty & txtNome.Text != String.Empty)
            {
                if (validaNomeSemEspecial.IsMatch(txtNome.Text))
                {
                    gridFlag.DataSource = objBLL.mostrarGrid(txtNome.Text);
                    gridFlag.DataBind();
                }

                else
                {
                    txtNome.Focus();
                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Use apenas caracteres alfabéticos no campo nome \n");
                    gridFlag.DataSource = objBLL.geraGridGeral();
                    gridFlag.DataBind();
                }

                gridFlag.DataSource = objBLL.geraColunaFlag(objBLL.mostrarGrid(txtNome.Text));
                gridFlag.DataBind();

            }
            else if (txtEmp.Text != String.Empty & txtMatr.Text != String.Empty)
            {
               
                gridFlag.DataSource = objBLL.geraColunaFlag(objBLL.consultaGridMatr(Convert.ToInt32(txtMatr.Text)));
                gridFlag.DataBind();

            }
            else if (txtEmp.Text != String.Empty)
            {
                gridFlag.DataSource = objBLL.geraColunaFlag(objBLL.geraConsultaGrid(Convert.ToInt32(txtEmp.Text)));
                gridFlag.DataBind();
                //txtEmp.Focus();
                //MostraMensagemTelaUpdatePanel(upUpdatePanel, "Campo Empresa vazio \n");
                //txtEmp.BackColor = Color.LightGray;
            }
             else if (txtMatr.Text != String.Empty)
            {
                gridFlag.DataSource = objBLL.geraColunaFlag(objBLL.consultaGridMatr(Convert.ToInt32(txtMatr.Text)));
                gridFlag.DataBind();
            }
        }


        protected void btnInserir_Click(object sender, EventArgs e)
        {
           

            if (txtEmp.Text == String.Empty || txtMatr.Text == String.Empty || txtNumRepr.Text == String.Empty || txtNome.Text == String.Empty)
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Preencha TODOS os campos para inserir. \n");
                txtEmp.BackColor = Color.LightGray;
                txtMatr.BackColor = Color.LightGray;
                txtNumRepr.BackColor = Color.LightGray;
                txtNome.BackColor = Color.LightGray;
            }
            else if (rbdFlag.SelectedValue =="")
            {
                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Selecione uma opção de Flag para inserir. \n");
            }
            
            else
            {
                ControleFlagsBLL objBLL = new ControleFlagsBLL();

                var user = (ConectaAD)Session["objUser"];

                string flagJudicial = "";

                string flagInsucesso = "";



        if (rbdFlag.SelectedValue == "J")
            {
                    flagJudicial = "S";

                    objBLL.InsereLinha(Convert.ToInt32(txtEmp.Text),
                                 Convert.ToInt32(txtMatr.Text),
                                 Convert.ToInt32(txtNumRepr.Text),
                                 Convert.ToString(txtNome.Text),
                                 user.nome,
                                 flagJudicial,
                                 flagInsucesso
                                 );

                    MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro inserido com sucesso. \n");

                    CarregaGridGeral();

            }

         else if (rbdFlag.SelectedValue == "I")
          {

              flagInsucesso = "S";
          }
         

                objBLL.InsereLinha(Convert.ToInt32(txtEmp.Text),
                                   Convert.ToInt32(txtMatr.Text),
                                   Convert.ToInt32(txtNumRepr.Text),
                                   Convert.ToString(txtNome.Text),
                                   user.nome,
                                   flagJudicial,
                                   flagInsucesso
                                   );


                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Registro inserido com sucesso. \n");

                CarregaGridGeral();

             
            }
            


        }

        protected void btnLimpar_Click(object sender, EventArgs e) {

            if (txtEmp.Text == String.Empty & txtMatr.Text == String.Empty & txtNumRepr.Text == String.Empty & txtNome.Text == String.Empty)
            {

                CarregaGridGeral();

            }
            else
            {
                txtEmp.Text = "";
                txtMatr.Text ="";
                txtNumRepr.Text ="";
                txtNome.Text = "";

                CarregaGridGeral();

            }
        }

       
        protected void btnGerar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ControleFlagsBLL objBll = new ControleFlagsBLL();

            dt = objBll.geraGridGeral();

            ArquivoDownload arqGeral = new ArquivoDownload();
            arqGeral.nome_arquivo = "Rel_Controle_Flags.xlsx";
            arqGeral.dados = dt;
            Session[ValidaCaracteres(arqGeral.nome_arquivo)] = arqGeral;
            string fullArqGeral = "WebFile.aspx?dwFile=" + ValidaCaracteres(arqGeral.nome_arquivo);
            AdicionarAcesso(fullArqGeral);
            AbrirNovaAba(upUpdatePanel, fullArqGeral, arqGeral.nome_arquivo);
        }
        #endregion

        #region Metodos



        //GRID
        protected void CarregaGridGeral()
        {

            ControleFlagsBLL objBLL = new ControleFlagsBLL();

            gridFlag.DataSource = objBLL.geraColunaFlag(objBLL.geraGridGeral()); 
            
            gridFlag.DataBind();
        }
        #endregion

        protected void gridFlag_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CarregaGridGeral();
            gridFlag.PageIndex = e.NewPageIndex;
            gridFlag.DataBind();
        }


        protected void gridFlag_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var user = (ConectaAD)Session["objUser"];

            if (e.CommandName == "Excluir")
            {
                ControleFlagsBLL objBLL = new ControleFlagsBLL();

                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                int indexLinha = gvr.RowIndex;

                int cod_emprs = Convert.ToInt32(((Label)gridFlag.Rows[indexLinha].FindControl("lblEmpresa")).Text);
                int num_rgtro = Convert.ToInt32(((Label)gridFlag.Rows[indexLinha].FindControl("lblNumRgtro")).Text);
                string exclusao = user.nome;

                MostraMensagemTelaUpdatePanel(upUpdatePanel, "Excluído com sucesso. \n");

                objBLL.deletaLinha(num_rgtro,cod_emprs, exclusao);

                //carrega o grid novo
                CarregaGridGeral();
            }
         

        }

        protected void gridFlag_DataBound(Object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                CarregaGridGeral();
            }
        }

        void DisplayCurrentPage()
        {
            // Calculate the current page number.
            int currentPage = gridFlag.PageIndex + 1;

            // Display the current page number. 
            //Message.Text = "Page " + currentPage.ToString()  +
            //  gridFlag.PageCount.ToString() + ".";
        }

    }
}