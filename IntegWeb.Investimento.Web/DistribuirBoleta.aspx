<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="DistribuirBoleta.aspx.cs" Inherits="IntegWeb.Investimento.Web.DistribuirBoleta" %>

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
            <h1>Processamento de Boletagem</h1>
            <asp:UpdatePanel runat="server" ID="upSimulacao">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <h3>Boleta:</h3>
                                <asp:FileUpload ID="fuDistribuicao" runat="server" CssClass="button" />
                                <h3>Estoque:</h3>
                                <asp:FileUpload ID="fuBaseDados" runat="server" CssClass="button" />
                                <asp:Button runat="server" OnClientClick="return postbackButtonClick();" ID="btnDistribuir" Text="Distribuir" OnClick="btnDistribuir_Click" CssClass="button" />
                                <h3><asp:Label Visible="false" ID="lblRegistros" runat="server" Text="Label" ForeColor="Red"></asp:Label></h3>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnDistribuir" />
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
