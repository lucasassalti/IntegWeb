using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.BLL.Cadastro;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.ENUM;
using System;
using System.Web.Script.Services;
using System.Web.Services;

namespace IntegWeb.Portal.Web
{
    public partial class AtualizacaoCadastralLogin : BasePage
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public static string ValidaLogin(long cpf, DateTime nascimento)
        {
            VerificaUsuario(new EmpregadoAtualizacaoBLL(), cpf, nascimento);

            return "Validação OK";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
        public static string AtualizarCadastro(long cpf, DateTime nascimento,
            string dddTelefone, Nullable<int> telefone, string dddCelular, int celular, string email)
        {
            Resultado res = new Resultado();
            EmpregadoAtualizacaoBLL empregadoAtualizacaoBLL = new EmpregadoAtualizacaoBLL();
            EmpregadoTipo empregadoTipo = VerificaUsuario(empregadoAtualizacaoBLL, cpf, nascimento);
            Guid guid = Guid.NewGuid();

            res = empregadoAtualizacaoBLL.SaveAtualizacao(new EMPREGADO_ATUALIZACAO()
            {
                NUM_CPF = cpf,
                DAT_NASCM = nascimento,
                TIP_EMPREGADO = (short)empregadoTipo,
                COD_EMAIL = email,
                COD_DDD = dddTelefone,
                NUM_TELEF = telefone,
                COD_DDDCEL = dddCelular,
                NUM_CELUL = celular,
                GUI_EMAIL = guid,
                CHR_EMAIL_CONFIRM = "0",
                DAT_ATUALIZACAO = DateTime.Now
            });

            if (res.Ok)
            {
                string nome = empregadoAtualizacaoBLL.GetNome(cpf, empregadoTipo);
                string corpoEmail = GerarCorpoEmail(nome, cpf, guid, empregadoTipo);
                new Email().EnviaEmail(email, "atendimento@funcesp.com.br", "Atualização Cadastral Funcesp", corpoEmail, string.Empty);
            }
            return res.Mensagem;
        }

        private static EmpregadoTipo VerificaUsuario(EmpregadoAtualizacaoBLL empregadoAtualizacaoBLL, long cpf, DateTime nascimento)
        {
            if (empregadoAtualizacaoBLL.VerificaEmpregado(cpf))
                throw new Exception("Já foi realizada a atualização para este CPF.");

            EmpregadoTipo empregadoTipo = empregadoAtualizacaoBLL.GetEmpregadoTipo(cpf, nascimento);

            if (empregadoTipo == EmpregadoTipo.Nenhum)
                throw new Exception("CPF e data de nascimento não encontrados.");

            return empregadoTipo;
        }

        private static string GerarCorpoEmail(string nome, long cpf, Guid guid, EmpregadoTipo empregadoTipo)
        {
            string url = Util.GetUrlPortal();
            string linkConfirmacao = string.Format("http://{0}/AtualizacaoCadastralConfirmacao.aspx?cpf={1}&cod={2}&tipo={3}", url, cpf, guid, (int)empregadoTipo);
            string linkVisualizacao = string.Format("http://{0}/AtualizacaoCadastral.aspx?cpf={1}&cod={2}&tipo={3}", url, cpf, guid, (int)empregadoTipo);
            string bootstrap = @"
    <style>
        * {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        *:before,
        *:after {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

        html {
            font-size: 10px;
            -webkit-tap-highlight-color: transparent;
        }

        body {
            margin: 0;
            font-family: ""Helvetica Neue"", Helvetica, Arial, sans-serif;
            font-size: 14px;
            line-height: 1.42857143;
            color: #333;
            background-color: #fff;
        }

        input,
        button,
        select,
        textarea {
            font-family: inherit;
            font-size: inherit;
            line-height: inherit;
        }

        a {
            color: #337ab7;
            text-decoration: none;
        }

        a:hover,
        a:focus {
            color: #23527c;
            text-decoration: underline;
        }

        a:focus {
            outline: thin dotted;
            outline: 5px -webkit-focus-ring-color;
            outline-offset: -2px;
        }

        figure {
            margin: 0;
        }

        img {
            vertical-align: middle;
        }

        .img-responsive,
        .thumbnail > img,
        .thumbnail a > img,
        .carousel-inner > .item > img,
        .carousel-inner > .item > a > img {
            display: block;
            max-width: 100%;
            height: auto;
        }

        .img-rounded {
            border-radius: 6px;
        }

        .img-thumbnail {
            display: inline-block;
            max-width: 100%;
            height: auto;
            padding: 4px;
            line-height: 1.42857143;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 4px;
            -webkit-transition: all .2s ease-in-out;
            -moz-transition: all .2s ease-in-out;
            transition: all .2s ease-in-out;
        }

        .img-circle {
            border-radius: 50%;
        }

        hr {
            margin-top: 20px;
            margin-bottom: 20px;
            border: 0;
            border-top: 1px solid #eee;
        }

        .sr-only {
            position: absolute;
            width: 1px;
            height: 1px;
            padding: 0;
            margin: -1px;
            overflow: hidden;
            clip: rect(0, 0, 0, 0);
            border: 0;
        }

        .sr-only-focusable:active,
        .sr-only-focusable:focus {
            position: static;
            width: auto;
            height: auto;
            margin: 0;
            overflow: visible;
            clip: auto;
        }

        h1 {
            font-family: inherit;
            font-weight: 500;
            font-size: 36px;
            line-height: 1.1;
            color: inherit;
            margin-top: 20px;
            margin-bottom: 10px;
        }

        h1 small {
            font-weight: normal;
            line-height: 1;
            color: #777;
            font-size: 65%;
        }
        
        p {
            margin: 0 0 10px;
        }

        .lead {
            margin-bottom: 20px;
            font-size: 16px;
            font-weight: 300;
            line-height: 1.4;
        }

        @media (min-width: 768px) {
            .lead {
                font-size: 21px;
            }
        }

        small,
        .small {
            font-size: 85%;
        }
    </style>";
            string mail = @"
    <style>
        body {
            background: #F2F2F2;
            text-align: center;
            padding: 33px;
        }

        .email_content {
            padding: 35px;
            background: #FFFFFF;
            width: 531px !important;
            margin: auto;
            color: #404040;
            margin-top: 40px;
        }

            .email_content h1 {
                font-size: 24px;
            }

            .email_content p {
                font-size: 18px;
                line-height: 27px;
                margin: 20px 0;
            }

            .email_content a {
                background: #D70B32;
                color: #FFFFFF;
                opacity: 1;
                font-size: 20px;
                line-height: 27px;
                font-weight: bold;
                width: 1005;
                text-align: center;
                border-radius: 6px;
                padding: 10px;
                margin: 20px 0;
                display: block;
            }
    </style>";
            string corpoEmail = string.Format(@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"" />
    <title>Home | Bauducco</title>
    <meta content='width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0' name='viewport' />
    <!--bootstrap.css-->
    {4}
    <!--mail.css-->
    {5}    
</head>
<body>
    <img src=""http://{3}/img/email/logo-funcesp.png"" alt=""Funcesp"">
    <div class=""email_content"">
        <h1>Olá, {0}!
      </h1>
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
        <a href=""{1}"">Confirme o e-mail e imprima o voucher</a>
        <p>
            Abraços
        </p>
        <p>
            <strong>Equipe Funcesp</strong>
        </p>
    </div>
    <div style=""width: 531px !important; margin: auto; margin-top: 55px;"">
        <p style=""font-size: 16px; line-height: 27px;"">
            Não consegue visualizar este email? Acesse o link abaixo: <a href=""{2}"" style=""color: #404040"">{2}</a>
        </p>
    </div>
</body>
</html>
", nome, linkConfirmacao, linkVisualizacao, url, bootstrap, mail);
            return corpoEmail;
        }
    }
}