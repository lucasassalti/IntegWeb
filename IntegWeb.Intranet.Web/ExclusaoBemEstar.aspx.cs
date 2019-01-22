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
    public partial class ExclusaoBemEstar : BasePage
    {
        #region .: Propriedades :.

        int? NUM_IDNTF_RPTANT = null;
        string USU_INC = "";

        #endregion

        #region .: Eventos :.

        protected void Page_Load(object sender, EventArgs e)
        {
            string COD_EMPRS = Request.QueryString["nempr"];
            string NUM_RGTRO_EMPRG = Request.QueryString["nreg"];
            string NOM_EMPRG = Request.QueryString["cpart"];
            NUM_IDNTF_RPTANT = Util.String2Int32(Request.QueryString["nrepr"]);
            USU_INC = Request.QueryString["funcDsLoginname"];
            int contador;

            ExclusaoBemEstarBLL bll = new ExclusaoBemEstarBLL();

            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(COD_EMPRS) && !String.IsNullOrEmpty(NUM_RGTRO_EMPRG))
                {

                    txtCodEmpresa.Text = COD_EMPRS;
                    txtCodMatricula.Text = NUM_RGTRO_EMPRG;
                    txtNumRepresentante.Text = NUM_IDNTF_RPTANT.ToString();
                    txtNome.Text = NOM_EMPRG;
                }

                if (String.IsNullOrEmpty(txtCodEmpresa.Text) && String.IsNullOrEmpty(txtCodMatricula.Text))
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Empresa e Matrícula Invalídos, Favor inserir Empresa e Matrícula !");
                    return;
                }

                grdBemestar.DataSource = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text));
                grdBemestar.DataBind();
                contador = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text)).Count();
              

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
   
        protected void btnDeletar_Click(object sender, EventArgs e)
        {
            ExclusaoBemEstarBLL bll = new ExclusaoBemEstarBLL();
            Resultado res = new Resultado();

            res = bll.DeleteData(Convert.ToInt16(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text));

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Excluído com sucesso. Participante irá receber a partir da próxima edição da Revista Bem Estar ");
                SalvarLog("D"); //Tipo de Alteração : Delete  
                grdBemestar.DataSource = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text));
                grdBemestar.DataBind();
                btnInserir.Visible = true;
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                grdBemestar.DataSource = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text));
                grdBemestar.DataBind();
              

            }

        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            ExclusaoBemEstarBLL bll = new ExclusaoBemEstarBLL();
            Resultado res = new Resultado();

            res = bll.Inserir(Convert.ToInt16(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text), Util.String2Int32(txtNumRepresentante.Text == "" ? null : txtNumRepresentante.Text), USU_INC);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, "Participante Inserido com Sucesso ");
                SalvarLog("I"); //Tipo de Alteração : Inserção
                grdBemestar.DataSource = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text));
                grdBemestar.DataBind();
                btnInserir.Visible = false;
            }
            else
            {
                MostraMensagemTelaUpdatePanel(UpdatePanel, res.Mensagem);
                grdBemestar.DataSource = bll.GetData(Convert.ToInt16(txtCodEmpresa.Text), int.Parse(txtCodMatricula.Text));
                grdBemestar.DataBind();
      
                btnInserir.Visible = false;
            }
        }

        #endregion

        #region .: Métodos :.

        private void SalvarLog(string tp_atu)
        {
            ExclusaoBemEstarBLL bll = new ExclusaoBemEstarBLL();
            FUN_TBL_EXCLUSAO_REVISTA_LOG obj = new FUN_TBL_EXCLUSAO_REVISTA_LOG();

            obj.COD_EMPRS = Convert.ToInt16(txtCodEmpresa.Text);
            obj.NUM_RGTRO_EMPRG = Convert.ToInt32(txtCodMatricula.Text);
            obj.NUM_IDNTF_RPTANT = Util.String2Int32(txtNumRepresentante.Text == "" ? null : txtNumRepresentante.Text);
            obj.TP_ATU = tp_atu;
            obj.USU_INC = USU_INC;
            bll.SaveLog(obj);

        }

        #endregion

    }
}