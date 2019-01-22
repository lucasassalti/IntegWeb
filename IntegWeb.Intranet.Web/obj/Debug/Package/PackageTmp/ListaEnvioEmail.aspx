<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaEnvioEmail.aspx.cs" Inherits="IntegWeb.Intranet.Web.ListaEnvioEmail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<html>
    <head>
         <link href="css/listemailestilo.css" rel="stylesheet" />

       <script type="text/javascript">
                function ShowProgressBar() {
                    document.getElementById('dvProgressBar').style.visibility = 'visible';
                }

                function HideProgressBar() {
                    document.getElementById('dvProgressBar').style.visibility = "hidden";
                }
    </script>
    </head>
 
 <body onload="javascript:HideProgressBar()">
  <header class="site-header flex-center" runat="server">
      <img src="img/bg_logo_invertido.jpg" /> <h3 class="section-title">Gerador Lista de Email</h3>
  </header>
     <br />
  <form runat="server">

<%--    <nav class="main-nav flex-center">
      <h1 class="section-subtitle">NAV</h1>
    </nav>--%>
    <section class="main-content flex-center" runat="server">


       <center> <table>
            <tr>
                <td>
                    <asp:Label ID="lblTipo" Text="Tipo de lista de e-mail" runat="server"></asp:Label>
                </td>
                <td>
            <asp:DropDownList runat="server" ID="ddlTipo" CssClass="aconselection">
            <asp:ListItem Value="1">Boleto Saúde</asp:ListItem>
        </asp:DropDownList>
                </td>
                <td>
                <asp:Button ID="btnGerar" runat="server" Text="Gerar lista" CssClass="btn btn-1 btn-1c" OnClick="btnGerar_Click" OnClientClick="javascript:ShowProgressBar()" />
               </td>
            </tr>
          <%--  <tr>
                <asp:RadioButtonList runat="server" ID="rdList" RepeatDirection="Horizontal" >
                    <asp:ListItem Value="op25" Text="Vencimento dia 25"></asp:ListItem>
                    <asp:ListItem Value="op30" Text="Vencimento dia 28/29/30"></asp:ListItem>
                    <asp:ListItem Value="opAll" Text="Todos os vencimentos"></asp:ListItem>
                </asp:RadioButtonList>
            </tr>--%>
        </table>
           <div id="dvProgressBar" style="visibility:hidden">
               <img src="img/dual-ring-loader.gif" width="150px" height="150px" /> <h4>Carregando...</h4>

           </div>
        </center>
        

    </section>

      <br />
      <br />
      <br />
<%--    <aside class="main-aside flex-center" runat="server">
      <h1 class="section-subtitle">ASIDE</h1>
    </aside>--%>

        <br />
  <br />
  <br />
  <br />
  <br />
  <br />
  <br />
  <br />
      <br />
      <br />






  <footer class="site-footer" runat="server">
  </footer>
      </form>
</body>

</html>