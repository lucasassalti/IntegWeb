using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial;

namespace IntegWeb.Previdencia.Web
{
    public partial class RelCustoJudicial : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Form.DefaultButton = this.btnPesquisar.UniqueID;

            pnlGrid_Mensagem.Visible = false;
            pnlGerarNovo_Mensagem.Visible = false;

            //ScriptManager.RegisterStartupScript(upCustoJudicial,
            //       upCustoJudicial.GetType(),
            //       "script",
            //       "_client_side_script();",
            //        true);

            if (!IsPostBack)
            {
                grdCustoJudicial.Sort("HDRDATHOR", SortDirection.Descending);
                CarregaDrop();
            }
        }

        private void CarregaDrop()
        {
            ValorReferenciaBLL obj = new ValorReferenciaBLL();
            CarregaDropDowDT(obj.CarregaUnidadeMonetaria(), ddlUnMonetaria);
            //CarregaDropDowDT(obj.CarregaCotacao(111), ddlDtAtualizaMnetaria);
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            txtDtIni.Text = String.Empty;
            txtDtFim.Text = String.Empty;
            txtVlrLimitador.Text = String.Empty;
            chkIncidir.Checked = true;
            ddlUnMonetaria.SelectedValue = "0";
            txtDtAtuMonetaria.Text = String.Empty;
            pnlGrid.Visible = false;
            pnlGerarNovo.Visible = true;            
        }

        protected void grdCustoJudicial_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
        }    

        protected void grdCustoJudicial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string CommandArgs = e.CommandArgument.ToString(); // .Split(',');

            //Rel_Cst_Judic_Corrig.rpt
            //relatorio.arquivo = relatorio_simples;
            //relatorio.parametros.Add(new Parametro() { parametro = "ANCODEMPRS", valor = txtCodEmpresa.Text });
            //relatorio.parametros.Add(new Parametro() { parametro = "ANNUMRGTROEMPRG", valor = txtCodMatricula.Text });
            //relatorio.parametros.Add(new Parametro() { parametro = "ASQUADRO", valor = "1" });
            //adPdf.nome_arquivo = "Extrato_Previdenciario_" + DateTime.Today.ToString("yyyy_MM_dd") + ".pdf";
            //adPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adPdf.nome_arquivo;
           
            switch (e.CommandName)
            {
                case "Visualizar":

                    Relatorio relatorio = new Relatorio();
                    RelatorioBLL relBLL = new RelatorioBLL();
                    String relatorio_nome = "Rel_Cst_Judic_Corrig.rpt";

                    relatorio = relBLL.Listar(relatorio_nome);
                    relatorio.get_parametro("pHDRDATHOR").valor = CommandArgs;

                    Session[relatorio_nome] = relatorio;
                    ReportCrystal.RelatorioID = relatorio_nome;

                    //ReportCrystal.VisualizaRelatorio();
                    //ReportCrystal.Visible = true;
                    ArquivoDownload adExtratoPdf = new ArquivoDownload();
                    adExtratoPdf.nome_arquivo = "Rel_Custo_Judicial.pdf";
                    adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adExtratoPdf.nome_arquivo;
                    adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                    ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

                    Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
                    string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
                    AdicionarAcesso(fullUrl);
                    AbrirNovaAba(upCustoJudicial, fullUrl, adExtratoPdf.nome_arquivo);
                break;
                case "Delete":
                    ValorReferenciaBLL obj = new ValorReferenciaBLL();
                    Resultado res = obj.DeletarCustoJudicial(DateTime.Parse(CommandArgs));
                    MostraMensagem(pnlGrid_Mensagem, res.Mensagem, (res.Ok) ? "n_ok" : "n_error");
                    grdCustoJudicial.DataBind();
                break;
            }
        }

        protected void btnGerar_Click(object sender, EventArgs e)
        {
            ValorReferenciaBLL obj = new ValorReferenciaBLL();

            if (String.IsNullOrEmpty(txtDtIni.Text) || String.IsNullOrEmpty(txtDtFim.Text))
            {
                MostraMensagem(pnlGerarNovo_Mensagem, "Atenção! Os campos período inicial e final são obrigatórios.");
                return;
            }

            if (ddlUnMonetaria.SelectedValue=="0")
            {
                MostraMensagem(pnlGerarNovo_Mensagem, "Atenção! O campo unidade monetária é obrigatório.");
                return;
            }

            if (String.IsNullOrEmpty(txtVlrLimitador.Text))
            {
                MostraMensagem(pnlGerarNovo_Mensagem, "Atenção! O campo valor limitador é obrigatório.");
                return;
            }

            if (String.IsNullOrEmpty(txtDtAtuMonetaria.Text))
            {
                MostraMensagem(pnlGerarNovo_Mensagem, "Atenção! O campo dt. atualização monetária é obrigatório.");
                return;
            }


            DateTime DAT_INI = DateTime.Parse(txtDtIni.Text); 
            DateTime DAT_FIN = DateTime.Parse(txtDtFim.Text); 
            Double LIM_DSP = 0;
            Double.TryParse(txtVlrLimitador.Text, out LIM_DSP); 

            string LIM_FLG = chkIncidir.Checked ? "1" : "0";
            short COD_UM = 0;
            short.TryParse(ddlUnMonetaria.SelectedValue.ToString(), out COD_UM);
            DateTime DAT_ATLZ = DateTime.Parse(txtDtAtuMonetaria.Text);

            Resultado res = obj.GerarCustoJudicial(DAT_INI, DAT_FIN, LIM_DSP, LIM_FLG, COD_UM, DAT_ATLZ);

            if (res.Ok)
            {                
                pnlGerarNovo.Visible = false;
                pnlGrid.Visible = true;
                grdCustoJudicial.DataBind();
                MostraMensagem(pnlGrid_Mensagem, res.Mensagem, "n_ok");
            }
            else
            {
                MostraMensagem(pnlGerarNovo_Mensagem, res.Mensagem, "n_error");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {            
            pnlGerarNovo.Visible = false;
            pnlGrid.Visible = true;
        }  
      
    }
}