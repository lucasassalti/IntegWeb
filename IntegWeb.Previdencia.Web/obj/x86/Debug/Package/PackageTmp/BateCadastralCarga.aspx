<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="BateCadastralCarga.aspx.cs" Inherits="IntegWeb.Previdencia.Web.BateCadastralCarga" %>

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

        <h1>Arquivos Patrocinadoras X Portal Funcesp</h1>

        <asp:Panel ID="pnlPesquisar" runat="server" class="tabelaPagina">
            <table>
                <tr>
                    <td>Entre com o arquivo de carga:</td>
                    <td>
                        <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="button" />
                    </td>
                    <td>
                        <asp:Button ID="btnProcessar" OnClick="btnProcessar_Click" OnClientClick="return postbackButtonClick();" CausesValidation="false" CssClass="button" runat="server" Text="Processar" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Processo_Mensagem" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
            <%--<div>
                <asp:Label ID="lblPartDel" runat="server" Text="Participantes com Alteração no Portal" Visible="false"
                    Style="margin-left: 16px"
                    Font-family="Arial"
                    ForeColor="Black"
                    Font-Size="Small"
                    Font-Bold="true" />
                <div>&nbsp</div>
                <asp:GridView ID="grdBateCadastral" runat="server"
                    EmptyDataText="Não há dados para exibir"
                    AutoGenerateColumns="false"
                    CssClass="Table"
                    Visible="false">
                    <Columns>
                        <asp:BoundField DataField="Empresa" HeaderText="Empresa" />
                        <asp:BoundField DataField="Matrícula" HeaderText="Matrícula" />
                        <asp:BoundField DataField="Nome" HeaderText="Nome Participante" />
                    </Columns>
                </asp:GridView>
            </div>--%>
        </asp:Panel>
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
