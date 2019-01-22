<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="IntTabelaMedicao.aspx.cs" Inherits="IntegWeb.Financeira.Web.IntTabelaMedicao" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="full_w">

        <div class="h_title">
        </div>
        <style>
            .Largura {
                width: 180px;
            }

            .Celula-p {
                width: 85px;
            }

            .BackgroundW {
                background-color: white;
                color: black;
            }

            .larguraTd {
                width: 300px;
            }
        </style>


        <script type="text/javascript">

            var tpPessoa;

            var updateProgress = null;

            function postbackButtonClick() {
                updateProgress = $find("<%= UpdateProg1.ClientID %>");
                window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
                return true;
            }

            function CarregarFuncoes() {
                
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                function EndRequestHandler(sender, args) {

                    $("#ContentPlaceHolder1_txtClienteFornecedor").keyup(
                        function LimpaTextoTempoRealCli() {
                            $("#ContentPlaceHolder1_txtCpfCnpj").val("");
                        }
                    );

                    $("#ContentPlaceHolder1_txtCpfCnpj").keyup(
                        function LimpaTextoTempoRealCli() {
                            $("#ContentPlaceHolder1_txtClienteFornecedor").val("");
                        }
                    );

                    // Atualiza o campo CONTRATO ao clicar em qualquer região do painel "Atualiza"
                    $("#ContentPlaceHolder1_pnlAtualiza").hover(function AdicionaZeros() {

                        var pagarReceber = $("#ContentPlaceHolder1_ddlPagarReceber").val();
                        var negocio = $("#ContentPlaceHolder1_ddlNegocio").val();
                        var operacao = $("#ContentPlaceHolder1_ddlOperacao").val();
                        var cpfCnpj = $("#ContentPlaceHolder1_txtCpfCnpj").val();

                        if ($("#ContentPlaceHolder1_txtCpfCnpj").val().length >= 1) {
                            if ($("#ContentPlaceHolder1_txtCpfCnpj").val().length < 14) {
                                for (var i = $("#ContentPlaceHolder1_txtCpfCnpj").val().length; i < 14; i++) {
                                    cpfCnpj = '0' + cpfCnpj;
                                }
                            }

                            if (pagarReceber !== null && pagarReceber !== "0" &&
                                negocio !== null && negocio !== "0" &&
                                operacao !== null && operacao !== "0" &&
                                cpfCnpj !== null && cpfCnpj !== "0") {
                                $("#ContentPlaceHolder1_txtContrato").val(pagarReceber + negocio + operacao + cpfCnpj);
                            }
                        }
                    });

                    // CHAMADA AJAX AUTOCOMPLETE CLIENTEFORNECEDOR //
                    $("#ContentPlaceHolder1_txtClienteFornecedor").autocomplete({

                        source: function (request, response) {

                            $.ajax({

                                url: '<%= ResolveUrl("IntTabelaMedicao.aspx/BuscaResultado") %>',

                                type: "POST",

                                dataType: "json",

                                contentType: "application/json; charset=utf-8",

                                data: "{'pBusca':'" + $("#ContentPlaceHolder1_txtClienteFornecedor").val() + "'}",

                                success: function (data) {

                                    response($.map(data.d, function (item) {
                                        return {
                                            label: item.nomeRazaoSocial,
                                            value: item.nomeRazaoSocial,
                                            id: item.cpfCnpj,
                                            tpPessoa: item.tipoCadastro,
                                            codigo: item.codigo, // identificação
                                            codigoEmpresa: item.codigoEmpresa, // Identifição da empresa que atua
                                            codigoAgencia: item.codigoAgencia, //agência bancária
                                            codigoBanco: item.codigoBanco, // Código banco
                                            codigotipoConta: item.codigotipoConta,
                                            codigoContaCorrente: item.codigoContaCorrente
                                        }
                                    }))
                                }
                            });
                        },

                        select: function (event, ui) {
                            $("#ContentPlaceHolder1_txtClienteFornecedor").val(ui.item.id);

                            $("#ContentPlaceHolder1_txtCpfCnpj").val(ui.item.id)


                            var tipoPess = 0;
                            var codigo = 0;
                            var codigoEmpresa = 0;
                            var codigoAgencia = 0;
                            var codigoBanco = 0;
                            var codigotipoConta = 0;
                            var codigoContaCorrente = 0;

                            tipoPess = ui.item.tpPessoa;
                            codigo = ui.item.codigo;
                            codigoEmpresa = ui.item.codigoEmpresa;
                            codigoAgencia = ui.item.codigoAgencia;
                            codigoBanco = ui.item.codigoBanco;
                            codigotipoConta = ui.item.codigotipoConta;
                            codigoContaCorrente = ui.item.codigoContaCorrente;
                            codigoAgencia = ui.item.codigoAgencia;

                            if (tipoPess.indexOf('pFisica') > -1) {
                                $("#ContentPlaceHolder1_txtTipoPessoa").val("Pessoa Física")
                                $("#ContentPlaceHolder1_hiddenTipoPessoa").val("F")
                                $("#ContentPlaceHolder1_hiddenTipoPesquisa").val(tipoPess)
                                $("#ContentPlaceHolder1_hiddenCodigoEmpresa").val(codigoEmpresa)
                                $("#ContentPlaceHolder1_hiddenCodigo").val(codigo)
                                $("#ContentPlaceHolder1_hiddenCodigoBanco").val(codigoBanco)
                                $("#ContentPlaceHolder1_hiddenCodigoContaCorrente").val(codigoContaCorrente)
                                $("#ContentPlaceHolder1_hiddenCodigoTipoConta").val(codigotipoConta)
                                $("#ContentPlaceHolder1_hiddenCodigoAgencia").val(codigoAgencia)
                            } else {
                                $("#ContentPlaceHolder1_txtTipoPessoa").val("Pessoa Jurídica")
                                $("#ContentPlaceHolder1_hiddenTipoPessoa").val("J")
                                $("#ContentPlaceHolder1_hiddenTipoPesquisa").val(tipoPess)
                                $("#ContentPlaceHolder1_hiddenCodigoEmpresa").val("0")
                                $("#ContentPlaceHolder1_hiddenCodigo").val(codigo)
                                $("#ContentPlaceHolder1_hiddenCodigoBanco").val(codigoBanco)
                                $("#ContentPlaceHolder1_hiddenCodigoContaCorrente").val(codigoContaCorrente)
                                $("#ContentPlaceHolder1_hiddenCodigoTipoConta").val(codigotipoConta)
                                $("#ContentPlaceHolder1_hiddenCodigoAgencia").val(codigoAgencia)
                            }
                        }
                    });
                     
                    // CHAMADA AJAX AUTOCOMPLETE CPFCNPJ //
                    $("#ContentPlaceHolder1_txtCpfCnpj").autocomplete({

                        source: function (request, response) {

                            $.ajax({

                                url: '<%= ResolveUrl("IntTabelaMedicao.aspx/BuscaResultadoCpfCnpj") %>',
                                
                                type: "POST",

                                dataType: "json",

                                contentType: "application/json; charset=utf-8",

                                data: "{'pBusca':'" + $("#ContentPlaceHolder1_txtCpfCnpj").val() + "'}",

                                success: function (data) {

                                    response($.map(data.d, function (item) {
                                        return {
                                            label: item.nomeRazaoSocial,
                                            value: item.cpfCnpj,
                                            id: item.cpfCnpj,
                                            tpPessoa: item.tipoCadastro,
                                            codigo: item.codigo, // identificação
                                            codigoEmpresa: item.codigoEmpresa, // Identifição da empresa que atua
                                            codigoAgencia: item.codigoAgencia, //Agencia bancaria
                                            codigoBanco: item.codigoBanco, // Código banco
                                            codigotipoConta: item.codigotipoConta, // Tipo de conta corrente
                                            codigoContaCorrente: item.codigoContaCorrente,
                                            codigoAgencia: item.codigoAgencia
                                        }
                                    }))
                                }
                            });
                        },

                        select: function (event, ui) {
                            $("#ContentPlaceHolder1_txtClienteFornecedor").val(ui.item.label);

                            $("#ContentPlaceHolder1_txtCpfCnpj").val(ui.item.id)


                            var tipoPess = 0;
                            var codigo = 0;
                            var codigoEmpresa = 0;
                            var codigoAgencia = 0;
                            var codigoBanco = 0;
                            var codigotipoConta = 0;
                            var codigoContaCorrente = 0;

                            tipoPess = ui.item.tpPessoa;
                            codigo = ui.item.codigo;
                            codigoEmpresa = ui.item.codigoEmpresa;
                            codigoAgencia = ui.item.codigoAgencia;
                            codigoBanco = ui.item.codigoBanco;
                            codigotipoConta = ui.item.codigotipoConta;
                            codigoContaCorrente = ui.item.codigoContaCorrente;
                            codigoAgencia = ui.item.codigoAgencia;

                            if (tipoPess.indexOf('pFisica') > -1) {
                                $("#ContentPlaceHolder1_txtTipoPessoa").val("Pessoa Física")
                                $("#ContentPlaceHolder1_hiddenTipoPessoa").val("F")
                                $("#ContentPlaceHolder1_hiddenTipoPesquisa").val(tipoPess)
                                $("#ContentPlaceHolder1_hiddenCodigoEmpresa").val(codigoEmpresa)
                                $("#ContentPlaceHolder1_hiddenCodigo").val(codigo)
                                $("#ContentPlaceHolder1_hiddenCodigoBanco").val(codigoBanco)
                                $("#ContentPlaceHolder1_hiddenCodigoContaCorrente").val(codigoContaCorrente)
                                $("#ContentPlaceHolder1_hiddenCodigoTipoConta").val(codigotipoConta)
                                $("#ContentPlaceHolder1_hiddenCodigoAgencia").val(codigoAgencia)

                            } else {
                                $("#ContentPlaceHolder1_txtTipoPessoa").val("Pessoa Jurídica")
                                $("#ContentPlaceHolder1_hiddenTipoPessoa").val("J")
                                $("#ContentPlaceHolder1_hiddenTipoPesquisa").val(tipoPess)
                                $("#ContentPlaceHolder1_hiddenCodigoEmpresa").val("0")
                                $("#ContentPlaceHolder1_hiddenCodigo").val(codigo)
                                $("#ContentPlaceHolder1_hiddenCodigoBanco").val(codigoBanco)
                                $("#ContentPlaceHolder1_hiddenCodigoContaCorrente").val(codigoContaCorrente)
                                $("#ContentPlaceHolder1_hiddenCodigoTipoConta").val(codigotipoConta)
                                $("#ContentPlaceHolder1_hiddenCodigoAgencia").val(codigoAgencia)
                            }
                        },
                        change: function (event, ui) {

                        }
                    });
                }
            }

            $(document).ready(function () {
                CarregarFuncoes();
            })


            $(function () {
                CarregarFuncoes();
            });

        </script>

        <asp:HiddenField ID="hiddenTipoPesquisa" runat="server" />
        <asp:HiddenField ID="hiddenTipoPessoa" runat="server" />
        <asp:HiddenField ID="hiddenCodigo" runat="server" />
        <asp:HiddenField ID="hiddenCodigoEmpresa" runat="server" />
        <asp:HiddenField ID="hiddenCodigoBanco" runat="server" />
        <asp:HiddenField ID="hiddenCodigoContaCorrente" runat="server" />
        <asp:HiddenField ID="hiddenCodigoTipoConta" runat="server" />
        <asp:HiddenField ID="hiddenCodigoAgencia" runat="server" />
        <div class="tabelaPagina">
            <h2>Correção tabela de Medição - Protheus</h2>


            <asp:UpdatePanel runat="server" ID="upUpdatepanel">

                <ContentTemplate>

                    <asp:Panel class="tabelaPagina" ID="pnlPesquisa" runat="server">

                        <table style="width: 100%">

                            <div class="formTable">

                                <table style="width: 100%" class="LarguraCelula">

                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl1" runat="server" Width="100px">Matrícula</asp:Label>
                                            <asp:TextBox ID="txtbuscaEmpregado" runat="server" onkeypress="mascara(this, soNumeros)"> </asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Width="100px">Código Empresa</asp:Label>
                                            <asp:TextBox ID="txtbuscaEmpresa" runat="server" onkeypress="mascara(this, soNumeros)"> </asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Width="100px">Código Convenente</asp:Label>
                                            <asp:TextBox ID="txtConvenente" runat="server" onkeypress="mascara(this, soNumeros)"> </asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" Width="100px">Banco</asp:Label>
                                            <asp:TextBox ID="txtBanco" onkeypress="mascara(this, soNumeros)" runat="server"> </asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" Width="100px">Agência</asp:Label>
                                            <asp:TextBox ID="txtAgencia" onkeypress="mascara(this, soNumeros)" runat="server"> </asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Width="100px">Data Inclusão</asp:Label>
                                            <asp:TextBox ID="txtDataInclusao" CssClass="date" onkeypress="mascara(this, data)" runat="server"> </asp:TextBox>
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Button runat="server" ID="btnPesquisar" Text="Pesquisar" CssClass="button" OnClientClick="return postbackButtonClick();" OnClick="btnPesquisar_Click" />

                                            <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CssClass="button" OnClick="btnLimpar_Click" />

                                            <asp:Button runat="server" ID="btnInserir" Text="Inserir" CssClass="button" OnClick="btnInserir_Click" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>


                                <asp:GridView ID="grdPesquisa" AutoGenerateColumns="false" AllowPaging="true"
                                    PageSize="10" OnPageIndexChanging="grd_PageIndexChanging" CssClass="Table"
                                    runat="server" EmptyDataText="A consulta não retornou dados" ClientIDMode="Static">
                                    <Columns>
                                        <asp:BoundField DataField="NUM_RGTRO_EMPRG" HeaderText="Empregado" />
                                        <asp:BoundField DataField="COD_CONVENENTE" HeaderText="Convenente" />
                                        <asp:BoundField DataField="COD_EMPRS" HeaderText="Empresa" />

                                        <asp:BoundField DataField="BANCO" HeaderText="Banco" />
                                        <asp:BoundField DataField="AGENCIA" HeaderText="Agência" />

                                        <asp:BoundField DataField="XNUMCT" HeaderText="Contrato" />
                                        <asp:BoundField DataField="PRODUT" HeaderText="Produto" />
                                        <asp:BoundField DataField="VALMED" HeaderText="Valor" />
                                        <asp:BoundField DataField="dtincl" HeaderText="Inclusão" />
                                        <asp:BoundField DataField="PROGRAMA" HeaderText="Prog./ Plano" />
                                        <asp:BoundField DataField="CCUSTO" HeaderText="Centro de Custo" />
                                        <asp:BoundField DataField="PATROCINADOR" HeaderText="Patrocinador" />
                                        <asp:BoundField DataField="seq_medctr" HeaderText="Sequência" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </table>

                    </asp:Panel>

                    <asp:Panel class="tabelaPagina " ID="pnlAtualiza" runat="server" Visible="false">

                        <table style="width: 100%" class="LarguraCelula">

                            <tr class="cab_td">
                                <td>Evento</td>
                                <td>Pagar/ Receber</td>
                                <td>Negócio</td>
                                <td>Operação</td>
                            </tr>
                            <tr class="cab_td">

                                <td>
                                    <asp:DropDownList CssClass="Largura" runat="server" ID="ddlEvento" Width="250px"></asp:DropDownList>
                                </td>

                                <td>
                                    <asp:DropDownList CssClass="Largura" runat="server" ID="ddlPagarReceber" Width="250px">
                                        <asp:ListItem Text="---Selecione---" Value="0" />
                                        <asp:ListItem Text="Pagar" Value="P" />
                                        <asp:ListItem Text="Receber" Value="R" />
                                    </asp:DropDownList>
                                </td>

                                <td>
                                    <asp:DropDownList CssClass="Largura" runat="server" ID="ddlNegocio" Width="250px">
                                        <asp:ListItem Text="---Selecione---" Value="0" />
                                        <asp:ListItem Text="Administrativo" Value="A" />
                                        <asp:ListItem Text="Farmácias" Value="F" />
                                        <asp:ListItem Text="Não credenciadas" Value="N" />
                                        <asp:ListItem Text="Saúde" Value="S" />
                                        <asp:ListItem Text="Previdência" Value="P" />
                                        <asp:ListItem Text="OPME" Value="O" />
                                    </asp:DropDownList>
                                </td>

                                <td>
                                    <asp:DropDownList CssClass="Largura" runat="server" ID="ddlOperacao" Width="250px">
                                        <asp:ListItem Text="---Selecione---" Value="0" />
                                        <asp:ListItem Text="Administrativo" Value="ADM" />
                                        <asp:ListItem Text="Associações" Value="ASS" />
                                        <asp:ListItem Text="Benefícios" Value="BEN" />
                                        <asp:ListItem Text="Capitalização" Value="CAP" />
                                        <asp:ListItem Text="Credenciados" Value="CRE" />
                                        <asp:ListItem Text="Empréstimos" Value="EMP" />
                                        <asp:ListItem Text="Farmácia" Value="FAR" />
                                        <asp:ListItem Text="Não Credenciados" Value="NCR" />
                                        <asp:ListItem Text="OPME" Value="OPM" />
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr class="cab_td">
                                <td>Cliente / Fornecedor</td>
                                <td>CPF / CNPJ</td>
                                <td>Tipo Participante</td>
                                <td>Contrato</td>
                            </tr>

                            <tr class="cab_td">
                                <td>
                                    <asp:TextBox ID="txtClienteFornecedor" runat="server" Width="250px"></asp:TextBox>
                                </td>

                                <td>
                                    <asp:TextBox ID="txtCpfCnpj" runat="server" Width="250px"></asp:TextBox>
                                </td>

                                <td>
                                    <asp:DropDownList CssClass="Largura" runat="server" ID="ddlTipoParticipante" Width="250px">
                                        <asp:ListItem Text="---Selecione---" Value="0" />
                                        <asp:ListItem Text="Usuario saúde" Value="01" />
                                        <asp:ListItem Text="Designado saúde" Value="02" />
                                        <asp:ListItem Text="Ativos" Value="03" />
                                        <asp:ListItem Text="Auto patrocinado" Value="04" />
                                        <asp:ListItem Text="Coligados" Value="05" />
                                        <asp:ListItem Text="Assistidos" Value="06" />
                                        <asp:ListItem Text="Pensões previdenciárias" Value="07" />
                                        <asp:ListItem Text="Pensões alimentares" Value="08" />
                                        <asp:ListItem Text="Fornecedores" Value="09" />
                                        <asp:ListItem Text="Rede credenciada" Value="10" />
                                        <asp:ListItem Text="Conveniadas" Value="11" />
                                        <asp:ListItem Text="Farmácia" Value="12" />
                                        <asp:ListItem Text="Prestador não credenciado" Value="13" />
                                        <asp:ListItem Text="OPME" Value="14" />
                                        <asp:ListItem Text="Associações" Value="15" />
                                    </asp:DropDownList>
                                </td>

                                <td>
                                    <asp:TextBox runat="server" ID="txtContrato" CssClass="BackgroundW" Enabled="false" Width="250px"></asp:TextBox>
                                </td>

                            </tr>

                            <tr class="cab_td">
                                <td>Produto</td>
                                <td>Valor</td>
                                <td>Data Vencimento</td>
                                <td>Tipo Pessoa</td>
                            </tr>

                            <tr class="cab_td">
                                <td>
                                    <asp:DropDownList runat="server" CssClass="Largura" ID="ddlProduto" Width="250px"></asp:DropDownList>
                                </td>

                                <td>
                                    <asp:TextBox runat="server" CssClass="Largura" ID="txtValor" onkeypress="mascara(this, moeda)" Width="250px"></asp:TextBox>
                                </td>

                                <td>
                                    <asp:TextBox runat="server" CssClass="Largura date" ID="txtDataVencimento" onkeypress="mascara(this, data)" Width="250px"></asp:TextBox>
                                </td>

                                <td>
                                    <asp:TextBox runat="server" ID="txtTipoPessoa" Enabled="false" CssClass="BackgroundW" Width="250px"></asp:TextBox>
                                </td>
                            </tr>


                            <tr class="cab_td">
                                <td>Programa/Plano</td>
                                <td>Submassa</td>
                                <td>Centro de Custo</td>
                                <td></td>
                            </tr>

                            <tr class="cab_td">
                                <td>
                                    <asp:DropDownList runat="server" CssClass="Largura" ID="ddlProgramaPlano" Width="250px"></asp:DropDownList>
                                </td>

                                <td>
                                    <asp:DropDownList runat="server" CssClass="Largura" ID="ddlSubmassa" Width="250px"></asp:DropDownList>
                                </td>

                                <td>
                                    <asp:DropDownList runat="server" CssClass="Largura" ID="ddlCentroCusto" Width="250px"></asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>



                            <tr class="cab_td">
                                <td>Patrocinador</td>
                                <td>Tipo de Liquidação</td>
                                <td>Tipo Processamento</td>
                                <td></td>
                            </tr>

                            <tr class="cab_td">
                                <td>
                                    <asp:DropDownList runat="server" CssClass="Largura" ID="ddlPatrocinador" Width="250px"></asp:DropDownList>
                                </td>

                                <td>
                                    <asp:DropDownList runat="server" CssClass="Largura" ID="ddlTipoLiquidacao" Width="250px"></asp:DropDownList>
                                </td>

                                <td>
                                    <asp:DropDownList runat="server" CssClass="Largura" ID="ddlTipoProcessamento" Width="250px"></asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>


                        </table>


                        <table style="width: 100%" class="LarguraCelula">
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="btnSalvar" Text="Salvar" CssClass="button" OnClick="btnSalvar_Click" />

                                    <asp:Button runat="server" ID="btnLimparInclusao" Text="Limpar" CssClass="button" OnClick="btnLimparInclusao_Click" />

                                    <asp:Button runat="server" ID="btnVoltar" Text="Voltar" CssClass="button" OnClick="btnVoltar_Click" />

                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server" Style="display: none">
                <ProgressTemplate>
                    <div id="carregando">
                        <div class="carregandoTxt">
                            <img src="img/processando.gif" />
                            <br />
                            <h2>Processando. Aguarde...</h2>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>



        </div>
    </div>


</asp:Content>
