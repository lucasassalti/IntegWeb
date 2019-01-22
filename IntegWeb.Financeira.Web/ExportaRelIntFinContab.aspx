<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ExportaRelIntFinContab.aspx.cs" Inherits="IntegWeb.Financeira.Web.ExportaRelFinContab" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Exportação de Informações Geradas pela Integração Financeira/Contábil</h1>

            <asp:Panel ID="pnlExportar" runat="server">
                <table>
                    <tr>
                        <td>Data Inicio Competência:</td>
                        <td>
                            <asp:TextBox ID="txtDatInicio" runat="server" CssClass="date" MaxLength="10" onkeypress="javascript:return mascara(this, data);" />
                            <asp:RequiredFieldValidator runat="server" ID="reqDatini" ControlToValidate="txtDatInicio" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" />
                            <asp:RangeValidator
                                runat="server"
                                ID="rangDataIni"
                                Type="Date"
                                ControlToValidate="txtDatInicio"
                                MaximumValue="31/12/9999"
                                MinimumValue="31/12/1000"
                                ErrorMessage="Data Inválida"
                                ForeColor ="Red"
                                Display="Dynamic" />
                             </td>
                    </tr>
                    <tr>
                        <td>Data Fim Competência:</td>
                        <td>
                            <asp:TextBox ID="txtDatFim" runat="server" CssClass="date" MaxLength="10" onkeypress="javascript:return mascara(this, data);" />
                            <asp:RequiredFieldValidator runat="server" ID="reqDataFim" ControlToValidate="txtDatFim" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" />
                            <asp:RangeValidator
                                runat="server"
                                ID="rangDataFim"
                                Type="Date"
                                ControlToValidate="txtDatFim"
                                MaximumValue="31/12/9999"
                                MinimumValue="31/12/1000"
                                ErrorMessage="Data Inválida"
                                ForeColor ="Red"
                                Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatórios" OnClick="btnRelatorio_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Processo_Mensagem" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
