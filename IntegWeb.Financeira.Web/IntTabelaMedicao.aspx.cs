using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IntegWeb.Entidades;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using IntegWeb.Framework.Aplicacao;
using IntegWeb.Financeira.Aplicacao.BLL;
using IntegWeb.Financeira.Aplicacao.BLL.Carga_Protheus;
using IntegWeb.Financeira.Aplicacao.ENTITY;


namespace IntegWeb.Financeira.Web
{
    public partial class IntTabelaMedicao : BasePage
    {
        IntTabelaMedicaoBLL IntegracaoProtheus = new IntTabelaMedicaoBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarInformacaoControles();
                PesquisaGridMedctr();
            }
        } 
        

        public void CarregarInformacaoControles()
        {
            // Evento 
            CarregaDropDowList(ddlEvento, IntegracaoProtheus.BuscaSx5010Evento().ToList<object>(), "x5_descri", "x5_chave");

            // Produto
            CarregaDropDowList(ddlProduto, IntegracaoProtheus.BuscaSb1010().ToList<object>(), "B1_Desc", "B1_Cod");

            // Submassa
            CarregaDropDowList(ddlSubmassa, IntegracaoProtheus.BuscaCv0010().ToList<object>(), "cv0_desc", "cv0_codigo");

            // Programa
            CarregaDropDowList(ddlProgramaPlano, IntegracaoProtheus.BuscaCtd010().ToList<object>(), "ctd_desc01", "ctd_item");

            // Custo
            CarregaDropDowList(ddlCentroCusto, IntegracaoProtheus.BuscaCtt010().ToList<object>(), "ctt_desc01", "ctt_custo");

            // Patrocinador
            CarregaDropDowList(ddlPatrocinador, IntegracaoProtheus.BuscaCth010().ToList<object>(), "cth_desc01", "cth_clvl");

            // Liquidação
            CarregaDropDowList(ddlTipoLiquidacao, IntegracaoProtheus.BuscaSx5010Liquidacao().ToList<object>(), "x5_descri", "x5_chave");

            // Processamento
            CarregaDropDowList(ddlTipoProcessamento, IntegracaoProtheus.BuscaCargaProtheusTipo().ToList<object>(), "DCR_CARGA_TIPO", "COD_CARGA_TIPO");
        }

        [WebMethod(EnableSession = true)]
        public static IEnumerable<PessFisicaJuridica> BuscaResultado(string pBusca)
        {
            IntTabelaMedicaoBLL IntegracaoProtheus = new IntTabelaMedicaoBLL();

            IEnumerable<PessFisicaJuridica> retorno;
            retorno = IntegracaoProtheus.BuscaEmpregado(pBusca);

            return retorno;

        }

        [WebMethod(EnableSession = true)]
        public static IEnumerable<PessFisicaJuridica> BuscaResultadoCpfCnpj(string pBusca)
        {
            IntTabelaMedicaoBLL IntegracaoProtheus = new IntTabelaMedicaoBLL();

            IEnumerable<PessFisicaJuridica> retorno;
            retorno = IntegracaoProtheus.BuscaCpfCnpj(pBusca);

            return retorno;

        }

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        public void PesquisaGridMedctr()
        {
           
            IntTabelaMedicaoBLL integProtheus = new IntTabelaMedicaoBLL();
            CarregaGrid("grdPesquisa", integProtheus.PesquisarGrid(), grdPesquisa);
        }

        protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdPesquisa"] != null)
            {
                grdPesquisa.PageIndex = e.NewPageIndex;
                grdPesquisa.DataSource = ViewState["grdPesquisa"];
                grdPesquisa.DataBind();
            }
        }

        protected void btnInserir_Click(object sender, EventArgs e)
        {
            pnlPesquisa.Visible = false;
            pnlAtualiza.Visible = true;
            btnSalvar.Visible = true;
            btnVoltar.Visible = true;
            btnLimparInclusao.Visible = true;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            pnlPesquisa.Visible = true;
            pnlAtualiza.Visible = false;
            btnSalvar.Visible = false;
            btnVoltar.Visible = false;
            btnLimparInclusao.Visible = false;
            limparCamposInclusao();
        }

        protected void btnLimparInclusao_Click(object sender, EventArgs e)
        {
            limparCamposInclusao();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtbuscaEmpregado.Text = string.Empty;
            txtbuscaEmpresa.Text = string.Empty;
            txtConvenente.Text = string.Empty;
            txtDataInclusao.Text = string.Empty;
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            DateTime dataInclusao;
            DateTime.TryParse(txtDataInclusao.Text, out dataInclusao);

            Int32 matricula, empresa, convenente;
            String banco, agencia;
            int.TryParse(txtbuscaEmpregado.Text, out matricula);
            int.TryParse(txtbuscaEmpresa.Text, out empresa);
            int.TryParse(txtConvenente.Text, out convenente);
            banco = txtBanco.Text;
            agencia = txtAgencia.Text;
            String dtInclusao = string.Empty;

            if (dataInclusao > DateTime.MinValue)
            {
                dtInclusao = dataInclusao.ToString("yyyyMMdd");
            }            

            IntTabelaMedicaoBLL medicaoBll = new IntTabelaMedicaoBLL();

            DataTable dt = new DataTable();
            
            if (!AlertaCamposPesquisa())
            {
                return;
            }
            else
            {
               dt = medicaoBll.PesquisaGridParametrizada(empresa, matricula, convenente, dtInclusao, banco, agencia);
            }
            
            CarregaGrid("grdPesquisa", dt, grdPesquisa);

        }

        private bool AlertaCamposPesquisa()
        {
            if (!String.IsNullOrEmpty(txtbuscaEmpregado.Text) || !String.IsNullOrEmpty(txtbuscaEmpresa.Text) ||
                !String.IsNullOrEmpty(txtConvenente.Text) || !String.IsNullOrEmpty(txtDataInclusao.Text) ||
                !String.IsNullOrEmpty(txtBanco.Text) || !String.IsNullOrEmpty(txtAgencia.Text))
            {
                return true;
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatepanel, "Atenção! Preencha pelo menos um campo para iniciar a pesquisa.");
                return false;
            }

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            decimal valor;
            decimal.TryParse(txtValor.Text, out valor);

            DateTime dataVencimento;
            DateTime.TryParse(txtDataVencimento.Text.Trim(), out dataVencimento);

            // Tratamento do tipo de pessoa (tipo de busca)
            String tipoBusca = hiddenTipoPesquisa.Value.Replace("pJuridica -", "").Replace("pFisica -", "").Trim();

            #region DataVencimento
            String dataRef = string.Empty;
            if (dataVencimento > DateTime.MinValue)
            {
                dataRef = dataVencimento.ToString("MMyyyy");
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatepanel, "A data de vencimento não pode ser nula!");
                return;
            }
            #endregion

            if (valor <= 0)
            {
                MostraMensagemTelaUpdatePanel(upUpdatepanel, "O valor precisa ser maior que zero!");
                return;
            }

            if (!ValidarCamposObrigatorios())
            {
                return;
            }

            #region Criação do Objeto
            IntTabelaMedicaoBLL medicaoBll = new IntTabelaMedicaoBLL();
            MEDCTR mdtr = new MEDCTR();

            /* Rotina de validação do Dígito validador bancário e Conta Corrente */
            PessFisicaJuridica psj = medicaoBll.BuscarInformacaoBancaria(hiddenCodigoBanco.Value, hiddenCodigoTipoConta.Value, hiddenCodigoContaCorrente.Value);

            /* Verifica qual tabela foi feito o select e o tipo de pessoa / convenente  - Adiciona o código, os demais adiciona zero. */
            mdtr.EVENTO = ddlEvento.SelectedValue;

            mdtr.COD_CONVENENTE = (tipoBusca == "Convenente") ? Convert.ToInt32(hiddenCodigo.Value) : 0;
            mdtr.COD_EMPRS = (tipoBusca == "Empregado") ? Convert.ToInt16(hiddenCodigoEmpresa.Value) : Convert.ToInt16(0); // Se for usuário, descriminar a empresa.
            mdtr.NUM_RGTRO_EMPRG = (tipoBusca == "Empregado") ? Convert.ToInt32(hiddenCodigo.Value) : 0;
            mdtr.NUM_MATR_PARTF = (tipoBusca == "Emprg_dpdte") ? Convert.ToInt32(hiddenCodigo.Value) : 0;
            mdtr.NUM_IDNTF_RPTANT = (tipoBusca == "Repres_uniao_fss") ? Convert.ToInt32(hiddenCodigo.Value) : 0;
            mdtr.NUM_IDNTF_DPDTE = (tipoBusca == "Dependente") ? Convert.ToInt32(hiddenCodigo.Value) : 0;

            mdtr.TIPOFOR = hiddenTipoPessoa.Value;
            mdtr.TIPOPAR = ddlTipoParticipante.SelectedValue.Trim();
            mdtr.XNUMCT = txtContrato.Text.Trim();
            mdtr.PRODUT = ddlProduto.SelectedValue.Trim();
            mdtr.VALMED = valor;
            mdtr.DTVENC = dataVencimento.ToString("yyyyMMdd");
            mdtr.PROGRAMA = ddlProgramaPlano.SelectedValue.Trim();
            mdtr.SUBMASSA = ddlSubmassa.SelectedValue.Trim();
            mdtr.CCUSTO = ddlCentroCusto.SelectedValue.Trim();
            mdtr.PATROCINADOR = ddlPatrocinador.SelectedValue.Trim();
            mdtr.XTPLIQ = ddlTipoLiquidacao.SelectedValue.Trim();
            mdtr.TP_PROC = Convert.ToInt16(ddlTipoProcessamento.SelectedValue);
            mdtr.COMPENSAVEL = "N";
            mdtr.ANO_FATURA = "0";
            mdtr.NUM_SEQ_ATEND = 0;
            mdtr.NUM_SEQ_FATURA = 0;
            mdtr.NUM_SEQ_ITEM = 0;
            mdtr.STATUS = "1";
            mdtr.XTPMED = "D";
            mdtr.DTREF = dataRef;
            mdtr.DTINCL = DateTime.Now.ToString("yyyyMMdd");
            mdtr.COD_ASSOC = 0;
            mdtr.SEQ_MEDCTR = medicaoBll.chaveSequencial() + 1;
            mdtr.PROJETO = " "; //Não pode ser nulo, na especificação pedia este campo nulo.
            mdtr.NOSSONUMERO = " "; //Não pode ser nulo, na especificação pedia este campo nulo.
            mdtr.NUM_LOTE = medicaoBll.chaveSequencialLote() + 1;            

            /* Informações bancárias */
            mdtr.BANCO = psj.codigoBanco.ToString();                  // cód. banco
            mdtr.AGENCIA = hiddenCodigoAgencia.Value.ToString();      // agencia
            mdtr.DVAGE = psj.codigoDigVerificadorAgencia.ToString();  // dig. verificador Agência
            mdtr.NUMCON = psj.codigoContaCorrente.ToString();         // número conta
            mdtr.DVNUMCON = psj.codigoDvContaCorrente.ToString();     // dg. Verificador C/c
            mdtr.DSC_VERBA = " ";
            #endregion


            #region Salvar (Chamar BLL/ DAL)
            Entidades.Resultado res = new Resultado();

            res = medicaoBll.SalverMedicao(mdtr);

            if (res.Ok)
            {
                MostraMensagemTelaUpdatePanel(upUpdatepanel, res.Mensagem);
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upUpdatepanel, @"Ocorreu um erro, verifique se os campos estão preenchidos adequadamente! \\n" + res.Mensagem);
            }
            #endregion

        }

        private bool ValidarCamposObrigatorios()
        {
            string produto = (ddlProduto.SelectedValue != "0") ? ddlProduto.SelectedValue : null;
            string ccusto = (ddlCentroCusto.SelectedValue != "0") ? ddlCentroCusto.SelectedValue : null;
            string programa = (ddlProgramaPlano.SelectedValue != "0") ? ddlProgramaPlano.SelectedValue : null;
            string patriocinador = (ddlPatrocinador.SelectedValue != "0") ? ddlPatrocinador.SelectedValue : null;
            string submassa = (ddlSubmassa.SelectedValue != "0") ? ddlSubmassa.SelectedValue : null;
            string contrato = txtContrato.Text;
            string operacao = (ddlOperacao.SelectedValue != "0") ? ddlOperacao.SelectedValue : null;
            string liquidacao = (ddlTipoLiquidacao.SelectedValue != "0") ? ddlTipoLiquidacao.SelectedValue : null;
            string processamento = (ddlTipoProcessamento.SelectedValue != "0") ? ddlTipoProcessamento.SelectedValue : null;


            if (!String.IsNullOrEmpty(produto) && !String.IsNullOrEmpty(ccusto) && !String.IsNullOrEmpty(operacao) &&
                 !String.IsNullOrEmpty(programa) && !String.IsNullOrEmpty(patriocinador) && !String.IsNullOrEmpty(submassa) &&
                 !String.IsNullOrEmpty(contrato) && !String.IsNullOrEmpty(liquidacao) && !String.IsNullOrEmpty(processamento)
             )
            {
                return true;
            }
            else
            {
                if (String.IsNullOrEmpty(produto))
                {
                    ddlProduto.Focus();
                }
                else if (String.IsNullOrEmpty(programa))
                {
                    ddlProgramaPlano.Focus();
                }
                else if (String.IsNullOrEmpty(submassa))
                {
                    ddlSubmassa.Focus();
                }
                else if (String.IsNullOrEmpty(ccusto))
                {
                    ddlCentroCusto.Focus();
                }
                else if (String.IsNullOrEmpty(patriocinador))
                {
                    ddlPatrocinador.Focus();
                }
                else if (String.IsNullOrEmpty(operacao))
                {
                    ddlOperacao.Focus();
                }
                else if (String.IsNullOrEmpty(contrato))
                {
                    txtContrato.Focus();
                }
                else if (String.IsNullOrEmpty(liquidacao))
                {
                    ddlTipoLiquidacao.Focus();
                }
                else if (String.IsNullOrEmpty(processamento))
                {
                    ddlTipoProcessamento.Focus();
                }

                MostraMensagemTelaUpdatePanel(upUpdatepanel, "Campo obrigatório não preenchido! ");
                return false;

            }
        }

        public void limparCamposInclusao()
        {
            txtClienteFornecedor.Text = String.Empty;
            txtContrato.Text = String.Empty;
            txtCpfCnpj.Text = String.Empty;
            txtDataVencimento.Text = String.Empty;
            txtTipoPessoa.Text = String.Empty;
            txtValor.Text = String.Empty;

    
            ddlTipoProcessamento.SelectedValue = "0";
            ddlCentroCusto.SelectedValue = "0";
            ddlEvento.SelectedValue = "0";
            ddlOperacao.SelectedValue = "0";
            ddlNegocio.SelectedValue = "0";
            ddlPagarReceber.SelectedValue = "0";
            ddlPatrocinador.SelectedValue = "0";
            ddlProduto.SelectedValue = "0";
            ddlProgramaPlano.SelectedValue = "0";
            ddlSubmassa.SelectedValue = "0";
            ddlTipoLiquidacao.SelectedValue = "0";
            ddlTipoParticipante.SelectedValue = "0";
            ddlTipoProcessamento.SelectedValue = "0";
        }

    }
}