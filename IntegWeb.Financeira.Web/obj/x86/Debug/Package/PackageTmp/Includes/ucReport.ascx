<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucReport.ascx.cs" Inherits="IntegWeb.Financeira.Web.Includes.ucReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<div class="full_w">
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
                    <CR:CrystalReportViewer GroupTreeImagesFolderUrl="" ToolbarImagesFolderUrl="" ID="CrystalReportViewer1" runat="server" CssClass="crystalClass" AutoDataBind="True" EnableDatabaseLogonPrompt="false" EnableParameterPrompt="False" ToolPanelWidth="100px" ToolPanelView="None" OnUnload="CrystalReportViewer1_Unload"/>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRelatorio" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
