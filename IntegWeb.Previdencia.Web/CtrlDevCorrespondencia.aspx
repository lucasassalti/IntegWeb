<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="CtrlDevCorrespondencia.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CtrlDevCorrespondencia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }


        function Carrega_Dados_Partic() {

            if ($('#ContentPlaceHolder1_TabContainer_tbInsercaoCorrespondencia_txtCodEmpresaInsert').val() != ""
                && $('#ContentPlaceHolder1_TabContainer_tbInsercaoCorrespondencia_txtMatriculaInsert').val() != "") {
                $('#ContentPlaceHolder1_TabContainer_tbInsercaoCorrespondencia_btnDadosPartic').click();
            }
        }

        function Carrega_Dados_Cred() {

            if ($('#ContentPlaceHolder1_TabContainer_tbInsercaoCorrespondencia_txtCodContratoInsert').val() != "") {
                $('#ContentPlaceHolder1_TabContainer_tbInsercaoCorrespondencia_btnDadosCredenciado').click();
            }
        }

    </script>

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="full_w">
                <h1>Controle de Devoluções de Correspondêcias</h1>
                <div class="tabelaPagina">
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">
                        <ajax:TabPanel ID="tbInsercaoCorrespondencia" HeaderText="Inserção das Correspondência" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <asp:Panel ID="pnlHabilitarInsert" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnInserirCredenciado" runat="server" CssClass="button" Text="Credenciado" OnClick="btnInserirCredenciado_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnInserirParticipante" runat="server" CssClass="button" Text="Participante" OnClick="btnInserirParticipante_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlCtrlDevCorrespInsert" Visible="false" runat="server">
                                    <table>
                                        <asp:Button ID="btnDadosPartic" OnClick="btnDadosPartic_Click" CssClass="button" runat="server" Text="Dados Partic" Style="display: none;" />
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCodEmpresaInsert" runat="server" Text="Cod.Empresa:" />
                                                <asp:TextBox ID="txtCodEmpresaInsert" runat="server" Width="80px"
                                                    onblur="Carrega_Dados_Partic();"
                                                    MaxLength="3" onkeypress="mascara(this, soNumeros)" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMatriculaInsert" runat="server" Text="Matricula:" />
                                                <asp:TextBox ID="txtMatriculaInsert"
                                                    onblur="Carrega_Dados_Partic();"
                                                    runat="server" Width="80px" MaxLength="10" onkeypress="mascara(this, soNumeros)" />
                                                <asp:Label ID="lblTitular" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>

                                            <td>
                                                <asp:Label ID="lblCodRepresInsert" runat="server" Text="Cod.Representante:" />
                                                <asp:DropDownList ID="ddlCodRepresInsert" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <div id="divDropdowns" runat="server" visible="false">
                                            <tr>
                                                <td>Tipo do documento:<asp:DropDownList ID="ddlTipoDocumentoInsert" runat="server"></asp:DropDownList>
                                                </td>
                                                <td>Data de Postagem:<asp:TextBox ID="txtDataPostagemInsert" runat="server" CssClass="date"
                                                    Style="width: 100px;" onkeypress="mascara(this, data)" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Motivo da Devolução:<asp:DropDownList ID="ddlMotivoDevolucaoInsert" runat="server" />
                                                </td>
                                                <td>Data da Devolução:<asp:TextBox ID="txtDataDevolucaoInsert" runat="server" CssClass="date"
                                                    Style="width: 100px;" onkeypress="mascara(this, data)" />
                                                </td>
                                            </tr>
                                        </div>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlInsertCredenciado" Visible="false" runat="server">
                                    <asp:Button ID="btnDadosCredenciado" OnClick="btnDadosCredenciado_Click" CssClass="button" runat="server" Text="Credenciado" Style="display: none;" />
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCodContratoInsert" runat="server" Text="Cod.Contrato:" />
                                                <asp:TextBox ID="txtCodContratoInsert" MaxLength="8" runat="server" Width="80px"
                                                    onblur="Carrega_Dados_Cred();"
                                                    onkeypress="mascara(this, soNumeros)" />
                                                <asp:Label ID="lblCredenciado" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlBotoesInsert" Visible="false" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAddCtrlDevCorresp" runat="server" CssClass="button" Visible="false" Text="Inserir" OnClick="btnAddCtrlDevCorresp_Click" />
                                                <asp:Button ID="btnAddCredCorresp" runat="server" CssClass="button" Visible="false" Text="Inserir" OnClick="btnAddCredCorresp_Click" />
                                                <asp:Button ID="btnLimparDados" runat="server" CssClass="button" Text="Limpar Dados" OnClick="btnLimparDados_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlGrdCabecalhoCorresp" runat="server" Visible="false">
                                    <asp:GridView ID="grdControleDevCorresp"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        Font-Size="10px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Etiqueta">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" Checked="true" runat="server" Text="" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" Checked="true" runat="server" Text="" class="span_checkbox" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Contrato">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumContrato" runat="server" Text='<%# Bind("NUM_CONTRATO") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNumContrato" runat="server" Text='<%# Bind("NUM_CONTRATO") %>' />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Empresa">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Matricula">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMatricula" runat="server" Text='<%# Bind("MATRICULA") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMatricula" runat="server" Text='<%# Bind("MATRICULA") %>' />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cod.Repres">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumRepress" runat="server" Text='<%# Bind("NUM_REP") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNumRepress" runat="server" Text='<%# Bind("NUM_REP") %>' />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nome">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNome" runat="server" Text='<%# Bind("NOME") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Endereço">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndereco" runat="server" Text='<%# Bind("ENDERECO") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Compl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComplente" runat="server" Text='<%# Bind("COMPLEMENTO") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nº">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("NUMERO") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bairro">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBairro" runat="server" Text='<%# Bind("BAIRRO") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Municipio">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMunicipio" runat="server" Text='<%# Bind("MUNICIPIO") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="UF">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUF" runat="server" Text='<%# Bind("UF") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CEP">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCEP" runat="server" Text='<%# Bind("CEP") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>

                                <asp:Panel ID="pnlGrdDetalhe" runat="server" Visible="false">
                                    <asp:GridView ID="grdEditAjusteCorresp"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        CssClass="Table"
                                        OnRowCommand="grdEditAjusteCorresp_RowCommand"
                                        OnRowDataBound="grdEditAjusteCorresp_RowDataBound"
                                        OnRowEditing="grdEditAjusteCorresp_RowEditing"
                                        OnRowUpdating="grdEditAjusteCorresp_RowUpdating"
                                        OnRowCancelingEdit="grdEditAjusteCorresp_RowCancelingEdit"
                                        ClientIDMode="Static"
                                        Font-Size="10px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tipo do Documento" HeaderStyle-Font-Size="10px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server"
                                                        Font-Size="10px" />
                                                    <asp:Label ID="lblTipoDocumentoDesc" runat="server" Visible="false" Text='<%# Bind("ID_TIPODOCUMENTO") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoDocumentoUpd" runat="server" Font-Size="10px" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data da Postagem">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataPostagem" runat="server" Text='<%# Bind("DATAPOSTAGEM","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDataPostagem" runat="server" CssClass="date"
                                                        Style="width: 100px;" Text='<%# Bind("DATAPOSTAGEM","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data da Devolução">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataDevolucao" runat="server" Text='<%# Bind("DATADEVOLUCAO","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDataDevolucao" runat="server" CssClass="date"
                                                        Style="width: 100px;" Text='<%# Bind("DATADEVOLUCAO","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Motivo Devolução" HeaderStyle-Font-Size="10px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoMotiDev" runat="server" Font-Size="10px" />
                                                    <asp:Label ID="lblTipoMotiDevDesc" runat="server" Visible="false" Text='<%# Bind("ID_TIPOMOTIVODEVOLUCAO") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoMotiDevUpd" Font-Size="10px" runat="server" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reenvio">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataReenvio" runat="server" Text='<%# Bind("Reenvio","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtReenvio" runat="server" CssClass="date"
                                                        Style="width: 100px;" Text='<%# Bind("Reenvio","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tempo Prazo" HeaderStyle-Font-Size="10px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTempoPrazo" runat="server" Text='<%# Bind("Tempo_Prazo") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTempoPrazoInc" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditarControle" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass="button" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>

                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="tbConsultaAjuste" HeaderText="Consulta e Ajuste" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <h2>Consulta e Ajuste das Correspondências</h2>
                                <asp:Panel ID="pnlCtrlDevCorrespConsulta" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnConsultar" runat="server" CssClass="button" Text="Consultar" OnClick="btnConsultar_Click" />
                                            </td>
                                        </tr>

                                        <asp:Panel ID="pnlCtrlDevCorrespPesquisar" runat="server" Visible="false">
                                            <tr>
                                                <td>Pesquisar Por Tipo Documento:
                                                <asp:DropDownList ID="ddlFiltroTipoDocumento" runat="server" />
                                                <asp:Button ID="btnFiltroTipoDocumento" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnFiltroTipoDocumento_Click" />
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                </asp:Panel>

                                <ajax:ModalPopupExtender
                                    ID="ModalPopupExtender1"
                                    runat="server"
                                    DropShadow="true"
                                    PopupControlID="panelPopup"
                                    TargetControlID="lnkFake"
                                    BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>

                                <asp:Panel ID="panelPopup" runat="server" Style="display: none; background-color: white; border: 1px solid black">
                                    <center><h3>Consulta Dados</h3></center>

                                    <asp:Panel ID="pnlPatrocinadorEnd" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>Empresa:
                                                    <asp:Label ID="lblEmpresaPatrociEnd" runat="server" /></td>
                                                <td>Matricula:
                                                    <asp:Label ID="lblMatriculaPatrociEnd" runat="server" /></td>
                                                <td>Repress:
                                                    <asp:Label ID="lblRepressPatrociEnd" runat="server" /></td>
                                                <td>Nome:
                                                    <asp:Label ID="lblNomePatrociEnd" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlCredenciadoEnd" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td>Contrato:
                                                    <asp:Label ID="lblContratoCredenciadoEnd" runat="server" /></td>
                                                <td>Nome:
                                                    <asp:Label ID="lblNomeCredenciadoEnd" runat="server" /></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <br />
                                    <table>
                                        <tr>
                                            <td>Endereço:<asp:Label ID="lblPopEndereco" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>Complemento:<asp:Label ID="lblPopComplemento" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>Nº:<asp:Label ID="lblPopNumero" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>CEP:<asp:Label ID="lblPopCep" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>Bairro:<asp:Label ID="lblPopBairro" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>Municipio:<asp:Label ID="lblPopMunicipio" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>UF:<asp:Label ID="lblPopUF" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnCancelarPopUp" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarPopUp_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlGrdAlteracoesCorresp" runat="server" Visible="false">
                                    <asp:GridView ID="grdAlteracoesCorresp"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        CssClass="Table"
                                        OnRowDataBound="grdAlteracoesCorresp_RowDataBound"
                                        OnRowCommand="grdAlteracoesCorresp_RowCommand"
                                        OnRowEditing="grdAlteracoesCorresp_RowEditing"
                                        OnRowUpdating="grdAlteracoesCorresp_RowUpdating"
                                        OnRowCancelingEdit="grdAlteracoesCorresp_RowCancelingEdit"
                                        ClientIDMode="Static"
                                        Font-Size="10px">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Endereço" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndereco" runat="server" Text='<%# Bind("ENDERECO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Compl." Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComplente" runat="server" Text='<%# Bind("COMPLEMENTO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nº" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumero" runat="server" Text='<%# Bind("NUMERO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bairro" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBairro" runat="server" Text='<%# Bind("BAIRRO") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Municipio" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMunicipio" runat="server" Text='<%# Bind("MUNICIPIO") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="UF" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUF" runat="server" Text='<%# Bind("UF") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CEP" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCEP" runat="server" Text='<%# Bind("CEP") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Contrato" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumContrato" runat="server" Text='<%# Bind("NUM_CONTRATO") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Empresa" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Matricula" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMatricula" runat="server" Text='<%# Bind("MATRICULA") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cod.Repres" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNumRepress" runat="server" Text='<%# Bind("NUM_REP") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Nome">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNome" runat="server" Visible="false" Text='<%# Bind("NOME") %>' />
                                                    <asp:LinkButton ID="lnkNome" runat="server" Text='<%# Bind("NOME") %>'
                                                        CommandName="Select">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="8px" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Tipo do Documento" HeaderStyle-Font-Size="8px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" disabled="true"
                                                        Font-Size="8px" />
                                                    <asp:Label ID="lblTipoDocumentoDesc" runat="server" Visible="false" Text='<%# Bind("ID_TIPODOCUMENTO") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoDocumentoUpd" runat="server" Font-Size="8px" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data da Postagem">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataPostagem" runat="server" Text='<%# Bind("DATAPOSTAGEM","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDataPostagem" runat="server" CssClass="date"
                                                        Style="width: 100px;" Text='<%# Bind("DATAPOSTAGEM","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="8px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data da Devolução">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataDevolucao" runat="server" Text='<%# Bind("DATADEVOLUCAO","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDataDevolucao" runat="server" CssClass="date"
                                                        Style="width: 100px;" Text='<%# Bind("DATADEVOLUCAO","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="8px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Motivo Devolução" HeaderStyle-Font-Size="8px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoMotiDev" runat="server" Font-Size="8px" disabled="true" />
                                                    <asp:Label ID="lblTipoMotiDevDesc" runat="server" Visible="false" Text='<%# Bind("ID_TIPOMOTIVODEVOLUCAO") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoMotiDevUpd" Font-Size="10px" runat="server" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reenvio">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataReenvio" runat="server" Text='<%# Bind("Reenvio","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtReenvio" runat="server" CssClass="date"
                                                        Style="width: 100px;" Text='<%# Bind("Reenvio","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="8px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tempo Prazo" HeaderStyle-Font-Size="8px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTempoPrazo" runat="server" Text='<%# Bind("Tempo_Prazo") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTempoPrazoInc" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditarControle" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass="button" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinkButton Text="" ID="lnkFake" runat="server" />
                                </asp:Panel>

                                <%--<asp:Panel ID="pnlGrdAlteracoesDetalheCorresp" runat="server">
                                    <asp:GridView ID="grdAlteracoesDetalheCorresp"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        EmptyDataText="Não retornou registros"
                                        AllowPaging="True"
                                        CssClass="Table"
                                        ClientIDMode="Static"
                                        Font-Size="8px"
                                        Visible="false">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Etiqueta">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" Checked="true" runat="server" Text="" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" Checked="true" runat="server" Text="" class="span_checkbox" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdReg" runat="server" Text='<%# Bind("ID_REG") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="8px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Tipo do Documento" HeaderStyle-Font-Size="8px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server"
                                                        Font-Size="8px" />
                                                    <asp:Label ID="lblTipoDocumentoDesc" runat="server" Visible="false" Text='<%# Bind("ID_TIPODOCUMENTO") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoDocumentoUpd" runat="server" Font-Size="8px" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data da Postagem">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataPostagem" runat="server" Text='<%# Bind("DATAPOSTAGEM","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDataPostagem" runat="server" CssClass="date"
                                                        Style="width: 100px;" Text='<%# Bind("DATAPOSTAGEM","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="8px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Data da Devolução">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataDevolucao" runat="server" Text='<%# Bind("DATADEVOLUCAO","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDataDevolucao" runat="server" CssClass="date"
                                                        Style="width: 100px;" Text='<%# Bind("DATADEVOLUCAO","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="8px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Motivo Devolução" HeaderStyle-Font-Size="8px">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoMotiDev" runat="server" Font-Size="8px" />
                                                    <asp:Label ID="lblTipoMotiDevDesc" runat="server" Visible="false" Text='<%# Bind("ID_TIPOMOTIVODEVOLUCAO") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTipoMotiDevUpd" Font-Size="8px" runat="server" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reenvio">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataReenvio" runat="server" Text='<%# Bind("Reenvio","{0:dd/MM/yyyy}") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtReenvio" runat="server" CssClass="date"
                                                        Style="width: 100px;" Text='<%# Bind("Reenvio","{0:dd/MM/yyyy}") %>' onkeypress="mascara(this, data)" />
                                                </EditItemTemplate>
                                                <HeaderStyle Font-Size="8px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tempo Prazo" HeaderStyle-Font-Size="8px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTempoPrazo" runat="server" Text='<%# Bind("Tempo_Prazo") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlTempoPrazoInc" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEditarControle" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass="button" />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>--%>
                            </ContentTemplate>
                        </ajax:TabPanel>


                    </ajax:TabContainer>
                </div>
            </div>
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
</asp:Content>
