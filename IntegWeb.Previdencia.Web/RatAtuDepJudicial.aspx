<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="RatAtuDepJudicial.aspx.cs" Inherits="IntegWeb.Previdencia.Web.RatAtuDepJudicial" %>

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

                    <h1>Rateio na Atualização do Depósito Judicial</h1>

                    <table>
                        <tr>
                            <td>Entre com o arquivo de carga:</td>
                            <td>
                                <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                            </td>
                            <td>
                                <asp:Button ID="btnProcessar" runat="server" Text="Processar" CssClass="button" OnClick="btnProcessar_Click" />
                            </td>

                            <td>


                                <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="button" OnClick="btnExportar_Click" />

                                <ajax:ModalPopupExtender
                                    ID="ModalPopupExtender1"
                                    runat="server"
                                    DropShadow="true"
                                    PopupControlID="panelPopUp"
                                    TargetControlID="btnExportar"
                                    BackgroundCssClass="modalBackground">
                                </ajax:ModalPopupExtender>
                                <asp:Panel ID="panelPopUp" runat="server" Style="display: none; background-color: white; border: 1px solid black">
                                    <h3>Exportação Planilha Calculada:</h3>
                                    <table>
                                        <tr>
                                            <td>Data de Geração:</td>
                                            <td>
                                                <asp:TextBox ID="txtDataGeracao" runat="server" CssClass="date" MaxLength="10" onkeypress="mascara(this, data)" Width="100px" />
                                                <asp:RequiredFieldValidator runat="server" ID="reqDataGeracao" ControlToValidate="txtDataGeracao" ErrorMessage="Obrigátório" ForeColor="Red" Display="Dynamic" ValidationGroup="ValidaGer" />
                                                <asp:RangeValidator
                                                    runat="server"
                                                    ID="rangDatGeracao"
                                                    Type="Date"
                                                    ControlToValidate="txtDataGeracao"
                                                    MaximumValue="31/12/9999"
                                                    MinimumValue="31/12/1000"
                                                    ErrorMessage="Data Inválida"
                                                    ForeColor="Red"
                                                    Display="Dynamic"
                                                    ValidationGroup="ValidaGer" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                  <asp:Button ID="btnGerarRelatorio" runat="server" Text="Gerar" CssClass="button" OnClick="btnGerarRelatorio_Click" />
                                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelar_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>


                            </td>

                        </tr>

                        <tr>
                            <td>
                                <asp:Label ID="lblPastaCritica" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>


                    </table>

                </div>
            </div>
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

</asp:Content>
