<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CartasReport.aspx.cs" Inherits="IntegWeb.Previdencia.Web.CartasReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var updateProgress = null;

        function postbackButtonClick() {
            updateProgress = $find("<%= UpdateProg1.ClientID %>");
            window.setTimeout("updateProgress.set_visible(true)", updateProgress.get_displayAfter());
            return true;
        }

        function EnterEvent(e) {
            if (e.keyCode == 13) {
                //$("#btnSubmitButton").trigger("click");
                postbackButtonClick();
            }
        }

    </script>

    <div class="full_w">

        <div class="h_title">
        </div>
        <h1>
            <asp:Label runat="server" ID="NomeRelatorio"></asp:Label>
        </h1>

        <div class="tabelaPagina">
            <h1>Relatório de Cartas</h1>
            <div class="MarginGrid">
                <table>
                    <tr>
                        <td>Digite o número da matrícula:
                        </td>
                        <td>
                            <asp:TextBox ID="txtMatricula" runat="server" onkeypress="mascara(this, soNumeros); return EnterEvent(event);" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Digite o número da empresa:
                        </td>
                        <td>
                            <asp:TextBox ID="txtEMpresa" runat="server" onkeypress="mascara(this, soNumeros); return EnterEvent(event);" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Data de emissão da Carta
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server"
                                ControlToValidate="txtDataEmissao" ErrorMessage="Informe uma data válida (mm/dd/yyyy)"
                                ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}"
                                Text="*"></asp:RegularExpressionValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataEmissao" runat="server" CssClass="date" onkeypress="mascara(this, data); return EnterEvent(event);" MaxLength="10" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnCarregarRelatorios" runat="server" CssClass="button" Text="Carregar Relatórios" OnClick="btnCarregarRelatorios_Click" OnClientClick="return postbackButtonClick();" />
                            <asp:Button ID="btnRelatorio1" runat="server" CssClass="button" Text="1ª parte do Relatório" OnClick="btnRelatorio1_Click" Visible="false" />
                        </td>
                        <td>
                            <asp:Button ID="btnRelatorio2" runat="server" CssClass="button" Text="2ª parte do Relatório" OnClick="btnRelatorio2_Click" Visible="false" />
                        </td>
                    </tr>
                </table>

            </div>
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
        <div class="MarginGrid">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>

                        <CR:CrystalReportViewer GroupTreeImagesFolderUrl="" ToolbarImagesFolderUrl="" ReportSourceID="" ID="CrystalReportViewer1" runat="server" CssClass="crystalClass" AutoDataBind="True" EnableDatabaseLogonPrompt="false" EnableParameterPrompt="False" ToolPanelWidth="100px" ToolPanelView="None" OnUnload="CrystalReportViewer1_Unload" />

                        <%--                        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                            <Report FileName="Relatorios/Relatorio_Tela.rpt">
                            </Report>
                        </CR:CrystalReportSource>--%>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnRelatorio1" />
                    <asp:PostBackTrigger ControlID="btnRelatorio2" />
                </Triggers>
            </asp:UpdatePanel>

        </div>
    </div>
</asp:Content>
