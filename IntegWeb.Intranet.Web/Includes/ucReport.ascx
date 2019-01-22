<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucReport.ascx.cs" Inherits="IntegWeb.Web.Includes.ucReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<div class="full_w">
    <div class="MarginGrid">
        <div>
            <CR:CrystalReportViewer GroupTreeImagesFolderUrl="" ToolbarImagesFolderUrl="" EnableDrillDown="false" ID="CrystalReportViewer1" runat="server" CssClass="crystalClass" AutoDataBind="false" EnableDatabaseLogonPrompt="false" EnableParameterPrompt="false" ToolPanelWidth="0px" ToolPanelView="None" OnUnload="CrystalReportViewer1_Unload"/>
        </div>
    </div>
</div>
