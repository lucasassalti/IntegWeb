<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="GerarXmlANS.aspx.cs" Inherits="IntegWeb.Saude.Web.GerarXmlANS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var updateProgress = null;
        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function fuPrestadores_onChange() {

            $('#ContentPlaceHolder1_fuPrestadores').removeAttr('disabled');
            $('#ContentPlaceHolder1_fuPrestadores').prev().css('color', '');
            $('#ContentPlaceHolder1_fuPrestadores').css('color', '');

            switch ($('#ContentPlaceHolder1_ddlTipo').val()) {
                case "RPI":
                case "RPA":
                case "RPV":
                    $('#ContentPlaceHolder1_fuPrestadores').attr('disabled', 'disabled');
                    $('#ContentPlaceHolder1_fuPrestadores').prev().css('color', 'silver');
                    $('#ContentPlaceHolder1_fuPrestadores').css('color', 'silver');
                    break;
                default:
                    break;
            }
        }

    </script>
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">
            <h1>Gerador de arquivos para ANS</h1>
            <asp:UpdatePanel runat="server" ID="upSimulacao">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <h3>Tipo de arquivo:</h3>
                                <asp:DropDownList runat="server" ID="ddlTipo" onchange="fuPrestadores_onChange();">
                                    <asp:ListItem Text="---Selecione---" Value="" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="RPI - Inclusão de prestadoras" Value="RPI"></asp:ListItem>
                                    <asp:ListItem Text="RPE - Exclusão de prestadoras" Value="RPE"></asp:ListItem>                                    
                                    <asp:ListItem Text="RPA - Alteração de prestadoras" Value="RPA"></asp:ListItem> 
                                    <asp:ListItem Text="RPV - Vinculação de prestadoras ao plano" Value="RPV"></asp:ListItem> 
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h3>Planilha com a lista:</h3>
                                <asp:FileUpload ID="fuListagem" runat="server" CssClass="button" accept=".xls,.xlsx" />
                                <br /><br />
                                <h3>Planilha completa prestadores ANS:</h3>
                                <asp:FileUpload ID="fuPrestadores" runat="server" CssClass="button" accept=".xls,.xlsx"/>
                                <asp:Button runat="server" OnClientClick="return postbackButtonClick();" ID="btnProcessar" Text="Gerar XML" OnClick="btnProcessar_Click" CssClass="button" />
                            </td>
                        </tr>
                    </table>

                    <script type="text/javascript"> fuPrestadores_onChange(); </script>

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
