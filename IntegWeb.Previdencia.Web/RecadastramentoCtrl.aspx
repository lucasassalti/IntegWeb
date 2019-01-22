<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RecadastramentoCtrl.aspx.cs" Inherits="IntegWeb.Previdencia.Web.RecadastramentoCtrl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<%--    <script type="text/javascript">
        function TextChanged() {
            if ($('#ContentPlaceHolder1_txtEmpresa').val() != '' &&
               $('#ContentPlaceHolder1_txtMatricula').val() != '' &&
               $('#ContentPlaceHolder1_txtRepresentante').val() != '')
        }
    </script>--%>

    <div class="full_w">

        <div class="h_title">
        </div>

        <h1>Controle de Recadastramento</h1>
        <asp:UpdatePanel runat="server" ID="upUpdatepanel">
            <ContentTemplate>
                <asp:Panel class="tabelaPagina" ID="pnlPesquisa" runat="server">
                    <table>
                        <tr>
                            <td>Dt. Base</td>
                            <td>
                                <asp:DropDownList ID="ddlPesqDtBase" runat="server" AutoPostBack="True" >
                                </asp:DropDownList>
                            </td>
                            <td>Situação</td>
                            <td colspan="4">
                                <asp:DropDownList ID="ddlPesqSituacao" runat="server" AutoPostBack="True" >
                                    <asp:ListItem Text="<TODAS>" Value="0" Selected="True" />
                                    <asp:ListItem Text="Pendente" Value="-1" />
                                    <asp:ListItem Text="Banco - Arquivo" Value="1" />
                                    <asp:ListItem Text="Pessoalmente" Value="2" />
                                    <asp:ListItem Text="Formulário" Value="3" />
                                    <asp:ListItem Text="Banco - Comprovante" Value="4" />
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Empresa</td>
                            <td>
                                <asp:TextBox ID="txtPesqEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                            <td>Matrícula</td>
                            <td>
                                <asp:TextBox ID="txtPesqMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                            <td>Representante</td>
                            <td>
                                <asp:TextBox ID="txtPesqRepresentante" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Participante</td>
                            <td colspan="6">
                                <asp:TextBox ID="txtPesqParticipante" Width="280px" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Período de recadastro</td>
                            <td>
                                <asp:TextBox ID="txtPesqDtIni" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                                </td><td>
                            até&nbsp&nbsp&nbsp</td>
                                <td colspan="4">
                            <asp:TextBox ID="txtPesqDtFim" runat="server" CssClass="date" Style="width: 100px;" onkeypress="mascara(this, data)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar   " OnClick="btnPesquisar_Click" />
                                <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                <asp:Button ID="btnNovo" runat="server" CssClass="button" Text="Novo" OnClick="btnNovo_Click" />
                                <asp:Label ID="pnlPesquisa_Mensagem" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlLista" class="tabelaPagina" Visible="true">
                    <asp:ObjectDataSource runat="server" ID="odsControleRecad"
                        TypeName="IntegWeb.Previdencia.Aplicacao.BLL.RecadastramentoBLL"
                        SelectMethod="GetData"
                        SelectCountMethod="GetDataCount"
                        EnablePaging="true"
                        SortParameterName="sortParameter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtPesqEmpresa" Name="pEmpresa" PropertyName="Text"
                                Type="Int32" ConvertEmptyStringToNull="true" />
                            <asp:ControlParameter ControlID="txtPesqMatricula" Name="pMatricula" PropertyName="Text"
                                Type="Int32" ConvertEmptyStringToNull="true" />
                            <asp:ControlParameter ControlID="txtPesqRepresentante" Name="pRepresentante" PropertyName="Text"
                                Type="Int32" ConvertEmptyStringToNull="true" />
                            <asp:ControlParameter ControlID="txtPesqParticipante" Name="pNome" PropertyName="Text"
                                Type="String" ConvertEmptyStringToNull="true" />
                            <asp:ControlParameter ControlID="ddlPesqSituacao" Name="pSituacao" PropertyName="Text"
                                Type="Int32" ConvertEmptyStringToNull="true" />
                            <asp:ControlParameter ControlID="txtPesqDtIni" Name="pDtRecad_ini" PropertyName="Text"
                                Type="DateTime" ConvertEmptyStringToNull="true" />
                            <asp:ControlParameter ControlID="txtPesqDtFim" Name="pDtRecad_final" PropertyName="Text"
                                Type="DateTime" ConvertEmptyStringToNull="true" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

                    <asp:GridView ID="grdControleRecad" runat="server"
                        DataKeyNames="DAT_REF_RECAD, NUM_CONTRATO, COD_EMPRS, NUM_RGTRO_EMPRG, NUM_IDNTF_RPTANT, NUM_MATR_PARTF, DTH_INCLUSAO"
                        OnRowEditing="grdControleRecad_RowEditing"
                        OnRowDeleting="grdControleRecad_RowDeleting"
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
                        DataSourceID="odsControleRecad">
                        <Columns>
                            <asp:BoundField HeaderText="Empresa" DataField="COD_EMPRS" SortExpression="COD_EMPRS" />
                            <asp:BoundField HeaderText="Matrícula" DataField="NUM_RGTRO_EMPRG" SortExpression="NUM_RGTRO_EMPRG" />
                            <asp:BoundField HeaderText="Representante" DataField="NUM_IDNTF_RPTANT" SortExpression="NUM_IDNTF_RPTANT" />
                            <asp:BoundField HeaderText="Participante" DataField="NOME" SortExpression="NOME" />
                            <asp:BoundField HeaderText="Tipo" DataField="PRE_TBL_RECADASTRAMENTO_TIPO.DSC_RECADASTRAMENTO" SortExpression="TIP_RECADASTRAMENTO" />
                            <asp:BoundField HeaderText="Dt. Recadastramento" DataField="DAT_RECADASTRAMENTO" SortExpression="DAT_RECADASTRAMENTO" DataFormatString="{0:dd/MM/yyyy}" />
                            <%--<asp:BoundField HeaderText="Assunto (Pleito)" DataField="PRE_TBL_ACAO_VR_TIPLTO.DESC_TIPLTO" SortExpression="COD_TIPLTO" />--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkAlterar" runat="server" CssClass="button"
                                        Text="Detalhes" CommandName="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CssClass="button" Text="Excluir" CommandName="Delete" OnClientClick="return confirm('Deseja realmente excluir?');"></asp:LinkButton>
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
                                <asp:Label ID="pnlDetalhe_Mensagem" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <table style="width: 100%; max-width: 80%;">

                        <tr>
                            <td style="font-weight: bolder">Dados da base</td>
                        </tr>
                        <tr class="cab_td">
                            <td>Dt. Base Geração</td>
                            <td>Núm. Contrato</td>
                            <td>Data Inclusão</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtDataBaseRef" runat="server" Enabled="false" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtContrato" runat="server" Enabled="false" onkeypress="mascara(this, soNumeros)" MaxLength="5"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtDtInclusao" runat="server" Enabled="false" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bolder">Identificação do Participante</td>
                        </tr>
                        <tr class="cab_td">
                            <td>Empresa</td>
                            <td>Matrícula</td>
                            <td>Representante</td>
                        </tr>
                        <tr class="cab_td">
                            <td>
                                <asp:TextBox ID="txtEmpresa" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="3" Enabled="false" OnTextChanged="txtMatricula_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                            <td>
                                <asp:HiddenField ID="hfNUM_MATR_PARTF" runat="server" />
                                <asp:TextBox ID="txtMatricula" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="6" Enabled="false" OnTextChanged="txtMatricula_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                            <td>
                                <asp:HiddenField ID="hfNUM_PRCINS_ASINSS" runat="server" />
                                <asp:TextBox ID="txtRepresentante" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="6" Enabled="false" OnTextChanged="txtMatricula_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="cab_td">
                            <td colspan="3">Participante</td>
                        </tr>
                        <tr class="cab_td">
                            <td colspan="1">
                                <asp:TextBox ID="txtNome" runat="server"  Width="100%" MaxLength="255"></asp:TextBox>
                            </td>
                            <td colspan="2">&nbsp</td>
                        </tr>
                        <tr class="cab_td">
                            <td colspan="1">Data Nascimento</td>
                            <td colspan="1">Data Falecimento</td>
                            <td colspan="1">DIB</td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                <asp:TextBox ID="txtDtNascimento" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                            </td>

                            <td colspan="1">
                                <asp:TextBox ID="txtDtFalecimento" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                            </td>

                            <td colspan="1">
                                <asp:TextBox ID="txtDIB" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox></td>
                            </td>
                        <tr>
                        <tr class="cab_td">
                            <td colspan="1">Dt. Recadastro</td>
                            <td colspan="2">Tipo Atendimento</td>
                        </tr>
                        <tr>
                            <td colspan="1">
                                <asp:TextBox ID="txtDtRecadastrado" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="ddlTipoAtendimento" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr class="cab_td">
                                <td colspan="1">Novo Prazo</td>
                                <td colspan="2">Observação</td>
                        </tr>

                        <tr>
                            <td colspan="1">
                                <asp:TextBox ID="txtNovoPrazo" runat="server" CssClass="date" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtObs" runat="server" Height="56px" Width="100%" MaxLength="2000"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtMatricula" EventName="TextChanged" />
                <asp:PostBackTrigger ControlID="btnSalvar" />
            </Triggers>--%>
        </asp:UpdatePanel>

    </div>

</asp:Content>
