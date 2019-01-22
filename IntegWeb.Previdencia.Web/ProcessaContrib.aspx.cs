using IntegWeb.Entidades.Previdencia.Calculo_Acao_Judicial;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace IntegWeb.Previdencia.Web
{
    public partial class ProcessaContrib : BasePage
    {
        #region Atributos
        AcaoJudicialBLL obj = new AcaoJudicialBLL();
        string mensagem = "";
        #endregion

        #region Eventos
        //
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                CarregaTela();

            }
            else
            {
                txtVlInicial.Enabled = chkVlInicial.Checked;
                txtVlComPleito.Enabled = chkVlComPleito.Checked;
                txtVlSemPleito.Enabled = chkVlSemPleito.Checked;

            }
        }

        #region Aba Tela Inicial

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!txtCodMatricula.Equals("") && !txtCodEmpresa.Equals(""))
            {

                obj.cod_emprs = int.Parse(txtCodEmpresa.Text);
                obj.matricula = int.Parse(txtCodMatricula.Text);

                DataTable dt = obj.CarregaProcesso();

                HdEmpresa.Value = txtCodEmpresa.Text;
                hdMatricula.Value = txtCodMatricula.Text;

                if (dt.Rows.Count > 0)
                {

                    divParticip.Visible = true;
                    TxtNom.Text = dt.Rows[0]["nom_emprg"].ToString();
                    hdNumPartif.Value = dt.Rows[0]["num_matr_partf"].ToString();

                    CarregaGrid("grdSrc", dt, grdSrc);

                }
                else
                {
                    divParticip.Visible = false;
                    MostraMensagemTelaUpdatePanel(upContrib, "Nenhum Registro encontrado!");
                }

            }
        }
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            divParticip.Visible = false;
            txtCodMatricula.Text = "";
            txtCodEmpresa.Text = "";

        }
        protected void btnInserir_Click(object sender, EventArgs e)
        {
            DivAcao(divInsert, divSelect);
        }
        protected void grdSrc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (ViewState["grdSrc"] != null)
            {
                grdSrc.PageIndex = e.NewPageIndex;
                grdSrc.DataSource = ViewState["grdSrc"];
                grdSrc.DataBind();
            }
        }
        protected void grdSrc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] commandArgs;

            switch (e.CommandName)
            {

                case "Deletar":

                    hdSeq.Value = e.CommandArgument.ToString();
                    try
                    {
                        if (Session["objUser"] != null)
                        {
                            var user = (ConectaAD)Session["objUser"];
                            obj.responsavel = user.login;
                            if (!hdNumPartif.Value.Equals("0"))
                            {
                                obj.num_seq_prc = int.Parse(hdSeq.Value);
                                obj.num_matr_partf = int.Parse(hdNumPartif.Value);

                                bool ret = obj.DeletarSrcContribuicoes(out mensagem);

                                if (ret)
                                {


                                    obj.cod_emprs = int.Parse(HdEmpresa.Value);
                                    obj.matricula = int.Parse(hdMatricula.Value);


                                    DataTable dt = obj.CarregaProcesso();

                                    CarregaGrid("grdSrc", dt, grdSrc);
                                }
                                MostraMensagemTelaUpdatePanel(upContrib, mensagem);

                            }
                            else
                            {
                                MostraMensagemTelaUpdatePanel(upContrib, "Problemas contate o administrador do sistema ");
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                        MostraMensagemTelaUpdatePanel(upContrib, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
                    }

                    break;


                case "processo":
                    commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

                    hdNumPartif.Value = commandArgs[1];
                    LimpaCampos();
                    hdSeq.Value = commandArgs[0];
                    CarregaParametros();
                    RegraTela(int.Parse(commandArgs[2]));
                    DivAcao(divInsert, divSelect);
                    CarregaRelatorioDisp();
                    break;
                default:
                    break;
            }
        }

        protected void grdParametro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] commandArgs;

            switch (e.CommandName)
            {
                case "Deletar":
                    commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    try
                    {
                        if (Session["objUser"] != null)
                        {
                            var user = (ConectaAD)Session["objUser"];
                            obj.responsavel = user.login;
                            if (!commandArgs[0].Equals("0"))
                            {
                                obj.num_matr_partf = int.Parse(commandArgs[0]);
                                obj.num_seq_prc = int.Parse(commandArgs[1]);
                                obj.tip_bnf = int.Parse(commandArgs[2]);
                                obj.id_acao_processo = 0;

                                switch (obj.tip_bnf)
                                {
                                    case 1:
                                        obj.id_acao_processo = 13;
                                        break;
                                    case 2:
                                        obj.id_acao_processo = 14;
                                        break;
                                    case 3:
                                        obj.id_acao_processo = 15;
                                        break;
                                    case 4:
                                        obj.id_acao_processo = 16;
                                        break;
                                }

                                bool ret = false;
                                string mensagem2 = "";

                                if (obj.DeletarParametro(out mensagem) && ValidaHiddenfield())
                                {
                                    ret = obj.ProcessaCalculoReal(out mensagem2);
                                    if (ret)
                                    {
                                        //CarregaRelatorioDisp();
                                        //LimpaCampos();
                                        //RegraTela(id_processo);
                                        ProcessarRelatorio(obj.CarregaRelatorio());

                                        hdNumPartif.Value = commandArgs[0];
                                        //LimpaCampos();
                                        hdSeq.Value = commandArgs[1];
                                        CarregaParametros();
                                        MostraMensagemTelaUpdatePanel(upContrib, mensagem);
                                    }
                                    else
                                    {
                                        MostraMensagemTelaUpdatePanel(upContrib, mensagem2);
                                    }
                                }
                                else MostraMensagemTelaUpdatePanel(upContrib, mensagem);

                                /*
                                MostraMensagemTelaUpdatePanel(upContrib, mensagem);
                                //obj.cod_emprs = int.Parse(HdEmpresa.Value);
                                //obj.matricula = int.Parse(hdMatricula.Value);
                                //DataTable dt = obj.CarregaProcesso();
                                //CarregaGrid("grdSrc", dt, grdSrc);
                                hdNumPartif.Value = commandArgs[0];
                                //LimpaCampos();
                                hdSeq.Value = commandArgs[1];
                                CarregaParametros();
                                */

                            }
                            else
                            {
                                MostraMensagemTelaUpdatePanel(upContrib, "Problemas contate o administrador do sistema");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MostraMensagemTelaUpdatePanel(upContrib, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
                    }

                    break;

                default:
                    break;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            //divParticip.Visible = false;
            //txtCodMatricula.Text = "";
            //txtCodEmpresa.Text = "";
            LimpaCampos();
            RegraTela(0);
            DivAcao(divSelect, divInsert);
        }

        #endregion

        #region Aba Salário Real de Contribuição
        protected void btnSrc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.responsavel = user.login;
                    int id;
                    if (!txtEmpresa.Text.Equals(""))
                        obj.cod_emprs = int.Parse(txtEmpresa.Text);

                    if (!txtMatricula.Text.Equals(""))
                        obj.matricula = int.Parse(txtMatricula.Text);


                    obj.num_matr_partf = int.Parse(hdNumPartif.Value);
                    obj.flag_abn = rdAbono.SelectedValue.Trim();
                    obj.num_pasta = txtPasta.Text;
                    obj.num_processo = txtProcesso.Text;
                    obj.obs_src = txtObs.Text;
                    obj.cod_tiplto = int.Parse(drpTipoPleito.SelectedValue);
                    obj.cod_vara = txtCodVara.Text;
                    obj.flag_acao_Jud = rdPoloAcJudic.SelectedValue.Trim();


                    bool ret = obj.ProcessaSrcContribuicoes(out mensagem, out id);

                    if (ret)
                    {

                        LimpaCampos();
                        obj.num_seq_prc = id;
                        hdSeq.Value = id.ToString();
                        RegraTela(1);
                        obj.id_acao_processo = 1;
                        ProcessarRelatorio(obj.CarregaRelatorio());
                        CarregaRelatorioDisp();

                    }
                    else
                    {

                        MostraMensagemTelaUpdatePanel(upContrib, mensagem);
                    }
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upContrib, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }
        protected void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            try
            {


                if (!txtMatricula.Equals("") && !txtEmpresa.Equals(""))
                {

                    obj.cod_emprs = int.Parse(txtEmpresa.Text);
                    obj.matricula = int.Parse(txtMatricula.Text);

                    DataTable dt = obj.CarregaParticipante();

                    lblNome.Visible = true;
                    if (dt.Rows.Count > 0)
                    {
                        lblNome.Text = dt.Rows[0]["nom_emprg"].ToString();
                        hdNumPartif.Value = dt.Rows[0]["NUM_MATR_PARTF"].ToString();
                    }
                    else
                    {
                        lblNome.Text = "Nenhum registro encontrado ";
                        hdNumPartif.Value = "0";
                    }

                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upContrib, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }
        #endregion

        #region Aba  Contribuição Previdenciária Período Ativo

        protected void btnPrcCTr_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.responsavel = user.login;
                    if (ValidaHiddenfield())
                    {

                        if (!txtDtAtu.Text.Equals(""))
                            obj.data_atualiz = DateTime.Parse(txtDtAtu.Text);

                        obj.num_seq_prc = int.Parse(hdSeq.Value);
                        obj.num_matr_partf = int.Parse(hdNumPartif.Value);

                        bool ret = obj.ProcessaCtrContribuicoes(out mensagem);

                        if (ret)
                        {
                            CarregaRelatorioDisp();
                            LimpaCampos();
                            RegraTela(2);
                            obj.id_acao_processo = 2;
                            ProcessarRelatorio(obj.CarregaRelatorio(), true);
                        }

                        MostraMensagemTelaUpdatePanel(upContrib, mensagem);
                    }
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upContrib, "Problemas contate o administrador do sistema ");
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upContrib, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }

        }

        #endregion

        #region Aba  Dados Sem Pleito

        protected void btnSalvSPleito_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.responsavel = user.login;
                    if (ValidaHiddenfield())
                    {
                        decimal srb = 0;

                        obj.num_seq_prc = int.Parse(hdSeq.Value);
                        obj.num_matr_partf = int.Parse(hdNumPartif.Value);

                        bool ret = obj.ProcessoDadosSemPleito(out mensagem);

                        bool retComPleito = obj.ProcessaDadosComPleito(out mensagem);

                        srb = obj.RetornaSrb(Convert.ToInt32(hdNumPartif.Value), Convert.ToInt32(hdSeq.Value));

                       
                            lblSrbValor.Text = srb.ToString();
                            lblSrbValor.Visible = true;
                            lblSrbTexto.Visible = true;
                        


                        if (ret)
                        {
                            RegraTela(3);
                        }

                        MostraMensagemTelaUpdatePanel(upContrib, mensagem);
                    }
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upContrib, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        #endregion

        #region Aba  Dados Com Pleito

        protected void btnSalvCPleito_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.responsavel = user.login;
                    if (ValidaHiddenfield())
                    {
                        obj.num_seq_prc = int.Parse(hdSeq.Value);
                        obj.num_matr_partf = int.Parse(hdNumPartif.Value);

                        bool ret = obj.ProcessaDadosComPleito(out mensagem);

                        if (ret)
                        {
                            RegraTela(4);
                        }

                        MostraMensagemTelaUpdatePanel(upContrib, mensagem);
                    }
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upContrib, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }
        #endregion

        #region Aba  Salário Real de Benefício

        protected void rdMensagem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdMensagem.SelectedValue == "1")
                tbMensagem.Visible = true;
            else
                tbMensagem.Visible = false;

        }

        protected void btnProcessarSrB_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.responsavel = user.login;
                    if (ValidaHiddenfield())
                    {
                        int id_processo = int.Parse(drpTipoBeneficio.SelectedValue);

                        obj.num_seq_prc = int.Parse(hdSeq.Value);
                        obj.num_matr_partf = int.Parse(hdNumPartif.Value);
                        obj.desc_mensagem = txtMensagem.Text;
                        obj.id_acao_processo = id_processo;
                        bool ret = obj.ProcessaSalReal(out mensagem, (rdMensagem.SelectedValue == "1"));

                        if (ret)
                        {
                            CarregaRelatorioDisp();
                            LimpaCampos();
                            RegraTela(id_processo);
                            obj.id_acao_processo = id_processo;
                            ProcessarRelatorio(obj.CarregaRelatorio(), true);
                        }

                        MostraMensagemTelaUpdatePanel(upContrib, mensagem);
                    }
                }

            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upContrib, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }

        #endregion

        #region Aba Parametros para Calculo Retroativo
        protected void BtbProcessarPar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.responsavel = user.login;
                    if (ValidaHiddenfield())
                    {
                        int id_processo = int.Parse(drpTpBenefPcr.SelectedValue);
                        obj.num_seq_prc = int.Parse(hdSeq.Value);
                        obj.num_matr_partf = int.Parse(hdNumPartif.Value);
                        obj.id_acao_processo = id_processo;
                        obj.nome_benficiario = " ";
                        obj.vl_inicial = 0;
                        obj.vl_compleito = 0;
                        obj.vl_sempleito = 0;
                        obj.mrc_ant_rsv = "N";
                        obj.mrc_cad_bnf = "N";
                        obj.mrc_cad_rsv_splto = "N";
                        obj.mrc_cad_rsv_cplto = "N";

                        //Retorna data da DIB(Data Inicio de Beneficio)
                        //DateTime dib = obj.RetornaDIB(int.Parse(hdNumPartif.Value), int.Parse(hdSeq.Value));

                        if (!string.IsNullOrEmpty(txtBeneficiario.Text))
                            obj.nome_benficiario = txtBeneficiario.Text;

                        if (!string.IsNullOrEmpty(txtDtAjuizamento.Text))
                            obj.dt_ajuizamento = DateTime.Parse(txtDtAjuizamento.Text);

                        if (!string.IsNullOrEmpty(txtDtFimPagamento.Text))
                            obj.dt_fin_pgto = DateTime.Parse(txtDtFimPagamento.Text);

                        //if (!string.IsNullOrEmpty(txtDtIniPagamento.Text) && Convert.ToDateTime(txtDtIniPagamento.Text) >= dib)

                        //{
                        if (!string.IsNullOrEmpty(txtDtIniPagamento.Text))
                        {
                            obj.dt_ini_pgto = DateTime.Parse(txtDtIniPagamento.Text);

                            if (!string.IsNullOrEmpty(txtVlComPleito.Text) && chkVlComPleito.Checked)
                                obj.vl_compleito = decimal.Parse(txtVlComPleito.Text);

                            if (!string.IsNullOrEmpty(txtVlSemPleito.Text) && chkVlSemPleito.Checked)
                                obj.vl_sempleito = decimal.Parse(txtVlSemPleito.Text);

                            if (!string.IsNullOrEmpty(txtVlInicial.Text) && chkVlInicial.Checked)
                                obj.vl_inicial = decimal.Parse(txtVlInicial.Text);

                            if (rdConsidera.Visible == true)
                                obj.mrc_ant_rsv = rdConsidera.SelectedValue;

                            if (chkVlInicial.Checked == true)
                                obj.mrc_cad_bnf = "S";

                            if (chkVlSemPleito.Checked == true)
                                obj.mrc_cad_rsv_splto = "S";

                            if (chkVlComPleito.Checked == true)
                                obj.mrc_cad_rsv_cplto = "S";

                            bool ret = obj.ProcessaPar(out mensagem);

                            if (ret)
                            {
                                LimpaCampos();
                                CarregaParametros();
                                RegraTela(id_processo);
                                CarregaRelatorioDisp();
                            }

                            MostraMensagemTelaUpdatePanel(upContrib, mensagem);
                        }
                        //else
                        //{
                        //    MostraMensagemTelaUpdatePanel(upContrib, "Erro, data de inicio pagamento é menor do que a DIB: " + dib.ToShortDateString());
                        //    txtDtIniPagamento.Focus();
                        //}


                    }
                }
            }
            catch (Exception ex)
            {

                MostraMensagemTelaUpdatePanel(upContrib, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }
        }
        protected void drpTpBenefPcr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpTpBenefPcr.SelectedIndex > 0)
            {
                if (drpTpBenefPcr.SelectedValue == "9")
                {
                    tbConsidera.Visible = true;
                }
                else
                    tbConsidera.Visible = false;
            }
        }
        #endregion

        #region Aba Cálculo Retroativo
        protected void btnCalcRetro_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["objUser"] != null)
                {
                    var user = (ConectaAD)Session["objUser"];
                    obj.responsavel = user.login;
                    if (ValidaHiddenfield())
                    {

                        if (drpCalcRetro.SelectedIndex > 0)
                        {


                            string[] aux = drpCalcRetro.SelectedValue.Split(char.Parse(","));

                            int id_processo = int.Parse(aux[0]);
                            hdTipoBeneficio.Value = aux[1];

                            obj.num_seq_prc = int.Parse(hdSeq.Value);
                            obj.num_matr_partf = int.Parse(hdNumPartif.Value);
                            obj.id_acao_processo = id_processo;
                            int? anoRef = Util.String2Int32(DateTime.Today.Year.ToString());
                            bool ret = obj.ProcessaCalculoReal(out mensagem);

                            if (ret)
                            {

                               
                                    obj.ProcessaProvisionamentoIR(anoRef);
                                 
                                

                                CarregaRelatorioDisp();
                                LimpaCampos();
                                RegraTela(id_processo);
                                obj.id_acao_processo = id_processo;
                                ProcessarRelatorio(obj.CarregaRelatorio());
                            }

                            MostraMensagemTelaUpdatePanel(upContrib, mensagem);
                        }
                        else
                        {
                            MostraMensagemTelaUpdatePanel(upContrib, "Selecione o tipo de benefício");
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MostraMensagemTelaUpdatePanel(upContrib, "Atenção \\n\\n Ocorreu um Erro:\\n\\n" + ex.Message);
            }


        }
        #endregion

        #region Aba Relatório
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Relatorio> list = new List<Relatorio>();
            string ck1 = String.Join(",", from item in ckRelarorios1.Items.Cast<ListItem>()
                                          where item.Selected
                                          select int.Parse(item.Value));

            string ck2 = String.Join(",", from item in ckRelarorios2.Items.Cast<ListItem>()
                                          where item.Selected
                                          select int.Parse(item.Value));

            if (!string.IsNullOrEmpty(ck1) || !string.IsNullOrEmpty(ck2))
            {

                if (!string.IsNullOrEmpty(ck2) && ckRelarorios2.Visible)
                {
                    if (drpBenefRel.SelectedIndex > 0)
                    {
                        if (!string.IsNullOrEmpty(ck1))
                        {
                            list = obj.CarregaRelatorioParam(ck1 + "," + ck2);
                        }
                        else
                            list = obj.CarregaRelatorioParam(ck2);

                    }
                    else
                        MostraMensagemTelaUpdatePanel(upContrib, "Selecione um tipo de benefício para continuar");
                }
                else
                    list = obj.CarregaRelatorioParam(ck1);


            }
            else
                MostraMensagemTelaUpdatePanel(upContrib, "Selecione um relatório para continuar!");

            ProcessarRelatorio(list);

        }

        protected void drpBenefRel_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdTipoBeneficio.Value = drpBenefRel.SelectedValue;
        }
        #endregion

        #endregion

        #region Métodos

        private void DivAcao(HtmlGenericControl divExibir, HtmlGenericControl divOcultar)
        {

            divExibir.Visible = true;
            divOcultar.Visible = false;

        }

        private void CarregaGrid(string nameView, DataTable dt, GridView grid)
        {

            ViewState[nameView] = dt;
            grid.DataSource = ViewState[nameView];
            grid.DataBind();
        }

        private void CarregaTela()
        {

            CarregaDropDowDT(obj.CarregaTipoPleito(), drpTipoPleito);
            RegraTela(0);

        }

        private void ProcessarRelatorio(List<Relatorio> list, bool alerta = false)
        {

            if (list.Count > 0)
            {
                SetParametrosRelatorio(list);
                ExibirRelaorio(list, alerta);
            }
        }

        private void SetParametrosRelatorio(List<Relatorio> list)
        {

            foreach (var relatorio in list)
            {
                foreach (var param in relatorio.parametros)
                {
                    switch (param.parametro)
                    {
                        case "v_num_matr_partf":
                        case "num_matr_partf":
                            param.valor = hdNumPartif.Value;
                            break;

                        case "v_num_sqncl_prc":
                        case "num_seq_bnf":
                            param.valor = hdSeq.Value;
                            break;

                        case "v_tip_bnf":
                        case "tip_bnf":
                            param.valor = hdTipoBeneficio.Value;
                            break;

                        default:
                            break;
                    }

                }
            }



        }

        private void ExibirRelaorio(List<Relatorio> list, bool alerta = false)
        {

            foreach (var item in list)
            {
                Session[item.relatorio] = item;
                //AbrirNovaAba(this.Page, "Report.aspx?Param=" + item.relatorio + (alerta ? "&Alert=true" : ""), item.relatorio);
                AbrirNovaAba(this.Page, "RelatorioWeb.aspx?Relatorio_nome=" + item.relatorio + "&PromptParam=false&Popup=true" + (alerta ? "&Alert=true" : ""), item.relatorio);
            }


        }

        private void RegraTela(int id_regra)
        {

            switch (id_regra)
            {
                case 0:// Aba Tela Inicial
                    TabSrc.Visible = true;
                    TabCrt.Visible = false;
                    TabCpleito.Visible = false;
                    TabSpleito.Visible = false;
                    TbSrb.Visible = false;
                    TabPCR.Visible = false;
                    TabRel.Visible = false;
                    TabCalcRetro.Visible = false;
                    tbConsidera.Visible = false;
                    divParametro.Visible = false;
                    break;

                case 1://Aba Salário Real de Contribuição
                    TabSrc.Visible = false;
                    TabCrt.Visible = true;
                    TabCpleito.Visible = false;
                    TabSpleito.Visible = false;
                    TbSrb.Visible = false;
                    TabPCR.Visible = false;
                    TabRel.Visible = true;
                    TabCalcRetro.Visible = false;
                    break;

                case 2://Aba  Contribuição Previdenciária Período Ativo
                    TabSrc.Visible = false;
                    TabCrt.Visible = true;
                    TabSpleito.Visible = true;
                    TabCpleito.Visible = false;
                    TbSrb.Visible = false;
                    TabPCR.Visible = false;
                    TabRel.Visible = true;
                    TabCalcRetro.Visible = false;
                    break;

                case 3://Aba  Dados Sem Pleito
                    TabSrc.Visible = false;
                    TabCrt.Visible = true;
                    TabSpleito.Visible = true;
                    TabCpleito.Visible = true;
                    TbSrb.Visible = false;
                    TabPCR.Visible = false;
                    TabRel.Visible = true;
                    TabCalcRetro.Visible = false;
                    break;

                case 4://Aba  Dados Com Pleito
                    TabSrc.Visible = false;
                    TabCrt.Visible = true;
                    TabCpleito.Visible = true;
                    TabSpleito.Visible = true;
                    TbSrb.Visible = true;
                    TabPCR.Visible = false;
                    TabRel.Visible = true;
                    TabCalcRetro.Visible = false;
                    break;


                case 5://Salário Real de Benefício (BSPS)
                case 6://Salário Real de Benefício (PSAP)
                case 7://Salário Real de Benefício (BD)
                case 8://Salário Real de Benefício (BPD)
                    TabSrc.Visible = false;
                    TabCrt.Visible = true;
                    TabCpleito.Visible = true;
                    TabSpleito.Visible = true;
                    TbSrb.Visible = true;
                    TabPCR.Visible = true;
                    TabRel.Visible = true;
                    TabCalcRetro.Visible = false;
                    break;

                case 9://Parâmetro de Cálculo Retroativo (BSPS)
                case 10://Parâmetro de Cálculo Retroativo (PSAP)  
                case 11://Parâmetro de Cálculo Retroativo (BD)
                case 12://Parâmetro de Cálculo Retroativo (BPD) 
                    TabSrc.Visible = false;
                    TabCrt.Visible = true;
                    TabCpleito.Visible = true;
                    TabSpleito.Visible = true;
                    TbSrb.Visible = true;
                    TabPCR.Visible = true;
                    TabCalcRetro.Visible = true;
                    TabRel.Visible = true;
                    break;

                case 13://Cálculo Retroativo (SRB)
                case 14://Cálculo Retroativo (PSAP)
                case 15://Cálculo Retroativo (BD)
                case 16://Cálculo Retroativo (BPD)
                    TabSrc.Visible = false;
                    TabCrt.Visible = true;
                    TabCpleito.Visible = true;
                    TabSpleito.Visible = true;
                    TbSrb.Visible = true;
                    TabPCR.Visible = true;
                    TabCalcRetro.Visible = true;
                    TabRel.Visible = true;
                    break;

                default:
                    break;
            }



        }

        private bool ValidaHiddenfield()
        {
            return (!hdNumPartif.Value.Equals("0") && !hdSeq.Value.Equals("0"));
        }

        private void CarregaParametros()
        {
            if (ValidaHiddenfield())
            {
                obj.num_seq_prc = int.Parse(hdSeq.Value);
                obj.num_matr_partf = int.Parse(hdNumPartif.Value);
                DataTable dt = obj.CarregaParametro();

                if (dt.Rows.Count > 0)
                {
                    CarregaGrid("grdParametro", dt, grdParametro);
                    divParametro.Visible = true;
                }
                else
                    divParametro.Visible = false;
            }
        }

        private void LimpaCampos()
        {

            drpTipoPleito.SelectedIndex = 0;
            drpTpBenefPcr.SelectedIndex = 0;
            txtBeneficiario.Text = "";
            txtDtAjuizamento.Text = "";
            txtDtFimPagamento.Text = "";
            txtDtIniPagamento.Text = "";
            txtVlComPleito.Text = "";
            txtVlSemPleito.Text = "";
            txtVlInicial.Text = "";

            rdMensagem.SelectedValue = "0";
            drpTipoBeneficio.SelectedIndex = 0;
            txtMensagem.Text = "";
            tbMensagem.Visible = false;

            txtDtAtu.Text = "";

            txtEmpresa.Text = "";
            txtMatricula.Text = "";
            lblNome.Visible = false;
            rdAbono.SelectedIndex = 0;
            rdPoloAcJudic.SelectedIndex = 0;
            txtPasta.Text = "";
            txtProcesso.Text = "";
            txtObs.Text = "";
            txtCodVara.Text = "";

            chkVlInicial.Checked = false;
            chkVlSemPleito.Checked = false;
            chkVlComPleito.Checked = false;

            txtVlInicial.Enabled = false;
            txtVlComPleito.Enabled = false;
            txtVlSemPleito.Enabled = false;

        }

        private void CarregaRelatorioDisp()
        {

            if (ValidaHiddenfield())
            {
                obj.num_seq_prc = int.Parse(hdSeq.Value);
                obj.num_matr_partf = int.Parse(hdNumPartif.Value);

                DataSet dt = obj.CarregarRelatoriosDisp();

                if (dt.Tables[0].Rows.Count > 0)
                {
                    CarregarCkBoxList(ckRelarorios1, dt.Tables[0], "titulo", "id_relatorio");
                }

                if (dt.Tables[1].Rows.Count > 0)
                {
                    divRels.Visible = true;
                    CarregarCkBoxList(ckRelarorios2, dt.Tables[1], "titulo", "id_relatorio");
                }
                else
                    divRels.Visible = false;
            }
        }

        //protected void ckRetroativo_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ckRetroativo.Checked == true)
        //    {
        //        tbAnoRef.Visible = true;
        //        tbAnoRef.Enabled = true;
        //        txtAnoRef.Text = "";
        //    }
        //    else
        //    {
        //        tbAnoRef.Visible = false;
        //        tbAnoRef.Enabled = false;
        //        txtAnoRef.Text = "";
        //    }
        //} 

        #endregion



    }
}