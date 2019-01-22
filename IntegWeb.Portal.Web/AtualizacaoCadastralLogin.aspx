<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AtualizacaoCadastralLogin.aspx.cs" Inherits="IntegWeb.Portal.Web.AtualizacaoCadastralLogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <meta charset="utf-8" />
    <%--<script src="js/jquery-2.1.1.js"></script>--%>
    <link rel="apple-touch-icon" sizes="76x76" href="img/apple-icon.png"/>

    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Home | Bauducco</title>
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport' />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <link href="css/main.css" rel="stylesheet" />

    <link rel="stylesheet" href="//code.jquery.com/mobile/1.5.0-alpha.1/jquery.mobile-1.5.0-alpha.1.min.css" />
    <script src="//code.jquery.com/jquery-3.2.1.min.js"></script>   
    <script src="//code.jquery.com/mobile/1.5.0-alpha.1/jquery.mobile-1.5.0-alpha.1.min.js"></script>

    <!--     Fonts and icons     -->
    <!-- <link href='https://fonts.googleapis.com/css?family=Cambo|Lato:400,700' rel='stylesheet' type='text/css'> -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i,800,800i" rel="stylesheet"/>
    <link href="https://fonts.googleapis.com/css?family=Rochester" rel="stylesheet"/>
    <link href="http://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" rel="stylesheet"/>
    <link href="css/fonts/pe-icon-7-stroke.css" rel="stylesheet"/>
    <style>
        h1,h2,a,p,span {
            text-shadow:none;
        }
    </style>
</head>
<body style="text-shadow:none">
    <nav class="navbar navbar-default navbar-fixed-top navbar-transparent" color-on-scroll="85">
        <!-- if you want to keep the navbar hidden you can add this class to the navbar "navbar-burger"-->
        <div class="container">
            <div class="navbar-header">
                <button id="menu-toggle" type="button" class="navbar-toggle" data-toggle="collapse" data-target="#example">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar bar1"></span>
                    <span class="icon-bar bar2"></span>
                    <span class="icon-bar bar3"></span>
                </button>
                <a href="#" class="navbar-brand">
                    <img src="img/logo.svg" alt=""/>
                </a>
            </div>
            <div class="collapse navbar-collapse">
                <ul class="nav navbar-nav navbar-right navbar-uppercase">
                    <li>
                        <a href="#">
                            <img src="img/iconfinder_facebook_circle_294710.svg" alt=""/>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <img src="img/iconfinder_instagram_circle_294711.svg" alt=""/>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <img src="img/iconfinder_twitter_circle_294709.svg" alt=""/>
                        </a>
                    </li>
                    <li>
                        <a href="#">
                            <img src="img/iconfinder_youtube_circle_294712.svg" alt=""/>
                        </a>
                    </li>
                </ul>
            </div>
            <!-- /.navbar-collapse -->
        </div>
    </nav>
    <div id="top" class="section section-header section-header-small">
        <div class="parallax filter filter-color-black">
            <div class="image" style="background-image: url('img/bg-house-bauducco@2x.png')"></div>
        </div>
    </div>
    <div id="section_content">
        <div class="container">
            <div class="content">
                <div class="col-md-6">
                    <div class="title">
                        <h1>Solicitação
                            <br/>
                            <span>de amizade
                                <img src="img/icones_banner.svg" alt=""/>
                            </span>
                        </h1>
                        <div class="row icone_banner_2">
                            <img src="img/icone_banner_2.svg" alt=""/>
                        </div>
                        <div class="row icone_banner_3">
                            <p>
                                VAMOS NOS CONECTAR
                                <img src="img/icone_banner_3.svg" alt=""/>
                            </p>
                        </div>
                    </div>

                    <div class="rule">
                        <h2>Atualize seus contatos e ganhe uma surpresa das casas Bauducco!</h2>
                        <p>Com seus dados atualizados você ficará sempre por dentro das novidades, vantagens, benefícios e avisos da Funcesp.</p>
                        <a href="#" data-toggle="modal" data-target="#myModal">Regras de Participação</a>
                    </div>
                </div>
            </div>

            <div class="col-md-6 div_content_form">
                <form id="formAtualizacaoCadastralLogin" runat="server">
                    <div class="div_form">
                        <div class="row icone_cartinha">
                            <img class="img-responsive" src="img/icone_cartinha.svg" alt=""/>
                        </div>

                        <div class="row instructions">
                            <p>
                                Preencha os campos abaixo para receber o <span>seu voucher!</span>
                            </p>
                        </div>

                        <div class="row">
                            <div class="card_form">
                                <div class="header">
                                    <h2>Sobre Você<img class="img_header_step_1" src="img/icone_check.svg" alt=""/></h2>
                                </div>
                                <asp:Panel id="divContent1" runat="server" class="content step_1">
                                    <asp:Panel class="error" id="divError" runat="server">
                                        <div class="row">
                                            <img src="img/icone_erro.svg" alt=""/>
                                        </div>
                                        <div class="row">
                                            <p id="pMensagem" runat="server">
                                            </p>
                                        </div>
                                        <div class="row">
                                            <a href="#this" class="btn btn-default btn-funcesp">Tente novamente</a>
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel class="form" id="divStep1" runat="server">
                                        <div class="form-group">
                                            <p class="info-block">*Campos obrigatórios</p>
                                        </div>

                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtCpf" runat="server" class="form-control cpf" placeholder="CPF*"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <img src="img/icone_id.svg" alt=""/>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="input-group">
                                                <asp:TextBox ID="txtDtNascimento" runat="server" class="form-control date" placeholder="Data de nascimento*"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <img src="img/icone_calendario.svg" alt=""/>
                                                </div>
                                            </div>
                                        </div>
                                                                                <input type="button" id="btnValidarLogin" value="Próximo" class="btn btn-default btn-funcesp show-page-loading-msg" data-theme="b" data-textonly="false" data-textvisible="false" data-msgtext="" data-icon="arrow-r" data-iconpos="right"/>
                                    </asp:Panel>
                                </asp:Panel>
                            </div>
                        </div>

                        <div class="row">
                            <div class="card_form">
                                <div class="header">
                                    <h2>Seus dados
                                        <img class="img_header_step_2" src="img/icone_check.svg" alt=""/></h2>
                                </div>
                                <asp:Panel id="divContent2" runat="server" class="content step_2">
                                    <div class="form-group">
                                        <p class="info-block">*Campos obrigatórios</p>
                                    </div>

                                    <div class="form-group">
                                        <p class="error-block">
                                            <img src="img/icone_erro.svg" alt=""/>
                                            <span>Email incorreto</span>
                                        </p>
                                    </div>

                                    <div class="form-group form-group-cel">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDDDCelular" runat="server" class="form-control ddd" placeholder="DDD*"></asp:TextBox>
                                            <asp:TextBox ID="txtCelular" runat="server" class="form-control tel celphone" placeholder="Celular*"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <img src="img/icone_celular.svg" alt=""/>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group form-group-tel">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDDDTelefone" runat="server" class="form-control ddd" placeholder="DDD"></asp:TextBox>
                                            <asp:TextBox ID="txtTelefone" runat="server" class="form-control tel phone" placeholder="Telefone"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <img class="estreito" src="img/icone_telefone.svg" alt=""/>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group form-group-email">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Email pessoal*" MaxLength="50"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <img src="img/icone_email.svg" alt=""/>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group form-group-confirm-email">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtEmailConfirmacao" runat="server" class="form-control" placeholder="Confirme seu email pessoal*"></asp:TextBox>
                                            <div class="input-group-addon">
                                                <img src="img/icone_email.svg" alt=""/>
                                            </div>
                                        </div>
                                        <p class="info-block">*Este campo deve ser igual ao campo email pessoal</p>
                                    </div>
                                    
                                    <input type="button" id="btnAtualizarCadastro" value="Próximo" class="btn btn-default btn-funcesp"/>
                                </asp:Panel>
                                
                                <asp:Panel ID="divFinish" runat="server" class="content finish">
                                    <h2>Estamos conectados!</h2>
                                    <p>
                                        Agora você vai receber nossas notícias em primeira mão. O voucher da <strong>Casa Bauducco</strong> foi enviado para o <strong>seu e-mail</strong>. Caso não encontre nossa mensagem, verifique sua caixa de spam ou promoções.
                                    </p>
                                </asp:Panel>
                            </div>
                        </div>

                        <div class="row">
                            <div class="not_spam">
                                <p class="">
                                    Não se preocupe! Respeitamos a sua privacidade e não fazemos spam.
                                </p>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <div id="section_panettone">
        <img src="img/panettone.png" alt=""/>
    </div>
    <footer>
        <div class="bg_footer_overlay">
            <div class="container">
                <img class="logo_rodape" src="img/logo_rodape.svg" alt=""/>
                <img class="logo-bauducco" src="img/logo-bauducco.png" alt=""/>
                <p>
                    Todos os direitos reservados. Campanha válida 00/00/0000
                </p>
            </div>
        </div>
    </footer>

    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <img src="img/icone_fechar.svg" alt=""/>
                        &nbsp;
                        &nbsp;
                        &nbsp;         
                    </button>
                    <h4 class="modal-title" id="myModalLabel">Regras de Participação</h4>
                </div>
                <div class="modal-body">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec a eros vel nisl sollicitudin faucibus. Mauris accumsan pulvinar sagittis. Vivamus blandit mattis ex in imperdiet. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Donec ligula orci, viverra eu augue id, ullamcorper euismod ligula. Mauris sollicitudin aliquet vulputate. Donec cursus tristique orci et aliquet. Nullam sed ipsum nec felis ullamcorper pulvinar. Fusce in risus eu quam tristique aliquam. Etiam ac feugiat turpis. </p>
                    <p>Nullam sit amet ligula a mi maximus viverra non vitae augue. Vivamus sit amet sodales dolor. Sed elementum felis a ligula viverra, vel aliquet est vehicula. Mauris ornare metus eu elit sodales, a varius metus aliquam. Donec et est non eros lobortis scelerisque. Nulla vel magna justo. Suspendisse a turpis nec lacus egestas sollicitudin. Vestibulum non orci id lacus egestas feugiat sit amet ac mi. Donec non sagittis lectus. Vivamus ut neque augue. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Curabitur finibus sodales enim, ac faucibus ligula suscipit sed. </p>
                    <p>Etiam tellus sapien, egestas eget auctor vitae, aliquet nec nisl. Curabitur lobortis aliquet rutrum. Aliquam viverra nibh sed lorem auctor convallis. Suspendisse eget blandit massa. Aliquam vehicula congue nunc, sed eleifend dolor varius vitae. Donec fermentum vitae nulla id auctor. Sed tempus, orci ac dapibus cursus, neque lectus lobortis diam, sodales luctus metus mauris eu neque. Sed a ipsum massa. </p>
                    <p>Vestibulum mattis, sem vel semper tincidunt, nulla mauris congue justo, rutrum dictum erat diam ac est. Donec vel lacus porttitor, porta magna in, tincidunt nunc. Vestibulum finibus, eros sollicitudin efficitur viverra, dui quam pretium eros, non tristique Leo lectus quis sem. Sed dolor ex, tempor elementum ante tempus, molestie sodales nibh. Quisque interdum at orci ut sagittis. Proin cursus suscipit faucibus. Praesent porta mauris neque, non venenatis dui placerat eget. Duis ultrices tellus vitae purus imperdiet ornare at vel tortor. Nunc rutrum sem est, ut elementum eros tincidunt quis. Vestibulum mattis tempor luctus. Aliquam elementum iaculis aliquet. Sed gravida molestie nulla, commodo consectetur neque. Praesent malesuada lectus quam, vel ultricies libero blandit in. Aliquam massa magna, varius id risus sit amet, aliquet pharetra erat. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nulla eu purus elementum magna condimentum venenatis placerat in tellus. Morbi blandit ex vitae sodales cursus. Aenean interdum odio sapien, ut scelerisque nibh placerat eget. Aenean porta tincidunt justo, id dapibus magna placerat feugiat. Vivamus posuere euismod eros sit amet convallis.</p>
                </div>
            </div>
        </div>
    </div>
</body>

<!--   core js files    -->
<script src="js/jquery-ui.js" type="text/javascript"></script>
<script src="js/bootstrap.js" type="text/javascript"></script>
<script src="js/jquery.mask.js" type="text/javascript"></script>

<!--  js library for devices recognition -->
<script type="text/javascript" src="js/modernizr.js"></script>

<!--  script for google maps   -->
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js"></script>

<!--   file where we handle all the script from the Gaia - Bootstrap Template   -->
<script type="text/javascript" src="js/main.js"></script>

<script type="text/javascript" src="js/funcoes_gerais.js"></script>
<script>
    
</script>
</html>
