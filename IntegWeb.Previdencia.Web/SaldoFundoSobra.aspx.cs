using IntegWeb.Previdencia.Aplicacao.BLL.Capitalizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using System.Data;
using IntegWeb.Framework;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace IntegWeb.Previdencia.Web
{
    public partial class SaldoFundoSobra : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                SaldoFundoSobraBLL objBLL = new SaldoFundoSobraBLL();
                CarregaDropDowList(dropSaldo, objBLL.GetPRE_TBL_GRUPO_EMPRS().ToList<object>(), "COD_GRUPO_EMPRS", "COD_GRUPO_EMPRS");
            }

        }

        #region .: Eventos :. 

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            grdDadosSaldo.DataBind();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {

            SaldoFundoSobraBLL objBLL = new SaldoFundoSobraBLL();



            txtAno.Text = "";
            txtMes.Text = "";

            CarregaDropDowList(dropSaldo,objBLL.GetPRE_TBL_GRUPO_EMPRS().ToList<object>(), "COD_GRUPO_EMPRS", "COD_GRUPO_EMPRS");
            
            //grdDadosSaldo.Visible = true;
            grdDadosSaldo.DataBind();


        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            Popup.Show();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
           
                SaldoFundoSobraBLL SaldoFundo = new SaldoFundoSobraBLL();
                PRE_TBL_ARQ_SALDO_FUNDO obj = new PRE_TBL_ARQ_SALDO_FUNDO();
                var user = (ConectaAD)Session["objUser"];


                    obj.COD_SALDO_FUNDO = Convert.ToInt16(txtInserirCodSaldo.Text);
                    obj.ANO_REF = Convert.ToInt16(txtInserirAno.Text);
                    obj.MES_REF = Convert.ToInt16(txtInserirMes.Text);
                    obj.COD_GRUPO_EMPRS = Convert.ToInt16(txtInserirCod.Text);
                    obj.NUM_PLBNF = Convert.ToInt16(txtInserirNumero.Text);
                    obj.VLR_SALDO_FUNDO = Convert.ToInt32(txtInserirValor.Text);
                    obj.LOG_INCLUSAO = user.nome;
                    obj.DTH_INCLUSAO = DateTime.Now;
                

                Resultado res = SaldoFundo.InserirSaldo(obj);

                if (res.Ok)
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Inclusão Feita com Sucesso");
                                        
                    grdDadosSaldo.EditIndex = -1;
                    grdDadosSaldo.PageIndex = 0;
                    grdDadosSaldo.DataBind();


                }
                else
                {
                    MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de inserir.\\nErro: " + res.Mensagem);
                }

               

            }

        protected void grdDadosSaldo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var user = (ConectaAD)Session["objUser"];

            switch (e.CommandName)
            {
                case "Gravar":

                    Resultado res = new Resultado();

                    SaldoFundoSobraBLL SaldoFundo = new SaldoFundoSobraBLL();
                    PRE_TBL_ARQ_SALDO_FUNDO obj = new PRE_TBL_ARQ_SALDO_FUNDO();

                   
                    
                        obj.VLR_SALDO_FUNDO = Convert.ToInt64(((TextBox)grdDadosSaldo.Rows[grdDadosSaldo.EditIndex].FindControl("txtSaldo")).Text);
                        obj.LOG_EXCLUSAO = user.login;
                        obj.DTH_EXCLUSAO = DateTime.Now;
                        obj.ANO_REF = Convert.ToInt16(((Label)grdDadosSaldo.Rows[grdDadosSaldo.EditIndex].FindControl("lblAnoRef")).Text);
                        obj.MES_REF = Convert.ToInt16(((Label)grdDadosSaldo.Rows[grdDadosSaldo.EditIndex].FindControl("lblMesRef")).Text);
                        obj.COD_GRUPO_EMPRS = Convert.ToInt16(((Label)grdDadosSaldo.Rows[grdDadosSaldo.EditIndex].FindControl("lblGrupo")).Text);
                        obj.NUM_PLBNF = Convert.ToInt16(((Label)grdDadosSaldo.Rows[grdDadosSaldo.EditIndex].FindControl("lblNum")).Text);
                    
                        res = SaldoFundo.AtualizaSaldo(obj);

                        if (res.Ok)
                        {
                            MostraMensagemTelaUpdatePanel(UpdatePanel, "Registro Atualizado com Sucesso");
                            grdDadosSaldo.EditIndex = -1;

                        }
                        else
                        {
                            MostraMensagemTelaUpdatePanel(UpdatePanel, "Ocorreu um erro na tentativa de atualizar.\\nErro: " + res.Mensagem);
                        }
                        break;
                    }
            }
        


        #endregion 

    }
}