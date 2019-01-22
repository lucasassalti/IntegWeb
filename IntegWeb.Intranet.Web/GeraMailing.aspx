<%@ Page Title="" Language="C#" MasterPageFile="~/Popup.Master" AutoEventWireup="true" CodeBehind="GeraMailing.aspx.cs" Inherits="IntegWeb.Intranet.Web.GeraMailing" %>

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
        <div class="tabelaPagina">
            <asp:UpdatePanel runat="server" ID="UpdatePanel">
                <ContentTemplate>
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">
                        <ajax:TabPanel ID="tbPrevidencia" HeaderText="Previdencia" runat="server" TabIndex="0">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnGerarPrevidencia" runat="server" Text="Gerar Arquivo" OnClick="btnGerarPrevidencia_Click" /></td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>

                        <ajax:TabPanel ID="TbSaude" HeaderText="Saude" runat="server" TabIndex="1">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnGerarSaude" runat="server" Text="Gerar Arquivo" OnClick="btnGerarSaude_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>
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
        </div>
    </div>

</asp:Content>
