
using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Entidades;
using IntegWeb.Framework;

public class BaseReportPage : BasePage
{

    public Resultado ValidaCampos(Relatorio relatorio)
    {
        Resultado res = new Resultado();

        //if (Session[ReportCrystal.RelatorioID] != null)
        if (relatorio != null)
        {
            //Relatorio obj = (Relatorio)Session[ReportCrystal.RelatorioID];
            List<Parametro> listP = relatorio.parametros;

            foreach (String cc in Request.Form.Keys)
            {
                int ip = cc.IndexOf("Param_");
                if (ip > -1)
                {
                    string parametro = cc.Substring(ip + 6);
                    listP.FirstOrDefault(p => p.parametro == parametro).valor = Request.Form[cc];
                }
            }
            bool sucesso = true;
            foreach (var par in listP)
            {
                if (par.permite_null == "N" && String.IsNullOrEmpty(par.valor))
                {
                    sucesso = false;
                    res.Erro("O campo " + par.descricao + " não pode estar em branco");
                }
                if (par.permite_null == "S" && par.tipo == "NumberField" && par.valor == "")
                {
                    par.valor = "0";
                }
                if (par.permite_null == "S" && par.tipo == "StringField" && par.valor == "")
                {
                    par.valor = "0";
                }
            }
            if (sucesso) res.Sucesso();
        }

        return res;
    }

    protected void GeraHtml(HtmlTable table, Relatorio relatorio)
    {
        List<Parametro> parametros = relatorio.parametros;
        string relatorio_nome = relatorio.relatorio;
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
                    {
                        RelatorioBLL relBLL = new RelatorioBLL();
                        CarregaDropDowDT(relBLL.ListarDrop(par.dropdowlist_consulta), DropDownList1);
                    }

                    if (par.valor_inicial.Length > 0)
                    {
                        DropDownList1.SelectedValue = par.valor_inicial;
                    }



                    HtmlTableCell cell1 = new HtmlTableCell();
                    cell1.Controls.Add(DropDownList1);
                    row.Cells.Add(cell1);

                }

                if (par.componente_web == "TextBox")
                {

                    ///////////////CONDIÇÃO EXCLUSIVA PARA O RELATÓRIO_CONTROLE_LIBERAÇÃO_FATURA//////////////////////
                    if (relatorio_nome == "Rel_Controle_liberacao.rpt" &&
                        par.parametro.Equals("matricula"))
                    {
                        label1.Visible = false;
                        var user = (ConectaAD)Session["objUser"];
                      
                        HiddenField hdnField = new HiddenField();
                        hdnField.ID = "Param_" + par.parametro;
                        if ((user.nome == "Wendel Lemes") || (user.nome == "Ana Claudia Alves Santos") || (user.nome == "Soraya Diba Alves de Lima"))
                        {
                            par.valor_inicial = " ";
                        }

                        else 
                        {
                            par.valor_inicial = user.login;
                        }


                        if (par.valor_inicial.Length > 0)
                            hdnField.Value = (par.valor_inicial.ToUpper() == "SYSDATE") ? DateTime.Now.ToString("dd/MM/yyyy") : par.valor_inicial;

                        HtmlTableCell cell2 = new HtmlTableCell();
                        cell2.Controls.Add(hdnField); ;
                        row.Cells.Add(cell2);
                    }
                        ///////////////FIM DA CONDIÇÃO EXCLUSIVA PARA O RELATÓRIO_CONTROLE_LIBERAÇÃO_FATURA//////////////////////
                    else
                    {
                        TextBox TextBox1 = new TextBox();
                        TextBox1.ID = "Param_" + par.parametro;
                        TextBox1.Visible = par.visivel == "S";
                        TextBox1.Style.Add("width", tamanho);
                        TextBox1.Enabled = par.habilitado == "S";

                        if (par.tipo == "DateField")
                        {
                            TextBox1.CssClass = "date";
                            TextBox1.Attributes.Add("onkeypress", "javascript:return mascara(this, data);");
                            TextBox1.Attributes.Add("MaxLength", "10");
                        }
                        else if (par.tipo == "NumberField")
                        {
                            TextBox1.CssClass = "number";
                            TextBox1.Attributes.Add("onkeypress", "javascript:return mascara(this, soNumeros);");
                            TextBox1.Attributes.Add("MaxLength", "10");
                        }

                        if (par.valor_inicial.Length > 0)
                        {
                            TextBox1.Text = (par.valor_inicial.ToUpper() == "SYSDATE") ? DateTime.Now.ToString("dd/MM/yyyy") : par.valor_inicial;
                        }
                        HtmlTableCell cell2 = new HtmlTableCell();
                        cell2.Controls.Add(TextBox1); ;
                        row.Cells.Add(cell2);
                    }
                }
                table.Rows.Add(row);
            }
        }
    }

}
