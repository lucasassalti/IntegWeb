<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="ExportaRelCI.aspx.cs" Inherits="IntegWeb.Saude.Web.ExportaRelCI" %>

<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>
            <div class="full_w">
                <div class="h_title">
                </div>
                <div class="tabelaPagina">
                    <h1>Relatório de Solicitação de CI por Congênere </h1>

                    <asp:Panel ID="pnlExportar" runat="server">
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
                                <td>
                                    <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatórios" OnClick="btnRelatorio_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
                <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" Visible="false"/>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
      <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
        <ProgressTemplate>
            <div id="carregando">
                <div class="carregandoTxt">
                    <img src="img/processando.gif" />
                    <br />
                    <br />
                    <h2>Processando. Aguarde...</h2>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
