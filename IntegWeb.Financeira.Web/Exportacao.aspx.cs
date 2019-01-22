using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Financeira.Aplicacao.BLL;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Financeira.Aplicacao;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Financeira.Web
{
    public partial class Exportacao : System.Web.UI.Page
    {
        BasePage objPage = new BasePage();
        RelatorioBLL rel = new RelatorioBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string relatorio = "Rel_Adesao_Nosso_Plano";
                string relatorio = Request.QueryString["Relatorio_nome"];
                if (!String.IsNullOrEmpty(relatorio))
                {
                    switch (relatorio)
                    {
                        case "TXT_PEFIN_SERASA":
                            table.Visible = false;
                            btnRelatorio.Visible = false;
                            btnTxtSerasa.Visible = true;
                            btnConsultarRemessas.Visible = true;
                            //ListaRemessas();
                            break;
                        default:
                            break;
                    }

                    Relatorio res = rel.Listar(relatorio);
                    Session["ObjRelatorio"] = res;
                    GeraHtml(res);
                }
                else
                    Response.Redirect("index.aspx");

            }
        }

        protected void GeraHtml(Relatorio relatorio)
        {
            List<Parametro> parametros = relatorio.parametros;
            NomeRelatorio.Text = relatorio.titulo;

            if (parametros.Count > 0)
            {
                string tamanho = "200px";
                System.Text.StringBuilder scriptAjax = new System.Text.StringBuilder();

                foreach (var par in parametros)
                {
                    HtmlTableRow row = new HtmlTableRow();

                    Label label1 = new Label();
                    label1.ID = "lbl" + par.parametro;
                    label1.Text = par.descricao;
                    label1.Visible = par.visivel == "S";

                    HtmlTableCell cell = new HtmlTableCell();
                    cell.Controls.Add(label1); ;
                    row.Cells.Add(cell);

                    if (par.componente_web == "DropDownList")
                    {
                        DropDownList DropDownList1 = new DropDownList();
                        DropDownList1.ID = "Param_" + par.parametro;
                        DropDownList1.Visible = par.visivel == "S";
                        DropDownList1.Enabled = par.habilitado == "S";
                        DropDownList1.Style.Add("width", tamanho);

                        if (par.dropdowlist_consulta.Length > 0)
                            objPage.CarregaDropDowDT(rel.ListarDrop(par.dropdowlist_consulta), DropDownList1);

                        HtmlTableCell cell1 = new HtmlTableCell();
                        cell1.Controls.Add(DropDownList1); ;
                        row.Cells.Add(cell1);

                    }

                    if (par.componente_web == "TextBox")
                    {
                        TextBox TextBox1 = new TextBox();
                        TextBox1.ID = "Param_" + par.descricao;
                        TextBox1.Visible = par.visivel == "S";
                        TextBox1.Style.Add("width", tamanho);
                        TextBox1.Enabled = true;

                        if (par.tipo == "DateField")
                        {
                            TextBox1.CssClass = "date";
                            TextBox1.Attributes.Add("onkeypress", "javascript:return mascara(this, data);");
                            TextBox1.Attributes.Add("MaxLength", "10");
                        }


                        if (par.valor_inicial.Length > 0)
                        {
                            TextBox1.Text = par.valor_inicial;
                        }
                        HtmlTableCell cell2 = new HtmlTableCell();
                        cell2.Controls.Add(TextBox1); ;
                        row.Cells.Add(cell2);
                    }



                    table.Rows.Add(row);

                }

            }
        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            ValidaCampos();
        }

        protected void btnTxtSerasa_Click(object sender, EventArgs e)
        {
            // Gera a remessa:
            Relatorio obj = (Relatorio)Session["ObjRelatorio"];
            ImportaBaseSerasaBLL bll = new ImportaBaseSerasaBLL();
            int ret = bll.GeraRemessa();

            ListaRemessas();

            // Exporta TXT SERASA:            
            //obj.parametros[0].valor = ret.ToString();
            //GeraRelatorio(obj);
        }

        public void ValidaCampos()
        {
            if (Session["ObjRelatorio"] != null)
            {

                Relatorio obj = (Relatorio)Session["ObjRelatorio"];
                List<Parametro> listP = obj.parametros;

                int contador = 0;
                List<Object> list = new List<Object>();
                foreach (String cc in Request.Form.Keys)
                {
                    if (cc.IndexOf("Param_") > -1)
                    {
                        list.Add(Request.Form[cc]);
                        listP[contador].valor = Request.Form[cc];
                        contador++;
                    }
                }

                foreach (var par in listP)
                {
                    if (par.permite_null == "N" && par.valor == "")
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + "O campo " + par.descricao + " não pode estar em branco" + "');", true);
                        return;
                    }
                }

                try
                {
                    GeraRelatorio(obj);

                }
                catch (Exception ex)
                {

                    objPage.MostraMensagemTelaUpdatePanel(upExcel, "Erro!! \\n\\nVerifique se o tipo de dados digitado esta correto.\\n\\nPor Exemplo:\\nSe o campo é do tipo data digite no formato dd/mm/yyyy.\\nSe o campo é do tipo númerico digite apenas números.\\n\\nMensagem do Banco de dados\\n\\n" + ex.Message);
                    return;
                }
            }

        }

        public void GeraRelatorio(Relatorio rels)
        {
            DataTable dt = rel.ListarDinamico(rels);

            if (dt.Rows.Count > 2) //Revisar este ponto pois a proc não poderia gerar linhas
            {
                lblRegistros.Text = "A remessa contém " + dt.Rows.Count.ToString() + " linhas";
                lblRegistros.Visible = true;
                //Dictionary<string, DataTable> dtRelatorio = new Dictionary<string, DataTable>();
                //var nomeArquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_" + rels.relatorio + "." + rels.relatorio_extensao;                
                foreach (DataRow row in dt.Rows)
                {
                    row[0] = BasePage.ValidaCaracteres(row[0].ToString());
                };
                ArquivoDownload txtSERASA = new ArquivoDownload();
                txtSERASA.dados = dt;
                txtSERASA.nome_arquivo = Convert.ToString(DateTime.Today.Year) + "_" + 
                                         Convert.ToString(DateTime.Today.Month) + "_" + 
                                         Convert.ToString(DateTime.Today.Day) + "_" + 
                                         rels.relatorio + "." + 
                                         rels.relatorio_extensao;
                Session[txtSERASA.nome_arquivo] = txtSERASA;
                BasePage.AbrirNovaAba(upExcel, "WebFile.aspx?dwFile=" + txtSERASA.nome_arquivo, txtSERASA.nome_arquivo);                  
            }
            else
            {
                objPage.MostraMensagemTelaUpdatePanel(upExcel, "Atenção!! \\n\\nNão foram encontrados dados para exportar.\\n\\nVerifique planilha e importe novamente.");
            }

        }

        protected void grdSex_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdSex"] != null)
            {
                grdSex.PageIndex = e.NewPageIndex;
                grdSex.DataSource = ViewState["grdSex"];
                grdSex.DataBind();
            }
        }


        protected void btnConsultarRemessas_Click(object sender, EventArgs e)
        {
            ListaRemessas();
        }


        private void ListaRemessas()
        {

            ImportaBaseSerasaBLL bll = new ImportaBaseSerasaBLL();
            DataTable dt = bll.ListaRemessas();
            divselect.Visible = true;
            ViewState["grdSex"] = dt;
            objPage.CarregarGridView(grdSex, dt);
        }
        
        protected void grdSex_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //e.Handled = false;
            switch (e.CommandName)
            {
                case "Exportar":
                    // Exporta TXT SERASA:            
                    Relatorio obj = (Relatorio)Session["ObjRelatorio"];
                    obj.parametros[0].valor = e.CommandArgument.ToString(); 
                    GeraRelatorio(obj);                   
                    break;
                    default:
                break;
            }
        }

        protected void grdSex_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdSex.EditIndex = -1;
            grdSex.PageIndex = 0;
            ListaRemessas();
        }

        protected void grdSex_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdSex.EditIndex = e.NewEditIndex;
            ListaRemessas();
        }

        protected void grdSex_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int pk = 0;
            int.TryParse(e.Keys[0].ToString(), out pk);

            int new_value = 0;
            int.TryParse(e.NewValues["COD_REMESSA_SERASA_PEFIN"].ToString(), out new_value);

            if (pk != new_value)
            {
                try
                {
                    ImportaBaseSerasaBLL bll = new ImportaBaseSerasaBLL();
                    Boolean res = bll.Atualizar(pk, new_value);
                    grdSex.EditIndex = -1;
                    grdSex.PageIndex = 0;
                    ListaRemessas();
                }
                catch (Exception ex)
                {
                    objPage.MostraMensagemTelaUpdatePanel(upExcel, "Problemas contate o administrador do sistema: //n" + ex.Message);
                    return;
                }

            } else {
                objPage.MostraMensagemTelaUpdatePanel(upExcel, "Atenção!! \\n\\nEntre com o número de remessa diferente.");
            }
        }

        protected void grdSex_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int pk = 0;
            int.TryParse(e.Keys[0].ToString(), out pk);

            try
            {
                ImportaBaseSerasaBLL bll = new ImportaBaseSerasaBLL();
                Boolean res = bll.Deletar_Remessa(pk);
                grdSex.EditIndex = -1;
                grdSex.PageIndex = 0;
                ListaRemessas();
            }
            catch (Exception ex)
            {
                objPage.MostraMensagemTelaUpdatePanel(upExcel, "Problemas contate o administrador do sistema: //n" + ex.Message);
                return;
            }
        }

    }
}