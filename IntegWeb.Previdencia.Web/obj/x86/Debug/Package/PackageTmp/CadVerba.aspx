<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadVerba.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CadVerba" %>

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


            <h1>Troca de Verbas (Incorporação x Simulação)</h1>

            <asp:UpdatePanel runat="server" ID="upVerba">
                <ContentTemplate>
                    <asp:Label ID="lblNome" Visible="false" ForeColor="Black" Font-Bold="true" Font-Size="Small" runat="server"></asp:Label>
                    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0">


                        <ajax:TabPanel ID="TabMatricula" HeaderText="Participante" runat="server" TabIndex="0">
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
                        <ajax:TabPanel ID="tabIni" HeaderText="Trocar" Visible="false" runat="server" TabIndex="1">
                            <ContentTemplate>
                                <table>

                                    <tr>

                                        <td>
                                            <asp:Button ID="btnConsultar" OnClick="btnConsultar_Click" runat="server" CssClass="button" Text="Consultar Verbas de Incorporação" /></td>
                                        <td>
                                            <asp:HiddenField ID="hdMatricula" runat="server" />
                                            <asp:HiddenField ID="HdEmpresa" runat="server" />

                                            <asp:Button ID="btnTrocarVerba" OnClick="btnTrocarVerba_Click"  runat="server" CssClass="button" Text="Trocar tipo de Cálculo Simulação para Incorporação" /></td>

                                    </tr>


                                </table>

                            </ContentTemplate>
                        </ajax:TabPanel>

                    </ajax:TabContainer>
                </ContentTemplate>
          
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
