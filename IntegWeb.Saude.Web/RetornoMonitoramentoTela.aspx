<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RetornoMonitoramentoTela.aspx.cs" Inherits="IntegWeb.Saude.Web.RetornoMonitoramentoTela" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function ValidateFileUpload(Source, args) {
            var fuData = document.getElementById('<%= FileUploadControl.ClientID %>');
            var FileUploadPath = fuData.value;

            if (FileUploadPath == '') {
                // Não Selecionou nenhum arquivo
                args.IsValid = false;
            }
            else {
                var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

                if (Extension == "xte") {
                    args.IsValid = true; // Extensão de arquivo válida
                }
                else {
                    args.IsValid = false; // Extensão de arquivo inválida
                }
            }
        }
    </script>
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">

            <h1>Retorno Monitoramento TISS</h1>

            <asp:UpdatePanel runat="server" ID="upSys">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <ajax:TabPanel ID="TabPanel1" HeaderText="Importar Arquivo" runat="server" TabIndex="1">
                            <ContentTemplate>
                                <div id="divAction" runat="server">

                                    <table>
                                        <tr>
                                            <td>Selecione o arquivo para importar:</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                                    ClientValidationFunction="ValidateFileUpload" ErrorMessage="Favor selecionar um arquivo XTE válido."></asp:CustomValidator>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>

                                                <asp:Button ID="btnUpLoad" runat="server" OnClick="btnUpLoad_Click" Text="Importar" OnClientClick="return postbackButtonClick();" CssClass="button" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>

                                                <h4>
                                                    <asp:Label ID="contador" runat="server" Text="Número de Registros importados :"></asp:Label></h4>
                                                <h4>
                                                    <asp:Label runat="server" ID="StatusLabel" Text="Upload Status: " /></h4>
                                            </td>

                                        </tr>


                                    </table>
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer1$TabPanel1$btnUpLoad" />
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
