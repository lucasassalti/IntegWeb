using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Previdencia.Aplicacao.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



namespace IntegWeb.Previdencia.Web
{
    public partial class AvisoPagamento : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{}
           
        }
        
        AvisoPagamentoBLL obj = new AvisoPagamentoBLL();
        AvisoRepresentanteBLL obj2 = new AvisoRepresentanteBLL();
       
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCodMatricula.Text = "";
            txtCodEmpresa.Text = "";
            txtCodRepres.Text = "";

            hidEmpresa.Value = "";
            hidMatricula.Value = "";
            hidRepress.Value = "";


            grdAvisoPagamento.Visible = false;
            grdAvisoRepress.Visible = false;

        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtCodRepres.Text) || (txtCodRepres.Text == "0"))
            {
                if (!string.IsNullOrEmpty(txtCodMatricula.Text) && !string.IsNullOrEmpty(txtCodEmpresa.Text))
                {

                    txtCodRepres.Text = "";
                    hidRepress.Value = "";

                    obj.cod_emprs = int.Parse(txtCodEmpresa.Text);
                    obj.num_registro = int.Parse(txtCodMatricula.Text);
                    
                    DataTable dt = obj.ConsultarAvisoPgto();

                    if (dt.Rows.Count > 0)
                    {

                        hidEmpresa.Value = dt.Rows[0]["EMPRESA"].ToString();
                        hidMatricula.Value = dt.Rows[0]["MATRICULA"].ToString();

                        //txtEmpresa.Text = dt.Rows[0]["EMPRESA"].ToString();
                        //txtMatricula.Text = dt.Rows[0]["MATRICULA"].ToString();
                        //txtNome.Text = dt.Rows[0]["NOME"].ToString();


                        grdAvisoRepress.Visible = false;
                        grdAvisoPagamento.Visible = true;
                       
                        CarregarGridView(grdAvisoPagamento, dt);

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "recarregarFunction();", true);
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upPanel, "Nenhum Registro encontrado!");
                    }

                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtCodMatricula.Text) && !string.IsNullOrEmpty(txtCodEmpresa.Text))
                {

                    obj2.cod_emprs = int.Parse(txtCodEmpresa.Text);
                    obj2.num_registro = int.Parse(txtCodMatricula.Text);
                    obj2.num_idntf_rptant = int.Parse(txtCodRepres.Text);

                    DataTable dt = obj2.ConsultarAvisoPgtoRepres();

                    
                    if (dt.Rows.Count > 0)
                    {

                        hidEmpresa.Value = dt.Rows[0]["EMPRESA"].ToString();
                        hidMatricula.Value = dt.Rows[0]["MATRICULA"].ToString();
                        hidRepress.Value = dt.Rows[0]["NUM_RPTANT"].ToString();

                        grdAvisoPagamento.Visible = false;
                        grdAvisoRepress.Visible = true;
                        CarregarGridView(grdAvisoRepress, dt);

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "recarregarFunction();", true);
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upPanel, "Nenhum Registro encontrado!");
                    }

                }
            }

          //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", "recarregarFunction();", true);

            
        }

        
    }
}