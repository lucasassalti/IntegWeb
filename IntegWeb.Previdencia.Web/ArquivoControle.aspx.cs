using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Previdencia.Aplicacao.BLL;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.BLL.Cadastro;
using IntegWeb.Previdencia.Aplicacao.DAL.Cadastro;
using IntegWeb.Framework;
using System.Collections.Specialized;
using IntegWeb.Entidades;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace IntegWeb.Previdencia.Web
{
    public partial class ArquivoControle : BasePage
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.TbEnvioMensagem.Visible = false;
            this.Form.DefaultButton = this.btnPesquisar.UniqueID;

            if (!IsPostBack)
            {
                txtMesGerar.Text = DateTime.Now.Month.ToString();
                txtAnoGerar.Text = DateTime.Now.Year.ToString();
                txtMesGerar_rec.Text = txtMesGerar.Text;
                txtAnoGerar_rec.Text = txtAnoGerar.Text;
                ArqPatrocinadoraEnvioBLL bll = new ArqPatrocinadoraEnvioBLL();
                CarregaDropDowList(ddlGrupo, bll.GetGrupoDdl().ToList<object>(), "DCR_GRUPO_EMPRS", "COD_GRUPO_EMPRS");
            }
        }

        protected void btnPublicar_Click(object sender, System.EventArgs e)
        {
            Resultado resultado = new Resultado();
            ArqPatrocinadoraEnvioBLL arqPatrocinadoraEnvioBLL = new ArqPatrocinadoraEnvioBLL();
            var user = (ConectaAD)Session["objUser"];            
            if (this.ValidaTelaEnvio())
            {
                string strMensagem = "";
                resultado = ValidaEnvio();
                if (resultado.Ok)
                {
                    arqPatrocinadoraEnvioBLL.caminho_servidor = Server.MapPath(@"~/");
                    resultado = arqPatrocinadoraEnvioBLL.Publicar(Util.String2Short(this.txtAnoGerar.Text), Util.String2Short(this.txtMesGerar.Text), Util.String2Short(this.ddlGrupo.SelectedValue), (user != null) ? user.login : "Desenv");
                    if (!resultado.Ok)
                    {
                        strMensagem = resultado.Mensagem;
                        //base.MostraMensagem(this.TbEnvioMensagem, "Arquivos publicados com sucesso", "n_ok");
                    }
                }
                else
                {
                    strMensagem = resultado.Mensagem + "\\n";
                }

                if (String.IsNullOrEmpty(strMensagem))
                {
                    base.MostraMensagem(this.TbEnvioMensagem, "Arquivos publicados com sucesso", "n_ok");
                    grdEnvio.DataBind();
                    //pnlPesquisa.Enabled = true;
                    //pnlPesquisa.Visible = false;
                }
                else
                {
                    base.MostraMensagem(this.TbEnvioMensagem, strMensagem, "n_warning");
                }
            }

        }

        private bool ValidaTelaEnvio()
        {
            bool result;
            if (string.IsNullOrEmpty(this.txtMesGerar.Text))
            {
                base.MostraMensagem(this.TbEnvioMensagem, "Campo Mês é obrigatório", "n_warning");
                result = false;
            }
            else if (string.IsNullOrEmpty(this.txtAnoGerar.Text))
            {
                base.MostraMensagem(this.TbEnvioMensagem, "Campo Ano é obrigatório", "n_warning");
                result = false;
            }
            else if (string.IsNullOrEmpty(this.ddlGrupo.SelectedValue) || this.ddlGrupo.SelectedValue=="0")
            {
                base.MostraMensagem(this.TbEnvioMensagem, "Selecione um Grupo para proceguir", "n_warning");
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        private Resultado ValidaEnvio()
        {
            Resultado result = new Resultado(true);
            ArqPatrocinadoraEnvioBLL BLL = new ArqPatrocinadoraEnvioBLL();
            ArqPatrocinadoraEnvioDAL.PRE_VIEW_ARQ_ENVIO_CONTROLE_AREA ARQ_ENVIO_CONTROLE = BLL.PacoteJaLiberado(Util.String2Short(this.txtMesGerar.Text), Util.String2Short(this.txtAnoGerar.Text), null, Util.String2Short(this.ddlGrupo.SelectedValue));

            if (ARQ_ENVIO_CONTROLE.QTD_PUBLICADOS>0)
            {
                //ret = "Atenção! Já existe(m) arquivo(s) de desconto LIBERADO(s) das seguintes areas: " + Environment.NewLine + Environment.NewLine + lista_liberados.Substring(0, lista_liberados.Length - 1) + "<br><b>Impossível LIBERAR novamente</b>";
                //base.MostraMensagem(this.TbEnvioMensagem, "Atenção! O pacote mensal já foi liberado para a Patrocinadora<br><b>Impossível LIBERAR novamente</b>", "n_warning");
                result.Erro("Atenção! O pacote mensal já foi liberado para a Patrocinadora " + this.ddlGrupo.SelectedItem.Text + "<br><b>Impossível publicar um novo pacote</b>");
            }else if (ARQ_ENVIO_CONTROLE.QTD_ENVIADOS == 0)
            {
                //base.MostraMensagem(this.TbEnvioMensagem, "Não existem arquivos disponíveis para envio para as Patrocinadoras", "n_warning");
                result.Erro("Não existem arquivos disponíveis para envio");
            }

            return result;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdEnvio.DataBind();
        }

        protected void grdEnvio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string[] id = e.CommandArgument.ToString().Split(',');
            var user = (ConectaAD)Session["objUser"];
            switch (e.CommandName)
            {
                case "Rejeitar":
                    ArqPatrocinadoraEnvioBLL arqPatrocinadoraEnvioBLL = new ArqPatrocinadoraEnvioBLL();
                    Resultado res = arqPatrocinadoraEnvioBLL.Rejeitar(Util.String2Short(this.txtAnoGerar.Text), Util.String2Short(this.txtMesGerar.Text),Util.String2Short(this.ddlGrupo.SelectedValue),short.Parse(e.CommandArgument.ToString()), (user != null) ? user.login : "Desenv");
                    if (res.Ok)
                    {
                        grdEnvio.DataBind();
                    }
                    break;
            }
        }

        protected void btnPesquisar_rec_Click(object sender, EventArgs e)
        {
            grdRecebidos.DataBind();
        }

        protected void grdRecebidos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

    }
}