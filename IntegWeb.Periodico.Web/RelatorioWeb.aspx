<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioWeb.aspx.cs" Inherits="IntegWeb.Periodico.Web.RelatorioWeb" MasterPageFile="~/Principal.Master" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="full_w">

    	<div class="h_title">
    	</div>
       <h1><asp:Label runat="server" ID="NomeRelatorio"></asp:Label> </h1>

    <div class="tabelaPagina">
        <table runat="server" id="table">
            <tr>
                <td colspan="2">
                 </td>

            </tr>
        </table>
           <div class="MarginGrid"><asp:Button ID="btnRelatorio" runat="server" CssClass="button" Text="Carregar Relatório" OnClick="btnRelatorio_Click" /></div>
                    <asp:UpdateProgress ID="UpdateProg1" DisplayAfter="0" runat="server">
                        <ProgressTemplate>
                              <div id="carregando">
                                <div class="carregandoTxt">
                                    <img src="img/processando.gif"/>
                                     <br /><h2>Processando. Aguarde...</h2>              
                                </div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
     
    </div>
    <div class="MarginGrid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>

                    <CR:CrystalReportViewer GroupTreeImagesFolderUrl="" ToolbarImagesFolderUrl="" ReportSourceID="CrystalReportSource1" ID="CrystalReportViewer1" runat="server" CssClass="crystalClass" AutoDataBind="True" EnableDatabaseLogonPrompt="false" EnableParameterPrompt="False" ToolPanelWidth="100px" ToolPanelView="None" />

                    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                        <Report FileName="Relatorios/Relatorio_Tela.rpt">
                        </Report>
                    </CR:CrystalReportSource>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRelatorio" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
</div>

</asp:Content>

