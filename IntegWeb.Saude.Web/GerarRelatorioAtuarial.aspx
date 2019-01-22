<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Principal.Master" CodeBehind="GerarRelatorioAtuarial.aspx.cs" Inherits="IntegWeb.Saude.Web.GeraRelatorioAtuarial" %>

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
            <h1>Relatórios Atuarias </h1>
            <asp:UpdatePanel runat="server" ID="upUpdatePanel">
                <ContentTemplate>

                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlTipoCarga" runat="server" Width="250px" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoCarga_SelectedIndexChanged">
                                    <asp:ListItem Text="BENEFICIÁRIOS" Value="REL_BENEFICIARIO" />
                                    <asp:ListItem Text="PRESTADORES" Value="REL_PRESTADORES" />
                                    <asp:ListItem Text="CO PARTICIPAÇÃO" Value="REL_CO_PARTICIPACAO" />
                                    <asp:ListItem Text="ESTUDO SAÚDE" Value="REL_ESTUDO_SAUDE" />
                                    <asp:ListItem Text="PLANOS" Value="REL_PLANOS" />
                                    <asp:ListItem Text="PROCEDIMENTOS" Value="REL_PROCEDIMENTOS" />
                                    <asp:ListItem Text="MENSALIDADES" Value="REL_MENSALIDADES" />
                                    <asp:ListItem Text="SINISTRO" Value="REL_SINISTRO" />

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="reqddlTipoCarga" ControlToValidate="ddlTipoCarga" ErrorMessage="Campo Obrigatório" ForeColor="Red" Display="Dynamic" ValidationGroup="ddlTipoCargaRel" />

                            </td>
                        </tr>
                    </table>
                    <div id="divData" runat="server" class="tabelaPagina" visible="false">
                        <table>
                            <tr>
                                <td>Data inicial: 
                                <asp:TextBox ID="txtDataInicio" CssClass="date" runat="server" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Data final:&nbsp;&nbsp;  
                                <asp:TextBox ID="txtDataFim" CssClass="date" runat="server" onkeypress="mascara(this, data)" MaxLength="10"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div id="divSinistro" runat="server" class="tabelaPagina" visible="false">
                        <table>
                            <tr>
                                <td>Mês: 
                                <asp:TextBox ID="txtMes" runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Ano:&nbsp;&nbsp;  
                                <asp:TextBox ID="txtAno"  runat="server" onkeypress="mascara(this, soNumeros)" MaxLength="4"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnGerarCarga" runat="server" CssClass="button" OnClick="btnGerarCarga_Click" Text="Gerar Relatório" OnClientClick="return postbackButtonClick();" ValidationGroup="ddlTipoCargaRel" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

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

</asp:Content>
