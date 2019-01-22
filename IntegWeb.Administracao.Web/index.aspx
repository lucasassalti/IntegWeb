<%@ Page Title="" Language="C#" MasterPageFile="~/Principal.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="IntegWeb.Administracao.Web.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="full_w">

        <div id="DivLogado" runat="server">
            <div class="h_title">Bem-vindo!</div>

            <p><strong>MISSÃO:</strong> Elaborar e Administrar com excelência planos de previdência e de saúde para que as pessoas possam viver e construir um futuro com qualidade e tranquilidade.</p>
            <p><strong>VISÃO:</strong> Ser a empresa que valoriza, acima de tudo, uma vida mais digna as pessoas de quem cuidamos.</p>
            <p><strong>VALORES:</strong> Respeito ao ser humano, Excelência, Competência e Comprometimento.</p>

        </div>
        <div id="DivNLogado" runat="server">
            <h2>ATENÇÃO</h2>
            <br />
           <p>Não existem acessos liberados para esse login , favor abrir um chamado no  <a   href="http://portalservicos/SMPortal/SitePages/Service%20Catalog.aspx">PORTAL SERVICE DESK </a>.</p>
            <p>Setor Infraestrutura De Tec Informação - ATI.</p>
        </div>
    </div>

</asp:Content>
