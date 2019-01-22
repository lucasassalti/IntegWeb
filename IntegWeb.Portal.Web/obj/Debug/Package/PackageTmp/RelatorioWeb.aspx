<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioWeb.aspx.cs" Inherits="IntegWeb.Previdencia.Web.RelatorioWeb" MasterPageFile="~/Popup.Master" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="full_w">
        <div id="area_parametro">
            <div class="h_title">
            </div>
            <h1>
                <asp:Label runat="server" ID="NomeRelatorio" ForeColor="White" Style="padding: 20px;"></asp:Label>
            </h1>
            <br />
            <div class="tabelaPagina" id="tabelaPagina" runat="server">
                <table runat="server" id="table">
                    <tr>
                        <td colspan="2"></td>
                    </tr>
                </table>
                <div class="MarginGrid">
                <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatório" OnClick="btnRelatorio_Click" /></div>
            </div>
        </div>
        <div class="MarginGrid">
            <uc1:ReportCrystal runat="server" ID="ReportCrystal" />
        </div>
    </div>

</asp:Content>

