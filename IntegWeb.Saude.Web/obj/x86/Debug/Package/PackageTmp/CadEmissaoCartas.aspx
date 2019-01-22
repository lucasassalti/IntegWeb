<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="CadEmissaoCartas.aspx.cs" Inherits="IntegWeb.Saude.Web.CadEmissaoCartas" %>

<%@ Register Src="~/Includes/ucReport.ascx" TagPrefix="uc1" TagName="ReportCrystal" %>
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
    <asp:UpdatePanel runat="server" ID="UpdatePanel">
        <ContentTemplate>

            <div class="full_w">

                <div class="tabelaPagina">

                    <h1>Emissão de Cartas - Cadastro</h1>

                    <table>
                        <tr>
                            <td>Data Inicio:
                                <asp:TextBox ID="txtDatIni" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px"></asp:TextBox>
                                <asp:RangeValidator
                                    runat="server"
                                    ID="rangDatIni"
                                    Type="Date"
                                    ControlToValidate="txtDatIni"
                                    MaximumValue="31/12/9999"
                                    MinimumValue="31/12/1000"
                                    ErrorMessage="Data Inválida"
                                    ForeColor="Red"
                                    Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>Data Fim: &nbsp
                                      <asp:TextBox ID="txtDatFim" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px"></asp:TextBox>
                                <asp:RangeValidator
                                    runat="server"
                                    ID="rangDatFim"
                                    Type="Date"
                                    ControlToValidate="txtDatFim"
                                    MaximumValue="31/12/9999"
                                    MinimumValue="31/12/1000"
                                    ErrorMessage="Data Inválida"
                                    ForeColor="Red"
                                    Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>Tipo Carta:
                                  <asp:DropDownList ID="ddlTipoRelatorio" runat="server">
                                      <asp:ListItem Text="Adesão" Value="ADESAO"></asp:ListItem>
                                      <asp:ListItem Text="Cancelamento" Value="CANCELAMENTO"></asp:ListItem>
                                      <asp:ListItem Text="Cancelamento RN412" Value="CANCELAMENTO_412"></asp:ListItem>
                                      <asp:ListItem Text="Cancelamento 24 anos" Value="CANCELAMENTO_24"></asp:ListItem>
                                      <asp:ListItem Text="Rev Aposentado" Value="REV_APOSENTADO"></asp:ListItem>
                                      <asp:ListItem Text="Rev Ativo" Value="REV_ATIVO"></asp:ListItem>
                                      <asp:ListItem Text="Troca" Value="TROCA"></asp:ListItem>
                                  </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnGerar" runat="server" CssClass="button" Text="Gerar" OnClick="btnGerar_Click" />
                                <asp:Button ID="btnLimpar" runat="server" CssClass="button" Text="Limpar" OnClick="btnLimpar_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <uc1:ReportCrystal runat="server" ID="ReportCrystal" Sytle="display: none;" Visible="false" />
            </div>
        </ContentTemplate>
        <%--      <Triggers>
            <asp:PostBackTrigger ControlID="btnOk" />
        </Triggers>--%>
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

</asp:Content>
