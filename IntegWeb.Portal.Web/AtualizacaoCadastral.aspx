<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AtualizacaoCadastral.aspx.cs" Inherits="IntegWeb.Portal.Web.AtualizacaoCadastral" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <link rel="apple-touch-icon" sizes="76x76" href="img/apple-icon.png">
    <link rel="icon" type="image/png" sizes="96x96" href="img/favicon.png">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Home | Bauducco</title>
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport' />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <link href="css/mail.css" rel="stylesheet" />

    <!--     Fonts and icons     -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i,800,800i" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Rochester" rel="stylesheet">
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet">
    <link href="css/fonts/pe-icon-7-stroke.css" rel="stylesheet">
</head>
<body>
    <img src="img/email/logo-funcesp.png" alt="">
    <div class="email_content">
        <h1 id="hNome" runat="server"></h1>
        <p>
            Você acabou de atualizar seus contatos e
        agora ficará sempre atualizado sobre as
        novidades, vantagens, benefícios que
        envolvem o seu plano e muito mais!
        </p>
        <p>
            Para acessar o seu voucher Casa
        Bauducco, basta clicar no botão abaixo e
        confirmar o seu e-mail.
        </p>
        <p>
            Esperamos que aproveite!
        </p>
            <asp:HyperLink id="hyperlinkConfirmacao" runat="server" Text="Confirme o e-mail e imprima o voucher"/> 
        <p>
            Abraços
        </p>
        <p>
            <strong>Equipe Funcesp</strong>
        </p>
    </div>
</body>
</html>
