<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Exportacao.aspx.cs" Inherits="IntegWeb.Periodico.Web.Exportacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="full_w">

        <div class="h_title">
        </div>
        <div class="MarginGrid">
            <table style="border: 1px solid #FF0000;">
                <tr>
                    <td style="text-align: center;">

                        <h4>
                            <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red">ATENÇÃO</asp:Label></h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h4><asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Red">Se ao término do processo não iniciar automaticamente o download do Excel verifique:</asp:Label></h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h4><asp:Label ID="Label3" runat="server" Text="Label" ForeColor="Red">1- A consulta retornou registros.</asp:Label></h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        <h4><asp:Label ID="Label4" runat="server" Text="Label" ForeColor="Red">2- O navegador esta bloqueando pop-up para este site.</asp:Label></h4>
                    </td>
                </tr>
            </table>
        </div>
        <h1>
            <asp:Label runat="server" ID="NomeRelatorio"></asp:Label>
        </h1>

        <div class="tabelaPagina">
            <table runat="server" id="table">
                <tr>
                    <td colspan="2"></td>

                </tr>
            </table>
            <div class="MarginGrid">
                <asp:UpdatePanel ID="upExcel" runat="server">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatório" OnClick="btnRelatorio_Click" />
                            </tr>
                            <tr>
                                <td>
                                    <h3>
                                        <asp:Label Visible="false" ID="lblRegistros" runat="server" Text="Label" ForeColor="Red"></asp:Label></h3>
                                </td>
                            </tr>
                        </table>
                        </div>
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
