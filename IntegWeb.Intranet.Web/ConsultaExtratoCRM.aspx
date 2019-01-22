<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaExtratoCRM.aspx.cs" Inherits="IntegWeb.Intranet.Web.ConsultaExtratoCRM" %>
  <script>
      function checkAll(flag, grupo) {
          var IDS = '';
          var sel = document.getElementsByTagName("input");
          for (i = 0; i < sel.length; i++) {
              var str = sel[i].id;
              var n = str.indexOf(grupo);
              if (n >= 0) {
                  IDS = sel[i].id
                  document.getElementById(IDS).checked = flag;
              }
          }
      }

</script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            font-family: Arial;
            font-size: 11px;
        }
        .btnLkPDF {
            position:absolute;
            left:680px;
            /*background: url(img/pdf-logo.png) no-repeat;*/
            /*width:32px;
            height:32px;*/
            border:0;
            
        }
    </style>

</head>
<body>
    <form id="form2" runat="server">
        <asp:Button ID="btnEnviarEmail" runat="server" OnClick="btnEnviarEmail_Click" Text="Enviar para o Email" Visible="False" />
        <asp:TextBox ID="txtEmailDestinatario" runat="server" Visible="False" Width="310px"></asp:TextBox>
        <%--<asp:Label ID="lblParticipante" runat="server" Text="...."></asp:Label>--%>
        <br />
        <div id="divParticipantes" runat="server">
        </div>
        <div id="divDocumentos" runat="server">
        </div>
        &nbsp;
        <div>
        </div>
    </form>
    
</body>
</html>
