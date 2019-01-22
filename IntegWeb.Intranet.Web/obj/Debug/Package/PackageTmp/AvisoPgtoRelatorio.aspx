<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AvisoPgtoRelatorio.aspx.cs" Inherits="IntegWeb.Intranet.Web.AvisoPgtoRelatorio" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" /> 
    <title>Funcesp - Demonstrativo de Pagamento PDF</title>
    <link href="css/estilo.css" rel="stylesheet" />
    <script type="text/javascript" src='crystalreportviewers13/js/crviewer/crv.js'></script>
</head>
<body>

    <form id="form1" runat="server">

    <div>

         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" CssClass="crystalClass" EnableDatabaseLogonPrompt="false" EnableParameterPrompt="False" ToolPanelWidth="100px" ToolPanelView="None" OnUnload="CrystalReportViewer1_Unload" />

                   <%-- <CR:CrystalReportSource ID="CrystalReportSource2" runat="server">
                        <Report FileName="AvisoPagamento.rpt">
                        </Report>
                    </CR:CrystalReportSource>--%>

    </div>

<%--        AutoDataBind="True"--%>
    </form>
</body>
</html>
