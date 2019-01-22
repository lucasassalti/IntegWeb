using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Saude.Aplicacao;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Periodico.Web
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
            lblRegistros.Text = "A consulta retornou " + dt.Rows.Count.ToString() + " registros";
            lblRegistros.Visible = true;
            
            if (dt.Rows.Count > 0)
            {
                Dictionary<string, DataTable> dtRelatorio = new Dictionary<string, DataTable>();
                var nomeArquivo = Convert.ToString(DateTime.Today.Year) + "_" + Convert.ToString(DateTime.Today.Month) + "_" + Convert.ToString(DateTime.Today.Day) + "_" + rels.relatorio + ".xls";
                dtRelatorio.Add(nomeArquivo, dt);
                Session["DtRelatorio"] = dtRelatorio;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'WebFile.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
            }

        }

    }
}