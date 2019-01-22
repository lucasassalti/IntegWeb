using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.BLL.Controladoria;
using System.Collections;
using System.Data;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Saude.Aplicacao.BLL;
using IntegWeb.Entidades.Framework;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Saude.Aplicacao.BLL.Cobranca;
using IntegWeb.Entidades.Saude.Cobranca;
using IntegWeb.Framework;

namespace IntegWeb.Saude.Web
{
    public partial class ajustePontualExtratoUtilizacao : BasePage
    {

        public void Page_Load(object sender, EventArgs e)
        {

        }

        // CENÁRIO 1: Recupera os parâmetros passados na tela.
        // CENÁRIO 2: Valida se alguma informação não foi preenchida devidamente (null ou vázio), se for, retorna.
        // CENÁRIO 3: Carrega o GridView e faz o Bind na tela.
        public void SelecionarExtratoUtilizacao()
        {
            var objAcertoPontual = new AcertoPontualCoparticipacao();
            objAcertoPontual.ano_Ficha_Caixa = txtAnoFichaCaixa.Text;
            objAcertoPontual.mes_Ficha_Caixa = txtMesFichaCaixa.Text;
            objAcertoPontual.cod_Empresa = Convert.ToInt32(txtCodEmpresa.Text);
            objAcertoPontual.num_Matricula = txtNumMatricula.Text;

            if ((ValidarParametros(objAcertoPontual.ano_Ficha_Caixa,
                                   objAcertoPontual.mes_Ficha_Caixa,
                                   objAcertoPontual.cod_Empresa,
                                   objAcertoPontual.num_Matricula)) == false)
            {
                return;
            }

            else
            {
                GridView1.AutoGenerateColumns = false;
                GridView1.DataSource = CarregarGridView(objAcertoPontual);
                GridView1.Visible = true;
                GridView1.DataBind();
                //  limparParametros();
            }
        }



        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //NewEditIndex propriedade usada para determinar o índice da linha.  
            GridView1.EditIndex = e.NewEditIndex;
            SelecionarExtratoUtilizacao();
        }


        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // A propriedade EditIndex  quando = -1, cancela o modo editor
            GridView1.EditIndex = -1;
            SelecionarExtratoUtilizacao();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            var linha = e.RowIndex;

            if (String.IsNullOrEmpty(Convert.ToString(linha)) || linha == -1)
            {
                return;
            }

            // Recupera o valor ORIGINAL das propriedades do objeto.
            String val_p_Partic_Original = GridView1.DataKeys[linha].Values[0].ToString();
            String idc_Internacao_Original = GridView1.DataKeys[linha].Values[1].ToString();
            String val_Partic_Original = GridView1.DataKeys[linha].Values[2].ToString().Replace(".", ",");

            var acertoPontual = new AcertoPontualCoparticipacao();

            // Preencho o objeto.
            acertoPontual.cod_Covenente = Convert.ToInt32(GridView1.Rows[linha].Cells[0].Text);
            acertoPontual.ano_Fatura = GridView1.Rows[linha].Cells[9].Text;
            acertoPontual.num_Seq_Fatura = Convert.ToInt32(GridView1.Rows[linha].Cells[10].Text);
            acertoPontual.num_Seq_Atend = Convert.ToInt32(GridView1.Rows[linha].Cells[11].Text);
            acertoPontual.num_Seq_Item = Convert.ToInt32(GridView1.Rows[linha].Cells[12].Text);
            acertoPontual.cod_Recurso = Convert.ToInt32(GridView1.Rows[linha].Cells[13].Text);
            acertoPontual.val_Particip = Convert.ToDouble(val_Partic_Original);
            acertoPontual.val_p_Particip = Convert.ToDouble(val_p_Partic_Original);
            acertoPontual.RCOCODPROCEDIMENTO = GridView1.Rows[linha].Cells[23].Text;
            acertoPontual.idc_Internacao = idc_Internacao_Original;

            //Input pela aplicação.
            acertoPontual.ano_Ficha_Caixa = txtAnoFichaCaixa.Text;
            acertoPontual.mes_Ficha_Caixa = txtMesFichaCaixa.Text;
            acertoPontual.cod_Empresa = Convert.ToInt32(txtCodEmpresa.Text);
            acertoPontual.num_Matricula = txtNumMatricula.Text;


            var bllAcertoPontual = new AcertoPontualCoparticipacaoBLL();
            var objAcertoAlteracao = new AcertoPontualCoparticipacao();
            if (ValidarParametros(acertoPontual.ano_Ficha_Caixa, acertoPontual.mes_Ficha_Caixa, acertoPontual.cod_Empresa, acertoPontual.num_Matricula))
            {
                // Adiciona os valores ALTERADOS no objeto.
                TextBox val_p_Particip_Atualizado = GridView1.Rows[linha].FindControl("txtValpParticip") as TextBox;
                TextBox val_Particip_Atualizado = GridView1.Rows[linha].FindControl("txtValParticip") as TextBox;
                DropDownList idc_Internacao_Atualizado = GridView1.Rows[linha].FindControl("ddlIDC_Internacao") as DropDownList;

                // Nome usuário.
                var userName = Context.User.Identity.Name.Split(char.Parse(@"\"))[1];

                if (!String.IsNullOrEmpty(userName) && ( Util.validarCampoNumerico(val_p_Particip_Atualizado.Text)) == true)
                {
                    objAcertoAlteracao.val_p_Particip = Convert.ToDouble(val_p_Particip_Atualizado.Text.Trim());
                    objAcertoAlteracao.idc_Internacao = idc_Internacao_Atualizado.SelectedValue;
                    objAcertoAlteracao.val_Particip   = Convert.ToDouble(val_Particip_Atualizado.Text.Trim());
                    objAcertoAlteracao.RCOCODPROCEDIMENTO = acertoPontual.RCOCODPROCEDIMENTO;

                    // Log da Alteração de Dados
                    bllAcertoPontual.InserirLogAcertoCoparticipacao(acertoPontual, DateTime.Now, userName, objAcertoAlteracao);

                    //Atualiza as informações
                    bllAcertoPontual.AtualizarAcertoCoparticipacao(acertoPontual, objAcertoAlteracao);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Ocorreu um erro. Um dos parâmetros não foi preenchido ou não foi preenchido adequadamente.\\n Por favor, Verifique as informações antes de continuar. \\n");
                    return;
                }
            }

            GridView1.EditIndex = -1;
            SelecionarExtratoUtilizacao();
        }

        // CENÁRIO 1: Validação se algum parâmetro está NULO ou VAZIO, se sim então gera uma mensagem de erro e retorna False.        
        public Boolean ValidarParametros(String cdAnoFichaCaixa, String cdMesFichaCaixa, Int32 cdCodEmpresa, String cdNumMatricula)
        {
            if (String.IsNullOrEmpty(cdAnoFichaCaixa) ||
                String.IsNullOrEmpty(Convert.ToString(cdCodEmpresa)) ||
                String.IsNullOrEmpty(cdMesFichaCaixa) ||
                String.IsNullOrEmpty(cdNumMatricula))
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Algum dos parâmetros não foi preenchido, favor validar as informações!\\n");
                return false;
            }
            return true;
        }

        // Instância o objeto BLL, para chamar o método 'ListarAcertoPontualCoparticipacao' que acessa a camada DAL.
        public DataTable CarregarGridView(AcertoPontualCoparticipacao objAcertoPontualParticip)
        {
            var ajustePontualBll = new AcertoPontualCoparticipacaoBLL();
            var gridResult = ajustePontualBll.ListarAcertoPontualCoparticipacao(objAcertoPontualParticip);
            return gridResult;
        }

        // Botão GerarGrid chama a rotina de validação e que retorna informações na tela.
        protected void btnGerarGrid_Click(object sender, EventArgs e)
        {
            SelecionarExtratoUtilizacao();
        }

        //  Quando invocado, limpa os campos da tela.
        private void limparParametros()
        {
            txtAnoFichaCaixa.Text = null;
            txtMesFichaCaixa.Text = null;
            txtCodEmpresa.Text = null;
            txtNumMatricula.Text = null;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            SelecionarExtratoUtilizacao();
        }

    }
}