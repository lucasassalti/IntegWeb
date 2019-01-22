<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdmVerba.aspx.cs" Inherits="IntegWeb.Previdencia.Web.AdmVerba" %>

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
    <div class="full_w">
        <div class="h_title">
        </div>
        <div class="tabelaPagina">


            <h1>Importação de Verbas </h1>



            <asp:UpdatePanel runat="server" ID="upVerba">
                <ContentTemplate>
                    <asp:Label ID="lblNome" Visible="false" ForeColor="Black" Font-Bold="true"  Font-Size="Small" runat="server"></asp:Label>
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">


                        <ajax:TabPanel ID="TabMatricula" HeaderText="Participante" runat="server" TabIndex="1">
                            <ContentTemplate>
                                <table>

                                    <tr>
                                        <td>Digite o Nº Empresa</td>
                                        <td>
                                            <asp:TextBox ID="txtCodEmpresa" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>Digite o Nº Matrícula</td>
                                        <td>
                                            <asp:TextBox ID="txtCodMatricula" Width="100px" onkeypress="mascara(this, soNumeros)" runat="server"></asp:TextBox><br />
                                            <asp:HiddenField ID="hdNumPartif" runat="server" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnBuscar" OnClick="btnPesquisar_Click" runat="server" CssClass="button" Text="Buscar Participante" /></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>
                        <ajax:TabPanel ID="tabImport" HeaderText="Importar/Deletar" Visible="false" runat="server" TabIndex="1">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Selecione uma planilha Excel
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button runat="server" OnClientClick="return postbackButtonClick();" ID="UploadButton" Text="Carregar Verbas de Diferença" OnClick="UploadButton_Click" CssClass="button" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDeletarVerba" OnClick="btnDeletarVerba_Click" runat="server" CssClass="button" Text="Excluir Verbas Carregadas" /></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="TabContainer$tabImport$UploadButton" />
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
