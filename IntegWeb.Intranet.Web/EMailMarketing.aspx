<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="EMailMarketing.aspx.cs" Inherits="IntegWeb.Intranet.Web.EMailMarketing" %>

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
            <h1>Processamento de HTML para e-mail marketing</h1>
            <asp:UpdatePanel runat="server" ID="upSimulacao">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <h3>NOME DA CAMPANHA:</h3>
                                <asp:TextBox ID="txtCampanha" runat="server" Width="221px" />
                                <h3>HTML (ZIP) OU IMAGEM:</h3>
                                <asp:FileUpload ID="uploadZip" runat="server" CssClass="button" />
                                <asp:Button runat="server" OnClientClick="return postbackButtonClick();" ID="btnProcessar" Text="Processar" OnClick="btnProcessar_Click" CssClass="button" />
                                <h3>
                                    <asp:Label Visible="false" ID="lblRegistros" runat="server" Text="Label" ForeColor="Red"></asp:Label>
                                </h3>
                                <h3>
                                    <code>
                                        <%--<asp:Label Visible="true" ID="lblHtml" runat="server" ForeColor="Red"></asp:Label>--%>
                                        <asp:HyperLink Visible="true" ID="hlkHtml" runat="server" ForeColor="Red" Target="_blank" Style="cursor:pointer" ></asp:HyperLink>
                                        </h3>
                                    </code>
                                    
                                </h3>                                 
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnProcessar" />
                </Triggers>
            </asp:UpdatePanel>
            &nbsp;<asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
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
        &nbsp;</div>
    &nbsp;</div>
&nbsp;
</asp:Content>
