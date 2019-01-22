<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="Accessdenied.aspx.cs" Inherits="IntegWeb.Portal.Web.Accessdenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <script>
        var countdownFrom = 5;  // number of seconds
        var countdownwin;

        function CountDownPopup() {
            //countdownwin = window.open("about:blank", "", "height=200,width=200");
            self.setInterval('CountDownTimer()', 1000)
        }
        function CountDownTimer() {
            if (countdownFrom < 0) {
                window.open('about:blank', '_self', '');
                window.close();
            }
            else {
                $('#ContentPlaceHolder1_lblMessagem').text('Esta janela será fechada em ' + countdownFrom + ' segundos');
                //doc = countdownwin.document;
                //doc.open('text/html');
                //doc.write("Closing in " + countdownFrom + " seconds");
                //doc.close();
            }
            countdownFrom--
        }

        CountDownPopup();
    </script>

	<div class="wpthemeInner">					
        <div class="centeredTextDiv">
            <span class="bigErrorText">Você não tem permissão para acessar esta funcionalidade.</span><br />
            <span class="smallErrorText"><asp:Label runat="server" ID="lblMessagem"></asp:Label></span>
        </div>
	</div>
                            
</asp:Content>
