<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RelSaoFrancisco.aspx.cs" Inherits="IntegWeb.Previdencia.Web.RelSaoFrancisco" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="MarginGrid">
        <asp:UpdatePanel runat="server" ID="UpdatePanel">
            <ContentTemplate>
                <div class="full_w">
                    <div class="tabelaPagina">
                        <h1>Extração Relatório São Francisco</h1>
                        <table>
                            <tr>
                                <td>Data Inicio:</td>
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
                                        ForeColor="Red"
                                        Display="Dynamic" />
                                </td>
                            </tr>
                            <tr>
                                <td>Data Fim:</td>
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
                                        ForeColor="Red"
                                        Display="Dynamic" />
                                </td>
                            </tr>
                            <tr>
                                <td>Tipo Relatório:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTipoRelatorio" runat="server">
                                        <asp:ListItem Text="Inclusão" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Cancelamento" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatório" OnClick="btnRelatorio_Click" />
                                </td>
                            </tr>
                        </table>
                        <%-- <table>
                    <tr>
                        <td>
                            <asp:Label ID="Processo_Mensagem" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>--%>
                    </div>
                    <uc1:ReportCrystal runat="server" ID="ReportCrystal" Style="display: none;" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    
</asp:Content>
