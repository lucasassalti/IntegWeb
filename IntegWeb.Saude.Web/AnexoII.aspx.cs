using System;
using System.Collections.Generic;
using IntegWeb.Entidades.Relatorio;
using IntegWeb.Entidades.Framework;
using System.Linq;
using System.Web.UI.WebControls;
using IntegWeb.Saude.Aplicacao.BLL.Processos;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using System.IO;
using System.Text;
using System.Web;
using IntegWeb.Framework;

namespace IntegWeb.Saude.Web
{
    public partial class Anexo2 : BasePage
    {
        #region DECLARAÇÕES CRYSTAL REPORTS

        Relatorio relatorio = new Relatorio();
        List<ArquivoDownload> lstAdPdf = new List<ArquivoDownload>();
        string relatorio_nome = "RelatorioAprovacao";
        string relatorio_nomeAnexoII = "RelatorioAnexoII";
        string relatorio_titulo = "Relatório Aprovação de Aumento";
        string relatorio_titulo_AnexoII = "Relatório Anexo II";
        string relatorio_simples = @"~/Relatorios/Rel_Planilha_Aprovacao.rpt";
        string relatorio_AnexoII = @"~/Relatorios/Rel_AnexoII.rpt";

        private bool InicializaRelatorioSimples(string Cod_hosp, string tipoRel)
        {
            // Faz a seleção se o relatório será o Relatório AnexoII ou Relatório de Aprovação.
            relatorio.titulo = (tipoRel == "relAprovacao") ? relatorio_titulo : relatorio_titulo_AnexoII;
            relatorio.parametros = new List<Parametro>();

            relatorio.arquivo = (tipoRel == "relAprovacao") ? relatorio_simples : relatorio_AnexoII;
            relatorio.parametros.Add(new Parametro() { parametro = "cod_hosp", valor = Cod_hosp });

            string nomeRel = (tipoRel == "relAprovacao") ? relatorio_nome : relatorio_nomeAnexoII;

            Session[nomeRel] = relatorio;
            ReportCrystal.RelatorioID = nomeRel;
            return true;
        }

        #endregion

        // Caminho para Gravar os ARQUIVOS DE EXPORTAÇÃO
        const string pastaAnexoII = @"C:\ANEXO_II\";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarInformacaoHospital();
            }
            //txtNomeArqExporta.Text = "ANEXO_" + Convert.ToDateTime(DateTime.Now).ToString("ddMMyyyy") + ".TXT";
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (ReportCrystal != null)
            {
                ReportCrystal.RelatorioID = null;
                ReportCrystal = null;
            }
        }

        AnexoIIBLL anexoBLL = new AnexoIIBLL();

        #region FUNÇÕES ÚTEIS

        private void limparCadastroPrestador()
        {
            ddlNmHospital.SelectedValue = "0";
            txtNumContrato.Text = "";
            txtNmHospital.Text = "";
            txtCidade.Text = "";
            txtRegional.Text = "";
            txtContato.Text = "";
            txtRegional.Text = "";
            txtInicioContrato.Text = "";
            txtCredenciador.Text = "";
            txtTbCodigoServico.Text = "";
            txtTbDescricaoCodigoServico.Text = "";
            txtNumContratoServicos.Text = "";
            ddlHospitalServico.SelectedValue = "0";
            txtCodigoServico.Text = "";
            txtDescricaoServico.Text = "";
            txtValorAtual.Text = "";
            txtVigenciaInicial.Text = "";
            txtTbDescricaoCodigoServico.Text = "";
            txtTbCodigoServico.Text = "";
        }


        /// <summary>
        ///  Método para carregar os combos de empresas/ ListBox / GridViews
        /// </summary>
        private void CarregarInformacaoHospital()
        {
            CarregaDropDowList(ddlConvenente, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
            CarregaDropDowList(ddlNmHospital, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
            CarregaDropDowList(ddlHospitalServico, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
            CarregaDropDowList(ddlHospitalCdEmp, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
            CarregarListBox(lstEmpresasExportacao, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");

            carregaGridLogExportacao();

        }


        /// <summary>
        ///  REGRA PARA CARREGAR GRID DE LOG'S;
        /// </summary>
        public void carregaGridLogExportacao()
        {
            DateTime inicio;
            DateTime.TryParse(txtDataIniLog.Text, out inicio);

            DateTime fim;
            DateTime.TryParse(txtDataFimLog.Text, out fim);

            Decimal prestador;
            Decimal.TryParse(txtPrestadorLog.Text, out prestador);

            Decimal procedimento;
            Decimal.TryParse(txtProcServLog.Text, out procedimento);

            gridLogExportacao.DataSource = anexoBLL.returnLog(inicio, fim, prestador, procedimento);
            gridLogExportacao.DataBind();
            gridLogExportacao.Visible = true;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridLogExportacao.PageIndex = e.NewPageIndex;

            carregaGridLogExportacao();
        }

        /// <summary>
        ///  Rotina para a gerar os relatórios:
        ///  1 - ANEXO II
        ///  2 - APROVAÇÃO
        /// </summary>
        public void gerarRelatorio(string tipoRel, bool lote, List<decimal> CodigoContratos)
        {
            if ((!String.IsNullOrEmpty(txtConvenente.Text) && tipoRel == "relAprovacao") || (rdblTipo.SelectedValue == "LOTE" && tipoRel == "relAprovacao")
                || (!String.IsNullOrEmpty(txtConvenente.Text) && tipoRel == "relAnexoII") || (rdblTipo.SelectedValue == "LOTE" && tipoRel == "relAnexoII")
                )
            {
                var usuario = (ConectaAD)Session["objUser"];

                if (lote == false)
                {
                    if (InicializaRelatorioSimples(txtConvenente.Text, tipoRel))
                    {
                        ArquivoDownload adExtratoPdf = new ArquivoDownload();
                        adExtratoPdf.nome_arquivo = relatorio_nome + ".pdf";
                        adExtratoPdf.caminho_arquivo = Server.MapPath(@"UploadFile\") + DateTime.Now.ToFileTime() + "_" + adExtratoPdf.nome_arquivo;
                        adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                        ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);

                        Session[ValidaCaracteres(adExtratoPdf.nome_arquivo)] = adExtratoPdf;
                        string fullUrl = "WebFile.aspx?dwFile=" + ValidaCaracteres(adExtratoPdf.nome_arquivo);
                        AdicionarAcesso(fullUrl);
                        AbrirNovaAba(upCisao, fullUrl, adExtratoPdf.nome_arquivo);
                    }
                    MostraMensagemTelaUpdatePanel(upCisao, "Relatórios gerados com sucesso!");
                }
                else
                {
                    foreach (var contrato in CodigoContratos)
                    {

                        // Gera relatórios em LOTE. 
                        if (InicializaRelatorioSimples(contrato.ToString(), tipoRel))
                        {
                            String diretorioCaminho = string.Format("D:\\Users\\{0}\\Desktop\\ANEXOII", Convert.ToString(usuario.login));

                            // verificar se existe um diretorio            
                            if (!Directory.Exists(diretorioCaminho))
                            {           
                                Directory.CreateDirectory(diretorioCaminho);
                            }

                            string nomeRel = (tipoRel == "relAnexoII") ? relatorio_nomeAnexoII : relatorio_nome;

                            ArquivoDownload adExtratoPdf = new ArquivoDownload();
                            adExtratoPdf.nome_arquivo = nomeRel + ".pdf";

                            adExtratoPdf.caminho_arquivo = diretorioCaminho + "\\" + contrato.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + adExtratoPdf.nome_arquivo;

                            adExtratoPdf.modo_abertura = System.Net.Mime.DispositionTypeNames.Inline;
                            ReportCrystal.ExportarRelatorioPdf(adExtratoPdf.caminho_arquivo);
                        }
                    }
                    MostraMensagemTelaUpdatePanel(upCisao, "Relatórios gerados com sucesso!");
                }
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Verifique se todos os campos foram preenchidos adequadamente.");
                return;
            }
        }

        #endregion

        #region REGRAS DE TELA - RADIO BUTTON

        protected void rdblTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Regras de tela baseadas nos radioButtons
            if (rdblTipo.SelectedValue == "GERAL")
            {
                limparListBox(lstServicos);
                lstServicos.SelectionMode = ListSelectionMode.Multiple;
                txtPerDesconto.Enabled = true;
                txtPercentReajuste.Enabled = true;
                txtDataVigencia.Enabled = true;
                txtValProposto.Enabled = false;
                txtEscalonado.Enabled = false;
                txtEscalonado.Text = "";
                SelecionaTodosLstBox(lstServicos);
                txtConvenente.Enabled = true;
                ddlConvenente.Enabled = true;
                txtValProposto.Text = "";
                txtDataVigencia.Text = "";
                lstServicos.Enabled = false;
            }
            else if (rdblTipo.SelectedValue == "ESCALONADO")
            {
                limparListBox(lstServicos);
                lstServicos.SelectionMode = ListSelectionMode.Single;
                txtPerDesconto.Enabled = true;
                txtPercentReajuste.Enabled = true;
                txtEscalonado.Enabled = true;
                txtEscalonado.Text = "";
                txtValProposto.Enabled = false;
                txtDataVigencia.Enabled = true;
                lstServicos.Enabled = true;
                txtConvenente.Enabled = true;
                ddlConvenente.Enabled = true;
            }

            else if (rdblTipo.SelectedValue == "PORVALOR")
            {
                limparListBox(lstServicos);
                lstServicos.SelectionMode = ListSelectionMode.Single;
                txtPerDesconto.Enabled = false;
                txtPercentReajuste.Enabled = false;
                txtEscalonado.Enabled = false;
                txtEscalonado.Text = "";
                txtValProposto.Enabled = true;
                txtDataVigencia.Enabled = true;
                lstServicos.Enabled = true;
                txtConvenente.Enabled = true;
                ddlConvenente.Enabled = true;

            }
            else if (rdblTipo.SelectedValue == "LOTE")
            {
                limparListBox(lstServicos);
                CarregarListBox(lstServicos, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
                lstServicos.SelectionMode = ListSelectionMode.Multiple;
                txtConvenente.Text = "";
                ddlConvenente.SelectedValue = "0";
                lstServicos.Enabled = true;
                txtEscalonado.Enabled = false;
                txtEscalonado.Text = "";
                txtConvenente.Enabled = false;
                ddlConvenente.Enabled = false;
                txtDataVigencia.Enabled = true;
                txtPercentReajuste.Enabled = true;
                txtPerDesconto.Enabled = true;
            }
        }


        #endregion

        #region Funções e regras - TELA PRINCIPAL

        protected void txtCodigoServico_Changed(object sender, EventArgs e)
        {
            if (txtCodigoServico.Text.ToString().Length == 8)
            {
                if (!String.IsNullOrEmpty(txtNumContratoServicos.Text))
                {
                    if (ChkDigitoTiss(txtCodigoServico.Text) == false || String.IsNullOrEmpty(txtCodigoServico.Text))
                    {
                        MostraMensagemTelaUpdatePanel(upCisao, "Atenção! \\n\\ Verifique o Código!");
                        return;
                    }

                    List<SAU_TB_SERV_X_HOSP_AND> servHosp = anexoBLL.CarregarServicosxHospital(txtNumContratoServicos.Text, txtCodigoServico.Text).ToList();

                    if (servHosp.Count == 0)
                    {
                        txtValorAtual.Enabled = true;
                        txtVigenciaInicial.Enabled = true;

                        String dtVigencia = anexoBLL.carregarHospital(null, Convert.ToInt32(txtNumContratoServicos.Text)).FirstOrDefault().DAT_FIM_VIGENCIA.ToString();
                        txtVigenciaInicial.Text = !String.IsNullOrEmpty((dtVigencia.ToString()).ToString()) ? Convert.ToDateTime(dtVigencia).ToString("dd/MM/yyyy") : "";

                        txtValorAtual.Text = "";

                        // Não encontrou o Serviço, e a vai utilizar a descrição da tabela base:
                        if (anexoBLL.VerificaExistenciaServico(txtCodigoServico.Text) == false && rdblDescricaoBase.SelectedValue == "yes")
                        {
                            MostraMensagemTelaUpdatePanel(upCisao, "Atenção! \\n\\ O código não existe! Consulte a tabela base.");
                            txtCodigoServico.Text = "";
                            return;
                        }
                        if (rdblDescricaoBase.SelectedValue == "yes")
                        {
                            txtDescricaoServico.Text = !string.IsNullOrEmpty(anexoBLL.CarregarServicos(txtCodigoServico.Text).FirstOrDefault().DESCRICAO) ? anexoBLL.CarregarServicos(txtCodigoServico.Text).FirstOrDefault().DESCRICAO.Trim() : "";
                        }
                        else
                        {
                            txtDescricaoServico.Text = "";
                        }
                    }
                    else
                    {
                       // txtDescricaoServico.Text = !String.IsNullOrEmpty(servHosp.FirstOrDefault().DESCRICAO) ? servHosp.FirstOrDefault().DESCRICAO : "";
                        txtValorAtual.Text = !String.IsNullOrEmpty(String.Format("{0:C}", servHosp.FirstOrDefault().VALOR.ToString())) ? servHosp.FirstOrDefault().VALOR.ToString() : "";
                        txtVigenciaInicial.Text = !String.IsNullOrEmpty(servHosp.FirstOrDefault().DAT_INI_VIGENCIA.ToString()) ? Convert.ToDateTime(servHosp.FirstOrDefault().DAT_INI_VIGENCIA).ToString("dd/MM/yyyy") : "";
                    }

                    if (txtCodigoServico.Text.PadLeft(2) == "99")
                    {
                        txtValorAtual.Text = "0";
                        txtValorAtual.Enabled = false;
                    }
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "Atenção \\n\\ A busca não retornou informações ou o contrato não foi preenchido!");
                }
            }
        }

        protected void ddlNmHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codHosp;
            int.TryParse(ddlNmHospital.SelectedItem.Value, out codHosp);

            if (codHosp == 0)
            {
                txtNumContrato.Enabled = true;
                txtNmHospital.Enabled = true;
                limparCadastroPrestador();
                return;
            }

            SAU_TB_HOSP_AND hosp = anexoBLL.retornaHospital(codHosp);
            if (hosp != null)
            {
                txtNumContrato.Text = hosp.COD_HOSP.ToString();
                if (hosp.COD_HOSP != 0)
                {
                    txtNumContrato.Enabled = false;
                    txtNmHospital.Enabled = false;
                }

                txtNmHospital.Text = hosp.NOME_FANTASIA;
                txtInicioContrato.Text = Convert.ToDateTime(hosp.DAT_INI_VIGENCIA).ToString("dd/MM/yyyy");
                txtCidade.Text = hosp.CIDADE;
                txtRegional.Text = hosp.REGIONAL;
                txtContato.Text = hosp.CONTATO;
                txtCredenciador.Text = hosp.CREDENCIADOR;
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upCisao, "A busca não retornou informações.");
            }
        }

        protected void ddlHospitalCdEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtcodEmpresaObsContratual.Text))
            {
                txtObservacoesContratuais.Text = anexoBLL.CarregarObservacaoContratual(txtcodEmpresaObsContratual.Text);
            }
        }

        public void liberaAlteracaoAumento(bool valida)
        {
            txtPerDesconto.Enabled = valida;
            txtPercentReajuste.Enabled = valida;
            txtValProposto.Enabled = valida;
            txtDataVigencia.Enabled = valida;
        }

        protected void ddlConvenente_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtConvenente.Text = ddlConvenente.SelectedItem.Value;

            if (!String.IsNullOrEmpty(txtConvenente.Text))
            {
                CarregaServicos(txtConvenente.Text);
            }
        }


        /// <summary>
        ///  Rotina para carregar serviços
        /// </summary>
        public void CarregaServicos(string cod_Hosp)
        {
            if (!String.IsNullOrEmpty(cod_Hosp) && cod_Hosp != "0")
            {
                CarregarListBox(lstServicos, anexoBLL.retornaEmpresaServico(cod_Hosp).ToList<object>(), "descServico", "codServico");
            }
            else
            {
                RemoverlinhasListBox(lstServicos);
                txtPerDesconto.Text = "";
                txtPercentReajuste.Text = "";
                txtValProposto.Text = "";
                txtDataVigencia.Text = "";
                txtConvenente.Text = "";
                liberaAlteracaoAumento(false);
            }
        }


        /// <summary>
        ///  A geração de Planilha de aprovação contém todas as regras de aumento - No que tange calculo matematico.
        ///  Cuidado ao atualizar: Basicamente é a regra principal do ANEXO II.
        /// </summary>
        public void GeraPlanilhaAprovacao()
        {
            var user = (ConectaAD)Session["objUser"];

            SAU_TB_LOG_AND log = new SAU_TB_LOG_AND();
            log.USUARIO = Convert.ToDecimal(user.login.Replace("F", "").Replace("E", ""));

            if (String.IsNullOrEmpty(txtDataVigencia.Text))
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Preencha a data de vigência!");
                return;
            }

            //Verifica se existe linha selecionada.
            if (contarLinhaSelecionada(lstServicos) <= 0 && rdblTipo.SelectedValue == "ESCALONADO")
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Selecione um serviço antes de prosseguir.");
                return;
            }

            // Verifica se o tipo de aumento foi selecionado.
            if (String.IsNullOrEmpty(rdblTipo.SelectedValue))
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Você não selecionou todos os campos,\\n\\Favor marcar um tipo de aumento. ");
                return;
            }

            // Verifica se o hospital foi selecionado e se a data de vigência foi preechida corretamente.
            if (String.IsNullOrEmpty(ddlConvenente.SelectedItem.Text) || String.IsNullOrEmpty(txtDataVigencia.Text))
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Verificar o preenchimento correto dos campos.");
                return;
            }

            //--------------------------- FIM DAS VALIDAÇÕES PARA PROSSEGUIR COM O AUMENTO ---------------------------------------------------//

            List<SAU_TB_SERV_X_HOSP_AND> ServHosp = new List<SAU_TB_SERV_X_HOSP_AND>();

            List<decimal> CodigoContratos = new List<decimal>();

            for (int x = lstServicos.Items.Count - 1; x >= 0; x--)
            {
                if (lstServicos.Items[x].Selected == true)
                {
                    // Se Opção "LOTE" estiver marcada, retorna lista de empresas, senão somente serviço da empresa selecionada.
                    ServHosp = (rdblTipo.SelectedValue == "LOTE") ? anexoBLL.CarregarServicosxHospital(lstServicos.Items[x].Value, null).ToList() : anexoBLL.CarregarServicosxHospital(ddlConvenente.SelectedValue, lstServicos.Items[x].Value).ToList();

                    CodigoContratos.Add(ServHosp.FirstOrDefault().COD_HOSP);

                    // Objeto que será atualizado na base.
                    SAU_TB_SERV_X_HOSP_AND servicoAtualizado = new SAU_TB_SERV_X_HOSP_AND();

                    /* DECLARANDO VARIÁVEIS DE DESCONTO / PORCENTAGENS */
                    Double porcentagemDesconto;
                    double.TryParse(txtPerDesconto.Text, out porcentagemDesconto);
                    Double porcentagemReajuste;
                    double.TryParse(txtPercentReajuste.Text, out porcentagemReajuste);
                    DateTime dataVigencia;
                    DateTime.TryParse(txtDataVigencia.Text, out dataVigencia);

                    foreach (var item in ServHosp)
                    {
                        if (rdblTipo.SelectedValue != "PORVALOR")
                        {
                            decimal? valorProposto;
                            /* AUMENTO GERAL OU ESCALONADO */
                            if (porcentagemReajuste <= 0)
                            {    // CALCULA O DESCONTO
                                valorProposto = (item.VALOR * (100 - Convert.ToDecimal(porcentagemDesconto) / 100));
                            }
                            else
                            {   // CALCULA O REAJUSTE
                                valorProposto = (item.VALOR * (1 + Convert.ToDecimal(porcentagemReajuste) / 100));
                            }
                            servicoAtualizado.COD_HOSP = !string.IsNullOrEmpty(txtConvenente.Text) ? Convert.ToDecimal(txtConvenente.Text) : item.COD_HOSP;
                            servicoAtualizado.COD_SERV = (rdblTipo.SelectedValue == "LOTE") ? item.COD_SERV : Convert.ToDecimal(lstServicos.Items[x].Value);
                            servicoAtualizado.VALOR_PROPOSTO = valorProposto;
                            servicoAtualizado.PORC_PROPOSTA = (!string.IsNullOrEmpty(txtPercentReajuste.Text)) ? Convert.ToDecimal(porcentagemReajuste) : 0;
                            servicoAtualizado.PORC_DESCONTO = (!string.IsNullOrEmpty(txtPerDesconto.Text)) ? Convert.ToDecimal(porcentagemDesconto) : 0;
                            servicoAtualizado.DAT_PROPOSTA = dataVigencia;

                            anexoBLL.atualizaServicoHospital(servicoAtualizado, log);
                        }
                        //Calcula valor por aumento.
                        else
                        {
                            if ((lstServicos.Items[x].Selected && (!String.IsNullOrEmpty(txtValProposto.Text))) && (!String.IsNullOrEmpty(txtDataVigencia.Text)))
                            {
                                Decimal ValProposto;
                                decimal.TryParse(txtValProposto.Text, out ValProposto);
                                Decimal pReajuste;
                                decimal.TryParse(txtPercentReajuste.Text, out pReajuste);
                                Decimal pDesconto;
                                decimal.TryParse(txtPerDesconto.Text, out pDesconto);

                                txtPercentReajuste.Text = "0";
                                txtPerDesconto.Text = "0";

                                if (item.VALOR > 0)
                                {
                                    if (item.VALOR < ValProposto)
                                    {   // CALCULA O REAJUSTE
                                        pReajuste = Convert.ToDecimal(((ValProposto / Convert.ToDecimal(item.VALOR) - 1) * 100).ToString("#.######"));
                                    }
                                    else if (item.VALOR > ValProposto)
                                    {   //CALCULA O DESCONTO
                                        pDesconto = Convert.ToDecimal(((ValProposto / Convert.ToDecimal(item.VALOR) - 1) * -1).ToString("#.######"));
                                    }
                                }
                                servicoAtualizado.COD_HOSP = Convert.ToDecimal(txtConvenente.Text);
                                servicoAtualizado.COD_SERV = Convert.ToDecimal(lstServicos.Items[x].Value);
                                servicoAtualizado.VALOR_PROPOSTO = Convert.ToDecimal(txtValProposto.Text);
                                servicoAtualizado.PORC_PROPOSTA = pReajuste;
                                servicoAtualizado.PORC_DESCONTO = pDesconto;
                                servicoAtualizado.DAT_PROPOSTA = Convert.ToDateTime(txtDataVigencia.Text);

                                anexoBLL.atualizaServicoHospital(servicoAtualizado, log);

                                if (cbxEmiteRel.Checked == true)
                                {
                                    gerarRelatorio("relAprovacao", false, CodigoContratos);
                                }
                            }
                        }
                    }
                }
            }
            /* GERAÇÃO DE RELATÓRIOS */
            if (rdblTipo.SelectedValue != "LOTE" && cbxEmiteRel.Checked == true)
            {
                gerarRelatorio("relAprovacao", false, CodigoContratos);
            }
            else if (rdblTipo.SelectedValue == "LOTE" && cbxEmiteRel.Checked == true)
            {
                /* GERAR O RELATÓRIO EM LOTE E SALVAR NA MÁQUINA DO USUÁRIO */
                gerarRelatorio("relAprovacao", true, CodigoContratos);
                MostraMensagemTelaUpdatePanel(upCisao, "Os relatórios foram gerados com sucesso, favor validar a pasta: ANEXOII");
            }

            // Seleciona a linha à baixo e carrega as informações - SOMENTE PARA ESCALONADO E PORVALOR.
            if (rdblTipo.SelectedValue == "ESCALONADO" || rdblTipo.SelectedValue == "PORVALOR")
            {
                lstServicos.SelectedIndex++;
                for (int x = lstServicos.Items.Count - 1; x >= 0; x--)
                {
                    if (lstServicos.Items[x].Selected == true)
                    {
                        SAU_TB_SERV_X_HOSP_AND cargaServicos = new SAU_TB_SERV_X_HOSP_AND();
                        cargaServicos = anexoBLL.CarregarServicosxHospital(txtConvenente.Text, lstServicos.Items[x].Value).FirstOrDefault();

                        txtValProposto.Text = cargaServicos.VALOR_PROPOSTO.ToString();
                        txtPercentReajuste.Text = cargaServicos.PORC_PROPOSTA.ToString();
                        txtDataVigencia.Text = (cargaServicos.DAT_PROPOSTA == DateTime.MinValue) ? "" : Convert.ToDateTime(cargaServicos.DAT_PROPOSTA).ToString("dd/MM/yyyy");
                        txtPerDesconto.Text = cargaServicos.PORC_DESCONTO.ToString();
                    }
                }
            }
        }

        /// <summary>
        ///  Botão Para gerar AUMENTOS.
        /// </summary>
        protected void btnPlanilhaAprovacao_Click(object sender, EventArgs e)
        {
            string CodigoHospital = txtConvenente.Text;
            if (!String.IsNullOrEmpty(CodigoHospital) && ddlConvenente.SelectedItem.Value != "0" || rdblTipo.SelectedValue == "LOTE")
            {
                GeraPlanilhaAprovacao();
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Selecione um prestador!");
            }
        }

        /// <summary>
        ///  Botão de confirmação de aumento / GERA RELATÓRIO ANEXO II
        /// </summary>
        protected void btnConfirmaAumento_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                ConfirmaAumento();
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        private void ConfirmaAumento()
        {
            var user = (ConectaAD)Session["objUser"];

            SAU_TB_LOG_AND log = new SAU_TB_LOG_AND();
            log.USUARIO = Convert.ToDecimal(user.login.Replace("F", "").Replace("E", ""));

            //
            if (rdblTipo.SelectedValue != "LOTE")
            {
                ConfirmacaoAumento(txtConvenente.Text);
            }

            List<decimal> CodigoContratos = new List<decimal>();

            if ((contarLinhaSelecionada(lstServicos) > 0 && rdblTipo.SelectedValue == "LOTE"))
            {
                for (int x = lstServicos.Items.Count - 1; x >= 0; x--)
                {
                    if (lstServicos.Items[x].Selected == true)
                    {
                        CodigoContratos.Add(Convert.ToDecimal(lstServicos.Items[x].Value));

                        AjustaDataVigencia(lstServicos.Items[x].Value);
                        anexoBLL.AtualizaValorServicosHosp(Convert.ToDecimal(lstServicos.Items[x].Value), log);
                    }
                }
            }
            // Só entra nesta chave, se a atualização for em lote.
            if (cbxEmiteRel.Checked == true && rdblTipo.SelectedValue == "LOTE")
            {
                gerarRelatorio("relAnexoII", true, CodigoContratos);
            }
        }

        /// <summary>
        ///  Método para atualizar data de vigência
        /// </summary>
        public void AjustaDataVigencia(string cdHosp)
        {
            var datas = anexoBLL.RetornarDataMaior(cdHosp);
            anexoBLL.AtualizaVigenciaServico(cdHosp, Convert.ToDateTime(datas.Item2).ToString("dd/MM/yyyy"), Convert.ToDateTime(datas.Item1).ToString("dd/MM/yyyy")); // CdHosp, DataMaior, DataVigência
        }

        /// <summary>
        ///  Botão Para Confirmar aumento e questionar se a data de vigência será atualizada
        /// </summary>
        private void ConfirmacaoAumento(string CodigoHospital)
        {
            var datas = anexoBLL.RetornarDataMaior(CodigoHospital);

            lblMsgmDatas.Text = String.Format(@"Deseja atualizar a data de Vigência do Anexo II, de <b> {0} </b> para <b> {1}  </b>?", Convert.ToDateTime(datas.Item2).ToString("dd/MM/yyyy"), Convert.ToDateTime(datas.Item1).ToString("dd/MM/yyyy"));

            ModalPopupValidaData.Show();

            return;
        }

        /// <summary>
        ///  Botão Para ATUALIZAR vigência
        /// </summary>
        protected void btnAjustaDataVigencia_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtConvenente.Text))
            {
                var user = (ConectaAD)Session["objUser"];

                SAU_TB_LOG_AND log = new SAU_TB_LOG_AND();
                log.USUARIO = Convert.ToDecimal(user.login.Replace("F", "").Replace("E", ""));

                //Atualiza da Vigência caso o usuário selecione a opção SIM.
                var datas = anexoBLL.RetornarDataMaior(txtConvenente.Text);
                anexoBLL.AtualizaVigenciaServico(txtConvenente.Text, Convert.ToDateTime(datas.Item2).ToString("dd/MM/yyyy"), Convert.ToDateTime(datas.Item1).ToString("dd/MM/yyyy")); // CdHosp, DataMaior, DataVigência

                //ATUALIZA VALOR HOSP
                anexoBLL.AtualizaValorServicosHosp(Convert.ToDecimal(txtConvenente.Text), log);

                List<Decimal> codigoHospital = new List<decimal>();
                codigoHospital.Add(Convert.ToDecimal(txtConvenente.Text));

                if (cbxEmiteRel.Checked == true && rdblTipo.SelectedValue != "LOTE")
                {
                    gerarRelatorio("relAnexoII", false, codigoHospital);
                }

                MostraMensagemTelaUpdatePanel(upCisao, "Aumento atualizado com sucesso!");
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Preencha o número de contrato!");
            }
        }

        /// <summary>
        ///  Botão Para NÃO atualizar vigência
        /// </summary>
        protected void btnNaoAjustaDataVigencia_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtConvenente.Text))
            {
                var user = (ConectaAD)Session["objUser"];

                SAU_TB_LOG_AND log = new SAU_TB_LOG_AND();
                log.USUARIO = Convert.ToDecimal(user.login.Replace("F", "").Replace("E", ""));

                //ATUALIZA VALOR HOSP
                anexoBLL.AtualizaValorServicosHosp(Convert.ToDecimal(txtConvenente.Text), log);

                List<Decimal> codigoHospital = new List<decimal>();
                codigoHospital.Add(Convert.ToDecimal(txtConvenente.Text));

                if (cbxEmiteRel.Checked == true && rdblTipo.SelectedValue != "LOTE")
                {
                    gerarRelatorio("relAnexoII", false, codigoHospital);
                }

                MostraMensagemTelaUpdatePanel(upCisao, "Aumento atualizado com sucesso!");
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Preencha o número de contrato!");
            }
        }

        #endregion

        #region Botões / Funções Inclusão de novos prestadores

        protected void btnUpdServicoAlterar_Click(object sender, EventArgs e)
        {
            SAU_TB_HOSP_AND prestador = new SAU_TB_HOSP_AND();

            if (ddlNmHospital.SelectedValue != "0")
            {
                prestador.COD_HOSP = Convert.ToInt32(txtNumContrato.Text);
                prestador.NOME_FANTASIA = txtNmHospital.Text;
                prestador.CONTATO = txtContato.Text;
                prestador.DAT_INICIO_CONTRATO = Convert.ToDateTime(txtInicioContrato.Text);
                prestador.CIDADE = txtCidade.Text;
                prestador.REGIONAL = txtRegional.Text;
                prestador.CREDENCIADOR = txtCredenciador.Text;

                if (anexoBLL.AtualizarPrestador(prestador) == true)
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "As informações foram atualizadas com sucesso!");
                    txtNumContrato.Enabled = false;
                    txtNmHospital.Enabled = false;
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Não foi possível atualizar as informações!");
                }
            }
            else
            {
                // Já existe um prestador com este código.
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Por favor, selecione um prestador!");
            }

        }

        protected void btnDelServicoExcluir_Click(object sender, EventArgs e)
        {
            SAU_TB_HOSP_AND prestador = new SAU_TB_HOSP_AND();

            if (ddlNmHospital.SelectedValue != "0")
            {
                prestador.COD_HOSP = Convert.ToInt32(txtNumContrato.Text);
                prestador.CONTATO = txtContato.Text;
                prestador.DAT_FIM_VIGENCIA = Convert.ToDateTime(txtInicioContrato.Text);

                if (anexoBLL.ExcluirPrestador(prestador) == true)
                {
                    //MostraMensagemTelaUpdatePanelRedirectTabPanel(upCisao, "Prestador excluído com sucesso!", Request.RawUrl, TabContainer.ActiveTab.ToString());
                    MostraMensagemTelaUpdatePanelRedirect(upCisao, "Prestador excluído com sucesso!", Request.RawUrl);
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Não foi possível excluir o prestador!");
                }
            }
            else
            {
                // Já existe um prestador com este código.
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Por favor, selecione um prestador!");
                txtNumContrato.Enabled = true;
                txtNmHospital.Enabled = true;
            }
        }

        protected void btnInsEmpresaSalvar_Click(object sender, EventArgs e)
        {
            SAU_TB_HOSP_AND prestador = new SAU_TB_HOSP_AND();

            if ((!String.IsNullOrEmpty(txtNumContrato.Text) && !String.IsNullOrEmpty(txtInicioContrato.Text)) == true)
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Preencha as infomações solicitadas!");
                return;
            }

            if (ddlNmHospital.SelectedValue == "0" && txtNmHospital.Enabled == true)
            {
                prestador.COD_HOSP = Convert.ToInt32(txtNumContrato.Text);
                prestador.NOME_FANTASIA = txtNmHospital.Text;
                prestador.CONTATO = txtContato.Text;
                prestador.DAT_INICIO_CONTRATO = Convert.ToDateTime(txtInicioContrato.Text);
                prestador.CIDADE = txtCidade.Text;
                prestador.REGIONAL = txtRegional.Text;
                prestador.CREDENCIADOR = txtCredenciador.Text;

                Boolean retornaInclusaoPrestador = anexoBLL.incluirPrestador(prestador);

                if (retornaInclusaoPrestador == true)
                {
                    //Empresa inserida com sucesso!

                    limparCadastroPrestador();
                    MostraMensagemTelaUpdatePanelRedirect(upCisao, "O Prestador foi inserido com sucesso!", Request.RawUrl);
                }
                else
                {
                    // Já existe um prestador com este código.
                    MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\O código já existe, favor confirmar a informação.");
                    limparCadastroPrestador();
                    ddlNmHospital = new DropDownList();

                    CarregaDropDowList(ddlNmHospital, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
                }
            }
            else
            {
                // Já existe um prestador com este código.
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Você não pode adicionar novamente um prestador que já existe.");
                limparCadastroPrestador();
                txtNumContrato.Enabled = true;
                txtNmHospital.Enabled = true;
                //CarregaDropDowList(ddlNmHospital, anexoBLL.carregarHospital(null, null).ToList<object>(), "NOME_FANTASIA", "COD_HOSP");
            }
        }

        public void btnCadEmpvoltar_Click(object sender, EventArgs e)
        {
            limparCadastroPrestador();
            txtNmHospital.Enabled = true;
            txtNumContrato.Enabled = true;
        }

        public void btnServicoLimpar_Click(object sender, EventArgs e)
        {
            txtNumContrato.Text = "";
            txtContato.Text = "";
            txtInicioContrato.Text = "";
        }

        #endregion

        #region Botões /Funções - Cadastro código de serviço

        public void CarregarObservacao(string CodigoServico)
        {
            if (!String.IsNullOrEmpty(CodigoServico))
            {
                SAU_TB_SERVICO_AND servico = new SAU_TB_SERVICO_AND();
                servico = anexoBLL.CarregarServicos(CodigoServico).FirstOrDefault();

                if (servico != null)
                {
                    txtTbDescricaoCodigoServico.Text = servico.DESCRICAO;
                }
            }
        }

        protected void btnPesquisarServico_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTbCodigoServico.Text))
            {
                CarregarObservacao(txtTbCodigoServico.Text);
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Preencha o código antes de continuar.");
                txtTbCodigoServico.Text = "";
                txtTbDescricaoCodigoServico.Text = "";
            }
        }

        protected void btnGridServicosTiss_Click(object sender, EventArgs e)
        {

        }

        protected void btnincluirServico_Click(object sender, EventArgs e)
        {
            CadastroCodigoServico("INCLUIR");
        }

        protected void btnAlterarServico_Click(object sender, EventArgs e)
        {
            CadastroCodigoServico("ALTERAR");
        }

        protected void btnExcluirServico_Click(object sender, EventArgs e)
        {
            CadastroCodigoServico("EXCLUIR");
        }

        public void btnVoltarServico_Click(object sender, EventArgs e)
        {
            limparCadastroPrestador();
        }

        public void btnTbServicoLimpar_Click(object sender, EventArgs e)
        {
            txtNumContratoServicos.Text = "";
            txtCodigoServico.Text = "";
            txtDescricaoServico.Text = "";
            txtValorAtual.Text = "";
            txtVigenciaInicial.Text = "";
            ddlHospitalServico.SelectedValue = "0";
        }

        public void btnLimparObsContratual_Click(object sender, EventArgs e)
        {
            txtObservacoesContratuais.Text = "";
            txtcodEmpresaObsContratual.Text = "";
            ddlHospitalCdEmp.SelectedValue = "0";
        }

        public void btnLimparTelaAumento_Click(object sender, EventArgs e)
        {
            txtConvenente.Text = "";
            RemoverlinhasListBox(lstServicos);
            txtEscalonado.Text = "";
            ddlConvenente.SelectedValue = "0";
            txtPercentReajuste.Text = "";
            txtValProposto.Text = "";
            txtPerDesconto.Text = "";
            txtDataVigencia.Text = "";
            cbxEmiteRel.Checked = false;
        }

        #endregion

        #region Botões /funções - Cadastro de SERVIÇO X HOSPITAL (DE x PARA)

        public void lmpServicoPrestador()
        {
            txtDescricaoServico.Text = "";
            txtValorAtual.Text = "";
            txtVigenciaInicial.Text = "";
            txtCodigoServico.Text = "";
        }

        protected void btnIncluirServicoPrestador_Click(object sender, EventArgs e)
        {
            CadastroServicoPorHospital("INCLUIR");
            lmpServicoPrestador();
        }

        protected void btnAlterarServicoPrestador_Click(object sender, EventArgs e)
        {
            CadastroServicoPorHospital("ALTERAR");
            lmpServicoPrestador();
        }

        protected void btnExcluirServicoPrestador_Click(object sender, EventArgs e)
        {
            CadastroServicoPorHospital("EXCLUIR");
            lmpServicoPrestador();
        }

        protected void btnServicoVoltar_Click(object sender, EventArgs e)
        {
            limparCadastroPrestador();
        }

        #endregion

        #region Botões /funções - Cadastro de Observações Contratuais

        protected void btnIncluirObsContratual_Click(object sender, EventArgs e)
        {
            CadastrarObservacaoContratual("INCLUIR");
        }

        protected void btnAlterarObsContratual_Click(object sender, EventArgs e)
        {
            CadastrarObservacaoContratual("ALTERAR");
        }

        protected void btnExcluirObsContratual_Click(object sender, EventArgs e)
        {
            CadastrarObservacaoContratual("EXCLUIR");
        }


        #endregion

        #region Botões /funções - Exporta arquivo para SCAN


        protected void btnCarregaLog_Click(object sender, EventArgs e)
        {
            carregaGridLogExportacao();
        }

        protected void btnGeraArquivoScan_Click(object sender, EventArgs e)
        {
            var user = (ConectaAD)Session["objUser"];

            SAU_TB_LOG_AND log = new SAU_TB_LOG_AND();
            log.USUARIO = Convert.ToDecimal(user.login.Replace("F", "").Replace("E", ""));


            List<String> itens = retornaValorListBox(lstEmpresasExportacao);

            if (itens.Count > 0)
            {
                anexoBLL.TrataExportacaoScan(itens, log);
            }
            else
            {
                MostraMensagemTelaUpdatePanel(upCisao, "Atenção\\n\\Nenhuma informação foi encontrada.");
            }
        }


        #endregion

        #region FUNÇÕES - CRUD

        public Resultado CadastroServicoPorHospital(string tipo)
        {
            Resultado res = new Resultado();

            decimal codServico;
            Decimal.TryParse(txtCodigoServico.Text, out codServico);

            decimal codHospital;
            Decimal.TryParse(txtNumContratoServicos.Text, out codHospital);

            string descricaoBase = rdblDescricaoBase.SelectedValue;
            String valorAnual = txtValorAtual.Text;

            DateTime dataVigencia;
            DateTime.TryParse(txtVigenciaInicial.Text, out dataVigencia);

            decimal valor;
            Decimal.TryParse(txtValorAtual.Text, out valor);

            SAU_TB_SERV_X_HOSP_AND servicoHosp = new SAU_TB_SERV_X_HOSP_AND
            {
                COD_HOSP = codHospital,
                COD_SERV = codServico,
                //DESCRICAO = txtDescricaoServico.Text.Trim(),
                DAT_INI_VIGENCIA = dataVigencia,
                VALOR = valor
            };
            if (tipo == "INCLUIR")
            {
                if (String.IsNullOrEmpty(txtCodigoServico.Text) || String.IsNullOrEmpty(txtNumContratoServicos.Text))
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "O formulário precisa ser preenchido!");
                }
                else if (anexoBLL.IncluirServicoxHospital(servicoHosp) == true)
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "O serviço foi cadastrado com sucesso!");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "O servição NÃO foi cadastrado, verique os dados.");
                }
            }

            else if (tipo == "ALTERAR")
            {
                if (String.IsNullOrEmpty(txtCodigoServico.Text) || String.IsNullOrEmpty(txtNumContratoServicos.Text))
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "O formulário precisa ser preenchido!");
                }
                else if (anexoBLL.AlterarServicoxHospital(servicoHosp) == true)
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "O serviço foi Alterado com sucesso!");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "O servição NÃO foi alterado, verique os dados.");
                }
            }

            else if (tipo == "EXCLUIR")
            {
                if (String.IsNullOrEmpty(txtCodigoServico.Text) || String.IsNullOrEmpty(txtNumContratoServicos.Text))
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "O formulário precisa ser preenchido!");
                }
                else if (anexoBLL.ExcluirServicoxHospital(servicoHosp) == true)
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "O serviço foi excluído com sucesso!");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "O servição NÃO foi excluido, verique os dados.");
                }
            }

            return res;
        }

        public Resultado CadastroCodigoServico(string tipo)
        {
            Resultado res = new Resultado();
            Int32 cdServico;
            Int32.TryParse(txtTbCodigoServico.Text, out cdServico);
            String descricao = txtTbDescricaoCodigoServico.Text;


            if (cdServico > 0 && !string.IsNullOrEmpty(descricao))
            {
                SAU_TB_SERVICO_AND servico = new SAU_TB_SERVICO_AND();
                servico.COD_SERV = cdServico;
                servico.DESCRICAO = descricao;

                if (tipo == "INCLUIR")
                {
                    if (anexoBLL.IncluirServico(servico) == true)
                    {
                        MostraMensagemTelaUpdatePanel(upCisao, "O serviço foi cadastrado com sucesso!");
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upCisao, "Ocorreu um erro ao tentar cadastrar o Serviço\\n\\ Este código de Serviço já está em uso.");
                    }
                }
                else if (tipo == "ALTERAR")
                {
                    if (anexoBLL.AlterarServico(servico) == true)
                    {
                        MostraMensagemTelaUpdatePanel(upCisao, "O serviço foi atualizado com sucesso!");
                    }
                    else
                    {
                        res.Erro("Ocorreu um erro ao tentar atualizar o Serviço.");

                    }
                }
                else if (tipo == "EXCLUIR")
                {
                    if (anexoBLL.ExcluirServico(servico) == true)
                    {
                        MostraMensagemTelaUpdatePanel(upCisao, "O serviço foi excluído com sucesso!");
                        txtTbCodigoServico.Text = "";
                        txtTbDescricaoCodigoServico.Text = "";
                    }
                    else
                    {
                        MostraMensagemTelaUpdatePanel(upCisao, "Ocorreu um erro ao tentar excluir o Serviço.");
                    }
                }
                CarregarObservacao(txtTbCodigoServico.Text);
            }
            return res;
        }

        public Resultado CadastrarObservacaoContratual(string tipo)
        {
            Resultado res = new Resultado();

            String Observacao = txtObservacoesContratuais.Text;
            //DateTime DataVigencia;
            //DateTime.TryParse(txtDtvigenciaObsContratual.Text, out DataVigencia);

            decimal codHospital;
            Decimal.TryParse(txtcodEmpresaObsContratual.Text, out codHospital);

            SAU_TB_HOSP_AND servicoHosp = new SAU_TB_HOSP_AND();
            servicoHosp.COD_HOSP = Convert.ToDecimal(codHospital);
            servicoHosp.OBSERVACAOCONTRATUAL = Observacao;


            if (tipo == "INCLUIR")
            {
                if (anexoBLL.incluirObservacaoContratual(servicoHosp) == true)
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "A Observação Contratual foi incluída com sucesso!");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "Ocorreu um erro ao tentar cadastrar a Observação Contratual.");
                }
            }

            else if (tipo == "ALTERAR")
            {
                if (anexoBLL.AlterarObservacaoContratual(servicoHosp) == true)
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "A Observação Contratual foi alterada com sucesso!");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "Ocorreu um erro ao tentar alterar a Observação Contratual.");
                }
            }

            else if (tipo == "EXCLUIR")
            {
                if (anexoBLL.ExcluirObservacaoContratual(servicoHosp) == true)
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "A Observação Contratual foi excluída com sucesso!");
                }
                else
                {
                    MostraMensagemTelaUpdatePanel(upCisao, "Ocorreu um erro ao tentar excluir a Observação Contratual.");
                    txtObservacoesContratuais.Text = "";
                }
            }

            return res;
        }



        #endregion

    }
}