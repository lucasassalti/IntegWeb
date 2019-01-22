using IntegWeb.Entidades;
using IntegWeb.Entidades.Framework;
using IntegWeb.Financeira.Aplicacao.BLL.Tesouraria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Financeira.Web
{
    public partial class ExportaRelFinContab : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
           
            
            Processo_Mensagem.Visible = false;
            
            DataTable dtCC = new DataTable();
            DataTable dtGB = new DataTable();

            ExportaRelIntFinContabBLL bll = new ExportaRelIntFinContabBLL();
            dtCC = bll.ListarDadosCC(Convert.ToDateTime(txtDatInicio.Text), Convert.ToDateTime(txtDatFim.Text));
            dtGB = bll.ListarDadosGB(Convert.ToDateTime(txtDatInicio.Text), Convert.ToDateTime(txtDatFim.Text));

            if (dtCC.Rows.Count > 0)
            {
                //Download do Excel 
                ArquivoDownload adXlsExportaRelCC = new ArquivoDownload();
                adXlsExportaRelCC.nome_arquivo = "Rel_Int_Contab.xls";
                //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
                adXlsExportaRelCC.dados = dtCC;
                Session[ValidaCaracteres(adXlsExportaRelCC.nome_arquivo)] = adXlsExportaRelCC;
                string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsExportaRelCC.nome_arquivo);
                //   AdicionarAcesso(fUrl);
                AbrirNovaAba(this, fUrl, adXlsExportaRelCC.nome_arquivo);
            }

            if (dtGB.Rows.Count > 0)
            {
                 //Download do Excel 
                ArquivoDownload adXlsExportaRelGB = new ArquivoDownload();
                adXlsExportaRelGB.nome_arquivo = "Rel_Int_Fin.xls";
                //adTxtRecad.caminho_arquivo = Server.MapPath(@"UploadFile\") + adTxtRecad.nome_arquivo;
                adXlsExportaRelGB.dados = dtGB;
                Session[ValidaCaracteres(adXlsExportaRelGB.nome_arquivo)] = adXlsExportaRelGB;
                string fUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adXlsExportaRelGB.nome_arquivo);
                //   AdicionarAcesso(fUrl);
                AbrirNovaAba(this, fUrl, adXlsExportaRelGB.nome_arquivo);
            }

            if (dtCC.Rows.Count == 0 && dtGB.Rows.Count == 0)
            {
                MostraMensagem(Processo_Mensagem, "Não há registros para serem exportados !");
                return;
            }

        }
    }
}