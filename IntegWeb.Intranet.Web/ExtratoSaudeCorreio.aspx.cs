using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Intranet.Aplicacao;
using IntegWeb.Intranet.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntegWeb.Intranet.Web
{
    public partial class ExtratoSaudeCorreio : BasePage
    {
        #region .: Propriedades :.

        int? NUM_IDNTF_RPTANT = null;
        string NUM_CPF_EMPRG;
        string USU_INC = "";
        decimal ID_CHAMADO = 0;

        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            string COD_EMPRS = Request.QueryString["nempr"];
            string NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
            string NOM_EMPRG = Request.QueryString["cpart"];
            NUM_IDNTF_RPTANT = Util.String2Int32(Request.QueryString["nrepr"]);
            USU_INC = Request.QueryString["funcDsLoginname"];
            NUM_CPF_EMPRG = Request.QueryString["pessDsCpf"];
            ID_CHAMADO = Convert.ToDecimal(Request.QueryString["idChamCdChamado"]);
            int contador;

            ExtratoSaudeCorreioBLL bll = new ExtratoSaudeCorreioBLL();

            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(COD_EMPRS) && !String.IsNullOrEmpty(NUM_RGTRO_EMPRG))
                {
                    txtCodEmpresa.Text = COD_EMPRS;
                    txtCodMatricula.Text = NUM_RGTRO_EMPRG;
                    txtCPF.Text = NUM_CPF_EMPRG.ToString();
                    txtRepresentante.Text = NUM_IDNTF_RPTANT.ToString();
                    txtNome.Text = NOM_EMPRG;
                }
                if (String.IsNullOrEmpty(txtCodEmpresa.Text) && String.IsNullOrEmpty(txtCodMatricula.Text))
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Empresa e Matrícula Invalídos, Favor inserir Empresa e Matrícula !");
                    return;
                }

                grdExtratoSaudeCorreio.DataSource = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), Convert.ToInt64(txtCodMatricula.Text));
                grdExtratoSaudeCorreio.DataBind();
                contador = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), Convert.ToInt64(txtCodMatricula.Text)).Count();
                grdExtratoSaudeCorreio.Visible = true;

                if (contador == 0)
                {
                    btnInserir.Visible = true;
                }
                else
                {
                    btnInserir.Visible = false;
                }

            }

        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            ExtratoSaudeCorreioBLL bll = new ExtratoSaudeCorreioBLL();
            Resultado res = new Resultado();


            NUM_CPF_EMPRG = Util.LimparCPF(txtCPF.Text);


            res = bll.Inserir(Convert.ToInt16(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), Int64.Parse(NUM_CPF_EMPRG), Util.String2Int32(txtRepresentante.Text == "" ? null : txtRepresentante.Text), USU_INC, ID_CHAMADO);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Participante Inserido com Sucesso para receber Extratos de Saúde pelo Correio !");
                SalvarLog("I"); //Tipo de Alteração : Inserção
                grdExtratoSaudeCorreio.DataSource = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), Convert.ToInt64(txtCodMatricula.Text));
                grdExtratoSaudeCorreio.DataBind();
                btnInserir.Visible = false;
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                grdExtratoSaudeCorreio.DataSource = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), Convert.ToInt64(txtCodMatricula.Text));
                grdExtratoSaudeCorreio.DataBind();
                btnInserir.Visible = false;
            }

        }

        protected void btnDeletar_Click(object sender, EventArgs e)
        {
            ExtratoSaudeCorreioBLL bll = new ExtratoSaudeCorreioBLL();
            Resultado res = new Resultado();

            NUM_CPF_EMPRG = Util.LimparCPF(txtCPF.Text);

            res = bll.DeleteData(Convert.ToInt16(txtCodEmpresa.Text), Convert.ToInt64(txtCodMatricula.Text));

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Participante Deletado com Sucesso, Esse Participante não irá receber Extrato de Saúde pelo Correio !");
                SalvarLog("D"); //Tipo de Alteração : Delete
                grdExtratoSaudeCorreio.DataSource = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), Convert.ToInt64(txtCodMatricula.Text));
                grdExtratoSaudeCorreio.DataBind();
                btnInserir.Visible = true;
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                grdExtratoSaudeCorreio.DataSource = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), Convert.ToInt64(txtCodMatricula.Text));
                grdExtratoSaudeCorreio.DataBind();

            }
        }

        #endregion

        #region .: Métodos :.

        private void SalvarLog(string tp_acao)
        {
            ExtratoSaudeCorreioBLL bll = new ExtratoSaudeCorreioBLL();
            FCESP_EXT_AMH_EXCECAO_LOG obj = new FCESP_EXT_AMH_EXCECAO_LOG();

            obj.COD_EMPRS = Convert.ToInt16(txtCodEmpresa.Text);
            obj.NUM_RGTRO_EMPRG = Convert.ToInt64(txtCodMatricula.Text);
            obj.NUM_CPF_EMPRG = Convert.ToInt64(NUM_CPF_EMPRG);
            obj.NUM_IDNTF_RPTANT = Util.String2Int32(txtRepresentante.Text == "" ? null : txtRepresentante.Text);
            obj.TP_ACAO = tp_acao;
            obj.USER_INC = USU_INC;
            bll.SaveLog(obj);

        }

        #endregion
    }
}