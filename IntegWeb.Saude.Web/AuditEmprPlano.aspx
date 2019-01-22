<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AuditEmprPlano.aspx.cs" Inherits="IntegWeb.Saude.Web.AuditEmprPlano" %>

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

                <div class="tabelaPagina">

                    <h1>Auditoria Empresa x Plano</h1>
                    <table>
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnProcessar" Text="Processar" CssClass="button" OnClick="btnProcessar_Click" />
                                  <asp:Button runat="server" ID="btnLimparCritica" Text="Limpar Criticas" CssClass="button"  />
                                  <asp:Button runat="server" ID="btnGerarExcel" Text="Gerar Criticas em Excel" CssClass="button"  />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblCriticas" runat="server" />
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnProcessar" />
        </Triggers>
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
