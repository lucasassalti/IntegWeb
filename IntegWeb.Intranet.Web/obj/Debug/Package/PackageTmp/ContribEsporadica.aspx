<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ContribEsporadica.aspx.cs" Inherits="IntegWeb.Intranet.Web.ContribEsporadica" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="full_w">

        <div class="h_title">
        </div>

        <h1>Depósitos Judiciais</h1>
        <asp:UpdatePanel runat="server" ID="upUpdatepanel">
            <ContentTemplate>
                <asp:Panel class="tabelaPagina" ID="pnlPesquisa" runat="server">
                    <table>
                        <tr>
                            <td>Empresa</td>
                            <td>
                                <asp:TextBox ID="txtPesqEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                            </td>
                            <td>Matrícula</td>
                            <td>
                                <asp:TextBox ID="txtPesqMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>CPF</td>
                            <td>
                                <asp:TextBox ID="txtPesqCpf" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox>
                            </td>
                            <td>Nome</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtPesNome" Width="100%" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>Dt. Vencimento</td>
                            <td>
                                <asp:TextBox ID="txtDtVencIni" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ControlToValidate="txtDtIni" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator>
                            </td>
                            <td>até&nbsp</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtDtVencFim" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" runat="server" ControlToValidate="txtDtFim" ErrorMessage="Informe uma data válida (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" Text="*"></asp:RegularExpressionValidator></td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar   " OnClick="btnPesquisar_Click" />
                                <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Novo" OnClick="btnNovo_Click" />
                                <asp:Label id="pnlPesquisa_Mensagem" runat="server" visible="False"></asp:Label> 
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlLista" class="tabelaPagina" Visible="true">
                    <asp:ObjectDataSource runat="server" ID="odsDepositoJudicial"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial.DepositoJudicialBLL"
                        SelectMethod="GetData"
                        SelectCountMethod="GetDataCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpPesquisa" Name="filType" PropertyName="SelectedValue"
                                Type="Int32" />
                            <asp:ControlParameter ControlID="txtPesquisa" Name="filValue" PropertyName="Text"
                                Type="String" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqEmpresa" Name="pEmpresa" PropertyName="Text"
                                Type="Int32" ConvertEmptyStringToNull="true"/>
                            <asp:ControlParameter ControlID="txtPesqMatricula" Name="pMatricula" PropertyName="Text"
                                Type="Int32" ConvertEmptyStringToNull="true"/>
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdLista" runat="server"
                        DataKeyNames="COD_DEPOSITO_JUDIC"
                        OnRowEditing="grdLista_RowEditing" 
                        OnRowDeleting="grdLista_RowDeleting"
                        OnRowCreated="GridView_RowCreated"     
                        OnRowDeleted="GridView_RowDeleted"                   
                        OnRowCancelingEdit="GridView_RowCancelingEdit"
                        AllowPaging="true"
                        AllowSorting="true"
                        AutoGenerateColumns="False"
                        EmptyDataText="A consulta não retornou registros"
                        CssClass="Table"
                        ClientIDMode="Static"
                        PageSize="8"
                        DataSourceID="odsDepositoJudicial">
                        <Columns>
                            <asp:BoundField HeaderText="Empresa" DataField="COD_EMPRS" SortExpression="COD_EMPRS" />
                            <asp:BoundField HeaderText="Matrícula" DataField="NUM_MATR_PARTF" SortExpression="NUM_MATR_PARTF" />
                            <asp:BoundField HeaderText="Nome Empregado" DataField="NOM_EMPRG" SortExpression="NOM_EMPRG" />
                            <asp:BoundField HeaderText="Nº Processo" DataField="NRO_PROCESSO" SortExpression="NRO_PROCESSO" />
                            <asp:BoundField HeaderText="Nº Pasta" DataField="NRO_PASTA" SortExpression="NRO_PASTA" />
                            <asp:BoundField HeaderText="Assunto (Pleito)" DataField="PRE_TBL_ACAO_VR_TIPLTO.DESC_TIPLTO" SortExpression="COD_TIPLTO" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                        Text="Detalhes" CommandName="Edit" CommandArgument='<%# Eval("COD_DEPOSITO_JUDIC") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                        Text="Excluir" CommandName="Delete" CommandArgument='<%# Eval("COD_DEPOSITO_JUDIC") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlDetalhe" class="tabelaPagina" Visible="false">

                    <table>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnSalvar" OnClick="btnSalvar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Salvar" />
                                <asp:Button ID="btnVoltar" OnClick="btnVoltar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Voltar" />
                            </td>
                            <td>
                                <asp:Label id="pnlDetalhe_Mensagem" runat="server" visible="False"></asp:Label> 
                            </td>
                        </tr>
                    </table>                    

                    <table style="width: 100%">
                        <tr>
                            <td style="font-weight: bolder" colspan="6">Identificação do Participante</td>
                        </tr>
                        <tr class="cab_td">
                            <td>Empresa</td>
                            <td>Matrícula</td>
                            <td>CPF</td>
                            <td>Plano</td>
                            <td>&nbsp;</td>
                            <td>Cadastro</td>
                        </tr>
                        <tr class="cab_td">
                            <td>                                
                                <asp:TextBox ID="txtEmpresa" runat="server" Enabled="false" OnTextChanged="txtMatricula_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                            <td>
                                <asp:HiddenField ID="hfNUM_MATR_PARTF" runat="server" />
                                <asp:TextBox ID="txtMatricula" runat="server" Enabled="false" OnTextChanged="txtMatricula_TextChanged" AutoPostBack="true" ></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtCPF" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPlano" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;<td>
                                    <asp:TextBox ID="txtDtCadastro" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="cab_td">
                            <td colspan="3">Participante</td>
                            <td>Adesão</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:TextBox ID="txtNome" runat="server" Enabled="false" Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDtAdesao" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            </td>
                        </tr>
                        <tr class="cab_td">
                            <td>Admissão</td>
                            <td>Demissão</td>
                            <td>Nascimento</td>
                            <td>Perfil</td>
                            <td>&nbsp;</td>
                            <td>Pasta</td>
                        </tr>
                        <tr class="cab_td">
                            <td>
                                <asp:TextBox ID="txtDtAdmissao" runat="server" Enabled="false"></asp:TextBox>
                            </td>

                            <td>
                                <asp:TextBox ID="txtDtDemissao" runat="server" Enabled="false"></asp:TextBox>
                            </td>

                            <td>
                                <asp:TextBox ID="txtDtNascimento" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPerfil" runat="server" Enabled="false"></asp:TextBox></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtPasta" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td style="font-weight: bolder" colspan="7">Dados Processo/Vara</td>
                        </tr>

                        <tr class="cab_td">
                            <td colspan="2">Número do Processo</td>
                            <td>Pólo-Ação Judicial</td>
                            <td>Vara</td>
                            <td colspan="3">Assunto (Pleito)</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:HiddenField ID="hfPK" runat="server" />
                                <asp:TextBox ID="txtNumProcesso" runat="server" Width="300px"></asp:TextBox></td>
                            <td>
                                <asp:DropDownList ID="ddlPoloAcaoJudicial" runat="server" Width="80px">
                                    <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Não" Value="N" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVara" runat="server"></asp:TextBox></td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlAssunto" runat="server">
                                </asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td style="font-weight: bolder" colspan="6">Depósitos:</td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Panel runat="server" ID="pnlPgtoDetalhe" class="tabelaPagina" Visible="false">
                                    <table style="width: 98%">
                                        <tr>
                                            <td style="font-weight: bolder" colspan="6">
                                                <asp:Button ID="btnSalvarPgto" OnClick="btnSalvar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Salvar" CommandName="Salvar" />
                                                <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" CausesValidation="false" CssClass="button" runat="server" Text="Cancelar" />
                                            </td>
                                        </tr>
                                        <tr class="cab_td">
                                            <td>Tipo Cadastro</td>
                                            <td>Número PP</td>
                                            <td>Tipo Solicitação</td>
                                            <td>Dt. Pagamento</td>
                                            <td>Forma Pagamento</td>
                                            <td>&nbsp</td>
                                        </tr>
                                        <tr class="cab_td">
                                            <td>
                                                <asp:HiddenField ID="hfCOD_DEPOSITO_JUDIC_PGTO" runat="server" />
                                                <asp:DropDownList ID="ddlTipoCadastro" runat="server">
                                                    <asp:ListItem Text="---Selecione---"  Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="PP - Previsão de Pagamento"  Value="PP"></asp:ListItem>
                                                    <asp:ListItem Text="PR - Previsão de Recebimento"  Value="PR"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>                                                
                                                <asp:TextBox ID="txtNumPP" runat="server"></asp:TextBox></td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoSolicitacao" runat="server">
                                                    <asp:ListItem Text="---Selecione---"  Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Jurídico/Fundação CESP"  Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Jurídico/MML"  Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Solicitação Patrocinador"  Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDtPagamento" class="date" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlFormaPagamento" runat="server">
                                                    <asp:ListItem Text="---Selecione---" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Cheque Administrativo" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Ficha de Compensação" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Guia Judicial" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Crédito em Conta" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                <td>
                                                    &nbsp;</td>
                                        </tr>
                                        <tr class="cab_td">
                                            <td colspan="2">Credor</td>
                                            <td>Valor BSPS</td>
                                            <td>Valor BD</td>
                                            <td>Valor CV</td>
                                            <td>Total</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtNomCredor" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVlrBSPS" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVlrBD" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtVlrCV" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblTotal" runat="server" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">Descrição</td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" Width="98%" Height="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="cab_td">
                                            <td colspan="2">Núm. Carta</td>
                                            <td>Dt Envio</td>
                                            <td>Dt Envio E-Mail</td>
                                            <td>&nbsp</td>
                                            <td>&nbsp</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtNumCarta" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDtEnvioCarta" CssClass="date" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDtEnvioEMail" CssClass="date" runat="server"></asp:TextBox>

                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlPgtoLista" class="tabelaPagina" Visible="true">
                                    <asp:ObjectDataSource runat="server" ID="odsDepositoJudicialPgto"
                                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.Calculo_Acao_Judicial.DepositoJudicialBLL"
                                        SelectMethod="GetDataPgto"
                                        SelectCountMethod="GetDataCountPgto"
                                        EnablePaging="true"
                                        SortParameterName="sortParameter">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="hfCOD_DEPOSITO_JUDIC" Name="pCOD_DEPOSITO_JUDIC" PropertyName="Value"
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>

                                    <asp:GridView ID="grdDepPgto" runat="server"
                                        DataKeyNames="COD_DEPOSITO_JUDIC_PGTO"
                                        OnRowEditing="grdDepPgto_RowEditing"                         
                                        OnRowDeleting="grdDepPgto_RowDeleting"
                                        OnRowCreated="GridView_RowCreated"     
                                        OnRowDeleted="GridView_RowDeleted"  
                                        AllowPaging="true"
                                        AllowSorting="true"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="A consulta não retornou registros"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        PageSize="8"
                                        DataSourceID="odsDepositoJudicialPgto">
                                        <Columns>
                                            <asp:BoundField HeaderText="Tipo Cadastro" DataField="TIP_CADASTRO" SortExpression="TIP_CADASTRO" />
                                            <asp:BoundField HeaderText="Número PP" DataField="NUM_PP" SortExpression="NUM_PP" />
                                            <asp:BoundField HeaderText="Dt. Pagamento" DataField="DTH_PAGAMENTO" SortExpression="DTH_PAGAMENTO" />
                                            <asp:BoundField HeaderText="Credor" DataField="NOM_CREDOR" SortExpression="NOM_CREDOR" />
                                            <asp:BoundField HeaderText="Total" DataField="VLR_BSPS" SortExpression="VLR_BSPS" />
                                            <asp:BoundField HeaderText="Dt. Envio Carta" DataField="DAT_CARTA_ENVIO" SortExpression="DAT_CARTA_ENVIO" />
                                            <asp:BoundField HeaderText="Dt. Envio E-Mail" DataField="DAT_EMAIL_ENVIO" SortExpression="DAT_EMAIL_ENVIO" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                                        Text="Detalhes" CommandName="Edit" CommandArgument='<%# Eval("COD_DEPOSITO_JUDIC_PGTO") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkExcluir" runat="server" CssClass="button"
                                                        Text="Excluir" CommandName="Delete" CommandArgument='<%# Eval("COD_DEPOSITO_JUDIC_PGTO") %>' OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
<%--            <Triggers>
                <asp:PostBackTrigger ControlID="btnSalvar" />
            </Triggers>--%>
        </asp:UpdatePanel>

    </div>

</asp:Content>
