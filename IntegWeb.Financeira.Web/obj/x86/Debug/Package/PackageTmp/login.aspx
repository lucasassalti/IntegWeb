<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="IntegWeb.Financeira.Web.login" %>

<%@ Register Src="~/Includes/ucLogin.ascx" TagPrefix="uc1" TagName="ucLogin" %>


<!DOCTYPE html>
<html>

<head>
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>FUNCESP - Financeira</title>
    <link rel="stylesheet" type="text/css" href="css/login.css" media="screen" />
    <link href="Content/jquery.fancybox.css" rel="stylesheet" />
    <link href="Content/themes/jquery.fancybox-1.3.1.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="js/jquery.fancybox.js"></script>
    <script type="text/javascript">
        function MostraMsg() {

            $("#lnkErro").click(function () {

                $(".fancybox").fancybox({
                    onStart: function (event, ui) {
                        $(this).parent().appendTo($("form:first"));

                    },
                    afterClose: function () {

                        $("[type='password']").removeAttr('disabled');
                    }

                });
                $("[type='password']").attr('disabled', 'disabled');

            });
            $(window).load(function () {
                $("#lnkErro").click();
            });
        }
        jQuery(document).ready(function () {

            $('.close-button').click(function () {

                $.fancybox.close();
            });
        });
    </script>
</head>
<body>

    <div class="wrap">
        <div id="content">
            <div id="main">
                <div class="full_w">
                    <form id="Form1" runat="server">
                      <center><img src="img/bg_logo.png" style="height: 78px; width: 93px;" /><br /><h2>FinanceiraWeb</h2></center>  
                        <%--<h1 class="tituloLogin">FinanceiraWeb</h1>--%>
                        <%--<asp:Label ID="lblApresentacao2" runat="server" Text="Sistema de Integração" ForeColor="Red"></asp:Label>--%>
                        <div style="clear: left">
                            <br />
                            <uc1:uclogin runat="server" id="ucLogin" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>

