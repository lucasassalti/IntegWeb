<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="intP_CadEmpresaProtheus.aspx.cs" Inherits="IntegWeb.Previdencia.Web.intP_CadEmpresaProtheus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }
    </script>

    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <div class="full_w">
                <div class="h_title">
                </div>
                <div class="tabelaPagina">
                    <h1>Integração Protheus - Cadastro de Empresa Fundação X Protheus</h1>

                    <asp:Panel ID="pnlInserir" runat="server">
                        <table>
                            <tr>
                                <td>Cod.Empresa:
                                </td>
                                <td>Cod.Protheus:
                                </td>
                                <td>Cod.Protheus Sub:
                                </td>
                                <td>Descrição Protheus:
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCodEmpresa" runat="server" Width="150px" MaxLength="3" onkeypress="mascara(this, soNumeros)" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodProtheus" runat="server" Width="150px" MaxLength="5" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodProtheusSub" runat="server" Width="150px" MaxLength="5" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescricaoProtheus" runat="server" Width="150px" MaxLength="40" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnAddEmpresaProtheus" runat="server" CssClass="button" Text="Inserir" OnClick="btnAddEmpresaProtheus_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlCadEmpresaProtheusGrid" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnConsultar" runat="server" CssClass="button" Text="Consultar" OnClick="btnConsultar_Click" />
                                </td>
                            </tr>

                              <asp:Panel ID="pnlCadEmpresaPesquisar" runat="server" Visible="false">
                            <tr>
                                <td>Pesquisar Por Cod.Protheus:
                                    <asp:TextBox ID="txtFiltrarCodProtheus" runat="server" Width="150px" MaxLength="5" onkeypress="mascara(this, soNumeros)" />
                                    <asp:Button ID="btnFiltrarCodProtheus" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnFiltrarCodProtheus_Click" />
                                </td>
                            </tr>
                            </asp:Panel>

                        </table>

                        <asp:GridView ID="grdDadosFundacaoXProtheus" runat="server"
                            AutoGenerateColumns="False"
                            EmptyDataText="A consulta não retornou registros"
                            CssClass="Table"
                            Font-Size="13px"
                            AllowPaging="True"
                            OnRowCommand="grdDadosFundacaoXProtheus_RowCommand"
                            OnRowEditing="grdDadosFundacaoXProtheus_RowEditing"
                            OnRowCancelingEdit="grdDadosFundacaoXProtheus_RowCancelingEdit"
                            OnRowUpdating="grdDadosFundacaoXProtheus_RowUpdating"
                            OnPageIndexChanging="grdDadosFundacaoXProtheus_PageIndexChanging"
                            CausesValidation="true"
                            ClientIDMode="Static"
                            Visible="false">

                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIDFundacaoXProtheus" runat="server" Text='<%# Bind("COD_PATR_PRV") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Código Fundação">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCodEmpresa" runat="server" Text='<%# Bind("COD_EMPRS") %>'
                                           MaxLength="3" onkeypress="mascara(this, soNumeros)" Width="100%" />
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Código Protheus">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodProtheus" runat="server" Text='<%# Bind("COD_PATR") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCodProtheus" runat="server" Text='<%# Bind("COD_PATR") %>'
                                           MaxLength="5" Width="100%" />
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Código Protheus Sub">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodProtheusSub" runat="server" Text='<%# Bind("COD_PATR_SUP") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCodProtheusSub" runat="server" Text='<%# Bind("COD_PATR_SUP") %>'
                                          MaxLength="5"  Width="100%" />
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Descrição Protheus">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescProtheus" runat="server" Text='<%# Bind("DCR_PATR") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescProtheus" runat="server" Text='<%# Bind("DCR_PATR") %>'
                                          MaxLength="40"  Width="100%" />
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Edit" CssClass="button" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CommandName="Update" CssClass="button" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CommandName="Cancel" CssClass="button" />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </asp:Panel>
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
