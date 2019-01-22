<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="HistoricoAtendOdonto.aspx.cs" Inherits="IntegWeb.Saude.Web.HistoricoAtendOdonto" %>

<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
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
                    <h1>Relatório de Histórico de Atendimento Odontológico  </h1>

                    <asp:Panel ID="pnlRelatorio" runat="server">
                        <table>
                            <tr>
                                <td>Número Protocolo:
                               
                                    <asp:TextBox ID="txtNumProtocolo" runat="server" MaxLength="8" onkeypress="mascara(this, soNumeros)" />
                                </td>
                            </tr>
                            <tr>
                                <td>Número CRG:
                              
                                    <asp:TextBox ID="txtNumCRG" runat="server" MaxLength="16" onkeypress="mascara(this, soNumeros)" />
                            </tr>
                            <tr>
                                <td>Número Contrato:
                                    <asp:TextBox ID="txtNumContrato" runat="server" MaxLength="12" onkeypress="mascara(this, soNumeros)" />
                                </td>
                            </tr>
                            <tr>
                                <td>Data de Pagamento:
                                    <asp:TextBox ID="txtDtPagto" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                    <asp:RequiredFieldValidator runat="server" ID="reqDtPagto" ControlToValidate="txtDtPagto" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaData" />
                                    <asp:RangeValidator
                                        runat="server"
                                        ID="rangDatPagto"
                                        Type="Date"
                                        ControlToValidate="txtDtPagto"
                                        MaximumValue="31/12/9999"
                                        MinimumValue="31/12/1000"
                                        ErrorMessage="Data Inválida"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="ValidaData" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatório" OnClick="btnRelatorio_Click" ValidationGroup="ValidaData" CausesValidation="true" />
                                    <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                     <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" Visible="false" />

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
