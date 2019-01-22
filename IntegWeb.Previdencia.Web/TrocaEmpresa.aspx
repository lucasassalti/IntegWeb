<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="TrocaEmpresa.aspx.cs" Inherits="IntegWeb.Previdencia.Web.TrocaEmpresa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">

        <div class="h_title">
        </div>

        <h1>Troca de Empresa do Participante</h1>

        <asp:Panel ID="pnlPesquisar" runat="server" class="tabelaPagina">
            <table>
                <tr>
                    <td>Num_Matre</td>
                    <td>
                        <asp:TextBox ID="txtMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server" MaxLength="6"></asp:TextBox></td>

                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnPesquisar" runat="server" CssClass="button" Text="Pesquisar" OnClick="btnPesquisar_Click" />
                        <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                        <asp:Label ID="pnlPesquisa_Mensagem" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="pnlCorporativo" runat="server" CssClass="tabelaPagina" Visible="false">
            <table>
                <tr>
                    <td style="font-weight: bolder" colspan="6">Corporativo</td>
                </tr>
                <tr>
                    <td>Código Empresa:
                          <asp:Label ID="lblEmpresaCorporativo" runat="server" Text="1" />
                    </td>
                </tr>
                <tr>
                    <td>Nome Empresa:
                          <asp:Label ID="lblNomeCorporativo" runat="server" Text="Fundação CESP" />
                    </td>
                </tr>
                <tr>
                    <td>Matrícula:
                          <asp:Label ID="lblMatricula" runat="server" Text="1" />
                    </td>
                    <td>Nome:
                          <asp:Label ID="lblNomeEmpregado" runat="server" Text="Lucas" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="pnlCapitalizacao" runat="server" CssClass="tabelaPagina" Visible="false">
            <table>
                <tr>
                    <td style="font-weight: bolder" colspan="6">Capitalização</td>
                </tr>
                <tr>
                    <td>Código Empresa:
                         <asp:Label ID="lblEmpresaCapitalizacao" runat="server" />
                    </td>
                    <td>Número Patrocinadora:
                         <asp:Label ID="lblNumeroPatrocinadora" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Nome Empresa:
                         <asp:Label ID="lblNomeCapitalizacao" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Matrícula:
                          <asp:Label ID="lblMatriculaCapitalizacao" runat="server" />
                    </td>
                    <td>Nome:
                          <asp:Label ID="lblParticipante" runat="server" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                         <asp:Button ID="btnTrocarEmpresa" runat="server" CssClass="button" Text="Trocar Empresa" OnClick="btnTrocarEmpresa_Click" 
                             OnClientClick="return confirm('Deseja realmente trocar o Participante de Empresa?');" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

    </div>
</asp:Content>
