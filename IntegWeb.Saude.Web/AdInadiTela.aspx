<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="AdInadiTela.aspx.cs" Inherits="IntegWeb.Saude.Web.AdInadiTela" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server" ID="upInaAd">
        <ContentTemplate>

            <asp:ValidationSummary ID="vsInaAd" runat="server" ForeColor="Red" ShowMessageBox="true"
                ShowSummary="false" />
            <div class="full_w">

                <div class="tabelaPagina">

                    <h1>Resumo Inadimplentes/Adimplentes </h1>
                    <h3>Criar Relatório 
                    </h3>
                    <table>
                        <tr>
                            <td>Para criar o relatório do mês atual selecione o botão abaixo:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnProcessar" CssClass="button" CausesValidation="false" OnClick="btnProcessar_Click" runat="server" Text="Processar..." />
                            </td>
                        </tr>
                    </table>
                    <h3>Consultar Relatório</h3>
                    <table>
                        <tr>
                            <td>Digite Mês/Ano referência para consultar:</td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>Mês
                                        </td>
                                        <td>Ano
                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="drpMes" runat="server">
                                                <asp:ListItem Selected="True" Text="--Selecine--" Value="0">
                                       
                                                </asp:ListItem>
                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            </asp:DropDownList>

                                        <td>
                                            <asp:TextBox ID="txtAno" AutoPostBack="true" runat="server" Width="50px" MaxLength="4" onkeypress="mascara(this, soNumeros)"></asp:TextBox><br />

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnConsultar" CssClass="button" CausesValidation="false" OnClick="btnConsultar_Click" runat="server" Text="Consultar" />
                            </td>

                        </tr>
                    </table>
                </div>
            </div>

            <div>

                <CR1:CrystalReportViewer GroupTreeImagesFolderUrl="" ToolbarImagesFolderUrl="" ReportSourceID="CrystalReportSource2" ID="CrystalReportViewer2" runat="server" CssClass="crystalClass" AutoDataBind="True" EnableDatabaseLogonPrompt="false" EnableParameterPrompt="False" ToolPanelWidth="100px" ToolPanelView="None" />

                <CR1:CrystalReportSource ID="CrystalReportSource2" runat="server">
                    <Report FileName="Relatorios/Relatorio_Tela.rpt">
                    </Report>
                </CR1:CrystalReportSource>
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
</asp:Content>
