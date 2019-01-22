<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucReport.ascx.cs" Inherits="IntegWeb.Investimento.Web.Includes.ucReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<div class="full_w">

    <div class="h_title">
    </div>
    <h1>
        <asp:Label runat="server" ID="NomeRelatorio"></asp:Label>
    </h1>

    <table runat="server" id="table">
        <tr>
            <td colspan="2"></td>

        </tr>
    </table>
    <div class="MarginGrid">

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

                    <%--  <CR:CrystalReportViewer GroupTreeImagesFolderUrl="" ToolbarImagesFolderUrl="" ReportSourceID="CrystalReportSource1" ID="CrystalReportViewer1" runat="server" CssClass="crystalClass" AutoDataBind="True" EnableDatabaseLogonPrompt="false" EnableParameterPrompt="False" ToolPanelWidth="10px" ToolPanelView="None" DisplayToolbar="False" BestFitPage="False" Height="490px" Width="800px" ReuseParameterValuesOnRefresh="True" />--%>

<%--                    <CR:CrystalReportViewer GroupTreeImagesFolderUrl="" ToolbarImagesFolderUrl="" ReportSourceID="CrystalReportSource1" ID="CrystalReportViewer1" runat="server" CssClass="crystalClass" AutoDataBind="True" EnableDatabaseLogonPrompt="false" EnableParameterPrompt="False" ToolPanelWidth="100px" ToolPanelView="None" BestFitPage="False" Height="898px" Width="1200px" ReuseParameterValuesOnRefresh="True" />--%>
                    <CR:CrystalReportViewer GroupTreeImagesFolderUrl="" ToolbarImagesFolderUrl="" ID="CrystalReportViewer1" runat="server" CssClass="crystalClass" AutoDataBind="True" EnableDatabaseLogonPrompt="false" EnableParameterPrompt="False" ToolPanelWidth="100px" ToolPanelView="None" OnUnload="CrystalReportViewer1_Unload"/>

<%--                    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                        <Report FileName="Relatorios/Relatorio_Tela.rpt">
                        </Report>
                    </CR:CrystalReportSource>--%>
                </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>


    </div>

</div>
