<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="HistoricoProcessamento.aspx.cs" Inherits="IntegWeb.Saude.Web.HistoricoProcessamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w  tabelaPagina">
        <div class="h_title">
        </div>
        <asp:UpdatePanel ID="upHistBoleto" runat="server">
            <ContentTemplate>
                <h2>Histórico de Processamento</h2>
                <br />
                <br />

                    <asp:GridView ID="grdHistorico" runat="server" AutoGenerateColumns="false" PageSize="5" EmptyDataText="A consulta não retornou dados">
                        <Columns>
                            <asp:BoundField DataField="execucao_id" HeaderText="ID Execução" />
                            <%--<asp:BoundField DataField="inicio" HeaderText="Hora Inicio" DataFormatString="{0:HH:mm:ss}" />--%>
                            <asp:BoundField DataField="inicio" HeaderText="Hora Inicio" />
                            <asp:BoundField DataField="fim" HeaderText="Hora Fim" DataFormatString="{0:HH:mm:ss}" />
                            <asp:BoundField DataField="dat_vencimento" HeaderText="Data de Vencimento" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="lote_nao_consolidado" HeaderText="Lote" />
                            <asp:BoundField DataField="mensagem" HeaderText="Status Do Processo" />
                        </Columns>
                    </asp:GridView>
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnTodasAsCobrancas" CssClass="button" CausesValidation="false" runat="server" Text="Todas as Cobranças" OnClick="btnTodasAsCobrancas_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnFlagAtivo" CssClass="button" CausesValidation="false" runat="server" Text="Com Flag Ativo" OnClick="btnFlagAtivo_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnInadimplentes" CssClass="button" CausesValidation="false" runat="server" Text="Inadimplentes" OnClick="btnInadimplentes_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnEnderecoNulo" CssClass="button" CausesValidation="false" runat="server" Text="Endereço Nulo" OnClick="btnEnderecoNulo_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>

        </asp:UpdatePanel>

        <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server" AssociatedUpdatePanelID="">
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
