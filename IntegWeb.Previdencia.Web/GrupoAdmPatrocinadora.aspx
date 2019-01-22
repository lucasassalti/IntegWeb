<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="GrupoAdmPatrocinadora.aspx.cs" Inherits="IntegWeb.Previdencia.Web.GrupoAdmPatrocinadora" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upGrupoAdm" runat="server">
        <ContentTemplate>
            <div class="full_w">

                <div class="h_title">
                </div>

                <h1>Controle de Acesso das Patrocinadoras</h1>

                <div id="divLista" runat="server" class="tabelaPagina">
                    <asp:Panel ID="pnlPesquisa" runat="server">
                        <table>
                            <tr>
                                <td>Grupo: </td>
                                <td>
                                    <asp:TextBox ID="txtNomGrupo" runat="server" MaxLength="1000" /></td>
                                <td>Empresa: </td>
                                <td>
                                    <asp:TextBox ID="txtEmpresa" runat="server" MaxLength="3" onkeypress="mascara(this, soNumeros)" /></td>
                            </tr>
                            <tr>
                                <td>&nbsp</td>

                                <td>
                                    <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                                    <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                    <asp:Button ID="btnInserirGrupo" runat="server" CssClass="button" Text="Inserir Grupo" OnClick="btnInserirGrupo_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                     &nbsp;
                    <asp:Panel ID ="pnlLista" runat ="server" >
                    <table>
                        <tr>
                            <td><b>Lista de Grupos</b></td>
                            <td></td>
                            <td></td>
                            <td><b>Lista de Empresas</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ListBox ID="lstGrupo" runat="server" SelectionMode="Single" Height="500px" Width="400px" AutoPostBack="true" OnSelectedIndexChanged="lstGrupo_SelectedIndexChanged"></asp:ListBox>
                            </td>
                            <td></td>
                            <td></td>
                            <td>
                                <br />
                                <br />
                                <asp:ListBox ID="lstEmpresa" runat="server" SelectionMode="Single" Height="500px" Width="400px" />
                                <br />
                                <asp:Label ID="lblAddEmpresa" Text="Adicionar Empresa: " runat="server" />
                                <asp:DropDownList ID="ddlEmpresas" runat="server" />
                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/img/i_add_page.png" OnClick="btnAdd_Click" ToolTip ="Adicionar Empresa" />
                                <asp:ImageButton ID="btnDel" runat="server" ImageUrl="~/img/i_minus_3.png" OnClick="btnDel_Click" ToolTip ="Excluir Empresa" Height="20px" Width="20px" />
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                </div>

                <div id="divInsere" runat="server" class="tabelaPagina">
                    <table>
                        <tr>
                            <td>Nome do Grupo: </td>
                            <td>
                                <asp:TextBox ID="txtNomeGrupo" runat="server" MaxLength="1000" Width="192px" />
                                <asp:RequiredFieldValidator runat="server" ID="reqNomeGrupo" ControlToValidate="txtNomeGrupo" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>Código Empresa: </td>
                            <td>
                                <asp:TextBox ID="txtCodEmpresa" runat="server" MaxLength="3" onkeypress="mascara(this, soNumeros)" Width="58px" />
                                <asp:RequiredFieldValidator runat="server" ID="reqCodEmp" ControlToValidate="txtCodEmpresa" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnAddGrupo" runat="server" CssClass="button" Text="Adicionar Grupo" OnClick="btnAddGrupo_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnVoltar" runat="server" CssClass="button" Text="Voltar" OnClick="btnVoltar_Click" CausesValidation="false" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
