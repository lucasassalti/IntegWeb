<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="Ferramentas.aspx.cs" Inherits="IntegWeb.Administracao.Web.Ferramentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <asp:UpdatePanel ID="upUsuarioAd" runat="server">
        <ContentTemplate>
            <div class="full_w">
                <div class="h_title"></div>
                <h2>Ferramentas Administrativas </h2>
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtScript" runat="server" TextMode="MultiLine" Width="600px" Height="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:Label ID="lblResultado" runat="server" Text="Comando executado com sucesso!" ForeColor="Green" Visible="false"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnExecutar" OnClick="btnExecutar_click" runat="server" Text="Executar" CssClass="button" CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="txtLog" runat="server" Width="600px" Height="200px" ForeColor="Red" Visible="false" ></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExecutar" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
