using IntegWeb.Entidades.Saude.Controladoria;
using IntegWeb.Saude.Aplicacao.BLL.Controladoria;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
//using StackExchange.Profiling.Data;

namespace IntegWeb.Saude.Web
{
    public partial class CadAnaliseSuS : BasePage
    {
        #region Atributos

        CadAnaliseSuSBLL obj = new CadAnaliseSuSBLL();
        
        Relatorio relatorio = new Relatorio();
        List<ArquivoDownload> lstAdPdf = new List<ArquivoDownload>();
        string relatorio_nome = "FormularioImpugSUS";
        string relatorio_titulo = "Relatório Impugnação - SUS";
        string relatorio_simples = @"~/Relatorios/FormularioImpugSUS.rpt";
        string nome_anexo = "Impugnacao_SUS";

        #endregion

        #region Eventos
              

        protected void Page_Load(object sender, EventArgs e)
        {
            //grdCadSus.Visible = false;

            if (!IsPostBack)
            {
               
                divPreenchimento.Visible = false;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (this.txtCodigoBeneficiario.Text.Equals(""))
            {
                base.MostraMensagemTelaUpdatePanel(this.upCadSus, "O Campo Código do Beneficiário é Obrigatório");
                this.LimpaCampos();
            }
            else
                grdCadSus.Visible = true;
                grdCadSus.PageIndex = 0;
                LimpaCampos();
                divPreenchimento.Visible = true;

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtCodigoBeneficiario.Text = "";
            txtBuscaAIHAPAC.Text = "";
            txtCompetencia.Text = "";
            grdCadSus.PageIndex = 0;
            grdCadSus.Visible = false;
            LimpaCampos();
        }

        protected void grdCadSus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCadSus.EditIndex = -1;
            grdCadSus.PageIndex = 0;
        }

        protected void grdCadSus_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCadSus.EditIndex = e.NewEditIndex;
        }
        
        #endregion

        protected void btnGeraRelatorio_Click(object sender, EventArgs e)
        {
            
            string NumeroRegistro;
            string DetalheMotivo;
            string MemoriaCalculo;
            string Documentos;
            string aihapac;
            
            //string ValPedido;
            

            // Verifica a Tipo de Petição
            string TipoPeticao = (rdbImpugnacao.Checked) ? "I" : "R";

            string PedidoA = (chkPedidoA.Checked) ? "S" : "N";
            string PedidoR = (chkPedidoR.Checked) ? "S": "N";
            string PedidoAR = (chkPedidoAR.Checked) ? "S" : "N";

            NumeroRegistro = txtNumeroRegistro.Text;
            DetalheMotivo = txtDetalheMotivo.Text;
            MemoriaCalculo = txtMemorialCalculo.Text;
            Documentos = txtDocsComprob.Text;
            
            // Verifica o Quadro Pedido

            string ValPedidoR = (PedidoR == "S") ? txtValorR.Text : "0";

            string ValPeditoAR = (PedidoAR == "S") ? txtValorAR.Text : "0";

            CadAnaliseSuSBLL bll = new CadAnaliseSuSBLL();
            int contador;
            contador = bll.ConsultarAIHPorUsuario(txtCodigoBeneficiario.Text).Count;
            string vAIH = bll.ConsultarAIHPorUsuario(txtCodigoBeneficiario.Text)[0].ToString();
                //(txtCodigoBeneficiario.Text).Count;

            if (txtBuscaAIHAPAC.Text == "")
            {
                if (contador > 1)
                {
                    MostraMensagemTelaUpdatePanel(upCadSus, "Participante possui mais do que 1 ABI cadastrado, favor especificar acima.");
                    return;
                }
                else
                {
                    txtBuscaAIHAPAC.Text = vAIH;
                }   
            }


            if (!InicializaRelatorio(txtCodigoBeneficiario.Text, txtBuscaAIHAPAC.Text, TipoPeticao, NumeroRegistro, txtTempestividade.Text, DetalheMotivo, MemoriaCalculo, Documentos, PedidoA, PedidoR, PedidoAR, ValPedidoR, ValPeditoAR, txtCompetencia.Text))
            {
                return;
            }  
            
            //ReportCrystal.VisualizaRelatorio();
            //ReportCrystal.Visible = true;
            ArquivoDownload adExtratoPdf = new ArquivoDownload();
            adExtratoPdf.nome_arquivo = nome_anexo + ".pdf";
            adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + adExtratoPdf.nome_arquivo;
            ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

            Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
            string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
            AdicionarAcesso(fullUrl);
            AbrirNovaAba(upCadSus, fullUrl, adExtratoPdf.nome_arquivo);
            
        }

        private bool InicializaRelatorio(string CodigoBenef, string CodigoAIHAPAC, string TipoPeticao, string NumeroRegistro, string Tempestividade, string DetalheMotivo, string MemoriaCalculo, string Documentos, string PedidoA, string PedidoR, string PedidoAR, string ValPedidoR, string ValPedidoAR, string Competencia)
        {

            relatorio.titulo = relatorio_titulo;

            relatorio.parametros = new List<Parametro>();
            relatorio.parametros.Add(new Parametro() { parametro = "paramTipoPeticao", valor = TipoPeticao });            
            relatorio.parametros.Add(new Parametro() { parametro = "paramRegistroProduto", valor = NumeroRegistro });
            relatorio.parametros.Add(new Parametro() { parametro = "paramChkPedidoA", valor = PedidoA });
            relatorio.parametros.Add(new Parametro() { parametro = "paramChkPedidoR", valor = PedidoR });
            relatorio.parametros.Add(new Parametro() { parametro = "paramChkPedidoAR", valor = PedidoAR });
            relatorio.parametros.Add(new Parametro() { parametro = "paramTempestividade", valor = Tempestividade });
            relatorio.parametros.Add(new Parametro() { parametro = "paramAIHAPAC", valor = CodigoAIHAPAC });
            relatorio.parametros.Add(new Parametro() { parametro = "paramCodigoBenef", valor = CodigoBenef });
            relatorio.parametros.Add(new Parametro() { parametro = "paramDetalheMotivo", valor = DetalheMotivo });
            relatorio.parametros.Add(new Parametro() { parametro = "paramMemorialCalculo", valor = MemoriaCalculo });
            relatorio.parametros.Add(new Parametro() { parametro = "paramDocumentos", valor = Documentos });
            relatorio.parametros.Add(new Parametro() { parametro = "paramValorPedidoR", valor = ValPedidoR });
            relatorio.parametros.Add(new Parametro() { parametro = "paramValorPedidoAR", valor = ValPedidoAR });
            relatorio.parametros.Add(new Parametro() { parametro = "paramCompetencia", valor = Competencia });

            relatorio.arquivo = relatorio_simples;

            Session[relatorio_nome] = relatorio;
            ReportCrystal.RelatorioID = relatorio_nome;

            return true;

        }

        public void LimpaCampos()
        {
            
            divPreenchimento.Visible = false;
            rdbImpugnacao.Checked = false;
            rdbRecurso.Checked = false;
            txtNumeroRegistro.Text = "";
            txtDetalheMotivo.Text = "";
            txtMemorialCalculo.Text = "";
            txtDocsComprob.Text = "";
            txtTempestividade.Text = "";
            chkPedidoA.Checked = false;
            chkPedidoR.Checked = false;
            chkPedidoAR.Checked = false;

            txtValorAR.Text = "";
            txtValorR.Text = "";
            
        }

        
    }
}