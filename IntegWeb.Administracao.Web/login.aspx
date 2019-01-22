<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="IntegWeb.Administracao.Web.login" %>

<%@ Register Src="~/Includes/ucLoginAdm.ascx" TagPrefix="uc1" TagName="ucLoginAdm" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="FCESP" content="ATD" />
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title>FUNCESP - Administração</title>
    <link rel="stylesheet" type="text/css" href="css/login.css" media="screen" />
    <link href="css/Content/jquery.fancybox.css" rel="stylesheet" />
    <link href="css/Content/themes/jquery.fancybox-1.3.1.css" rel="stylesheet" />
    <link href="css/Content/jquery-ui.css" rel="stylesheet" />
    <link href="css/themes/base/jquery-ui.css" rel="stylesheet" />

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
                        <center><img src="img/bg_logo.png" style="height: 78px; width: 93px;" /><br /><h2>Administração de Acesso</h2></center> 
                        <%--<h1 class="tituloLogin">Sistema de Administração</h1>--%>
                        <div style="clear: both"></div>
                        <br />
                        <uc1:ucLoginAdm runat="server" ID="ucLoginAdm" />


                    </form>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
