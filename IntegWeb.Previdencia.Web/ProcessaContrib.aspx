<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ProcessaContrib.aspx.cs" Inherits="IntegWeb.Previdencia.Web.ProcessaContrib" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $(".date").datepicker({
                    dateFormat: 'dd/mm/yy',
                    dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                    dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                    dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                    monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                    monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                    nextText: 'Próximo',
                    prevText: 'Anterior'
                });
            }

        });
    </script>
    <div class="full_w">

        <div class="h_title">
        </div>

        <h1>Salário Real e Benefício Inicial </h1>
        <div class="MarginGrid">
            <asp:UpdatePanel runat="server" ID="upContrib">
                <ContentTemplate>
                    <div class="tabelaPagina" id="divSelect" runat="server">

                        <table>

                            <tr>
                                <td>Digite o nº Empresa</td>
                                <td>
                                    <asp:TextBox ID="txtCodEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Digite o nº Matrícula</td>
                                <td>
                                    <asp:TextBox ID="txtCodMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnPesquisar" OnClick="btnPesquisar_Click" runat="server" CssClass="button" Text="Pesquisar   " />

                                    <asp:Button ID="btnLimpar" OnClick="btnLimpar_Click" runat="server" CssClass="button" Text="Limpar" />


                                    <asp:Button ID="btnInserir" OnClick="btnInserir_Click" runat="server" CssClass="button" Text="Inserir Novo Processo" /></td>
                            </tr>
                        </table>

                        <div id="divParticip" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td>Nome do Participante</td>
                                    <td>
                                        <asp:TextBox ID="TxtNom" Width="500px" Enabled="false" runat="server"></asp:TextBox></td>
                                </tr>
                            </table>
                            <div class="formTable">
                                <asp:HiddenField ID="hdMatricula" runat="server" />
                                <asp:HiddenField ID="HdEmpresa" runat="server" />
                                <asp:GridView AllowPaging="true" AutoGenerateColumns="false" PageSize="10" OnRowCommand="grdSrc_RowCommand"
                                    EmptyDataText="A consulta não retornou dados" CssClass="Table" OnPageIndexChanging="grdSrc_PageIndexChanging" ClientIDMode="Static" ID="grdSrc" runat="server">
                                    <Columns>
                                        <asp:BoundField HeaderText="Sequência" DataField="num_sqncl_prc" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Num Matri" DataField="num_matr_partf" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Data do Cálculo" DataField="dth_processamento" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                        <asp:BoundField HeaderText="Nº Processo" DataField="nro_processo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Nº da Pasta" DataField="nro_pasta" ItemStyle-HorizontalAlign="Center" />
                                          <asp:BoundField HeaderText="Situação" DataField="DESC_MR" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Último Processo Executado" DataField="desc_processo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                                    Text="Continuar Processo" CommandName="processo" CommandArgument='<%# Eval("num_sqncl_prc")+","+Eval("num_matr_partf")+","+Eval("id_acao_processo") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditar" runat="server" CssClass="button"
                                                    Text="Deletar Processo" OnClientClick="return confirm('Atenção!! \n\nDeseja Realmente deletar o processo?');" CommandName="Deletar" CommandArgument='<%# Eval("num_sqncl_prc") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>


                    </div>

                    <div class="formTable" id="divInsert" runat="server" visible="false">
                        <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" runat="server" CssClass="button" Text="Voltar á Tela Inicial" />
                        <br />
                        <br />
                        <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">

                            <ajax:TabPanel ID="TabSrc" HeaderText="Salário Real de Contribuição" runat="server" TabIndex="1">
                                <ContentTemplate>

                                    <table>
                                        <tr>

                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Código da Empresa</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:TextBox ID="txtEmpresa" CausesValidation="false" runat="server"></asp:TextBox>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td colspan="2">
                                                <table>
                                                    <tr>
                                                        <td>Registro Empregado</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtMatricula" AutoPostBack="true" OnTextChanged="txtMatricula_TextChanged" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblNome" Visible="false" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hdNumPartif" Value="0" runat="server" />
                                                <asp:HiddenField ID="hdSeq" Value="0" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>


                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>Número da Pasta</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtPasta" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>

                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Número do Processo</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:TextBox ID="txtProcesso" runat="server"></asp:TextBox>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Código da Vara</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:TextBox ID="txtCodVara" runat="server"></asp:TextBox>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>

                                        </tr>

                                        <tr>

                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Tipo de Pleito</td>

                                                    </tr>
                                                    <tr>

                                                        <td>

                                                            <asp:DropDownList ID="drpTipoPleito" Width="150px" runat="server"></asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Polo da Ação Judicial</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:RadioButtonList ID="rdPoloAcJudic" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Não" Value="N" Selected="True" />
                                                                <asp:ListItem Text="Sim" Value="S" />
                                                            </asp:RadioButtonList>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>Considera abono para contribuição?</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdAbono" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Não" Value="N" Selected="True" />
                                                                <asp:ListItem Text="Sim" Value="S" />
                                                            </asp:RadioButtonList>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>Observação</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtObs" Height="90px" TextMode="MultiLine" runat="server" Width="526px"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Button ID="btnSrc" OnClick="btnSrc_Click" runat="server" CssClass="button" Text="Processar SRC's" />
                                            </td>
                                        </tr>
                                    </table>

                                </ContentTemplate>
                            </ajax:TabPanel>

                            <ajax:TabPanel ID="TabCrt" HeaderText="Contribuição Período Ativo" runat="server" TabIndex="1">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>Informe a data da atualização:
                                             
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>
                                                <br></br>
                                                        <table>
                                                            <tr>

                                                                <td>Data da Atualização</td>

                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtDtAtu" onkeypress="mascara(this, data)" CssClass="date" MaxLength="10" runat="server"></asp:TextBox>
                                                                </td>

                                                            </tr>

                                                        </table>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnPrcCTr" OnClick="btnPrcCTr_Click" runat="server" CssClass="button" Text="Processar CTR" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajax:TabPanel>

                            <ajax:TabPanel ID="TabSpleito" HeaderText="Dados sem Pleito" runat="server" TabIndex="3">
                                <ContentTemplate>
                                    <asp:Button ID="btnSalvSPleito" OnClick="btnSalvSPleito_Click" runat="server" CssClass="button" Text="Salvar dados sem Pleito" />
                                     <asp:Label ID="lblSrbTexto" runat="server" CssClass="text" Text="SRB(Salário Real de Benefício):" Visible="false"></asp:Label> 
                                    <asp:Label ID="lblSrbValor" runat="server" CssClass="text" Text="Valor" Visible="false"></asp:Label>
                                </ContentTemplate>
                            </ajax:TabPanel>

                            <ajax:TabPanel ID="TabCpleito" HeaderText="Dados com Pleito" runat="server" TabIndex="2">
                                <ContentTemplate>
                                    <asp:Button ID="btnSalvCPleito" OnClick="btnSalvCPleito_Click" runat="server" CssClass="button" Text="Salvar dados com Pleito" />
                                </ContentTemplate>
                            </ajax:TabPanel>

                            <ajax:TabPanel ID="TbSrb" HeaderText="Salário Real de Benefício" runat="server" TabIndex="4">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Tipo de Benefício</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:DropDownList ID="drpTipoBeneficio" runat="server">
                                                                <asp:ListItem Text="--Selecione--" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="BSPS" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="PSAP" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="BD" Value="7"></asp:ListItem>
                                                                <asp:ListItem Text="BPD" Value="8"></asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>

                                                </table>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Deseja alterar a mensagem?</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:RadioButtonList AutoPostBack="true" ID="rdMensagem" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdMensagem_SelectedIndexChanged">
                                                                <asp:ListItem Text="Não" Value="0" Selected="True" />
                                                                <asp:ListItem Text="Sim" Value="1" />
                                                            </asp:RadioButtonList>

                                                        </td>
                                                    </tr>

                                                </table>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table runat="server" id="tbMensagem" visible="false">
                                                    <tr>

                                                        <td>Mensagem</td>

                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtMensagem" Width="800" runat="server"></asp:TextBox>
                                                        </td>

                                                    </tr>

                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnProcessarSrB" OnClick="btnProcessarSrB_Click" runat="server" CssClass="button" Text="Processar" />
                                            </td>
                                        </tr>
                                    </table>

                                </ContentTemplate>
                            </ajax:TabPanel>

                            <ajax:TabPanel ID="TabPCR" HeaderText="Parâmetro de Cálculo Retroativo " runat="server" TabIndex="5">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                <table>
                                                    <tr>

                                                        <td>Nome beneficiário</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:TextBox ID="txtBeneficiario" Width="327px" CausesValidation="false" runat="server"></asp:TextBox>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Tipo de Benefício</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:DropDownList AutoPostBack="true" ID="drpTpBenefPcr" runat="server" Width="162px" OnSelectedIndexChanged="drpTpBenefPcr_SelectedIndexChanged">
                                                                <asp:ListItem Text="--Selecione--" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="BSPS" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="PSAP" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="BD" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="BPD" Value="12"></asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Valor Inicial</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:TextBox ID="txtVlInicial" CausesValidation="false" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                            <asp:CheckBox ID="chkVlInicial" CausesValidation="false" runat="server" onclick=" txtTarget = $('#txtVlInicial'); (txtTarget.attr('disabled')==null) ? txtTarget.attr('disabled','disabled') : txtTarget.removeAttr('disabled');"/>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Reserva com Pleito</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:TextBox ID="txtVlComPleito" CausesValidation="false" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                            <asp:CheckBox ID="chkVlComPleito" CausesValidation="false" runat="server" onclick=" txtTarget = $('#txtVlComPleito'); (txtTarget.attr('disabled')==null) ? txtTarget.attr('disabled','disabled') : txtTarget.removeAttr('disabled');"/>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Reserva sem Pleito</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:TextBox ID="txtVlSemPleito" CausesValidation="false" runat="server" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                                                            <asp:CheckBox ID="chkVlSemPleito" CausesValidation="false" runat="server" onclick=" txtTarget = $('#txtVlSemPleito'); (txtTarget.attr('disabled')==null) ? txtTarget.attr('disabled','disabled') : txtTarget.removeAttr('disabled');"/>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Data Prescrição</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:TextBox ID="txtDtAjuizamento" onkeypress="mascara(this, data)" MaxLength="10" CssClass="date" CausesValidation="false" runat="server"></asp:TextBox>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Data Início Pagamento</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:TextBox ID="txtDtIniPagamento" onkeypress="mascara(this, data)" MaxLength="10" CssClass="date" CausesValidation="false" runat="server"></asp:TextBox>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Data Fim Pagamento</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:TextBox ID="txtDtFimPagamento" onkeypress="mascara(this, data)" MaxLength="10" CssClass="date" CausesValidation="false" runat="server"></asp:TextBox>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table id="tbConsidera" runat="server" visible="false">
                                                    <tr>

                                                        <td>Considera Adiamento de Reserva?</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:RadioButtonList ID="rdConsidera" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Não" Value="N" Selected="True" />
                                                                <asp:ListItem Text="Sim" Value="S" />
                                                            </asp:RadioButtonList>

                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="BtbProcessarPar" OnClick="BtbProcessarPar_Click" runat="server" CssClass="button" Text="Processar" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divParametro" runat="server" visible="false" class="tabelaPagina">
                                        <h2>Lista de Parâmetros</h2>
                                        <asp:GridView AllowPaging="true" AutoGenerateColumns="false" PageSize="10"
                                            EmptyDataText="A consulta não retornou dados" CssClass="Table" ClientIDMode="Static" ID="grdParametro" runat="server"
                                            OnRowCommand="grdParametro_RowCommand">
                                            <Columns>
                                                <asp:BoundField HeaderText="Benefício" DataField="dscr_bnf" />
                                                <asp:BoundField HeaderText="Beneficiário" DataField="beneficiario" />
                                                <asp:BoundField HeaderText="Valor Inicial" DataField="vlr_bnf_ini" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Reserva C/ Pleito" DataField="rsv_cplto" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Reserva S/ Pleito" DataField="rsv_splto" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Data Início" DataField="data_inic_pagto" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                                <asp:BoundField HeaderText="Data Fim" DataField="data_fim_pagto" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                                <asp:BoundField HeaderText="Data Ajuizamento" DataField="DATA_AJZTO" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/M/yyyy}" />
                                                <asp:BoundField HeaderText="Valor" DataField="bnf_ult_pagto" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Data/Horário da Execução" DataField="hdrdathor" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                                            Text="Excluir" CommandName="Deletar" CommandArgument='<%# Eval("num_matr_partf")+","+Eval("num_sqncl_prc")+","+Eval("tip_bnf") %>' OnClientClick="return confirm('Atenção!! \n\nTem certeza que deseja excluir este parâmetro?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </ajax:TabPanel>

                            <ajax:TabPanel ID="TabCalcRetro" HeaderText="Cálculo Retroativo" runat="server" TabIndex="5">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>

                                                        <td>Tipo de Benefício</td>

                                                    </tr>
                                                    <tr>

                                                        <td>
                                                            <asp:HiddenField ID="hdTipoBeneficio" Value="0" runat="server" />

                                                            <asp:DropDownList ID="drpCalcRetro" runat="server">
                                                                <asp:ListItem Text="--Selecione--" Value="0" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="BSPS" Value="13,1"></asp:ListItem>
                                                                <asp:ListItem Text="PSAP" Value="14,2"></asp:ListItem>
                                                                <asp:ListItem Text="BD" Value="15,3"></asp:ListItem>
                                                                <asp:ListItem Text="BPD" Value="16,4"></asp:ListItem>
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>

                                                     <tr></tr>
<%--                                                    <tr></tr>
                                                    <tr>
                                                        <td>Calcular Provisionamento de IR?</td>
                                                        <td>&nbsp</td>
                                                        <td>
                                                            <asp:CheckBox ID="ckRetroativo" runat="server" OnCheckedChanged="ckRetroativo_CheckedChanged" Text="Sim" AutoPostBack="true" />
                                                        </td>
                                                    </tr>--%>

                                                </table>

<%--                                                <asp:Table ID="tbAnoRef" runat="server" Visible="false" Enabled="false">
                                                    <asp:TableRow runat="server" ID="trAnoRef">
                                                        <asp:TableCell ID="TableCell1" runat="server">
                                                            Selecione o ano de referencia:
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell2" runat="server">&nbsp</asp:TableCell>

                                                        <asp:TableCell ID="TableCell3" runat="server">&nbsp</asp:TableCell>
                                                        <asp:TableCell ID="TableCell4" runat="server"></asp:TableCell>
                                                        <asp:TableCell ID="TableCell5" runat="server"></asp:TableCell>
                                                        <asp:TableCell ID="TableCell6" runat="server">
                                                            <asp:TextBox ID="txtAnoRef" runat="server" MaxLength="4" onkeypress="mascara(this, soNumeros)" Width="35px" />
                                                            <asp:RequiredFieldValidator runat="server" ID="reqAnoRef" ControlToValidate="txtAnoRef" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="grpValidaReferencia" />
                                                            <asp:RangeValidator
                                                                runat="server"
                                                                ID="rangAnoRef"
                                                                Type="Integer"
                                                                ControlToValidate="txtAnoRef"
                                                                MaximumValue="2050"
                                                                MinimumValue="1900"
                                                                ErrorMessage="Ano inválido"
                                                                ForeColor="Red"
                                                                Display="Dynamic" 
                                                                ValidationGroup="grpValidaReferencia"/>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>--%>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnCalcRetro" OnClick="btnCalcRetro_Click" runat="server" CssClass="button" Text="Processar" />
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajax:TabPanel>

                            <ajax:TabPanel ID="TabRel" HeaderText="Relatórios" runat="server" TabIndex="5">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <h4>Selecione o relatório:</h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <asp:CheckBoxList ID="ckRelarorios1" runat="server">
                                                </asp:CheckBoxList>
                                                <br />

                                            </td>
                                        </tr>

                                    </table>
                                    <div id="divRels" runat="server" visible="false">
                                        <table>
                                            <tr>
                                                <td>
                                                    <h4>Para os relatórios abaixo, selecione um tipo de Benefício.</h4>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>
                                                    <table>
                                                        <tr>

                                                            <td>Tipo de Benefício</td>

                                                        </tr>
                                                        <tr>

                                                            <td>

                                                                <asp:DropDownList OnSelectedIndexChanged="drpBenefRel_SelectedIndexChanged" AutoPostBack="true" ID="drpBenefRel" runat="server">
                                                                    <asp:ListItem Text="--Selecione--" Value="0" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="BSPS" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="PSAP" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="BD" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="BPD" Value="4"></asp:ListItem>
                                                                </asp:DropDownList>

                                                            </td>
                                                        </tr>

                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <br />
                                                    <asp:CheckBoxList ID="ckRelarorios2" runat="server">
                                                    </asp:CheckBoxList>
                                                    <br />

                                                </td>
                                            </tr>

                                        </table>
                                    </div>
                                    <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" CssClass="button" Text="Buscar os Relatórios Selecionados" />
                                </ContentTemplate>
                            </ajax:TabPanel>

                        </ajax:TabContainer>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer$TabSrc$btnSrc" />
                    <asp:PostBackTrigger ControlID="TabContainer$TabCrt$btnPrcCTr" />
                    <asp:PostBackTrigger ControlID="TabContainer$TbSrb$btnProcessarSrB" />
                    <asp:PostBackTrigger ControlID="TabContainer$TabCalcRetro$btnCalcRetro" />
                    <asp:PostBackTrigger ControlID="TabContainer$TabRel$btnBuscar" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
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
