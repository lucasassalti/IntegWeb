<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="GerarPlanAtendeSUS.aspx.cs" Inherits="IntegWeb.Saude.Web.GerarPlanAtendeSUS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

    </script>
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Gerador de Planilha para o SUS</h1>
            <asp:UpdatePanel runat="server" ID="upSimulacao">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <h3>Importar Arquivo XML:</h3>                                
                                <asp:FileUpload ID="fuCancelados" runat="server" CssClass="button" />
                                <br /><br />
                                <asp:Button runat="server" OnClientClick="return postbackButtonClick();" ID="btnProcessar" Text="Gerar Excel" OnClick="btnProcessar_Click" CssClass="button" />
                                <h3><asp:Label Visible="false" ID="lblRegistros" runat="server" Text="Label" ForeColor="Red"></asp:Label></h3>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnProcessar" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
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
        </div>
    </div>
</asp:Content>
