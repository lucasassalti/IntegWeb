<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="TravaDataCorte.aspx.cs" Inherits="IntegWeb.Financeira.Web.TravaDataCorte" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Controle Trava Data/Corte</h1>
            <asp:Panel ID="pnlTrava" runat="server">
                <table>
                    <tr>
                        <td>Status:</td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="btnHabilitarTrava" runat="server" CssClass="button" Text="Ativar/Desativar Trava" OnClick="btnHabilitarTrava_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
